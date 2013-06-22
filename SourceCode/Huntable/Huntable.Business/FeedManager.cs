using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huntable.Data;
using Snovaspace.Util.Logging;
using System.Data.Objects;
using Snovaspace.Util.FileDataStore;
namespace Huntable.Business
{
    public static class FeedManager
    {
        #region Enum Constants

        public enum FeedType
        {
            User_Interest = 1,
            User_Skill = 2,
            User_Education = 3,
            User_School = 4,
            Feed_Update = 5,
            Current_Job = 6,
            Current_Company = 7,
            Follow = 8,
            Profile_Picture = 9,
            Link_share = 10,
            Got_Connected = 11,
            Endorsed = 12,
            Job_Post = 13,
            Profile_Viewed = 14,

            User_Potfolio_Photo = 15,
            User_Portfolio_video = 16,
            User_Photo = 17,
            User_Video = 18,
            Company_Video = 19,
            Company_Portfolio_Photo = 20,

            Like_Main_Feed = 21,
            Like_User_Portfolio_Photo = 22,
            Like_User_Portfolio_Video = 23,
            Like_User_Photo = 24,
            Like_User_Video = 25,
            Like_Company_Video = 26,
            Like_Company_Portfolio_Photo = 27,
            Like_User_Feed = 28,

            Comment_Main_Feed = 29,
            Comment_User_Portfolio_Photo = 30,
            Comment_User_Portfolio_Video = 31,
            Comment_User_Photo = 32,
            Comment_User_Video = 33,
            Comment_Company_Video = 34,
            Comment_Company_Portfolio_Photo = 35,
            Comment_User_Feed = 36,

            Like_User_Profile = 37,
            Like_Company_Profile = 38,
            Like_Company_Product = 39,
            Comment_Company_Product = 40,

            Multiple_User_Potfolio_Photo = 41,
            Multiple_User_Photo = 42,
            Multiple_Company_Portfolio_Photo = 43,

            Like_Comment = 44,
            Company_Product = 45,
            Wall_Picture = 46
        }

        public enum FeedRetrievePage
        {
            Networking = 1,
            Visual_CV_Activity = 2,
            Business_Activity = 3,
            Employee_Activity = 4
            //Alert = 5
            //Pictures = 3,
            //Videos = 4,
            //Likes = 5,
            //Company_Product = 8,
            //Visual_CV = 9,
            //Company_view = 10
        }

        public enum ajaxPopup
        {
            ProfilePhotoId,
            UserPhotoId,
            UserPortfolioPhotoId,
            CompanyPortfolioPhotoId,
            UserVideoId,
            CompanyVideoId

        }

        #endregion

        #region Feed Entry - Create

