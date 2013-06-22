using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using EO.Pdf;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.Business
{
    public class UserProfileManager
    {
        public static void SaveEmploymentHistory(EmploymentHistory history, int? id, int userId)
        {
            LoggingManager.Debug("Entering SaveEmploymentHistory  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                EmploymentHistory historyToSave;
                if (id.HasValue)
                {
                    historyToSave = context.EmploymentHistories.First(h => h.Id == id);
                    var id3 = history.Id;
                    var skillupdate =
                        historyToSave.UserEmploymentSkills.FirstOrDefault(s => s.EmploymentHistoryId == historyToSave.Id);
                    if (skillupdate != null)
                    {
                        if (history.SkillId != null)
                        {
                            skillupdate.MasterSkillId = (int) history.SkillId;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (history.SkillId != null)
                            historyToSave.UserEmploymentSkills.Add(new UserEmploymentSkill { MasterSkillId = (int)history.SkillId });
                        context.SaveChanges();
                    }
                }
                else
                    {
                        historyToSave = new EmploymentHistory {UserId = userId};
                        if (history.SkillId != null)
                            historyToSave.UserEmploymentSkills.Add(new UserEmploymentSkill
                                                                       {MasterSkillId = (int) history.SkillId});


                        context.EmploymentHistories.AddObject(historyToSave);

                    }

                    historyToSave.JobTitle = history.JobTitle;
                    historyToSave.CompanyId = history.CompanyId;
                    historyToSave.FromMonthID = history.FromMonthID;
                    historyToSave.FromYearID = history.FromYearID;
                    historyToSave.ToMonthID = history.ToMonthID;
                    historyToSave.ToYearID = history.ToYearID;
                    historyToSave.EmploymentPresent = history.EmploymentPresent;
                    historyToSave.Description = history.Description;
                    historyToSave.IndustryId = history.IndustryId;
                    historyToSave.IsCurrent = history.IsCurrent;
                    historyToSave.CountryId = history.CountryId;
                    historyToSave.Town = history.Town;
                    historyToSave.LevelId = history.LevelId;



                    //List<UserEmploymentSkill> skillsToRemove =
                    //    historyToSave.UserEmploymentSkills.Where(
                    //        s => history.UserEmploymentSkills.Any(d => d.MasterSkillId != s.MasterSkillId)).ToList();
                    //foreach (
                    //    var skillToAdd in
                    //        history.UserEmploymentSkills.Where(
                    //            s =>
                    //            historyToSave.UserEmploymentSkills.Count == 0 ||
                    //            historyToSave.UserEmploymentSkills.All(us => s.MasterSkillId != us.MasterSkillId)))
                    //{
                    //    historyToSave.UserEmploymentSkills.Add(new UserEmploymentSkill
                    //                                               {MasterSkillId = skillToAdd.MasterSkillId});
                    //    FeedManager.addFeedNotification(FeedManager.FeedType.User_Skill, userId, skillToAdd.MasterSkillId,
                    //                                    null);
                    //}

                    //foreach (UserEmploymentSkill t in skillsToRemove)
                    //{
                    //    context.DeleteObject(t);
                    //}
                    context.SaveChanges();

                    if (history.IsCurrent)
                    {
                        var userFeedManager = new UserFeedManager();
                        var list = context.EmploymentHistories.Where(x => x.UserId == userId);
                        foreach (var employmentHistory in list)
                        {
                            employmentHistory.IsCurrent = false;
                        }
                        historyToSave.IsCurrent = true;
                        context.SaveChanges();
                        FeedManager.addFeedNotification(FeedManager.FeedType.Current_Job, userId, historyToSave.Id, null);
                        var socialManager = new SocialShareManager();
                        var msg = "[UserName]" + " " + "has updated Current Experience in Huntable to - " +
                                  history.JobTitle;
                        socialManager.ShareOnFacebook(userId, msg, "");
                        //FeedManager.addFeedNotification(FeedManager.FeedType.Current_Company, userId, history.Id, null);
                        //userFeedManager.SaveUserFeed(userId, "Employment", history.JobTitle + " at " + historyToSave.MasterCompany.Description);
                    }
                    else
                    {
                        var socialManager = new SocialShareManager();
                        var msg = "[UserName]" + " " + "has updated Past Experience in Huntable to - " +
                                  history.JobTitle;
                        socialManager.ShareOnFacebook(userId, msg, "");
                    }
                }

            
            LoggingManager.Debug("Exiting SaveEmploymentHistory  - UserProfileManager");
        }

        public static void SaveEducatinalHistories(EducationHistory history, int? id, int userId)
        {
            LoggingManager.Debug("Entering SaveEducatinalHistories  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                EducationHistory historyToSave;
                if (id.HasValue)
                {
                    historyToSave = context.EducationHistories.First(h => h.ID == id);
                }
                else
                {
                    historyToSave = new EducationHistory { UserId = userId};
                    context.EducationHistories.AddObject(historyToSave);
                }
                historyToSave.Institution = history.Institution;
                historyToSave.IsSchool = history.IsSchool;
                historyToSave.Degree = history.Degree;
                historyToSave.Description = history.Description;
                historyToSave.FromMonthID = history.FromMonthID;
                historyToSave.FromYearID = history.FromYearID;
                historyToSave.ToMonthID = history.ToMonthID;
                historyToSave.ToYearID = history.ToYearID;
                //if(history.IsSchool==true)
                //{
                //    FeedManager.addFeedNotification(FeedManager.FeedType.User_School, userId,historyToSave.ID,null);
                //}
                context.SaveChanges();
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Education, userId,historyToSave.ID,null);
                if (historyToSave.IsSchool == true&&historyToSave.Degree==null)
                {
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "has added a school in Huntable to - " + history.Institution;
                    socialManager.ShareOnFacebook(userId, msg, "");
                }
                else
                {
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "has updated his Education in Huntable to - " + history.Degree;
                    socialManager.ShareOnFacebook(userId, msg, "");
                }
                var userFeedManager = new UserFeedManager();
                //userFeedManager.SaveUserFeed(userId, "Education", history.Degree);
            }
            LoggingManager.Debug("Exiting SaveEducatinalHistories  - UserProfileManager");
        }

        public static void DeleteEmploymentHistory(int historyId)
        {
            LoggingManager.Debug("Entering DeleteEmploymentHistory  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var history = context.EmploymentHistories.First(e => e.Id == historyId);
                //EmploymentHistoryVideo

                //foreach (var record in history.EmploymentHistoryAchievements.ToList())
                //{
                //    context.DeleteObject(record);
                //}

                //foreach (var record in history.EmploymentHistoryPortfolios.ToList())
                //{
                //    //foreach (var feedUserPorfolioPhotoe in record.FeedUserPorfolioPhotoes)
                //    //{
                //    //    context.DeleteObject(feedUserPorfolioPhotoe);
                //    //}

                //    context.DeleteObject(record);
                //}

                //foreach (var record in history.EmploymentHistoryVideos.ToList())
                //{
                //    context.DeleteObject(record);
                //}

                //foreach (var record in history.FeedUserEmployementHistories.ToList())
                //{
                //    context.DeleteObject(record);
                //}

                //List<UserEmploymentSkill> skillsTodelete = history.UserEmploymentSkills.ToList();
                //foreach (UserEmploymentSkill t in skillsTodelete)
                //{
                //    context.DeleteObject(t);
                //}
                history.IsDeleted = true;
                FeedManager.deleteFeedNotitifation(FeedManager.FeedType.Current_Company, history.UserId,history.Id);
                FeedManager.deleteFeedNotitifation(FeedManager.FeedType.Current_Job, history.UserId, history.Id);
                FeedManager.deleteFeedNotitifation(FeedManager.FeedType.Endorsed, history.UserId, history.Id);
               
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting DeleteEmploymentHistory  - UserProfileManager");
        }
        
        public static void DeleteEducationHistory(int historyId,int userId)
        {
            LoggingManager.Debug("Entering DeleteEducationHistory  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var history = context.EducationHistories.First(h => h.ID == historyId);
                context.DeleteObject(history);
                context.SaveChanges();
                var socialManager = new SocialShareManager();
                var msg = "[UserName]" + " " + "has changed profile Education details";
                socialManager.ShareOnFacebook(userId, msg, "");
            }
            LoggingManager.Debug("Exiting DeleteEducationHistory  - UserProfileManager");
        }

        public static string CreatePDFForUrl(string url, string fileName)
        {
            LoggingManager.Debug("Entering CreatePDFForUrl  - UserProfileManager");
            string path = Path.GetTempPath();
            HtmlToPdf.Options.PageSize = PdfPageSizes.A4;
            HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.None;
            HtmlToPdf.Options.OutputArea = new RectangleF(0f, 0f, PdfPageSizes.A4.Width, PdfPageSizes.A4.Height);
            HtmlToPdf.Options.ZoomLevel = 1.33f;
            string pdfFilePath = Path.Combine(path, fileName);
            using (Stream fpdf = File.OpenWrite(pdfFilePath))
            {
                HtmlToPdf.ConvertUrl(url, fpdf);
                fpdf.Flush();
                fpdf.Close();
            }
            LoggingManager.Debug("Exiting CreatePDFForUrl  - UserProfileManager");
            return pdfFilePath;
        }

        public static void UploadPortfolioPicture(FileUpload fileUploadControl, int? employmentHistoryId, int userId)
        {
            LoggingManager.Debug("Entering UploadPortfolioPicture  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                EmploymentHistory historyToSave;
                if (employmentHistoryId.HasValue)
                {
                    historyToSave = context.EmploymentHistories.First(h => h.Id == employmentHistoryId);
                }
                else
                {
                    historyToSave = new EmploymentHistory { UserId = userId };
                    context.EmploymentHistories.AddObject(historyToSave);
                }
                var portfolioToSave = new EmploymentHistoryPortfolio();
                portfolioToSave.EmplementHistoryId = historyToSave.Id;
                var loadFileFromFileUpload = new FileStoreService().LoadImageAndResize(Constants.AvatarBasePathKey, fileUploadControl);
                if (loadFileFromFileUpload !=
                    null)
                    portfolioToSave.FileId = (int)loadFileFromFileUpload;
                historyToSave.EmploymentHistoryPortfolios.Add(portfolioToSave);
                context.SaveChanges();
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Potfolio_Photo, userId,portfolioToSave.Id, null);
                var socialManager = new SocialShareManager();
                var msg = "https://huntable.co.uk/LoadFile.ashx?id=" + portfolioToSave.FileId;
                socialManager.ShareOnFacebook(userId, "[UserName] had added a picture to profile - "+historyToSave.JobTitle+"-"+"in Huntable" ,msg);
            }
            LoggingManager.Debug("Exiting UploadPortfolioPicture  - UserProfileManager");
        }
        public static void UploadJobVideo(int? employmentHistoryId, string videoLink, int userId)
        {
            LoggingManager.Debug("Entering UploadJobVideo  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                EmploymentHistory historyToSave;
                if (employmentHistoryId.HasValue)
                {
                    historyToSave = context.EmploymentHistories.First(h => h.Id == employmentHistoryId);
                }
                else
                {
                    historyToSave = new EmploymentHistory { UserId = userId };
                    context.EmploymentHistories.AddObject(historyToSave);
                }
                var videoToSave = new EmploymentHistoryVideo();
                videoToSave.EmplementHistoryId = historyToSave.Id;
                videoToSave.VideoURL = videoLink;
                historyToSave.EmploymentHistoryVideos.Add(videoToSave);
                context.SaveChanges();
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Portfolio_video, userId, videoToSave.Id, null);
                var socialManager = new SocialShareManager();
                var msg = "[UserName]" + " " + "has added video for - "+historyToSave.JobTitle+" "+"in Huntable";
                socialManager.ShareVideoOnFacebook(userId, msg, videoToSave.VideoURL);
            }
            LoggingManager.Debug("Exiting UploadJobVideo  - UserProfileManager");
        }
        public static void AddJobAchievement(int? employmentHistoryId, string summary, int userId)
        {
            LoggingManager.Debug("Entering AddJobAchievement  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                EmploymentHistory historyToSave;
                if (employmentHistoryId.HasValue)
                {
                    historyToSave = context.EmploymentHistories.First(h => h.Id == employmentHistoryId);
                }
                else
                {
                    historyToSave = new EmploymentHistory { UserId = userId };
                    context.EmploymentHistories.AddObject(historyToSave);
                }
                var achivementToSave = new EmploymentHistoryAchievement();
                achivementToSave.EmplementHistoryId = historyToSave.Id;
                achivementToSave.Summary = summary;
                historyToSave.EmploymentHistoryAchievements.Add(achivementToSave);
                context.SaveChanges();
                var socialManager = new SocialShareManager();
                var msg = "[UserName]"+" "+"has added acievements to - "+historyToSave.JobTitle+" "+"in Huntable";
                socialManager.ShareOnFacebook(userId, msg, "");
            }
            LoggingManager.Debug("Exiting AddJobAchievement  - UserProfileManager");
        }
        public static string GetPortfolioPictureDisplayUrl(int id)
        {
            return new FileStoreService().GetDownloadUrl(id);
        }

        public static EmploymentHistoryPortfolio GetemploeymentPortfolio(int eid)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                EmploymentHistoryPortfolio query = context.EmploymentHistoryPortfolios.FirstOrDefault(x => x.Id == eid);
                return query;
            }
        }
        public static List<UserPortfolio> GetPictures(int userid)
        {
            LoggingManager.Debug("Entering  GetPictures  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var query = context.UserPortfolios.Where(x => x.UserId == userid).Distinct().OrderBy(x=>x.AddedDateTime).ToList();
                LoggingManager.Debug("Exiting  GetPictures  - UserProfileManager");
                return query;
            }           
        } 
        public static List<UserVideo> GetVideos(int userid)
        {
            LoggingManager.Debug("Entering  GetVideos  - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var query = context.UserVideos.Where(x => x.UserId == userid).Distinct().OrderBy(x=>x.AddedDateTime).Distinct().ToList();
                LoggingManager.Debug("Exiting  GetVideos  - UserProfileManager");
                return query;
            }
        }
        public static List<PreferredFeedUserCompaniesFollwer> GetcompaniesFollowers(int userid)
        {
            LoggingManager.Debug("Entering GetcompaniesFollowers - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var query = context.PreferredFeedUserCompaniesFollwers.Where(x => x.FollowingUserId == userid).Distinct().ToList();
                LoggingManager.Debug("Exiting GetcompaniesFollowers  - UserProfileManager");
                return query;
            }
        }
        public static List<PreferredFeedUserUser> GetUserFollowings(int userid)
        {
            LoggingManager.Debug("Entering GetUserFollowings - UserProfileManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var query = context.PreferredFeedUserUsers.Where(x => x.FollowingUserId == userid).Distinct().ToList();
                LoggingManager.Debug("Exiting  GetUserFollowings - UserProfileManager");
                return query;
            }
        }
    }
}