        public static void addFeedNotification(FeedType type, int currentUserId, int? refRecordId, string userComments)
        {
            LoggingManager.Debug("Entering addFeedNotification - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int mainFeedId = 0;
                #region Main Feed Entry
                List<string> lstPhotoType = new List<string>() { 
                    FeedType.Company_Portfolio_Photo.ToString(),
                    FeedType.User_Photo.ToString(),
                    FeedType.User_Potfolio_Photo.ToString() ,
                    //FeedType.Multiple_Company_Portfolio_Photo.ToString(),
                    //FeedType.Multiple_User_Photo.ToString(),
                    //FeedType.Multiple_User_Potfolio_Photo.ToString()
                };
                string strType = type.ToString();
                #region profile picture update
                if (type == FeedType.Profile_Picture)
                    deleteFeedNotitifation(FeedType.Profile_Picture, currentUserId, currentUserId);
                #endregion

                #region Mainfeed entry
                FeedUserMain obj = new FeedUserMain
                {
                    Date = DateTime.Now,
                    IsDelete = false,
                    Type = type.ToString(),
                    UserId = currentUserId
                };
                context.FeedUserMains.AddObject(obj);
                context.SaveChanges();
               
                mainFeedId = obj.Id;
                #endregion

                #region Mainfeed entry for multiple photo
                int multipleFeedId = 0;
                if (lstPhotoType.Contains(strType))
                {
                    string strMultipleFeedType = "";
                    if (type == FeedType.Company_Portfolio_Photo)
                        strMultipleFeedType = FeedType.Multiple_Company_Portfolio_Photo.ToString();
                    else if (type == FeedType.User_Photo)
                        strMultipleFeedType = FeedType.Multiple_User_Photo.ToString();
                    else if (type == FeedType.User_Potfolio_Photo)
                        strMultipleFeedType = FeedType.Multiple_User_Potfolio_Photo.ToString();

                    var mainfeed = (from m in context.FeedUserMains
                                    where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                    && m.IsDelete != true
                                    && m.Type == strType
                                    && m.UserId == currentUserId
                                    select m);//.OrderByDescending(a => a.Date).FirstOrDefault();
                    if (mainfeed != null)
                    {
                        #region Multiplephoto update
                        var multiplemainfeed = (from m in context.FeedUserMains
                                                where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                                && m.IsDelete != true
                                                && m.Type == strMultipleFeedType
                                                && m.UserId == currentUserId
                                                select m).OrderByDescending(a => a.Date).FirstOrDefault();
                        if (multiplemainfeed != null)
                        {
                            multipleFeedId = multiplemainfeed.Id;
                            multiplemainfeed.Date = DateTime.Now;
                            multiplemainfeed.RefFeedId = null;
                            context.SaveChanges();
                        }
                        else
                        {
                            int? id = null;
                            if (mainfeed.Count() == 1)
                                id = mainfeed.FirstOrDefault().Id;
                            FeedUserMain objMultiple = new FeedUserMain
                            {
                                Date = DateTime.Now,
                                IsDelete = false,
                                Type = strMultipleFeedType,
                                UserId = currentUserId,
                                RefFeedId = id
                            };
                            context.FeedUserMains.AddObject(objMultiple);
                            context.SaveChanges();
                            multipleFeedId = objMultiple.Id;
                        }
                        #endregion

                        foreach (var feed in mainfeed)
                        {
                            feed.RefFeedId = multipleFeedId;
                        }
                        context.SaveChanges();
                    }
                }
                #endregion
                #region Like and Comment Update
                switch (type)
                {
                    case FeedType.Like_User_Feed:
                    case FeedType.Comment_User_Feed:
                        #region Like_User_Feed  Comment_User_Feed
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserUserFeeds.Any(a => a.UserFeedId == refRecordId)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                        rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                if (type == FeedType.Like_User_Feed)
                                {
                                    var socialManager = new SocialShareManager();
                                    var msg = "[UserName]" + " " + "liked a feed in Huntable";
                                    socialManager.ShareOnFacebook(currentUserId, msg, "");
                                }                              
                            }
                        }
                        #endregion
                        break;                                           
                    case FeedType.Like_User_Portfolio_Photo:
                    case FeedType.Comment_User_Portfolio_Photo:
                        #region Like_User_Portfolio_Photo  Comment_User_Portfolio_Photo
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == refRecordId)
                                            select m);
                            //var feed = context.FeedUserMains.FirstOrDefault(u => u.UserId == currentUserId);
                            //var photoid =
                            //    context.FeedUserPorfolioPhotoes.FirstOrDefault(p => p.FeedId ==feed.Id);
                            var pic = context.UserPortfolios.FirstOrDefault(a => a.Id == refRecordId);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var socialManager = new SocialShareManager();
                                if (pic.PictureId == 0)
                                {
                                    var msg = "[UserName]" + " " + "liked a picture in Huntable";
                                    socialManager.ShareOnFacebook(currentUserId, msg, "");
                                }
                                else
                                {
                                    var picurl = "https://huntable.co.uk/LoadFile.ashx?id=" + pic.PictureId;
                                    var msg = "[UserName]" + " " + "liked a picture in Huntable";
                                    socialManager.ShareOnFacebook(currentUserId, msg, picurl);
                                }
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Portfolio_Video:
                    case FeedType.Comment_User_Portfolio_Video:
                        #region Like_User_Portfolio_Video  Comment_User_Portfolio_Video
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == refRecordId)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var socialManager = new SocialShareManager();
                                var msg = "[UserName]" + " " + "liked portfolio video in Huntable";
                                socialManager.ShareOnFacebook(currentUserId, msg, "");
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Photo:
                    case FeedType.Comment_User_Photo:
                        #region Like_User_Photo  Comment_User_Photo
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserPhotoes.Any(a => a.UserPhotoId == refRecordId)
                                            select m);
                            var pic = context.UserPortfolios.FirstOrDefault(a => a.Id == refRecordId);
                            var user = context.Users.FirstOrDefault(u => u.Id == pic.UserId);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var username = context.Users.FirstOrDefault(w => w.Id == currentUserId);
                                var socialManager = new SocialShareManager();
                                if (type == FeedType.Like_User_Photo)
                                {
                                    if (pic.PictureId == 0)
                                    {
                                        var msg = "[UserName]" + " " + "liked" + " " + username.FirstName + " " +
                                                  username.LastName + "'s photo in Huntable";
                                        socialManager.ShareOnFacebook(currentUserId, msg, "");
                                    }

                                    else
                                    {
                                        var picurl = "https://huntable.co.uk/LoadFile.ashx?id=" + pic.PictureId;
                                        var msg = "[UserName]" + " " + "liked" + " " + user.FirstName + " " +
                                                  user.LastName + "'s photo in Huntable";
                                        socialManager.ShareOnFacebook(currentUserId, msg, picurl);
                                    }
                                }
                                else if (type == FeedType.Comment_User_Photo)
                                {
                                    if (pic.PictureId == 0)
                                    {
                                        var msg = "[UserName]" + " " + "commented on" + " " + username.FirstName + " " +
                                                  username.LastName + "'s photo in Huntable";
                                        socialManager.ShareOnFacebook(currentUserId, msg, "");
                                    }

                                    else
                                    {
                                        var picurl = "https://huntable.co.uk/LoadFile.ashx?id=" + pic.PictureId;
                                        var msg = "[UserName]" + " " + "commented on" + " " + user.FirstName + " " +
                                                  user.LastName + "'s photo in Huntable";
                                        socialManager.ShareOnFacebook(currentUserId, msg, picurl);
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Video:
                    case FeedType.Comment_User_Video:
                        #region Like_User_Video  Comment_User_Video
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserVideos.Any(a => a.UserVideoId == refRecordId)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var username = context.Users.FirstOrDefault(w => w.Id == currentUserId);
                                var socialManager = new SocialShareManager();
                                var msg = "[UserName]" + " " + "liked" + " " + username.FirstName + " " + username.LastName + "'s video in Huntable";
                                socialManager.ShareOnFacebook(currentUserId, msg, "");
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Company_Video:
                    case FeedType.Comment_Company_Video:
                        #region Like_Company_Video  Comment_Company_Video
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == refRecordId)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var companyname = context.Companies.FirstOrDefault(w => w.Id == currentUserId);
                                var socialManager = new SocialShareManager();
                                var msg = "[UserName]" + " " + "liked" + " " + companyname.CompanyName + "'s video in Huntable";
                                socialManager.ShareOnFacebook(currentUserId, msg, "");
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Company_Portfolio_Photo:
                    case FeedType.Comment_Company_Portfolio_Photo:
                        #region Like_Company_Portfolio_Photo  Comment_Company_Portfolio_Photo
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == refRecordId)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var socialManager = new SocialShareManager();
                                var companyname = context.Companies.FirstOrDefault(w => w.Id == currentUserId);
                                var msg = "[UserName]" + " " + "liked" + " " + companyname.CompanyName + "'s portfolio picture in Huntable";
                                socialManager.ShareOnFacebook(currentUserId, msg, "");
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Main_Feed:
                    case FeedType.Like_Comment:
                    case FeedType.Comment_Main_Feed:
                        #region Like_Main_Feed  Comment_Main_Feed
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == refRecordId && a.FeedUserMain.IsDelete != true)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();                              
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Company_Profile:
                    case FeedType.Like_User_Profile:
                        #region Like_Company_Profile  Like_User_Profile
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserProfiles.Any(a => a.ProfileUserId == refRecordId)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var socialManager = new SocialShareManager();                            
                                var user =context.Users.FirstOrDefault(u => u.Id == currentUserId && u.IsCompany == null);
                                if (user != null)
                                {
                                    var msg = "[UserName]" + " " + "liked" + " " + user.Name + "'s profile in Huntable";
                                    socialManager.ShareOnFacebook(currentUserId, msg, "");
                                }                                
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Company_Product:
                    case FeedType.Comment_Company_Product:
                        #region Like_Company_Profile  Comment_Company_Product
                        {
                            var mainfeed = (from m in context.FeedUserMains
                                            where EntityFunctions.TruncateTime(m.Date) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && m.IsDelete != true
                                            && m.Type == strType
                                            && m.Id != mainFeedId
                                            && m.FeedUserCompanyProducts.Any(a => a.CompanyProductId == refRecordId)
                                            select m);
                            if (mainfeed != null)
                            {
                                foreach (var rec in mainfeed)
                                {
                                    if (rec.UserId == currentUserId)
                                        rec.IsDelete = true;
                                    else
                                    rec.RefFeedId = mainFeedId;
                                }
                                context.SaveChanges();
                                var socialManager = new SocialShareManager();
                                var user = context.Users.FirstOrDefault(u => u.Id == currentUserId);
                                var companyname = context.Companies.FirstOrDefault(w => w.Userid ==user.Id );
                                if (companyname != null)
                                {
                                    var msg = "[UserName]" + " " + "liked" + " " + companyname.CompanyName +
                                              "'s product in Huntable";
                                    socialManager.ShareOnFacebook(currentUserId, msg, "");
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                #endregion
                #endregion
                #region Reference table entry
                switch (type)
                {
                    case FeedType.User_Interest:
                        #region User Interest Feed Entry
                        FeedUserInterest objInterest = new FeedUserInterest
                        {
                            FeedId = mainFeedId,
                            MasterInterestId = refRecordId
                        };
                        context.FeedUserInterests.AddObject(objInterest);
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Skill:
                        #region User Skill Feed Entry
                        FeedUserSkill objSkill = new FeedUserSkill
                        {
                            FeedId = mainFeedId,
                            MasterSkillId = refRecordId
                        };
                        context.FeedUserSkills.AddObject(objSkill);
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Education:
                    case FeedType.User_School:
                        #region User Education History Feed Entry
                        FeedUserEducationSchool objEducationSchool = new FeedUserEducationSchool
                        {
                            FeedId = mainFeedId,
                            EducationHistoryId = refRecordId
                        };
                        context.FeedUserEducationSchools.AddObject(objEducationSchool);
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Feed_Update:
                    case FeedType.Link_share:
                    case FeedType.Like_User_Feed:
                    case FeedType.Comment_User_Feed:
                        #region User Feed Feed Entry
                        FeedUserUserFeed objUserFeed = new FeedUserUserFeed
                        {
                            FeedId = mainFeedId,
                            UserFeedId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserUserFeeds.AddObject(objUserFeed);
                        context.SaveChanges();
                        UpdateOwnerId(mainFeedId, objUserFeed.UserFeed.UserID);
                        #endregion
                        break;
                    case FeedType.Current_Job:
                    case FeedType.Current_Company:
                    case FeedType.Endorsed:
                        #region User Employement History Feed Entry
                        FeedUserEmployementHistory objEmployementHistory = new FeedUserEmployementHistory
                        {
                            FeedId = mainFeedId,
                            EmploymentHistoryId = refRecordId
                        };
                        context.FeedUserEmployementHistories.AddObject(objEmployementHistory);
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Follow:
                        #region User Follow Feed Entry
                        FeedUserFollow objFollow = new FeedUserFollow
                        {
                            FeedId = mainFeedId,
                            FollowingUserId = refRecordId
                        };
                        context.FeedUserFollows.AddObject(objFollow);
                        context.SaveChanges();

                        addFeedUser(currentUserId, refRecordId);
                        #endregion
                        break;
                    case FeedType.Profile_Picture:
                        break;
                    case FeedType.Got_Connected:
                        addFeedUser(currentUserId, refRecordId);
                        break;
                    case FeedType.Job_Post:
                        #region User Job Post Feed Entry
                        FeedUserJob objJob = new FeedUserJob
                        {
                            FeedId = mainFeedId,
                            JobId = refRecordId
                        };
                        context.FeedUserJobs.AddObject(objJob);
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Profile_Viewed:
                        #region User Profile Visitor Feed Entry
                        FeedUserVisitor objVisitor = new FeedUserVisitor
                        {
                            FeedId = mainFeedId,
                            VisitorUserId = refRecordId
                        };
                        context.FeedUserVisitors.AddObject(objVisitor);
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Potfolio_Photo:
                    case FeedType.Like_User_Portfolio_Photo:
                    case FeedType.Comment_User_Portfolio_Photo:
                        #region User Porfolio Photo Update Feed Entry
                        FeedUserPorfolioPhoto objPorfolioPhoto = new FeedUserPorfolioPhoto
                        {
                            FeedId = mainFeedId,
                            PortfolioPhotoId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserPorfolioPhotoes.AddObject(objPorfolioPhoto);
                        context.SaveChanges();
                        if (multipleFeedId > 0)
                        {
                            FeedUserPorfolioPhoto objPorfolioPhoto1 = new FeedUserPorfolioPhoto
                            {
                                FeedId = multipleFeedId,
                                PortfolioPhotoId = refRecordId,
                                Comments = userComments
                            };
                            context.FeedUserPorfolioPhotoes.AddObject(objPorfolioPhoto1);
                            context.SaveChanges();
                        }
                        UpdateOwnerId(mainFeedId, objPorfolioPhoto.EmploymentHistoryPortfolio.EmploymentHistory.UserId);
                        #endregion
                        break;
                    case FeedType.User_Portfolio_video:
                    case FeedType.Like_User_Portfolio_Video:
                    case FeedType.Comment_User_Portfolio_Video:
                        #region User Porfolio Video Update Feed Entry
                        FeedUserPortfolioVideo objPortfolioVideo = new FeedUserPortfolioVideo
                        {
                            FeedId = mainFeedId,
                            PortfolioVideoId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserPortfolioVideos.AddObject(objPortfolioVideo);
                        context.SaveChanges();
                        UpdateOwnerId(mainFeedId, objPortfolioVideo.EmploymentHistoryVideo.EmploymentHistory.UserId);
                        #endregion
                        break;
                    case FeedType.Wall_Picture:
                    case FeedType.User_Photo:
                    case FeedType.Like_User_Photo:
                    case FeedType.Comment_User_Photo:
                        #region User Photo Update Feed Entry
                        FeedUserPhoto objPhoto = new FeedUserPhoto
                        {
                            FeedId = mainFeedId,
                            UserPhotoId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserPhotoes.AddObject(objPhoto);
                        context.SaveChanges();
                        if (multipleFeedId > 0)
                        {
                            FeedUserPhoto objPhoto1 = new FeedUserPhoto
                            {
                                FeedId = multipleFeedId,
                                UserPhotoId = refRecordId,
                                Comments = userComments
                            };
                            context.FeedUserPhotoes.AddObject(objPhoto1);
                            context.SaveChanges();
                        }
                        UpdateOwnerId(mainFeedId, objPhoto.UserPortfolio.UserId);
                        #endregion
                        break;
                    case FeedType.User_Video:
                    case FeedType.Like_User_Video:
                    case FeedType.Comment_User_Video:
                        #region User Video Update Feed Entry
                        FeedUserVideo objVideo = new FeedUserVideo
                        {
                            FeedId = mainFeedId,
                            UserVideoId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserVideos.AddObject(objVideo);
                        context.SaveChanges();
                        UpdateOwnerId(mainFeedId, objVideo.UserVideo.UserId ?? 0);
                        #endregion
                        break;
                    case FeedType.Company_Video:
                    case FeedType.Like_Company_Video:
                    case FeedType.Comment_Company_Video:
                        #region Company Video Update Feed Entry
                        FeedUserCompanyVideo objComVideo = new FeedUserCompanyVideo
                        {
                            FeedId = mainFeedId,
                            CompanyVideoId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserCompanyVideos.AddObject(objComVideo);
                        context.SaveChanges();
                        UpdateOwnerId(mainFeedId, objComVideo.CompanyVideo.Company.Userid ?? 0);
                        #endregion
                        break;
                    case FeedType.Company_Portfolio_Photo:
                    case FeedType.Like_Company_Portfolio_Photo:
                    case FeedType.Comment_Company_Portfolio_Photo:
                        #region Company Photo Update Feed Entry
                        FeedUserCompanyPhoto objCompPhoto = new FeedUserCompanyPhoto
                        {
                            FeedId = mainFeedId,
                            CompanyPhotoId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserCompanyPhotoes.AddObject(objCompPhoto);
                        context.SaveChanges();
                        if (multipleFeedId > 0)
                        {
                            FeedUserCompanyPhoto objCompPhoto1 = new FeedUserCompanyPhoto
                            {
                                FeedId = multipleFeedId,
                                CompanyPhotoId = refRecordId,
                                Comments = userComments
                            };
                            context.FeedUserCompanyPhotoes.AddObject(objCompPhoto1);
                            context.SaveChanges();
                        }
                        UpdateOwnerId(mainFeedId, objCompPhoto.CompanyPortfolio.Company.Userid ?? 0);
                        #endregion
                        break;
                    case FeedType.Like_Main_Feed:
                    case FeedType.Like_Comment:
                    case FeedType.Comment_Main_Feed:
                        #region Main Feed Like Update Feed Entry
                        FeedUserMainFeed objMainFeedLike = new FeedUserMainFeed
                        {
                            FeedId = mainFeedId,
                            ReferencedFeedId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserMainFeeds.AddObject(objMainFeedLike);
                        context.SaveChanges();
                        UpdateOwnerId(mainFeedId, objMainFeedLike.FeedUserMain1.UserId);
                        #endregion
                        break;
                    case FeedType.Like_Company_Profile:
                    case FeedType.Like_User_Profile:
                        #region User Interest Feed Entry
                        FeedUserProfile objProfile = new FeedUserProfile
                        {
                            FeedId = mainFeedId,
                            ProfileUserId = refRecordId
                        };
                        context.FeedUserProfiles.AddObject(objProfile);
                        context.SaveChanges();
                        UpdateOwnerId(mainFeedId, objProfile.ProfileUserId ?? 0);
                        #endregion
                        break;
                    case FeedType.Company_Product:
                    case FeedType.Like_Company_Product:
                    case FeedType.Comment_Company_Product:
                        #region Main Feed Like Update Feed Entry
                        FeedUserCompanyProduct objCompanyProduct = new FeedUserCompanyProduct
                        {
                            FeedId = mainFeedId,
                            CompanyProductId = refRecordId,
                            Comments = userComments
                        };
                        context.FeedUserCompanyProducts.AddObject(objCompanyProduct);
                        context.SaveChanges();
                        UpdateOwnerId(mainFeedId, objCompanyProduct.CompanyProduct.Company.Userid ?? 0);
                        #endregion
                        break;
                }
                #endregion
            }
            LoggingManager.Debug("Exiting addFeedNotification - Feedmanager");
        }

        private static void UpdateOwnerId(int mainFeedId, int userId)
        {
            LoggingManager.Debug("Entering UpdateOwnerId - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var mainfeed = (from m in context.FeedUserMains
                                where m.Id == mainFeedId
                                select m).FirstOrDefault();
                if (mainfeed != null)
                {
                    List<string> lstTypes = new List<string>()
                    {
                        FeedType.Comment_Company_Portfolio_Photo.ToString(),
                        FeedType.Comment_Company_Product.ToString(),
                        FeedType.Comment_Company_Video.ToString(),
                        FeedType.Comment_Main_Feed.ToString(),
                        FeedType.Comment_User_Feed.ToString(),
                        FeedType.Comment_User_Photo.ToString(),
                        FeedType.Comment_User_Portfolio_Photo.ToString(),
                        FeedType.Comment_User_Portfolio_Video.ToString(),
                        FeedType.Comment_User_Video.ToString(),
                        FeedType.Like_Comment.ToString(),
                        FeedType.Like_Company_Portfolio_Photo.ToString(),
                        FeedType.Like_Company_Product.ToString(),
                        FeedType.Like_Company_Profile.ToString(),
                        FeedType.Like_Company_Video.ToString(),
                        FeedType.Like_Main_Feed.ToString(),
                        FeedType.Like_User_Feed.ToString(),
                        FeedType.Like_User_Photo.ToString(),
                        FeedType.Like_User_Portfolio_Photo.ToString(),
                        FeedType.Like_User_Portfolio_Video.ToString(),
                        FeedType.Like_User_Profile.ToString(),
                        FeedType.Like_User_Video.ToString()                        
                    };
                    if (lstTypes.Contains(mainfeed.Type))
                    {
                        mainfeed.OwnerUserId = userId;
                        context.SaveChanges();
                    }
                }
            }
            LoggingManager.Debug("Exiting UpdateOwnerId - Feedmanager");
        }

        public static void addCommentFeedNotification(int feedId, int currentUserId, string userComments)
        {
            LoggingManager.Debug("Entering addFeedNotification - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var feed = (from m in context.FeedUserMains
                            where m.Id == feedId
                            select m).FirstOrDefault();
                if (feed != null)
                {
                    FeedType Type = FeedType.Comment_Main_Feed;
                    int refRecordId = 0;
                    #region Feed type
                    switch ((FeedManager.FeedType)Enum.Parse(typeof(FeedManager.FeedType), feed.Type))
                    {
                        case FeedManager.FeedType.User_Interest:
                        case FeedManager.FeedType.User_Skill:
                        case FeedManager.FeedType.User_Education:
                        case FeedManager.FeedType.User_School:
                        case FeedManager.FeedType.Current_Job:
                        case FeedManager.FeedType.Current_Company:
                        case FeedManager.FeedType.Follow:
                        case FeedManager.FeedType.Profile_Picture:
                        case FeedManager.FeedType.Got_Connected:
                        case FeedManager.FeedType.Endorsed:
                        case FeedManager.FeedType.Job_Post:
                        case FeedManager.FeedType.Profile_Viewed:
                        case FeedManager.FeedType.Multiple_Company_Portfolio_Photo:
                        case FeedManager.FeedType.Multiple_User_Photo:
                        case FeedManager.FeedType.Multiple_User_Potfolio_Photo:
                            Type = FeedType.Comment_Main_Feed;
                            refRecordId = feed.Id;
                            break;
                        case FeedManager.FeedType.Feed_Update:
                        case FeedManager.FeedType.Link_share:
                        case FeedManager.FeedType.Like_User_Feed:
                        case FeedManager.FeedType.Comment_User_Feed:
                            {
                                Type = FeedType.Comment_User_Feed;
                                var rec = feed.FeedUserUserFeeds.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.UserFeedId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.User_Potfolio_Photo:
                        case FeedManager.FeedType.Like_User_Portfolio_Photo:
                        case FeedManager.FeedType.Comment_User_Portfolio_Photo:
                            {
                                Type = FeedType.Comment_User_Portfolio_Photo;
                                var rec = feed.FeedUserPorfolioPhotoes.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.PortfolioPhotoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.User_Portfolio_video:
                        case FeedManager.FeedType.Like_User_Portfolio_Video:
                        case FeedManager.FeedType.Comment_User_Portfolio_Video:
                            {
                                Type = FeedType.Comment_User_Portfolio_Video;
                                var rec = feed.FeedUserPortfolioVideos.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.PortfolioVideoId ?? 0;
                            }
                            break;
                        case FeedType.Wall_Picture:
                        case FeedManager.FeedType.User_Photo:
                        case FeedManager.FeedType.Like_User_Photo:
                        case FeedManager.FeedType.Comment_User_Photo:
                            {
                                Type = FeedType.Comment_User_Photo;
                                var rec = feed.FeedUserPhotoes.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.UserPhotoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.User_Video:
                        case FeedManager.FeedType.Like_User_Video:
                        case FeedManager.FeedType.Comment_User_Video:
                            {
                                Type = FeedType.Comment_User_Video;
                                var rec = feed.FeedUserVideos.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.UserVideoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Company_Video:
                        case FeedManager.FeedType.Like_Company_Video:
                        case FeedManager.FeedType.Comment_Company_Video:
                            {
                                Type = FeedType.Comment_Company_Video;
                                var rec = feed.FeedUserCompanyVideos.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.CompanyVideoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Company_Portfolio_Photo:
                        case FeedManager.FeedType.Like_Company_Portfolio_Photo:
                        case FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                            {
                                Type = FeedType.Comment_Company_Portfolio_Photo;
                                var rec = feed.FeedUserCompanyPhotoes.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.CompanyPhotoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Like_Main_Feed:
                        case FeedManager.FeedType.Like_Comment:
                        case FeedManager.FeedType.Comment_Main_Feed:
                            {
                                Type = FeedType.Comment_Main_Feed;
                                var rec = feed.FeedUserMainFeeds.FirstOrDefault(a => a.FeedUserMain.IsDelete != true);
                                if (rec != null)
                                    refRecordId = rec.ReferencedFeedId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Like_Company_Profile:
                        case FeedManager.FeedType.Like_User_Profile:
                            Type = FeedType.Comment_Main_Feed;
                            refRecordId = feed.Id;
                            break;
                        case FeedType.Company_Product:
                        case FeedManager.FeedType.Like_Company_Product:
                        case FeedManager.FeedType.Comment_Company_Product:
                            {
                                Type = FeedType.Comment_Company_Product;
                                var rec = feed.FeedUserCompanyProducts.FirstOrDefault(a => a.FeedUserMain.IsDelete != true);
                                if (rec != null)
                                    refRecordId = rec.CompanyProductId ?? 0;
                            }
                            break;
                    }
                    #endregion

                    addFeedNotification(Type, currentUserId, refRecordId, userComments);
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "commented on a feed in Huntable";
                    socialManager.ShareOnFacebook(currentUserId, msg, "");
                }
            }
            LoggingManager.Debug("Exiting addFeedNotification - Feedmanager");
        }

        static void addFeedUser(int currentUserId, int? derivedUserId)
        {
            LoggingManager.Debug("Entering addFeedUser - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                FeedUserDerivedUser objUser = new FeedUserDerivedUser()
                {
                    DerivedUserId = derivedUserId,
                    UserId = currentUserId,
                    CreatedDate = DateTime.Now
                };
                context.FeedUserDerivedUsers.AddObject(objUser);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting addFeedUser - Feedmanager");
        }

        #endregion

        #region Feed Entry - Delete

        public static void deleteFeedNotitifation(FeedType type, int currentUserId, int refRecordId)
        {
            LoggingManager.Debug("Entering deleteFeedNotitifation - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                string typeStr = type.ToString();
                switch (type)
                {
                    case FeedType.User_Interest:
                        #region User Interest Feed Entry
                        var objInterest = context.FeedUserInterests.Where(a => a.MasterInterestId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objInterest)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Skill:
                        #region User Skill Feed Entry
                        var objSkill = context.FeedUserSkills.Where(a => a.MasterSkillId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objSkill)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Education:
                    case FeedType.User_School:
                        #region User Education History Feed Entry
                        var objEducationSchool = context.FeedUserEducationSchools.Where(a => a.EducationHistoryId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objEducationSchool)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Feed_Update:
                    case FeedType.Link_share:
                    case FeedType.Like_User_Feed:
                    case FeedType.Comment_User_Feed:
                        #region User Feed Feed Entry
                        var objUserFeed = context.FeedUserUserFeeds.Where(a => a.UserFeedId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objUserFeed)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Current_Job:
                    case FeedType.Current_Company:
                    case FeedType.Endorsed:
                        #region User Employement History Feed Entry
                        var objEmployementHistory = context.FeedUserEmployementHistories.Where(a => a.EmploymentHistoryId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objEmployementHistory)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Follow:
                        #region User Follow Feed Entry
                        var objFollow = context.FeedUserFollows.Where(a => a.FollowingUserId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objFollow)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        deleteFeedUser(currentUserId, refRecordId);
                        #endregion
                        break;
                    case FeedType.Profile_Picture:
                        var objProfilePicture = context.FeedUserMains.Where(a => a.UserId == refRecordId && a.Type == typeStr && a.UserId == currentUserId);
                        foreach (var data in objProfilePicture)
                        {
                            data.IsDelete = true;
                            foreach (var subdata in data.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        break;
                    case FeedType.Got_Connected:
                        var objfeed = context.FeedUserMains.Where(a => a.UserId == refRecordId && a.Type == typeStr && a.UserId == currentUserId);
                        foreach (var data in objfeed)
                        {
                            data.IsDelete = true;
                            foreach (var subdata in data.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        break;
                    case FeedType.Job_Post:
                        #region User Job Post Feed Entry
                        var objJob = context.FeedUserJobs.Where(a => a.JobId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objJob)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Profile_Viewed:
                        #region User Profile Visitor Feed Entry
                        var objVisitor = context.FeedUserVisitors.Where(a => a.VisitorUserId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objVisitor)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Potfolio_Photo:
                    case FeedType.Like_User_Portfolio_Photo:
                    case FeedType.Comment_User_Portfolio_Photo:
                        #region User Porfolio Photo Update Feed Entry
                        var objPorfolioPhoto = context.FeedUserPorfolioPhotoes.Where(a => a.PortfolioPhotoId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objPorfolioPhoto)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Portfolio_video:
                    case FeedType.Like_User_Portfolio_Video:
                    case FeedType.Comment_User_Portfolio_Video:
                        #region User Porfolio Video Update Feed Entry
                        var objPortfolioVideo = context.FeedUserPortfolioVideos.Where(a => a.PortfolioVideoId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objPortfolioVideo)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Wall_Picture:
                    case FeedType.User_Photo:
                    case FeedType.Like_User_Photo:
                    case FeedType.Comment_User_Photo:
                        #region User Photo Update Feed Entry
                        var objPhoto = context.FeedUserPhotoes.Where(a => a.UserPhotoId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objPhoto)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.User_Video:
                    case FeedType.Like_User_Video:
                    case FeedType.Comment_User_Video:
                        #region User Video Update Feed Entry
                        var objVideo = context.FeedUserVideos.Where(a => a.UserVideoId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objVideo)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Company_Video:
                    case FeedType.Like_Company_Video:
                    case FeedType.Comment_Company_Video:
                        #region Company Video Update Feed Entry
                        var objComVideo = context.FeedUserCompanyVideos.Where(a => a.CompanyVideoId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objComVideo)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Company_Portfolio_Photo:
                    case FeedType.Like_Company_Portfolio_Photo:
                    case FeedType.Comment_Company_Portfolio_Photo:
                        #region Company Photo Update Feed Entry
                        var objCompPhoto = context.FeedUserCompanyPhotoes.Where(a => a.CompanyPhotoId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objCompPhoto)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Like_Main_Feed:
                    case FeedType.Like_Comment:
                    case FeedType.Comment_Main_Feed:
                    case FeedType.Multiple_Company_Portfolio_Photo:
                    case FeedType.Multiple_User_Photo:
                    case FeedType.Multiple_User_Potfolio_Photo:
                        #region Main Feed Like Update Feed Entry
                        var objMainFeedLike = context.FeedUserMainFeeds.Where(a => a.ReferencedFeedId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objMainFeedLike)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Like_Company_Profile:
                    case FeedType.Like_User_Profile:
                        #region User Profile Feed Entry
                        var objProfile = context.FeedUserProfiles.Where(a => a.ProfileUserId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objProfile)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                    case FeedType.Company_Product:
                    case FeedType.Like_Company_Product:
                    case FeedType.Comment_Company_Product:
                        #region Main Feed Like Update Feed Entry
                        var objCompanyProduct = context.FeedUserCompanyProducts.Where(a => a.CompanyProductId == refRecordId && a.FeedUserMain.UserId == currentUserId);
                        foreach (var data in objCompanyProduct)
                        {
                            data.FeedUserMain.IsDelete = true;
                            foreach (var subdata in data.FeedUserMain.FeedUserMainFeeds)
                            {
                                subdata.FeedUserMain.IsDelete = true;
                            }
                        }
                        context.SaveChanges();
                        #endregion
                        break;
                }
            }
            LoggingManager.Debug("Exiting deleteFeedNotitifation - Feedmanager");
        }
        static void deleteFeedUser(int currentUserId, int? derivedUserId)
        {
            LoggingManager.Debug("Entering deleteFeedUser - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var obj = context.FeedUserDerivedUsers.Where(a => a.UserId == currentUserId && a.DerivedUserId == derivedUserId);
                foreach (var data in obj)
                {
                    context.FeedUserDerivedUsers.DeleteObject(data);
                }
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting deleteFeedUser - Feedmanager");
        }
        public static void deleteFeed(int FeedId)
        {
            LoggingManager.Debug("Entering deleteFeed - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var feed = (from r in context.FeedUserMains
                            where r.Id == FeedId
                            select r).FirstOrDefault();
                if (feed != null)
                {
                    feed.IsDelete = true;
                    foreach (var subdata in feed.FeedUserMainFeeds)
                    {
                        subdata.FeedUserMain.IsDelete = true;
                    }
                    context.SaveChanges();
                }
            }
            LoggingManager.Debug("Exiting deleteFeed - Feedmanager");
        }
        public static void HideFeed(int FeedId, int UserId)
        {
            LoggingManager.Debug("Entering deleteFeed - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                FeedUserMainHiddenFeed hide = new FeedUserMainHiddenFeed();
                hide.HiddenFeedId = FeedId;
                hide.UserId = UserId;
                context.FeedUserMainHiddenFeeds.AddObject(hide);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting deleteFeed - Feedmanager");
        }
        #endregion

        #region Feed Entry - Read

        public static FeedList GetFeedsPagewise(FeedRetrievePage pageType, int currentUserId, int profileUserId, int LatestFeedId, int PageIndex, int Pagesize)
        {
            LoggingManager.Debug("Entering GetFeedsPagewise - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                FeedList objResult = new FeedList();
                objResult.pageSize = Pagesize;

                objResult.LatestFeedId = LatestFeedId;
                objResult.PageIndex = PageIndex + 1;

                List<int> retrieveUserList_1st = new List<int>();
                List<int> retrieveUserList_2nd = new List<int>();
                List<int> retrieveUserList_3rd = new List<int>();

                #region retrieveFeedUserList
                switch (pageType)
                {
                    case FeedRetrievePage.Business_Activity:
                    case FeedRetrievePage.Visual_CV_Activity:
                        #region retrieveConnectedUserList
                        retrieveUserList_1st = (from e in context.Users
                                                where e.ReferralId == profileUserId
                                                select e.Id).ToList();

                        retrieveUserList_2nd = (from e in context.Users
                                                where e.User1.Any(a => a.ReferralId == profileUserId)
                                                select e.Id).ToList();

                        retrieveUserList_3rd = (from e in context.Users
                                                where e.User1.Any(a => a.User1.Any(b => b.ReferralId == profileUserId))
                                                select e.Id).ToList();
                        #endregion
                        break;
                    case FeedRetrievePage.Employee_Activity:
                        break;
                    case FeedRetrievePage.Networking:
                        #region retrieveConnectedUserList
                        retrieveUserList_1st = (from e in context.Users
                                                where e.ReferralId == profileUserId
                                                select e.Id).ToList();

                        retrieveUserList_2nd = (from e in context.Users
                                                where e.User1.Any(a => a.ReferralId == profileUserId)
                                                select e.Id).ToList();

                        retrieveUserList_3rd = (from e in context.Users
                                                where e.User1.Any(a => a.User1.Any(b => b.ReferralId == profileUserId))
                                                select e.Id).ToList();
                        #endregion
                        break;
                }
                #endregion
                #region Commented code

                //#region feedType
                //List<string> feedType = new List<string>();
                //feedType.AddRange(Enum.GetNames(typeof(FeedType)));
                //feedType.Remove(FeedType.Got_Connected.ToString());
                //feedType.Remove(FeedType.Profile_Viewed.ToString());
                //#endregion

                //string Connect = FeedType.Got_Connected.ToString();
                //string profile_view = FeedType.Profile_Viewed.ToString();
                //var rec = from m in context.FeedUserMains
                //          where feedType.Contains(m.Type)
                //          && (retrieveFeedUserList.Any(a => a == m.UserId)
                //              || retrieveUserList_1st.AsEnumerable().Any(a => a == m.UserId && m.Type == Connect)
                //              || retrieveUserList_2nd.AsEnumerable().Any(a => a == m.UserId && m.Type == Connect)
                //              || retrieveUserList_3rd.AsEnumerable().Any(a => a == m.UserId && m.Type == Connect)
                //              )
                //         && (
                //         (LatestFeedId == 0 || m.Id > LatestFeedId)
                //         && (OldestFeedId == 0 || m.Id < OldestFeedId)
                //         )
                //          && m.IsDelete != true
                //          select m;

                //switch (pageType)
                //{
                //    case FeedRetrievePage.Employee_Activity:
                //        break;
                //    case FeedRetrievePage.Business_Activity:
                //    case FeedRetrievePage.Visual_CV_Activity:
                //        rec = rec.Union(
                //              from m in context.FeedUserMains
                //              where m.Type == profile_view
                //              && m.UserId == currentUserId
                //              && m.IsDelete != true
                //         && (
                //         (LatestFeedId == 0 || m.Id > LatestFeedId)
                //         && (OldestFeedId == 0 || m.Id < OldestFeedId)
                //         )
                //              select m
                //            );
                //        break;
                //    case FeedRetrievePage.Networking:
                //        rec = rec.Union(
                //              from m in context.FeedUserMains
                //              where m.Type == profile_view
                //              && m.FeedUserVisitors.Any(a => a.VisitorUserId == currentUserId)
                //              && m.IsDelete != true
                //         && (
                //         (LatestFeedId == 0 || m.Id > LatestFeedId)
                //         && (OldestFeedId == 0 || m.Id < OldestFeedId)
                //         )
                //              select m
                //            );
                //        break;
                //}
                //rec = rec.OrderByDescending(a => a.Date);
                #endregion


                #region Retrieve feeds
                ObjectParameter output = new ObjectParameter("totalRecords", typeof(int));
                var data = context.GetFeedList((int)pageType, currentUserId, profileUserId, LatestFeedId, PageIndex, Pagesize, output).Select(a => a.Id).ToList();
                var rec = from m in context.FeedUserMains
                          where data.Contains(m.Id)
                          orderby m.Date descending
                          select m;
                if (rec != null && rec.Count() > 0)
                {
                    objResult.totalRecords = (int)output.Value;
                    objResult.feeds = new List<FeedDetail>();
                    //var MainFeeds = rec.Take(Pagesize);
                    //if (LatestFeedId != 0)
                    //    MainFeeds = rec;
                    //if (MainFeeds != null && MainFeeds.Count() > 0)
                    //{
                    foreach (var feed in rec)
                    {
                        var feedData = GetFeedDetail(feed, currentUserId, retrieveUserList_1st, retrieveUserList_2nd, retrieveUserList_3rd);
                        feedData.MainfeedId = feed.Id;
                        objResult.feeds.Add(feedData);
                    }
                    if (objResult.feeds.Count > 0)
                    {
                        if (LatestFeedId != 0)
                        {
                            int id = objResult.feeds.FirstOrDefault().MainfeedId;
                            if (LatestFeedId < id)
                                objResult.LatestFeedId = id;
                        }
                        //else if (OldestFeedId != 0)
                        //{
                        //    int id = objResult.feeds.LastOrDefault().feedId;
                        //    if (OldestFeedId > id)
                        //        objResult.OldestFeedId = id;
                        //}
                        else
                        {
                            int id = objResult.feeds.FirstOrDefault().MainfeedId;
                            if (LatestFeedId < id)
                                objResult.LatestFeedId = id;

                            //id = objResult.feeds.LastOrDefault().feedId;
                            //objResult.OldestFeedId = id;
                        }
                    }
                    //}
                }
                #endregion
                return objResult;
            }
            LoggingManager.Debug("Exiting GetFeedsPagewise - Feedmanager");
        }
        public static FeedDetail GetFeedDetail(FeedUserMain feed, int currentUserId, List<int> retrieveUserList_1st, List<int> retrieveUserList_2nd, List<int> retrieveUserList_3rd)
        {
            FeedDetail obj = new FeedDetail();
            obj.feedId = feed.Id;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                #region Feed Description
                switch ((FeedType)Enum.Parse(typeof(FeedType), feed.Type))
                {
                    case FeedType.User_Interest:
                        obj.feedDescription = FeedContentManager.getFeed_User_Interest(feed, currentUserId);
                        break;
                    case FeedType.User_Skill:
                        obj.feedDescription = FeedContentManager.getFeed_User_Skill(feed, currentUserId);
                        break;
                    case FeedType.User_Education:
                    case FeedType.User_School:
                        obj.feedDescription = FeedContentManager.getFeed_User_EducationHistory(feed, currentUserId);
                        break;
                    case FeedType.Feed_Update:
                    case FeedType.Link_share:
                        obj.feedDescription = FeedContentManager.getFeed_Link_share(feed, currentUserId);
                        break;
                    case FeedType.Current_Job:
                    case FeedType.Current_Company:
                    case FeedType.Endorsed:
                        obj.feedDescription = FeedContentManager.getFeed_User_EmployementHistory(feed, currentUserId);
                        break;
                    case FeedType.Follow:
                        obj.feedDescription = FeedContentManager.getFeed_Follow(feed, currentUserId);
                        break;
                    case FeedType.Profile_Picture:
                        obj.feedDescription = FeedContentManager.getFeed_Profile_Picture(feed, currentUserId);
                        break;
                    case FeedType.Got_Connected:
                        obj.feedDescription = FeedContentManager.getFeed_Got_Connected(feed, currentUserId
                            , retrieveUserList_1st.Any(a => a == feed.UserId)
                            , retrieveUserList_2nd.Any(a => a == feed.UserId)
                            , retrieveUserList_3rd.Any(a => a == feed.UserId));
                        break;
                    case FeedType.Job_Post:
                        obj.feedDescription = FeedContentManager.getFeed_Job_Post(feed, currentUserId);
                        break;
                    case FeedType.Profile_Viewed:
                        obj.feedDescription = FeedContentManager.getFeed_Profile_Viewed(feed, currentUserId);
                        break;
                    case FeedType.User_Potfolio_Photo:
                    case FeedType.Multiple_User_Potfolio_Photo:
                        obj.feedDescription = FeedContentManager.getFeed_User_Potfolio_Photo(feed, currentUserId);
                        break;
                    case FeedType.User_Portfolio_video:
                        obj.feedDescription = FeedContentManager.getFeed_User_Portfolio_video(feed, currentUserId);
                        break;
                    case FeedType.User_Photo:
                    case FeedType.Multiple_User_Photo:
                        obj.feedDescription = FeedContentManager.getFeed_User_Photo(feed, currentUserId);
                        break;
                    case FeedType.Wall_Picture:
                        obj.feedDescription = FeedContentManager.getFeed_Wall_Picture(feed, currentUserId);
                        break;
                    case FeedType.User_Video:
                        obj.feedDescription = FeedContentManager.getFeed_User_video(feed, currentUserId);
                        break;
                    case FeedType.Company_Video:
                        obj.feedDescription = FeedContentManager.getFeed_Company_Video(feed, currentUserId);
                        break;
                    case FeedType.Company_Portfolio_Photo:
                    case FeedType.Multiple_Company_Portfolio_Photo:
                        obj.feedDescription = FeedContentManager.getFeed_Company_Portfolio_Photo(feed, currentUserId);
                        break;
                    case FeedType.Company_Product:
                        obj.feedDescription = FeedContentManager.getFeed_Company_Product(feed, currentUserId);
                        break;
                    case FeedType.Like_User_Feed:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Link_share(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_User_Feed:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Link_share(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    case FeedType.Like_User_Portfolio_Photo:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_Potfolio_Photo(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_User_Portfolio_Photo:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_Potfolio_Photo(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    case FeedType.Like_User_Portfolio_Video:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_Portfolio_video(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_User_Portfolio_Video:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_Portfolio_video(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    case FeedType.Like_User_Photo:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_Photo(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_User_Photo:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_Photo(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    case FeedType.Like_User_Video:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_video(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_User_Video:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_User_video(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    case FeedType.Like_Company_Video:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Company_Video(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_Company_Video:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Company_Video(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    case FeedType.Like_Company_Portfolio_Photo:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Company_Portfolio_Photo(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_Company_Portfolio_Photo:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Company_Portfolio_Photo(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    case FeedType.Like_Main_Feed:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                //case FeedType.Like_Comment:
                                obj.feedDescription = FeedContentManager.getLikeComment_Main_Feed(feed, objMain, currentUserId, true, retrieveUserList_1st, retrieveUserList_2nd, retrieveUserList_3rd);
                            }
                        }
                        break;
                    case FeedType.Comment_Main_Feed:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Main_Feed(feed, objMain, currentUserId, false, retrieveUserList_1st, retrieveUserList_2nd, retrieveUserList_3rd);
                            }
                        }
                        break;
                    case FeedType.Like_Company_Profile:
                    case FeedType.Like_User_Profile:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Profile(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Like_Company_Product:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Company_Product(feed, objMain, currentUserId, true);
                            }
                        }
                        break;
                    case FeedType.Comment_Company_Product:
                        {
                            var objMain = getMainFeed(feed);
                            if (objMain != null)
                            {
                                obj.feedId = objMain.Id;
                                obj.feedDescription = FeedContentManager.getLikeComment_Company_Product(feed, objMain, currentUserId, false);
                            }
                        }
                        break;
                    default:
                        obj.feedDescription = FeedContentManager.getFeed_Common_Content(feed, currentUserId);
                        break;
                }
                #endregion
            }
            return obj;
        }

        public static FeedLikeDetail GetFeedLikes(int feedId, int currentUserId, FeedType type, int refRecordId)
        {
            LoggingManager.Debug("Entering GetFeedLikes - Feedmanager");

            FeedLikeDetail objResult = new FeedLikeDetail();
            objResult.feedId = feedId;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                string strType = type.ToString();

                #region Feed type
                switch (type)
                {
                    case FeedManager.FeedType.Like_User_Feed:
                        #region Like_User_Feed
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserUserFeeds.Any(a => a.UserFeedId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);                            
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Portfolio_Photo:
                        #region Like_User_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                            
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Portfolio_Video:
                        #region Like_User_Portfolio_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                           
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Photo:
                        #region Like_User_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPhotoes.Any(a => a.UserPhotoId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);                                                      
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Video:
                        #region Like_User_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserVideos.Any(a => a.UserVideoId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                            
                            
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Company_Video:
                        #region Like_Company_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                          
                            
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Company_Portfolio_Photo:
                        #region Like_Company_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                            
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Comment:
                        #region Like_Comment
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == refRecordId && a.FeedUserMain.IsDelete != true)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                            
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Main_Feed:
                        #region Like_Main_Feed
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);                           
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Company_Profile:
                        #region Like_Company_Profile
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserProfiles.Any(a => a.ProfileUserId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                            
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Profile:
                        #region Like_User_Profile
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserProfiles.Any(a => a.ProfileUserId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(),
                                                                                    rec.Any(
                                                                                        a => a.UserId == currentUserId),
                                                                                    feedId, type.ToString(), refRecordId);
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Company_Product:
                        #region Like_Company_Product
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == refRecordId)
                                      select r;
                            objResult.LikeCount = rec.Count();
                            objResult.LikeLinkHTML = FeedContentManager.getLikeLink(feedId, rec.Count(), rec.Any(a => a.UserId == currentUserId)
                                , type.ToString(), refRecordId);
                            objResult.LikeHeader = FeedContentManager.getLikeHeader(rec.Count(), rec.Any(a => a.UserId == currentUserId),
                                feedId, type.ToString(), refRecordId);
                           
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                #endregion
            }
            return objResult;

            LoggingManager.Debug("Exiting GetFeedLikes - Feedmanager");
        }

        public static FeedCommentDetail GetFeedComments(int feedId, int currentUserId)
        {
            LoggingManager.Debug("Entering GetFeedComments - Feedmanager");
            FeedCommentDetail objComment = new FeedCommentDetail();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var feed = (from m in context.FeedUserMains
                            where m.Id == feedId
                            select m).FirstOrDefault();
                if (feed != null)
                {
                    int oldestFeedId = 0;
                    int Pagesize = 3;
                    #region Feed type
                    switch ((FeedManager.FeedType)Enum.Parse(typeof(FeedManager.FeedType), feed.Type))
                    {
                        case FeedManager.FeedType.User_Interest:
                        case FeedManager.FeedType.User_Skill:
                        case FeedManager.FeedType.User_Education:
                        case FeedManager.FeedType.User_School:
                        case FeedManager.FeedType.Current_Job:
                        case FeedManager.FeedType.Current_Company:
                        case FeedManager.FeedType.Follow:
                        case FeedManager.FeedType.Profile_Picture:
                        case FeedManager.FeedType.Got_Connected:
                        case FeedManager.FeedType.Endorsed:
                        case FeedManager.FeedType.Job_Post:
                        case FeedManager.FeedType.Profile_Viewed:
                        case FeedManager.FeedType.Multiple_Company_Portfolio_Photo:
                        case FeedManager.FeedType.Multiple_User_Photo:
                        case FeedManager.FeedType.Multiple_User_Potfolio_Photo:
                        case FeedManager.FeedType.Like_Company_Profile:
                        case FeedManager.FeedType.Like_User_Profile:
                            objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Main_Feed, feed.Id);
                            break;
                        case FeedManager.FeedType.Feed_Update:
                        case FeedManager.FeedType.Link_share:
                        case FeedManager.FeedType.Like_User_Feed:
                        case FeedManager.FeedType.Comment_User_Feed:
                            {
                                var rec = feed.FeedUserUserFeeds.FirstOrDefault();
                                if (rec != null)
                                {
                                    int refId = rec.UserFeedId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Feed, refId);
                                }
                            }
                            break;
                        case FeedManager.FeedType.User_Potfolio_Photo:
                        case FeedManager.FeedType.Like_User_Portfolio_Photo:
                        case FeedManager.FeedType.Comment_User_Portfolio_Photo:
                            {
                                var rec = feed.FeedUserPorfolioPhotoes.FirstOrDefault();
                                if (rec != null)
                                {
                                    int refId = rec.PortfolioPhotoId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Portfolio_Photo, refId);
                                }
                            }
                            break;
                        case FeedManager.FeedType.User_Portfolio_video:
                        case FeedManager.FeedType.Like_User_Portfolio_Video:
                        case FeedManager.FeedType.Comment_User_Portfolio_Video:
                            {
                                var rec = feed.FeedUserPortfolioVideos.FirstOrDefault();
                                if (rec != null)
                                {
                                    int refId = rec.PortfolioVideoId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Portfolio_Video, refId);
                                }
                            }
                            break;
                        case FeedManager.FeedType.Wall_Picture:
                        case FeedManager.FeedType.User_Photo:
                        case FeedManager.FeedType.Like_User_Photo:
                        case FeedManager.FeedType.Comment_User_Photo:
                            {
                                var rec = feed.FeedUserPhotoes.FirstOrDefault();
                                if (rec != null)
                                {
                                    int refId = rec.UserPhotoId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Photo, refId);
                                }
                            }
                            break;
                        case FeedManager.FeedType.User_Video:
                        case FeedManager.FeedType.Like_User_Video:
                        case FeedManager.FeedType.Comment_User_Video:
                            {
                                var rec = feed.FeedUserVideos.FirstOrDefault();
                                if (rec != null)
                                {
                                    int refId = rec.UserVideoId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Video, refId);
                                }
                            }
                            break;
                        case FeedManager.FeedType.Company_Video:
                        case FeedManager.FeedType.Like_Company_Video:
                        case FeedManager.FeedType.Comment_Company_Video:
                            {
                                var rec = feed.FeedUserCompanyVideos.FirstOrDefault();
                                if (rec != null)
                                {
                                    int refId = rec.CompanyVideoId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Company_Video, refId);
                                }
                            }
                            break;
                        case FeedManager.FeedType.Company_Portfolio_Photo:
                        case FeedManager.FeedType.Like_Company_Portfolio_Photo:
                        case FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                            {
                                var rec = feed.FeedUserCompanyPhotoes.FirstOrDefault();
                                if (rec != null)
                                {
                                    int refId = rec.CompanyPhotoId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Company_Portfolio_Photo, refId);
                                }
                            }
                            break;
                        //case FeedManager.FeedType.Like_Comment:
                        case FeedManager.FeedType.Like_Main_Feed:
                        case FeedManager.FeedType.Comment_Main_Feed:
                            {
                                var rec = feed.FeedUserMainFeeds.FirstOrDefault(a => a.FeedUserMain.IsDelete != true);
                                if (rec != null)
                                {
                                    int refId = rec.ReferencedFeedId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Main_Feed, refId);
                                }
                            }
                            break;
                        case FeedManager.FeedType.Company_Product:
                        case FeedManager.FeedType.Like_Company_Product:
                        case FeedManager.FeedType.Comment_Company_Product:
                            {
                                var rec = feed.FeedUserCompanyProducts.FirstOrDefault(a => a.FeedUserMain.IsDelete != true);
                                if (rec != null)
                                {
                                    int refId = rec.CompanyProductId ?? 0;
                                    objComment = FeedManager.GetFeedComments(feed.Id, feed.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Company_Product, refId);
                                }
                            }
                            break;
                    }
                    #endregion
                }
            }
            return objComment;
            LoggingManager.Debug("Exiting GetFeedComments - Feedmanager");
        }

        public static FeedCommentDetail GetFeedComments(int feedId, int feedUserId, int currentUserId, int OldestFeedId, int Pagesize, FeedType type, int refRecordId)
        {
            LoggingManager.Debug("Entering GetFeedComments - Feedmanager");
            //OldestFeedId = 0;
            //Pagesize = 10;
            FeedCommentDetail objResult = new FeedCommentDetail();
            objResult.feedId = feedId;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                string strType = type.ToString();
                #region Feed type
                switch (type)
                {
                    case FeedManager.FeedType.Comment_User_Feed:
                        #region Comment_User_Feed
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserUserFeeds.Any(a => a.UserFeedId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserUserFeeds.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Portfolio_Photo:
                        #region Comment_User_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserPorfolioPhotoes.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Portfolio_Video:
                        #region Comment_User_Portfolio_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserPortfolioVideos.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Photo:
                        #region Comment_User_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPhotoes.Any(a => a.UserPhotoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserPhotoes.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Video:
                        #region Comment_User_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserVideos.Any(a => a.UserVideoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserVideos.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Company_Video:
                        #region Comment_Company_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserCompanyVideos.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                        #region Comment_Company_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserCompanyPhotoes.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Main_Feed:
                        #region Comment_Main_Feed
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserMainFeeds.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Company_Product:
                        #region Comment_Company_Product
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where (OldestFeedId == 0 || r.Id < OldestFeedId)
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserCompanyProducts.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                if (res.Count() > Pagesize)
                                {
                                    res = res.Skip(res.Count() - Pagesize).Take(Pagesize);
                                    objResult.DisplayedCommentCount += Pagesize;
                                }
                                else
                                {
                                    objResult.DisplayedCommentCount += res.Count();
                                }
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                }
                #endregion
            }
            return objResult;

            LoggingManager.Debug("Exiting GetFeedComments - Feedmanager");
        }

        public static FeedCommentDetail GetLatestFeedComments(int feedId, int feedUserId, FeedType Type, int refRecordId, int currentUserId, int LatestFeedId)
        {
            LoggingManager.Debug("Entering GetLatestFeedComments - Feedmanager");

            FeedCommentDetail objResult = new FeedCommentDetail();
            objResult.feedId = feedId;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                string strType = Type.ToString();
                #region Feed type
                switch (Type)
                {
                    case FeedManager.FeedType.Comment_User_Feed:
                        #region Comment_User_Feed
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserUserFeeds.Any(a => a.UserFeedId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserUserFeeds.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Portfolio_Photo:
                        #region Comment_User_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserPorfolioPhotoes.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();

                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Portfolio_Video:
                        #region Comment_User_Portfolio_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserPortfolioVideos.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();

                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Photo:
                        #region Comment_User_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserPhotoes.Any(a => a.UserPhotoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserPhotoes.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();
                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_User_Video:
                        #region Comment_User_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserVideos.Any(a => a.UserVideoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserVideos.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();

                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Company_Video:
                        #region Comment_Company_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserCompanyVideos.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();

                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                        #region Comment_Company_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserCompanyPhotoes.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();

                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Main_Feed:
                        #region Comment_Main_Feed
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == refRecordId && a.FeedUserMain.IsDelete != true)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserMainFeeds.FirstOrDefault(a => a.FeedUserMain.IsDelete != true).Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();

                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Comment_Company_Product:
                        #region Comment_Company_Product
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strType
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == refRecordId)
                                      select r;
                            if (rec.Count() > 0)
                            {
                                objResult.TotalCommentCount = rec.Count();
                                var res = (from r in rec
                                           where r.Id > LatestFeedId
                                           orderby r.Date ascending
                                           select new FeedComment
                                           {
                                               feedId = r.Id,
                                               date = r.Date,
                                               name = r.User.FirstName + " " + r.User.LastName,
                                               profilePictureId = r.User.PersonalLogoFileStoreId,
                                               comments = r.FeedUserCompanyProducts.FirstOrDefault().Comments,
                                               userId = r.UserId,
                                               mainFeedUserId = feedUserId
                                           });
                                objResult.DisplayedCommentCount = (objResult.TotalCommentCount - res.Count());
                                objResult.DisplayedCommentCount += res.Count();

                                objResult.comments = FeedContentManager.getComments(res.ToList(), currentUserId);
                                objResult.OldestCommentId = objResult.comments.FirstOrDefault().feedId;
                                objResult.LatestCommentId = objResult.comments.LastOrDefault().feedId;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                            }
                            else
                            {
                                objResult.TotalCommentCount = 0;
                                objResult.DisplayedCommentCount = 0;
                                objResult.OldestCommentId = 0;
                                objResult.LatestCommentId = 0;
                                objResult.CommentHeader = FeedContentManager.getCommentHeaderLink(feedId, feedUserId, objResult.OldestCommentId, Type, refRecordId, objResult.DisplayedCommentCount, objResult.TotalCommentCount);
                                objResult.CommentLinkHTML = FeedContentManager.getCommentLink(feedId);
                                objResult.comments = null;
                            }
                        }
                        #endregion
                        break;
                }
                #endregion
            }

            LoggingManager.Debug("Exiting GetLatestFeedComments - Feedmanager");
            return objResult;
        }

        public static FeedCommentDetail GetLatestFeedComments(int feedId, int currentUserId, int LatestFeedId)
        {
            LoggingManager.Debug("Entering GetLatestFeedComments - Feedmanager");

            FeedCommentDetail objResult = new FeedCommentDetail();
            objResult.feedId = feedId;
            int feedUserId = 0;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                FeedType Type = FeedType.Comment_Main_Feed;
                int refRecordId = 0;
                var feed = (from m in context.FeedUserMains
                            where m.Id == feedId
                            select m).FirstOrDefault();
                if (feed != null)
                {
                    feedUserId = feed.UserId;
                    #region Feed type
                    switch ((FeedManager.FeedType)Enum.Parse(typeof(FeedManager.FeedType), feed.Type))
                    {
                        case FeedManager.FeedType.User_Interest:
                        case FeedManager.FeedType.User_Skill:
                        case FeedManager.FeedType.User_Education:
                        case FeedManager.FeedType.User_School:
                        case FeedManager.FeedType.Current_Job:
                        case FeedManager.FeedType.Current_Company:
                        case FeedManager.FeedType.Follow:
                        case FeedManager.FeedType.Profile_Picture:
                        case FeedManager.FeedType.Got_Connected:
                        case FeedManager.FeedType.Endorsed:
                        case FeedManager.FeedType.Job_Post:
                        case FeedManager.FeedType.Profile_Viewed:
                        case FeedManager.FeedType.Multiple_Company_Portfolio_Photo:
                        case FeedManager.FeedType.Multiple_User_Photo:
                        case FeedManager.FeedType.Multiple_User_Potfolio_Photo:
                        case FeedManager.FeedType.Like_Company_Profile:
                        case FeedManager.FeedType.Like_User_Profile:
                            Type = FeedType.Comment_Main_Feed;
                            refRecordId = feed.Id;
                            break;
                        case FeedManager.FeedType.Feed_Update:
                        case FeedManager.FeedType.Link_share:
                        case FeedManager.FeedType.Like_User_Feed:
                        case FeedManager.FeedType.Comment_User_Feed:
                            {
                                Type = FeedType.Comment_User_Feed;
                                var rec = feed.FeedUserUserFeeds.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.UserFeedId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.User_Potfolio_Photo:
                        case FeedManager.FeedType.Like_User_Portfolio_Photo:
                        case FeedManager.FeedType.Comment_User_Portfolio_Photo:
                            {
                                Type = FeedType.Comment_User_Portfolio_Photo;
                                var rec = feed.FeedUserPorfolioPhotoes.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.PortfolioPhotoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.User_Portfolio_video:
                        case FeedManager.FeedType.Like_User_Portfolio_Video:
                        case FeedManager.FeedType.Comment_User_Portfolio_Video:
                            {
                                Type = FeedType.Comment_User_Portfolio_Video;
                                var rec = feed.FeedUserPortfolioVideos.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.PortfolioVideoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Wall_Picture:
                        case FeedManager.FeedType.User_Photo:
                        case FeedManager.FeedType.Like_User_Photo:
                        case FeedManager.FeedType.Comment_User_Photo:
                            {
                                Type = FeedType.Comment_User_Photo;
                                var rec = feed.FeedUserPhotoes.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.UserPhotoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.User_Video:
                        case FeedManager.FeedType.Like_User_Video:
                        case FeedManager.FeedType.Comment_User_Video:
                            {
                                Type = FeedType.Comment_User_Video;
                                var rec = feed.FeedUserVideos.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.UserVideoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Company_Video:
                        case FeedManager.FeedType.Like_Company_Video:
                        case FeedManager.FeedType.Comment_Company_Video:
                            {
                                Type = FeedType.Comment_Company_Video;
                                var rec = feed.FeedUserCompanyVideos.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.CompanyVideoId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Company_Portfolio_Photo:
                        case FeedManager.FeedType.Like_Company_Portfolio_Photo:
                        case FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                            {
                                Type = FeedType.Comment_Company_Portfolio_Photo;
                                var rec = feed.FeedUserCompanyPhotoes.FirstOrDefault();
                                if (rec != null)
                                    refRecordId = rec.CompanyPhotoId ?? 0;
                            }
                            break;
                        //case FeedManager.FeedType.Like_Comment:
                        case FeedManager.FeedType.Like_Main_Feed:
                        case FeedManager.FeedType.Comment_Main_Feed:
                            {
                                Type = FeedType.Comment_Main_Feed;
                                var rec = feed.FeedUserMainFeeds.FirstOrDefault(a => a.FeedUserMain.IsDelete != true);
                                if (rec != null)
                                    refRecordId = rec.ReferencedFeedId ?? 0;
                            }
                            break;
                        case FeedManager.FeedType.Company_Product:
                        case FeedManager.FeedType.Like_Company_Product:
                        case FeedManager.FeedType.Comment_Company_Product:
                            {
                                Type = FeedType.Comment_Company_Product;
                                var rec = feed.FeedUserCompanyProducts.FirstOrDefault(a => a.FeedUserMain.IsDelete != true);
                                if (rec != null)
                                    refRecordId = rec.CompanyProductId ?? 0;
                            }
                            break;
                    }
                    #endregion

                }
                string strType = Type.ToString();
                objResult = GetLatestFeedComments(feedId, feedUserId, Type, refRecordId, currentUserId, LatestFeedId);

            }
            return objResult;

            LoggingManager.Debug("Exiting GetLatestFeedComments - Feedmanager");
        }

        public static int GetCommentLikes(int refRecordId)
        {
            LoggingManager.Debug("Entering GetFeedComments - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                string strType = FeedType.Like_Comment.ToString();
                return context.FeedUserMains.Count(a => a.Type == strType
                    && a.FeedUserMainFeeds.Any(b => b.ReferencedFeedId == refRecordId && b.FeedUserMain.IsDelete != true) && a.IsDelete != true);
            }
            LoggingManager.Debug("Exiting GetFeedComments - Feedmanager");
        }
        public static bool GetIfCommentLiked(int currentUserId, int refRecordId)
        {
            LoggingManager.Debug("Entering GetFeedComments - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                string strType = FeedType.Like_Comment.ToString();
                return context.FeedUserMains.Any(a => a.Type == strType
                    && a.FeedUserMainFeeds.Any(b => b.ReferencedFeedId == refRecordId && b.FeedUserMain.IsDelete != true)
                    && a.IsDelete != true && a.UserId == currentUserId);
            }
            LoggingManager.Debug("Exiting GetFeedComments - Feedmanager");
        }

        public static bool CheckUserFollow(int currenctUserId, int followingUserId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.PreferredFeedUserUsers.Where(x => x.UserId == followingUserId && x.FollowingUserId == currenctUserId).Any();
            }

            return false;
        }

        public static Company getCompany(int userid)
        {
            LoggingManager.Debug("Entering getCompany  - FeedManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userid);
                var company = context.Companies.FirstOrDefault(x => x.Userid == user.Id && user.IsCompany == true);

                LoggingManager.Debug("Extinging getCompany  - FeedManager.cs");

                return company;
            }
        }
        public static User getUser(int userid)
        {
            LoggingManager.Debug("Entering getUser  - FeedManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userid);

                LoggingManager.Debug("Extinging getUser  - FeedManager.cs");

                return user;
            }
        }
        public static FeedUserMain getMainFeed(FeedUserMain mainFeed)
        {
            LoggingManager.Debug("Entering getMainFeed  - FeedManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                #region Feed type
                string strType = "";
                string strType2 = "";
                switch ((FeedType)Enum.Parse(typeof(FeedType), mainFeed.Type))
                {
                    case FeedType.Comment_Company_Portfolio_Photo:
                    case FeedType.Like_Company_Portfolio_Photo:
                        #region Company_Portfolio_Photo
                        {
                            var refRecord = context.FeedUserCompanyPhotoes.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Company_Portfolio_Photo.ToString();
                                //strType2 = FeedType.Multiple_Company_Portfolio_Photo.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where r.Type == strType //|| r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == refRecord.CompanyPhotoId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Company_Product:
                    case FeedType.Like_Company_Product:
                        #region Company_Product
                        {
                            var refRecord = context.FeedUserCompanyProducts.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Company_Product.ToString();
                                //strType2 = FeedType.Multiple_Company_Portfolio_Photo.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where r.Type == strType //|| r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == refRecord.CompanyProductId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Company_Video:
                    case FeedType.Like_Company_Video:
                        #region Company_Video
                        {
                            var refRecord = context.FeedUserCompanyVideos.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Company_Video.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType)
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == refRecord.CompanyVideoId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Main_Feed:
                    case FeedType.Like_Main_Feed:
                    case FeedType.Like_Comment:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserMainFeeds.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                var rec = (from r in context.FeedUserMains
                                           where r.IsDelete != true
                                       && r.Id == refRecord.ReferencedFeedId
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Profile:
                    case FeedType.Like_Company_Profile:
                        return mainFeed;
                        break;
                    case FeedType.Comment_User_Feed:
                    case FeedType.Like_User_Feed:
                        #region User_Feed
                        {
                            var refRecord = context.FeedUserUserFeeds.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Feed_Update.ToString();
                                strType2 = FeedType.Link_share.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType || r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserUserFeeds.Any(a => a.UserFeedId == refRecord.UserFeedId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Photo:
                    case FeedType.Like_User_Photo:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserPhotoes.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Photo.ToString();
                                strType2 = FeedType.Wall_Picture.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType || r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserPhotoes.Any(a => a.UserPhotoId == refRecord.UserPhotoId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Portfolio_Photo:
                    case FeedType.Like_User_Portfolio_Photo:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserPorfolioPhotoes.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Potfolio_Photo.ToString();
                                //strType2 = FeedType.Multiple_User_Potfolio_Photo.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where r.Type == strType //|| r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == refRecord.PortfolioPhotoId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Portfolio_Video:
                    case FeedType.Like_User_Portfolio_Video:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserPortfolioVideos.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Portfolio_video.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType)
                                       && r.IsDelete != true
                                       && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == refRecord.PortfolioVideoId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Video:
                    case FeedType.Like_User_Video:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserVideos.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Video.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType)
                                       && r.IsDelete != true
                                       && r.FeedUserVideos.Any(a => a.UserVideoId == refRecord.UserVideoId)
                                           select r).FirstOrDefault();
                                return rec;
                            }
                        }
                        #endregion
                        break;
                    default:
                        return mainFeed;
                        break;
                }
                #endregion


                return null;
            }
            LoggingManager.Debug("Extinging getMainFeed  - FeedManager.cs");
        }

        public static UserLikeList GetUserPhotoLikes(int currentUserId, int profileUserId, int oldestLikeId, int pagesize, string width)
        {
            LoggingManager.Debug("Entering GetUserLikes - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                UserLikeList objResult = new UserLikeList();

                List<string> lstTypes = new List<string>();
                lstTypes.Add(FeedType.Like_Company_Portfolio_Photo.ToString());
                lstTypes.Add(FeedType.Like_User_Photo.ToString());
                lstTypes.Add(FeedType.Like_User_Portfolio_Photo.ToString());

                string sType = FeedType.Like_Main_Feed.ToString();
                string sType1 = FeedType.Profile_Picture.ToString();
                #region Retrieve feeds
                var rec = from m in context.FeedUserMains
                          where (lstTypes.Contains(m.Type)
                          || (m.Type == sType && m.FeedUserMainFeeds.Any(a => a.FeedUserMain.Type == sType1 && a.FeedUserMain.IsDelete != true)))
                          && m.IsDelete != true
                          && (oldestLikeId == 0 || m.Id < oldestLikeId)
                          && m.UserId == profileUserId
                          orderby m.Date descending
                          select m;
                if (rec != null && rec.Count() > 0)
                {
                    objResult.totalLikes = rec.Count();
                    objResult.detail = new List<UserLikeDetail>();

                    var res = rec.Take(pagesize);
                    objResult.pendingLikes = objResult.totalLikes - pagesize;
                    if (objResult.pendingLikes < 0)
                        objResult.pendingLikes = 0;
                    foreach (var data in res)
                    {
                        switch ((FeedManager.FeedType)Enum.Parse(typeof(FeedManager.FeedType), data.Type))
                        {
                            case FeedType.Like_Company_Portfolio_Photo:
                                {
                                    var main = data.FeedUserCompanyPhotoes.FirstOrDefault();
                                    if (main != null)
                                    {
                                        var detail = GetLikeAndCommentCount(FeedType.Company_Portfolio_Photo, main.CompanyPhotoId ?? 0, currentUserId);
                                        objResult.detail.Add(FeedContentManager.GetCompanyPortfolioPhotoDetail(data.Id, main.CompanyPortfolio, detail, width));
                                    }
                                }
                                break;
                            case FeedType.Like_Main_Feed:
                                {
                                    var detail = GetLikeAndCommentCount(FeedType.Like_Main_Feed, data.Id, currentUserId);
                                    objResult.detail.Add(FeedContentManager.GetUserProfilePhotoDetail(data.Id, data.User, detail, width));
                                }
                                break;
                            case FeedType.Like_User_Photo:
                                {
                                    var main = data.FeedUserPhotoes.FirstOrDefault();
                                    if (main != null)
                                    {
                                        var detail = GetLikeAndCommentCount(FeedType.User_Photo, main.UserPhotoId ?? 0, currentUserId);
                                        objResult.detail.Add(FeedContentManager.GetUserPhotoDetail(data.Id, main.UserPortfolio, detail, width));
                                    }
                                }
                                break;
                            case FeedType.Like_User_Portfolio_Photo:
                                {
                                    var main = data.FeedUserPorfolioPhotoes.FirstOrDefault();
                                    if (main != null)
                                    {
                                        var detail = GetLikeAndCommentCount(FeedType.User_Potfolio_Photo, main.PortfolioPhotoId ?? 0, currentUserId);
                                        objResult.detail.Add(FeedContentManager.GetUserPorfolioPhotoesDetail(data.Id, main.EmploymentHistoryPortfolio, detail, width));
                                    }
                                }
                                break;
                        }
                    }

                    objResult.OldestLikeId = 0;

                    if (objResult.detail.Count > 0)
                    {
                        int id = objResult.detail.LastOrDefault().feedId;
                        if (oldestLikeId > id || oldestLikeId == 0)
                            objResult.OldestLikeId = id;
                    }
                }
                #endregion
                return objResult;
            }
            LoggingManager.Debug("Exiting GetUserLikes - Feedmanager");
        }

        public static LikeComment GetLikeAndCommentCount(FeedType type, int refRecordId, int currentUserId)
        {
            LoggingManager.Debug("Entering GetLikeAndCommentCount - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LikeComment objResult = new LikeComment();
                string strLike, strComment;
                switch (type)
                {
                    case FeedType.Comment_Company_Portfolio_Photo:
                    case FeedType.Like_Company_Portfolio_Photo:
                        #region Company_Portfolio_Photo
                        {
                            strComment = FeedType.Comment_Company_Portfolio_Photo.ToString();
                            strLike = FeedType.Like_Company_Portfolio_Photo.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Company_Product:
                    case FeedType.Like_Company_Product:
                        #region Company_Product
                        {
                            strComment = FeedType.Comment_Company_Product.ToString();
                            strLike = FeedType.Like_Company_Product.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Company_Video:
                    case FeedType.Like_Company_Video:
                        #region Company_Video
                        {
                            strComment = FeedType.Comment_Company_Video.ToString();
                            strLike = FeedType.Like_Company_Video.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Main_Feed:
                    case FeedType.Like_Main_Feed:
                        #region Main_Feed
                        {
                            strComment = FeedType.Comment_Main_Feed.ToString();
                            strLike = FeedType.Like_Main_Feed.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Feed:
                    case FeedType.Like_User_Feed:
                        #region Main_Feed
                        {
                            strComment = FeedType.Comment_User_Feed.ToString();
                            strLike = FeedType.Like_User_Feed.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserUserFeeds.Any(a => a.UserFeedId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Photo:
                    case FeedType.Like_User_Photo:
                        #region Main_Feed
                        {
                            strComment = FeedType.Comment_User_Photo.ToString();
                            strLike = FeedType.Like_User_Photo.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserPhotoes.Any(a => a.UserPhotoId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Portfolio_Photo:
                    case FeedType.Like_User_Portfolio_Photo:
                        #region Main_Feed
                        {
                            strComment = FeedType.Comment_User_Portfolio_Photo.ToString();
                            strLike = FeedType.Like_User_Portfolio_Photo.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Portfolio_Video:
                    case FeedType.Like_User_Portfolio_Video:
                        #region Main_Feed
                        {
                            strComment = FeedType.Comment_User_Portfolio_Video.ToString();
                            strLike = FeedType.Like_User_Portfolio_Video.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Video:
                    case FeedType.Like_User_Video:
                        #region Main_Feed
                        {
                            strComment = FeedType.Comment_User_Video.ToString();
                            strLike = FeedType.Like_User_Video.ToString();
                            var rec = from r in context.FeedUserMains
                                      where (r.Type == strComment || r.Type == strLike)
                                      && r.IsDelete != true
                                      && r.FeedUserVideos.Any(a => a.UserVideoId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Comment:
                        #region Like_Comment
                        {
                            strLike = FeedType.Like_Comment.ToString();
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strLike
                                      && r.IsDelete != true
                                      && r.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == refRecordId && a.FeedUserMain.IsDelete != true)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                //objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Company_Profile:
                        #region Like_Company_Profile
                        {
                            strLike = FeedType.Like_Company_Profile.ToString();
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strLike
                                      && r.IsDelete != true
                                      && r.FeedUserProfiles.Any(a => a.ProfileUserId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                //objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Profile:
                        #region Like_User_Profile
                        {
                            strLike = FeedType.Like_User_Profile.ToString();
                            var rec = from r in context.FeedUserMains
                                      where r.Type == strLike
                                      && r.IsDelete != true
                                      && r.FeedUserProfiles.Any(a => a.ProfileUserId == refRecordId)
                                      select r;
                            if (rec != null)
                            {
                                objResult.IsLikedByCurrentUser = rec.Any(a => a.UserId == currentUserId && a.Type == strLike);
                                //objResult.TotalComments = rec.Count(a => a.Type == strComment);
                                objResult.TotalLikes = rec.Count(a => a.Type == strLike);
                            }
                        }
                        #endregion
                        break;
                }
                return objResult;
            }
            LoggingManager.Debug("Exiting GetLikeAndCommentCount - Feedmanager");
        }

        public static PopupDetail getPopupDetail(string type, int currentUserId, int RefRecordId)
        {
            LoggingManager.Debug("Entering getPopupDetail - Feedmanager");
            PopupDetail objResult = new PopupDetail();
            objResult.FeedType = type;
            objResult.RefRecordId = RefRecordId;
            objResult.feedId = 0;
            string strOtherType = "";
            string strOtherType1 = "";
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                switch ((FeedManager.FeedType)Enum.Parse(typeof(FeedManager.FeedType), type))
                {
                    case FeedType.Company_Portfolio_Photo:
                    case FeedType.Multiple_Company_Portfolio_Photo:
                        #region Company_Portfolio_Photo
                        {
                            strOtherType = FeedType.Multiple_Company_Portfolio_Photo.ToString();
                            var rec = (from r in context.FeedUserMains
                                       where (r.Type == type || r.Type == strOtherType)
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == RefRecordId)
                                       select r).FirstOrDefault();
                            if (rec != null)
                            {
                                objResult.OwnerDetail = FeedContentManager.getOwnerDetail(currentUserId, rec.User);
                                objResult.feedUserId = rec.UserId;
                                if (rec.Type == type)
                                    objResult.feedId = rec.Id;

                                var photo = rec.FeedUserCompanyPhotoes.FirstOrDefault(a => a.CompanyPhotoId == RefRecordId).CompanyPortfolio;
                                objResult.Detail = FeedContentManager.GetImage(photo.PortfolioImageid);
                                objResult.Description = photo.PortfolioDescription;
                                objResult.TimeStamp = FeedContentManager.GetTimeLine(rec.Date);

                                objResult.NextRecordId = 0;
                                objResult.PrevRecordId = 0;
                                var recPrev = (from r in context.CompanyPortfolios
                                               where r.Id < RefRecordId
                                               && r.Company.Userid == rec.UserId
                                               && r.FeedUserCompanyPhotoes.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id descending
                                               select r).FirstOrDefault();
                                if (recPrev != null)
                                    objResult.PrevRecordId = recPrev.Id;

                                var recNext = (from r in context.CompanyPortfolios
                                               where r.Id > RefRecordId
                                               && r.Company.Userid == rec.UserId
                                                 && r.FeedUserCompanyPhotoes.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id ascending
                                               select r).FirstOrDefault();
                                if (recNext != null)
                                    objResult.NextRecordId = recNext.Id;
                            }
                            objResult.LikeFeedType = FeedType.Like_Company_Portfolio_Photo.ToString();
                            objResult.CommentFeedType = FeedType.Comment_Company_Portfolio_Photo.ToString();

                        }
                        #endregion
                        break;
                    case FeedType.Profile_Picture:
                        #region Profile_Picture
                        {
                            var rec = (from r in context.FeedUserMains
                                       where r.Type == type
                                       && r.IsDelete != true
                                       && r.UserId == RefRecordId
                                       select r).FirstOrDefault();
                            if (rec != null)
                            {
                                objResult.OwnerDetail = FeedContentManager.getOwnerDetail(currentUserId, rec.User);
                                objResult.feedUserId = rec.UserId;
                                objResult.feedId = rec.Id;

                                var photo = rec.User;
                                objResult.Detail = FeedContentManager.GetImage(photo.PersonalLogoFileStoreId);
                                objResult.Description = "";
                                objResult.TimeStamp = FeedContentManager.GetTimeLine(rec.Date);
                                objResult.NextRecordId = 0;
                                objResult.PrevRecordId = 0;
                            }
                            objResult.LikeFeedType = FeedType.Like_Main_Feed.ToString();
                            objResult.CommentFeedType = FeedType.Comment_Main_Feed.ToString();
                        }
                        #endregion
                        break;
                    case FeedType.Wall_Picture:
                    case FeedType.User_Photo:
                    case FeedType.Multiple_User_Photo:
                        #region User_Photo
                        {
                            strOtherType = FeedType.Multiple_User_Photo.ToString();
                            strOtherType1 = FeedType.Wall_Picture.ToString();
                            var rec = (from r in context.FeedUserMains
                                       where (r.Type == type || r.Type == strOtherType || r.Type == strOtherType1)
                                       && r.IsDelete != true
                                       && r.FeedUserPhotoes.Any(a => a.UserPhotoId == RefRecordId)
                                       select r).FirstOrDefault();
                            if (rec != null)
                            {
                                objResult.OwnerDetail = FeedContentManager.getOwnerDetail(currentUserId, rec.User);
                                objResult.feedUserId = rec.UserId;
                                if (rec.Type == type)
                                    objResult.feedId = rec.Id;

                                var photo = rec.FeedUserPhotoes.FirstOrDefault(a => a.UserPhotoId == RefRecordId).UserPortfolio;
                                objResult.Detail = FeedContentManager.GetImage(photo.PictureId);
                                objResult.Description = photo.PictureDescription;
                                objResult.TimeStamp = FeedContentManager.GetTimeLine(rec.Date);

                                objResult.NextRecordId = 0;
                                objResult.PrevRecordId = 0;
                                var recPrev = (from r in context.UserPortfolios
                                               where r.Id < RefRecordId
                                               && r.UserId == rec.UserId
                                                 && r.FeedUserPhotoes.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id descending
                                               select r).FirstOrDefault();
                                if (recPrev != null)
                                    objResult.PrevRecordId = recPrev.Id;

                                var recNext = (from r in context.UserPortfolios
                                               where r.Id > RefRecordId
                                               && r.UserId == rec.UserId
                                                 && r.FeedUserPhotoes.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id ascending
                                               select r).FirstOrDefault();
                                if (recNext != null)
                                    objResult.NextRecordId = recNext.Id;
                            }
                            objResult.LikeFeedType = FeedType.Like_User_Photo.ToString();
                            objResult.CommentFeedType = FeedType.Comment_User_Photo.ToString();
                        }
                        #endregion
                        break;
                    case FeedType.User_Potfolio_Photo:
                    case FeedType.Multiple_User_Potfolio_Photo:
                        #region User_Potfolio_Photo
                        {
                            strOtherType = FeedType.Multiple_User_Potfolio_Photo.ToString();
                            var rec = (from r in context.FeedUserMains
                                       where (r.Type == type || r.Type == strOtherType)
                                       && r.IsDelete != true
                                       && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == RefRecordId)
                                       select r).FirstOrDefault();
                            if (rec != null)
                            {
                                objResult.OwnerDetail = FeedContentManager.getOwnerDetail(currentUserId, rec.User);
                                objResult.feedUserId = rec.UserId;
                                if (rec.Type == type)
                                    objResult.feedId = rec.Id;

                                var photo = rec.FeedUserPorfolioPhotoes.FirstOrDefault(a => a.PortfolioPhotoId == RefRecordId).EmploymentHistoryPortfolio;
                                objResult.Detail = FeedContentManager.GetImage(photo.FileId);
                                objResult.Description = "";
                                objResult.TimeStamp = FeedContentManager.GetTimeLine(rec.Date);

                                objResult.NextRecordId = 0;
                                objResult.PrevRecordId = 0;
                                var recPrev = (from r in context.EmploymentHistoryPortfolios
                                               where r.Id < RefRecordId
                                               && r.EmploymentHistory.UserId == rec.UserId
                                                 && r.FeedUserPorfolioPhotoes.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id descending
                                               select r).FirstOrDefault();
                                if (recPrev != null)
                                    objResult.PrevRecordId = recPrev.Id;

                                var recNext = (from r in context.EmploymentHistoryPortfolios
                                               where r.Id > RefRecordId
                                               && r.EmploymentHistory.UserId == rec.UserId
                                                 && r.FeedUserPorfolioPhotoes.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id ascending
                                               select r).FirstOrDefault();
                                if (recNext != null)
                                    objResult.NextRecordId = recNext.Id;
                            }
                            objResult.LikeFeedType = FeedType.Like_User_Portfolio_Photo.ToString();
                            objResult.CommentFeedType = FeedType.Comment_User_Portfolio_Photo.ToString();
                        }
                        #endregion
                        break;
                    case FeedType.Company_Video:
                        #region Company_Video
                        {
                            var rec = (from r in context.FeedUserMains
                                       where r.Type == type
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == RefRecordId)
                                       select r).FirstOrDefault();
                            if (rec != null)
                            {
                                objResult.OwnerDetail = FeedContentManager.getOwnerDetail(currentUserId, rec.User);
                                objResult.feedUserId = rec.UserId;
                                if (rec.Type == type)
                                    objResult.feedId = rec.Id;

                                var photo = rec.FeedUserCompanyVideos.FirstOrDefault(a => a.CompanyVideoId == RefRecordId).CompanyVideo;
                                objResult.Detail = FeedContentManager.GetVideo(photo.VideoUrl);
                                objResult.Description = "";
                                objResult.TimeStamp = FeedContentManager.GetTimeLine(rec.Date);

                                objResult.NextRecordId = 0;
                                objResult.PrevRecordId = 0;
                                var recPrev = (from r in context.CompanyVideos
                                               where r.Id < RefRecordId
                                               && r.Company.Userid == rec.UserId
                                                 && r.FeedUserCompanyVideos.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id descending
                                               select r).FirstOrDefault();
                                if (recPrev != null)
                                    objResult.PrevRecordId = recPrev.Id;

                                var recNext = (from r in context.CompanyVideos
                                               where r.Id > RefRecordId
                                               && r.Company.Userid == rec.UserId
                                                 && r.FeedUserCompanyVideos.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id ascending
                                               select r).FirstOrDefault();
                                if (recNext != null)
                                    objResult.NextRecordId = recNext.Id;
                            }
                            objResult.LikeFeedType = FeedType.Like_Company_Video.ToString();
                            objResult.CommentFeedType = FeedType.Comment_Company_Video.ToString();
                        }
                        #endregion
                        break;
                    case FeedType.User_Portfolio_video:
                        #region User_Portfolio_video
                        {
                            var rec = (from r in context.FeedUserMains
                                       where r.Type == type
                                       && r.IsDelete != true
                                       && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == RefRecordId)
                                       select r).FirstOrDefault();
                            if (rec != null)
                            {
                                objResult.OwnerDetail = FeedContentManager.getOwnerDetail(currentUserId, rec.User);
                                objResult.feedUserId = rec.UserId;
                                if (rec.Type == type)
                                    objResult.feedId = rec.Id;

                                var photo = rec.FeedUserPortfolioVideos.FirstOrDefault(a => a.PortfolioVideoId == RefRecordId).EmploymentHistoryVideo;
                                objResult.Detail = FeedContentManager.GetVideo(photo.VideoURL);
                                objResult.Description = "";
                                objResult.TimeStamp = FeedContentManager.GetTimeLine(rec.Date);

                                objResult.NextRecordId = 0;
                                objResult.PrevRecordId = 0;
                                var recPrev = (from r in context.EmploymentHistoryVideos
                                               where r.Id < RefRecordId
                                               && r.EmploymentHistory.UserId == rec.UserId
                                                 && r.FeedUserPortfolioVideos.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id descending
                                               select r).FirstOrDefault();
                                if (recPrev != null)
                                    objResult.PrevRecordId = recPrev.Id;

                                var recNext = (from r in context.EmploymentHistoryVideos
                                               where r.Id > RefRecordId
                                               && r.EmploymentHistory.UserId == rec.UserId
                                                 && r.FeedUserPortfolioVideos.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id ascending
                                               select r).FirstOrDefault();
                                if (recNext != null)
                                    objResult.NextRecordId = recNext.Id;
                            }
                            objResult.LikeFeedType = FeedType.Like_User_Portfolio_Video.ToString();
                            objResult.CommentFeedType = FeedType.Comment_User_Portfolio_Video.ToString();
                        }
                        #endregion
                        break;
                    case FeedType.User_Video:
                        #region User_Video
                        {
                            var rec = (from r in context.FeedUserMains
                                       where r.Type == type
                                       && r.IsDelete != true
                                       && r.FeedUserVideos.Any(a => a.UserVideoId == RefRecordId)
                                       select r).FirstOrDefault();
                            if (rec != null)
                            {
                                objResult.OwnerDetail = FeedContentManager.getOwnerDetail(currentUserId, rec.User);
                                objResult.feedUserId = rec.UserId;
                                if (rec.Type == type)
                                    objResult.feedId = rec.Id;

                                var photo = rec.FeedUserVideos.FirstOrDefault(a => a.UserVideoId == RefRecordId).UserVideo;
                                objResult.Detail = FeedContentManager.GetVideo(photo.VideoUrl);
                                objResult.Description = photo.VideoTitle;
                                objResult.TimeStamp = FeedContentManager.GetTimeLine(rec.Date);

                                objResult.NextRecordId = 0;
                                objResult.PrevRecordId = 0;
                                var recPrev = (from r in context.UserVideos
                                               where r.Id < RefRecordId
                                               && r.UserId == rec.UserId
                                                 && r.FeedUserVideos.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id descending
                                               select r).FirstOrDefault();
                                if (recPrev != null)
                                    objResult.PrevRecordId = recPrev.Id;

                                var recNext = (from r in context.UserVideos
                                               where r.Id > RefRecordId
                                               && r.UserId == rec.UserId
                                                 && r.FeedUserVideos.Any(a => a.FeedUserMain.IsDelete != true)
                                               orderby r.Id ascending
                                               select r).FirstOrDefault();
                                if (recNext != null)
                                    objResult.NextRecordId = recNext.Id;
                            }
                            objResult.LikeFeedType = FeedType.Like_User_Video.ToString();
                            objResult.CommentFeedType = FeedType.Comment_User_Video.ToString();
                        }
                        #endregion
                        break;
                }
            }
            LoggingManager.Debug("Exiting getPopupDetail - Feedmanager");
            return objResult;
        }

        public static Popup_LikedDetail GetLikedUser(int feedId, string type, int currentUserId, int RefRecordId, int OldestFeedId, int pageSize)
        {
            LoggingManager.Debug("Entering GetLikedUser - Feedmanager");
            Popup_LikedDetail objResult = new Popup_LikedDetail();
            objResult.feedId = feedId;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                switch ((FeedManager.FeedType)Enum.Parse(typeof(FeedManager.FeedType), type))
                {
                    case FeedType.Like_Company_Portfolio_Photo:
                        #region Like_Company_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_Main_Feed:
                    case FeedManager.FeedType.Like_Comment:
                        #region Like_Main_Feed
                        {
                            string strType = FeedType.Profile_Picture.ToString();
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserMainFeeds.Any(a => a.ReferencedFeedId == RefRecordId
                                          && a.FeedUserMain.IsDelete != true)
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Photo:
                        #region Like_User_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserPhotoes.Any(a => a.UserPhotoId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Portfolio_Photo:
                        #region Like_User_Portfolio_Photo
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Feed:
                        #region Like_User_Feed
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserUserFeeds.Any(a => a.UserFeedId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Portfolio_Video:
                        #region Like_User_Portfolio_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_User_Video:
                        #region Like_User_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserVideos.Any(a => a.UserVideoId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Company_Video:
                        #region Like_Company_Video
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Company_Profile:
                    case FeedManager.FeedType.Like_User_Profile:
                        #region Like_Company_Profile Like_User_Profile
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserProfiles.Any(a => a.ProfileUserId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    case FeedManager.FeedType.Like_Company_Product:
                        #region Like_Company_Product
                        {
                            var rec = from r in context.FeedUserMains
                                      where r.Type == type
                                      && r.IsDelete != true
                                      && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == RefRecordId)
                                      orderby r.Date ascending
                                      select r;
                            if (rec != null)
                            {
                                bool liked = rec.Any(a => a.UserId == currentUserId);
                                int cnt = rec.Count();
                                var rec1 = rec.Where(r => (OldestFeedId == 0 || r.Id < OldestFeedId) && r.UserId != currentUserId);
                                if (pageSize > 0)
                                    rec1 = rec1.Take(pageSize);
                                objResult.LikedUsers = FeedContentManager.getLikedUser(feedId, currentUserId, rec1.ToList());
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            LoggingManager.Debug("Exiting GetLikedUser - Feedmanager");
            return objResult;
        }

        public static bool GetIfProfileLiked(int currentUserId, int ProfileUserId, string type)
        {
            LoggingManager.Debug("Entering GetFeedComments - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                string strType = type;
                return context.FeedUserMains.Any(a => a.Type == strType
                    && a.FeedUserProfiles.Any(b => b.ProfileUserId == ProfileUserId && b.FeedUserMain.IsDelete != true)
                    && a.IsDelete != true && a.UserId == currentUserId);
            }
            LoggingManager.Debug("Exiting GetFeedComments - Feedmanager");
        }

        public static FeedUserMain getMainFeed(FeedType type, int RefRecordId)
        {
            LoggingManager.Debug("Entering getMainFeed  - FeedManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                #region Feed type
                string strType = "";
                string strType2 = "";
                switch (type)
                {
                    case FeedType.Company_Product:
                        #region Company_Product
                        {
                            strType = FeedType.Company_Product.ToString();
                            //strType2 = FeedType.Multiple_Company_Portfolio_Photo.ToString();
                            var rec = (from r in context.FeedUserMains
                                       where r.Type == strType //|| r.Type == strType2)
                                           //&& r.IsDelete != true
                                           && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == RefRecordId)
                                       select r).FirstOrDefault();
                            return rec;
                        }
                        #endregion
                        break;
                }
                #endregion


                return null;
            }
            LoggingManager.Debug("Extinging getMainFeed  - FeedManager.cs");
        }

        public static Company getCompany(string companyName)
        {
            LoggingManager.Debug("Entering getCompany  - FeedManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var company = context.Companies.FirstOrDefault(x => x.CompanyName.ToLower() == companyName.ToLower());

                LoggingManager.Debug("Extinging getCompany  - FeedManager.cs");

                return company;
            }
        }

        public static int GetFeedsAlertCount(int currentUserId)
        {
            LoggingManager.Debug("Entering GetFeedsPagewise - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //List<string> lstTypes = new List<string>();
                //lstTypes.Add(FeedType.Like_User_Feed.ToString());
                //lstTypes.Add(FeedType.Comment_User_Feed.ToString());
                //lstTypes.Add(FeedType.Like_User_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Comment_User_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Like_User_Portfolio_Video.ToString());
                //lstTypes.Add(FeedType.Comment_User_Portfolio_Video.ToString());
                //lstTypes.Add(FeedType.Like_User_Photo.ToString());
                //lstTypes.Add(FeedType.Comment_User_Photo.ToString());
                //lstTypes.Add(FeedType.Like_User_Video.ToString());
                //lstTypes.Add(FeedType.Comment_User_Video.ToString());
                //lstTypes.Add(FeedType.Like_Company_Video.ToString());
                //lstTypes.Add(FeedType.Comment_Company_Video.ToString());
                //lstTypes.Add(FeedType.Like_Company_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Comment_Company_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Like_Main_Feed.ToString());
                //lstTypes.Add(FeedType.Comment_Main_Feed.ToString());
                //lstTypes.Add(FeedType.Like_Company_Profile.ToString());
                //lstTypes.Add(FeedType.Like_User_Profile.ToString());
                //lstTypes.Add(FeedType.Like_Company_Product.ToString());
                //lstTypes.Add(FeedType.Comment_Company_Product.ToString());
                //lstTypes.Add(FeedType.Like_Comment.ToString());

                var data = context.GetFeedAlertListCount(currentUserId).FirstOrDefault().cnt;
                //var rec = from m in context.FeedUserMains
                //          where data.Contains(m.Id)
                //          orderby m.Date descending
                //          select m;

                //int cnt = (from r in context.FeedUserMains
                //           where r.UserId != currentUserId
                //            && r.IsDelete != true
                //            && r.RefFeedId == null
                //          && lstTypes.Contains(r.Type)
                //          && context.GetFeedAlertUserList(currentUserId, r.Type, r.Id, r.OwnerUserId).FirstOrDefault() > 0
                //          && !r.FeedUserAlertReads.Any(a => a.UserId == currentUserId)
                //           select r).Count();

                return data??0;
            }
            LoggingManager.Debug("Exiting GetFeedsPagewise - Feedmanager");
        }
        public static List<FeedAlert> GetFeedsAlerts(int currentUserId)
        {
            LoggingManager.Debug("Entering GetFeedsPagewise - Feedmanager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //List<string> lstTypes = new List<string>();
                //lstTypes.Add(FeedType.Like_User_Feed.ToString());
                //lstTypes.Add(FeedType.Comment_User_Feed.ToString());
                //lstTypes.Add(FeedType.Like_User_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Comment_User_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Like_User_Portfolio_Video.ToString());
                //lstTypes.Add(FeedType.Comment_User_Portfolio_Video.ToString());
                //lstTypes.Add(FeedType.Like_User_Photo.ToString());
                //lstTypes.Add(FeedType.Comment_User_Photo.ToString());
                //lstTypes.Add(FeedType.Like_User_Video.ToString());
                //lstTypes.Add(FeedType.Like_Company_Video.ToString());
                //lstTypes.Add(FeedType.Comment_Company_Video.ToString());
                //lstTypes.Add(FeedType.Like_Company_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Comment_Company_Portfolio_Photo.ToString());
                //lstTypes.Add(FeedType.Like_Main_Feed.ToString());
                //lstTypes.Add(FeedType.Comment_Main_Feed.ToString());
                //lstTypes.Add(FeedType.Like_Company_Profile.ToString());
                //lstTypes.Add(FeedType.Like_User_Profile.ToString());
                //lstTypes.Add(FeedType.Like_Company_Product.ToString());
                //lstTypes.Add(FeedType.Comment_Company_Product.ToString());
                //lstTypes.Add(FeedType.Like_Comment.ToString());


                List<FeedAlert> objResult = null;
                var data = context.GetFeedAlertList(currentUserId, 1, 5).Select(a => a.Id).ToList();
                var res = from m in context.FeedUserMains
                          where data.Contains(m.Id)
                          orderby m.Date descending
                          select m;
                //var res = (from r in context.FeedUserMains
                //           where r.UserId != currentUserId
                //           && r.IsDelete != true
                //           && r.RefFeedId == null
                //           && lstTypes.Contains(r.Type)
                //           && context.GetFeedAlertUserList(currentUserId, r.Type, r.Id, r.OwnerUserId).FirstOrDefault().Value > 0
                //           && !r.FeedUserAlertReads.Any(a => a.UserId == currentUserId)
                //           orderby r.Date descending
                //           select r).ToList();
                //if (res.Count() < 5)
                //{
                //    res.AddRange((from r in context.FeedUserMains
                //                  where r.UserId != currentUserId
                //                  && r.IsDelete != true
                //                  && r.RefFeedId == null
                //                  && lstTypes.Contains(r.Type)
                //                  && context.GetFeedAlertUserList(currentUserId, r.Type, r.Id, r.OwnerUserId).FirstOrDefault().Value > 0
                //                  orderby r.Date descending
                //                  select r).Take(5 - res.Count()).OrderByDescending(a => a.Date).ToList());
                //}
                if (res.Count() > 0)
                {
                    objResult = new List<FeedAlert>();
                    foreach (var feed in res)
                    {
                        FeedAlert obj = new FeedAlert();
                        obj.feedId = feed.Id;
                        #region Feed Description
                        switch ((FeedType)Enum.Parse(typeof(FeedType), feed.Type))
                        {
                            #region Unused types
                            //case FeedType.User_Interest:
                            //case FeedType.User_Skill:
                            //case FeedType.User_Education:
                            //case FeedType.User_School:
                            //case FeedType.Feed_Update:
                            //case FeedType.Link_share:
                            //case FeedType.Current_Job:
                            //case FeedType.Current_Company:
                            //case FeedType.Endorsed:
                            //case FeedType.Follow:
                            //case FeedType.Profile_Picture:
                            //case FeedType.Got_Connected:
                            //case FeedType.Job_Post:
                            //case FeedType.Profile_Viewed:
                            //case FeedType.User_Potfolio_Photo:
                            //case FeedType.Multiple_User_Potfolio_Photo:
                            //case FeedType.User_Portfolio_video:
                            //case FeedType.User_Photo:
                            //case FeedType.Multiple_User_Photo:
                            //case FeedType.Wall_Picture:
                            //case FeedType.User_Video:
                            //case FeedType.Company_Video:
                            //case FeedType.Company_Portfolio_Photo:
                            //case FeedType.Multiple_Company_Portfolio_Photo:
                            //case FeedType.Company_Product:                            
                            #endregion
                            case FeedType.Like_User_Feed:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Link_share(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_User_Feed:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Link_share(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_User_Portfolio_Photo:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_Potfolio_Photo(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_User_Portfolio_Photo:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_Potfolio_Photo(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_User_Portfolio_Video:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_Portfolio_video(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_User_Portfolio_Video:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_Portfolio_video(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_User_Photo:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_Photo(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_User_Photo:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_Photo(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_User_Video:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_video(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_User_Video:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_User_video(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_Company_Video:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Company_Video(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_Company_Video:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Company_Video(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_Company_Portfolio_Photo:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Company_Portfolio_Photo(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_Company_Portfolio_Photo:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Company_Portfolio_Photo(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_Main_Feed:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    //case FeedType.Like_Comment:
                                    obj.feedDescription = FeedContentManager.getAlert_Main_Feed(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_Main_Feed:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Main_Feed(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_Company_Profile:
                            case FeedType.Like_User_Profile:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Profile(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Like_Company_Product:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Company_Product(feed, currentUserId, true);
                                    //}
                                }
                                break;
                            case FeedType.Comment_Company_Product:
                                {
                                    //var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Company_Product(feed, currentUserId, false);
                                    //}
                                }
                                break;
                            case FeedType.Like_Comment:
                                {
                                    var objMain = getMainFeed(feed);
                                    //if (objMain != null)
                                    //{
                                    //    obj.feedId = objMain.Id;
                                    obj.feedDescription = FeedContentManager.getAlert_Like_Comment(feed,objMain, currentUserId, true);
                                    //}
                                }
                                break;
                            default:
                                obj.feedDescription = FeedContentManager.getFeed_Common_Content(feed, currentUserId);
                                break;
                        }
                        #endregion
                        objResult.Add(obj);
                    }
                    foreach (var feed in objResult)
                    {
                        if (!context.FeedUserAlertReads.Any(a => a.FeedUserMainId == feed.feedId && a.UserId == currentUserId))
                        {
                            #region Make read entry
                            FeedUserAlertRead objRead = new FeedUserAlertRead();
                            objRead.FeedUserMainId = feed.feedId;
                            objRead.UserId = currentUserId;
                            context.FeedUserAlertReads.AddObject(objRead);
                            context.SaveChanges();
                            #endregion
                        }
                    }
                }
                return objResult;
            }
            LoggingManager.Debug("Exiting GetFeedsPagewise - Feedmanager");
        }

        public static FeedDetail getMainFeed(int feedId, int currentUserId, List<int> retrieveUserList_1st, List<int> retrieveUserList_2nd, List<int> retrieveUserList_3rd)
        {
            LoggingManager.Debug("Entering getMainFeed  - FeedManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var mainFeed = (from r in context.FeedUserMains
                                where r.Id == feedId
                                orderby r.Date descending
                                select r).FirstOrDefault();
                #region Feed type
                string strType = "";
                string strType2 = "";
                FeedUserMain obj = new FeedUserMain();
                switch ((FeedType)Enum.Parse(typeof(FeedType), mainFeed.Type))
                {
                    case FeedType.Comment_Company_Portfolio_Photo:
                    case FeedType.Like_Company_Portfolio_Photo:
                        #region Company_Portfolio_Photo
                        {
                            var refRecord = context.FeedUserCompanyPhotoes.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Company_Portfolio_Photo.ToString();
                                //strType2 = FeedType.Multiple_Company_Portfolio_Photo.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where r.Type == strType //|| r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyPhotoes.Any(a => a.CompanyPhotoId == refRecord.CompanyPhotoId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Company_Product:
                    case FeedType.Like_Company_Product:
                        #region Company_Product
                        {
                            var refRecord = context.FeedUserCompanyProducts.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Company_Product.ToString();
                                //strType2 = FeedType.Multiple_Company_Portfolio_Photo.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where r.Type == strType //|| r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyProducts.Any(a => a.CompanyProductId == refRecord.CompanyProductId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Company_Video:
                    case FeedType.Like_Company_Video:
                        #region Company_Video
                        {
                            var refRecord = context.FeedUserCompanyVideos.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Company_Video.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType)
                                       && r.IsDelete != true
                                       && r.FeedUserCompanyVideos.Any(a => a.CompanyVideoId == refRecord.CompanyVideoId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_Main_Feed:
                    case FeedType.Like_Main_Feed:
                    case FeedType.Like_Comment:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserMainFeeds.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                var rec = (from r in context.FeedUserMains
                                           where r.IsDelete != true
                                       && r.Id == refRecord.ReferencedFeedId
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Like_User_Profile:
                    case FeedType.Like_Company_Profile:
                        obj = mainFeed;
                        break;
                    case FeedType.Comment_User_Feed:
                    case FeedType.Like_User_Feed:
                        #region User_Feed
                        {
                            var refRecord = context.FeedUserUserFeeds.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.Feed_Update.ToString();
                                strType2 = FeedType.Link_share.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType || r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserUserFeeds.Any(a => a.UserFeedId == refRecord.UserFeedId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Photo:
                    case FeedType.Like_User_Photo:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserPhotoes.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Photo.ToString();
                                strType2 = FeedType.Wall_Picture.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType || r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserPhotoes.Any(a => a.UserPhotoId == refRecord.UserPhotoId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Portfolio_Photo:
                    case FeedType.Like_User_Portfolio_Photo:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserPorfolioPhotoes.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Potfolio_Photo.ToString();
                                //strType2 = FeedType.Multiple_User_Potfolio_Photo.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where r.Type == strType //|| r.Type == strType2)
                                       && r.IsDelete != true
                                       && r.FeedUserPorfolioPhotoes.Any(a => a.PortfolioPhotoId == refRecord.PortfolioPhotoId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Portfolio_Video:
                    case FeedType.Like_User_Portfolio_Video:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserPortfolioVideos.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Portfolio_video.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType)
                                       && r.IsDelete != true
                                       && r.FeedUserPortfolioVideos.Any(a => a.PortfolioVideoId == refRecord.PortfolioVideoId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    case FeedType.Comment_User_Video:
                    case FeedType.Like_User_Video:
                        #region Main_Feed
                        {
                            var refRecord = context.FeedUserVideos.FirstOrDefault(a => a.FeedId == mainFeed.Id);
                            if (refRecord != null)
                            {
                                strType = FeedType.User_Video.ToString();
                                var rec = (from r in context.FeedUserMains
                                           where (r.Type == strType)
                                       && r.IsDelete != true
                                       && r.FeedUserVideos.Any(a => a.UserVideoId == refRecord.UserVideoId)
                                           select r).FirstOrDefault();
                                obj = rec;
                            }
                        }
                        #endregion
                        break;
                    default:
                        obj = mainFeed;
                        break;
                }
                #endregion

                return GetFeedDetail(obj, currentUserId, retrieveUserList_1st, retrieveUserList_2nd, retrieveUserList_3rd);
            }
            LoggingManager.Debug("Extinging getMainFeed  - FeedManager.cs");
        }
        #endregion

    }

}
