using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business.BatchJobs;
using Huntable.Data;
using Huntable.Data.EntityExtensions;
using Huntable.Data.Enums;
using Huntable.Entities.SearchCriteria;
using LinkedIn.ServiceEntities;
using Snovaspace.Util;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.Business
{
    public class UserManager
    {
        public void SendVerificationEMail(string emailAddress, string applicationBaseUrl)
        {
            LoggingManager.Debug("Entering SendVerificationEMail - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                SendVerificationEMail(context.Users.FirstOrDefault(x => x.EmailAddress == emailAddress), applicationBaseUrl);
            }
            LoggingManager.Debug("Exiting SendVerificationEMail - UserManager");
        }
        public void Unfollow(int loginuseruid, int otheruserid)
        {
            LoggingManager.Debug("Entering Unfollow - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var delete = context.PreferredFeedUserUsers.Where(x => x.FollowingUserId == otheruserid && x.UserId == loginuseruid).ToList();
                var cmpnyid = context.Companies.FirstOrDefault(x => x.Userid == loginuseruid);
                foreach (var del in delete)
                {
                    FeedManager.deleteFeedNotitifation(FeedManager.FeedType.Follow, otheruserid, del.UserId);
                        
                    context.DeleteObject(del);
                    context.SaveChanges();
                }
                if (cmpnyid != null)
                {
                    var cmpdelete = context.PreferredFeedUserCompaniesFollwers.Where(x => x.FollowingUserId == otheruserid && x.CompanyID == cmpnyid.Id).ToList();
                    foreach (var dele in cmpdelete)
                    {


                        FeedManager.deleteFeedNotitifation(FeedManager.FeedType.Follow, otheruserid, Convert.ToInt32((dele.Company.User.Id).ToString()));
                        context.DeleteObject(dele);
                        context.SaveChanges();
                    }
                }
            }
            LoggingManager.Debug("Exiting Unfollow - UserManager");
        }
        public int  Companyfollower(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.PreferredFeedUserCompaniesFollwers.Where(x => x.FollowingUserId == userId).ToList();
                return result.Count;
            }
        }
        public int ProfileVisitedCount(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.UserProfileVisitedHistories.Count(p => p.UserId == userId);
                return result;
            }
        }
        public static void FollowUser(int loginUserId, int userIdToFollow)
        {
            LoggingManager.Debug("Entering FollowUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var followup = new PreferredFeedUserUser { UserId = userIdToFollow, FollowingUserId = loginUserId };
                new UserMessageManager().FollowUser(userIdToFollow, loginUserId);
                context.AddToPreferredFeedUserUsers(followup);
                context.SaveChanges();
                FeedManager.addFeedNotification(FeedManager.FeedType.Follow, loginUserId, followup.UserId, null);
                var socialManager = new SocialShareManager();
                var otheruser = context.Users.FirstOrDefault(u => u.Id == userIdToFollow);
                var msg = "[UserName]" + " " + "is following" + " " +otheruser.FirstName+" "+otheruser.LastName+" "+"in Huntable";
                socialManager.ShareOnFacebook(loginUserId, msg, "");
                new Snovaspace.Util.Utility().RunAsTask(() => new PeopleYouMayKnowDelete().Run(loginUserId,userIdToFollow));


            }
            LoggingManager.Debug("Exiting FollowUser - UserManager");
        }
        public void UpdateInvitedStatus(int friendsToInviteId, int userId)
        {
            LoggingManager.Debug("Entering UpdateInvitedStatus - UserManager");
            using (var dataContext = huntableEntities.GetEntitiesWithNoLock())
            {
                var friendsToInvite = dataContext.FriendsToInvites.FirstOrDefault(x => x.FriendInvitationId == friendsToInviteId && x.UserId == userId);
                if (friendsToInvite != null) friendsToInvite.Invited = true;
                dataContext.SaveChanges();
            }
            LoggingManager.Debug("Exiting UpdateInvitedStatus - UserManager");
        }
       public static List<EmploymentHistory> GetEmployeeJobTitle(int loginid)
       {
           LoggingManager.Debug("Entering GetEmployeeJobTitle - UserManager");
           using (var context = huntableEntities.GetEntitiesWithNoLock())
           {
               var result = context.EmploymentHistories.Where(x => x.UserId == loginid);
               LoggingManager.Debug("Exiting GetEmployeeJobTitle - UserManager");
               return result.ToList();
           }
           
       }
        public List<User> GetUserfollower(int userid)
        {
            List<User> userfollowing=null;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Entering GetUserfollower - UserManager");
                var cmpny = context.Users.FirstOrDefault(x => x.Id == userid);

                if (cmpny != null && (cmpny.IsCompany == null ||cmpny.IsCompany==false))
                {
                     userfollowing = (from followerid in context.PreferredFeedUserUsers
                                                join uid in context.Users on followerid.FollowingUserId equals uid.Id
                                                where followerid.UserId == userid
                                                select uid).Distinct().ToList();
                    userfollowing.ToList().ForEach(uid =>
                                                       {
                                                           uid.CurrentCompany = uid.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(e => e.MasterCompany.Description).FirstOrDefault();
                                                           uid.CurrentPosition =
                         uid.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(
                             e => e.JobTitle).FirstOrDefault();
                                                           uid.City = uid.City;

                                                       }
                        );
                    LoggingManager.Debug("Exiting GetUserfollower - UserManager");
                   
                }
                else
                {
                    var firstOrDefault = context.Companies.FirstOrDefault(x => x.Userid == userid);
                    if (firstOrDefault != null)
                    {
                        int cmpid = firstOrDefault.Id;
                       userfollowing = (from followerid in context.PreferredFeedUserCompaniesFollwers
                                                    join uid in context.Users on followerid.FollowingUserId equals uid.Id
                                                    where followerid.CompanyID == cmpid
                                                    select uid).Distinct().ToList();
                        userfollowing.ToList().ForEach(uid =>
                                                           {
                                                               uid.CurrentCompany = uid.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(e => e.MasterCompany.Description).FirstOrDefault();
                                                               uid.CurrentPosition =
                                                                   uid.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(
                                                                       e => e.JobTitle).FirstOrDefault();
                                                               uid.City = uid.City;

                                                           }
                            );
                        LoggingManager.Debug("Exiting GetUserfollower - UserManager");
                      
                    }
                }
                return userfollowing;
            }


        }
        public List<User> GetUserFollowings(int userid)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Entering GetUserFollowings - UserManager");
                List<User> userfollowing = (from followerid in context.PreferredFeedUserUsers
                                            join uid in context.Users on followerid.UserId equals uid.Id
                                            where followerid.FollowingUserId == userid
                                            select uid).Distinct().ToList();

                
                userfollowing.ToList().ForEach(uid =>
                {
                    uid.CurrentCompany = uid.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(e => e.MasterCompany.Description).FirstOrDefault();
                    uid.CurrentPosition =
                        uid.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(
                            e => e.JobTitle).FirstOrDefault();
                    uid.City = uid.City;

                }
                    );
                LoggingManager.Debug("Exiting GetUserFollowings - UserManager");
                return userfollowing;
            }

        }


        public static string GetUserSkill(int userId)
        {
            using (var dataContext = huntableEntities.GetEntitiesWithNoLock())
            {
                return dataContext.UserEmploymentSkills.Where(e => e.EmploymentHistory.UserId == userId).Select(e => e.MasterSkill.Description).FirstOrDefault();
            }
        }

        public static string GetUserIndustry(int userId)
        {
            using (var dataContext = huntableEntities.GetEntitiesWithNoLock())
            {
                return dataContext.MasterIndustries.Where(i => dataContext.Users.Any(u => u.Id == userId && u.ExpectedIndustry == i.Id)).Select(i => i.Description).FirstOrDefault();
            }
        }

        public void UpdateCancelledStatus(int friendsToInviteId, int userId)
        {
            LoggingManager.Debug("Entering UpdateCancelledStatus - UserManager");
            using (var dataContext = huntableEntities.GetEntitiesWithNoLock())
            {
                var friendsToInvite = dataContext.FriendsToInvites.FirstOrDefault(x => x.FriendInvitationId == friendsToInviteId && x.UserId == userId);
                if (friendsToInvite != null) friendsToInvite.Cancelled = true;
                dataContext.SaveChanges();
            }
            LoggingManager.Debug("Exiting UpdateCancelledStatus - UserManager");
        }

        public List<Data.Invitation> FriendsToInvite(int userId, int recordCount)
        {
            LoggingManager.Debug("Entering FriendsToInvite - UserManager");
            var invitations = new List<Data.Invitation>();

            var uniqueInvitations = new List<Data.Invitation>();

            using (var dataContext = huntableEntities.GetEntitiesWithNoLock())
            {
                var friendsToInvite = dataContext.FriendsToInvites.Include("Invitation").Where(x => x.UserId == userId && x.Invited == false && x.Cancelled == false).ToList().Take(recordCount);
                invitations.AddRange(friendsToInvite.Select(friendstoinvitation => friendstoinvitation.Invitation));
            }

            foreach (var invitation in invitations)
            {
                invitation.Name = invitation.Name.Split(new[] { '@' })[0];

                if (uniqueInvitations.Count(x => x.Name == invitation.Name) == 0)
                {
                    uniqueInvitations.Add(invitation);
                }
            }
            LoggingManager.Debug("Exiting FriendsToInvite - UserManager");
            return invitations;
        }

        public List<Data.Invitation> GetFeaturedRecruiters(int userId, int recordCount)
        {
            LoggingManager.Debug("Entering GetFeaturedRecruiters - UserManager");
            var invitations = new List<Data.Invitation>();

            using (var dataContext = huntableEntities.GetEntitiesWithNoLock())
            {
                var friendsToInvite = dataContext.FriendsToInvites.Include("Invitation").Where(x => x.UserId == userId && x.Invited == false && x.Cancelled == false).ToList().Take(recordCount);
                invitations.AddRange(friendsToInvite.Select(friendstoinvitation => friendstoinvitation.Invitation));
            }
            LoggingManager.Debug("Exiting GetFeaturedRecruiters - UserManager");
            return invitations;
        }

        public void SendVerificationEMail(User user, string applicationBaseUrl)
        {
            LoggingManager.Debug("Entering SendVerificationEMail - UserManager");
            var url = applicationBaseUrl + @"ConfirmEmailAddress.aspx?Id=" + user.Id;

            // Sending the email.
            var emailTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.VerifyYouremail);
            var verifyEmailValues = new Hashtable { { "Server Url", applicationBaseUrl }, { "LINK", url } };
            var body = SnovaUtil.LoadTemplate(emailTemplate.TemplateText, verifyEmailValues);
            var subject = emailTemplate.Subject;
            SnovaUtil.SendEmail(subject, body, user.EmailAddress);
            LoggingManager.Debug("Exiting SendVerificationEMail - UserManager");
        }

        public static double GetProfilePercentCompleted(int userId)
        {
            LoggingManager.Debug("Entering GetProfilePercentCompleted - UserManager");
            double percentCompleted = 0;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                if (user.PersonalLogoFileStoreId.HasValue)
                {
                    percentCompleted = 5;
                }
                if (!string.IsNullOrWhiteSpace(user.Summary))
                {
                    percentCompleted += 20;
                }
                if (user.EmploymentHistories.Any(h => h.IsCurrent))
                {
                    percentCompleted += 5;
                }
                if (user.EmploymentHistories.Any(h => h.IsCurrent == false))
                {
                    percentCompleted += 20;
                }
                if (user.EducationHistories.Any(h => h.IsSchool))
                {
                    percentCompleted += 10;
                }
                if (user.EducationHistories.Any(h => h.IsSchool == false))
                {
                    percentCompleted += 20;
                }
                if (user.UserInterests.Any())
                {
                    percentCompleted += 10;
                }
            }
            if (percentCompleted <= 1)
            {
                percentCompleted = 1;
            }
            LoggingManager.Debug("Exiting GetProfilePercentCompleted - UserManager");
            return percentCompleted;
        }

        public static void UploadUserPicture(FileUpload fileUploadControl, int userId)
        {
            LoggingManager.Debug("Entering UploadUserPicture - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                user.PersonalLogoFileStoreId = new FileStoreService().LoadImageAndResize(Constants.AvatarBasePathKey, fileUploadControl);
                FeedManager.addFeedNotification(FeedManager.FeedType.Profile_Picture,userId,null,null );
                context.SaveChanges();
                var socialManager = new SocialShareManager();               
                var url = "https://huntable.co.uk/LoadFile.ashx?id=" + user.PersonalLogoFileStoreId;
                socialManager.ShareOnFacebook(userId, "[UserName] has changed profile picture",url);
            }
            LoggingManager.Debug("Exiting UploadUserPicture - UserManager");
        }
        public static void UploadCompanyPicture1(FileUpload fileUploadControl, int userId)
        {
            LoggingManager.Debug("Entering UploadUserPicture - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                int usid = user.Id;
                var cmpny = context.Companies.FirstOrDefault(u => u.Userid == usid);
                user.PersonalLogoFileStoreId = new FileStoreService().LoadImageAndResize(Constants.AvatarBasePathKey, fileUploadControl);
                if (cmpny != null) cmpny.CompanyLogoId = user.PersonalLogoFileStoreId;
                FeedManager.addFeedNotification(FeedManager.FeedType.Profile_Picture, userId, null, null);
                context.SaveChanges();
                var socialManager = new SocialShareManager();              
                var url = "https://huntable.co.uk/LoadFile.ashx?id=" + cmpny.CompanyLogoId;
                socialManager.ShareOnFacebook(userId, "[UserName] updated their Company logo in Huntable", url);
            }
            LoggingManager.Debug("Exiting UploadUserPicture - UserManager");
        }

        public static int? UploadCompanyPicture(FileUpload fileUploadControl, int userId)
        {
            LoggingManager.Debug("Entering UploadCompanyPicture - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
               
                var user = context.Users.First(u => u.Id == userId);

                user.CompanyLogoFileStoreId = new FileStoreService().LoadImageAndResize(Constants.CompanyBasePathKey, fileUploadControl);
                context.SaveChanges();
                var socialManager = new SocialShareManager();               
                var url = "https://huntable.co.uk/LoadFile.ashx?id=" + user.CompanyLogoFileStoreId;
                socialManager.ShareOnFacebook(userId, "[UserName] updated their Company logo in Huntable", url);
                LoggingManager.Debug("Exiting UploadCompanyPicture - UserManager");
                return user.CompanyLogoFileStoreId;
                
            }
           
        }

        public static void ImportProfileFromLinkedIn(int userId, Person person)
        {
            LoggingManager.Debug("Entering ImportProfileFromLinkedIn - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (context.Users != null)
                {
                    var user = context.Users.First(u => u.Id == userId);
                    if (person != null)
                    {
                        if (person.Location != null)
                        {
                            user.City = person.Location.Name;
                            user.Line1 = person.MainAddress;

                            int countyId = MasterDataManager.AllCountries.Where(
                                c => !string.IsNullOrEmpty(c.Code) && c.Code.ToLower() == person.Location.Country.Code.ToLower()).
                                Select(
                                    c => c.Id).FirstOrDefault();
                            if (countyId > 0)
                            {
                                user.CountryID = countyId;
                            }
                        }
                        if (person.DateOfBirth.Day > 0 && person.DateOfBirth.Month > 0 && person.DateOfBirth.Year > 1753)
                        {
                            user.DOB = new DateTime(person.DateOfBirth.Year, person.DateOfBirth.Month, person.DateOfBirth.Day);
                        }
                        user.HomeAddress = person.MainAddress;
                        if (person.PhoneNumbers != null && person.PhoneNumbers.Count(p => p.Type.ToLower() == "home") > 0)
                        {
                            user.PhoneNumber = person.PhoneNumbers.First(p => p.Type.ToLower() == "home").Number;
                        }
                        if (person.PhoneNumbers != null && person.PhoneNumbers.Count(p => p.Type.ToLower() == "work") > 0)
                        {
                            user.PhoneNumber = person.PhoneNumbers.First(p => p.Type.ToLower() == "work").Number;
                        }
                        if (person.PhoneNumbers != null && person.PhoneNumbers.Count(p => p.Type.ToLower() == "mobile") > 0)
                        {
                            user.PhoneNumber = person.PhoneNumbers.First(p => p.Type.ToLower() == "mobile").Number;
                        }
                        user.Summary = person.Summary;
                        user.Title = person.Headline;
                       
                       
                        UpdateEducatinalHistory(person, user);
                        UpdateEmploymentHistory(person, user);
                    }

                    user.IsProfileUpdated = true;
                }
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting ImportProfileFromLinkedIn - UserManager");
        }

        private static void UpdateEmploymentHistory(Person person, User userCV)
        {
            LoggingManager.Debug("Entering UpdateEmploymentHistory - UserManager");
            if (person.Positions != null && person.Positions.Count > 0)
            {
                foreach (var position in person.Positions)
                {
                    var empHistory = new EmploymentHistory();
                    if (position.Company != null)
                    {
                        empHistory.CompanyId = MasterDataManager.GetMasterCompanyId(position.Company.Name);
                    }
                    empHistory.Description = position.Summary;
                    if (position.StartDate != null && position.StartDate.Month > 0 && position.StartDate.Year > 0)
                    {
                        empHistory.FromMonthID = MasterDataManager.AllMonths.First(m => m.ID == position.StartDate.Month).ID;
                        if (MasterDataManager.AllYears.Any(y => y.Description == position.StartDate.Year.ToString()))
                        {
                            empHistory.FromYearID = MasterDataManager.AllYears.First(y => y.Description == position.StartDate.Year.ToString()).ID;
                        }
                    }
                    if (position.EndDate != null && position.EndDate.Month > 0 && position.EndDate.Year > 0)
                    {
                        empHistory.ToMonthID = MasterDataManager.AllMonths.First(m => m.ID == position.EndDate.Month).ID;
                        if (MasterDataManager.AllYears.Any(y => y.Description == position.EndDate.Year.ToString()))
                        {
                            empHistory.ToYearID = MasterDataManager.AllYears.First(y => y.Description == position.EndDate.Year.ToString()).ID;
                        }
                    }
                    empHistory.JobTitle = position.Title;
                    empHistory.IsCurrent = position.IsCurrent;
                    userCV.EmploymentHistories.Add(empHistory);
                }
            }
            LoggingManager.Debug("Exiting UpdateEmploymentHistory - UserManager");
        }

        private static void UpdateEducatinalHistory(Person person, User user)
        {
            LoggingManager.Debug("Entering UpdateEducatinalHistory - UserManager");
            if (person.Educations != null && person.Educations.Count > 0)
            {
                foreach (var education in person.Educations)
                {
                    var eduHistory = new EducationHistory { CurrentEducation = education.IsCurrent, Description = education.FieldOfStudy };
                    if (!string.IsNullOrEmpty(education.Degree))
                        eduHistory.Degree = education.Degree;

                    if (education.StartDate != null && education.StartDate.Year > 0)
                    {
                        if (MasterDataManager.AllYears.Any(y => y.Description == education.StartDate.Year.ToString()))
                        {
                            eduHistory.FromYearID = MasterDataManager.AllYears.First(y => y.Description == education.StartDate.Year.ToString()).ID;

                            eduHistory.FromMonthID = education.StartDate.Month > 0 ? MasterDataManager.AllMonths.First(m => m.ID == education.StartDate.Month).ID : MasterDataManager.AllMonths.First(m => m.ID == 1).ID;
                        }
                    }
                  

                    if (education.EndDate != null && education.EndDate.Year > 0)
                    {
                        if (MasterDataManager.AllYears.Any(y => y.Description == education.EndDate.Year.ToString()))
                        {
                            eduHistory.ToYearID = MasterDataManager.AllYears.First(y => y.Description == education.EndDate.Year.ToString()).ID;
                            eduHistory.ToMonthID = education.EndDate.Month > 0 ? MasterDataManager.AllMonths.First(m => m.ID == education.EndDate.Month).ID : MasterDataManager.AllMonths.First(m => m.ID == 1).ID;
                        }
                    }

                   

                    eduHistory.Institution = education.SchoolName;
                    user.EducationHistories.Add(eduHistory);
                }
            }
            LoggingManager.Debug("Exiting UpdateEducatinalHistory - UserManager");
        }

        public static List<User> SearchUsers(UserSearchCriteria criteria, out int totalRecordsCount, int? recordsCount = null, int? pageIndex = null, bool updateProfileVisitedCount = false, int? loginUserId = null)
        {
            LoggingManager.Debug("Search for users entry into user manager.");
            LoggingManager.Debug(criteria.ToString());

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                IQueryable<User> query = context.Users.Include("MasterCountry").Where(u => u.IsVerified.HasValue && u.IsVerified.Value&&u.IsCompany==null);

                if (criteria.Id.HasValue)
                {
                    query = query.Where(u => u.Id == criteria.Id);
                }
                else
                {
                    if (criteria.Keywords != "Keywords" && !string.IsNullOrEmpty(criteria.Keywords))
                    {
                        criteria.Keywords = criteria.Keywords.ToLower();

                        query = query.Where(u => u.FirstName.ToLower().Contains(criteria.Keywords)
                                            || (u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(criteria.Keywords)
                                            || u.LastName.ToLower().Contains(criteria.Keywords)
                                            || u.EmailAddress.ToLower().Contains(criteria.Keywords)
                                            || u.Summary.ToLower().Contains(criteria.Keywords)
                                            || u.Line1.ToLower().Contains(criteria.Keywords)
                                            || u.Line2.ToLower().Contains(criteria.Keywords)
                                            || u.Line3.ToLower().Contains(criteria.Keywords)
                                            || u.City.ToLower().Contains(criteria.Keywords)
                                            || u.HomeAddress.ToLower().Contains(criteria.Keywords)
                                            || u.PositionLookingFor.ToLower().Contains(criteria.Keywords)
                                            || u.BlogAddress.ToLower().Contains(criteria.Keywords)
                                            || u.State.ToLower().Contains(criteria.Keywords)
                                            || u.ZipCode.ToLower().Contains(criteria.Keywords)
                                            || u.County.ToLower().Contains(criteria.Keywords)
                                            || u.EmploymentHistories.Any(s => s.UserEmploymentSkills.Any(sk => sk.MasterSkill.Description.ToLower().Contains(criteria.Keywords)))
                                            || u.EmploymentHistories.Any(s => s.MasterCompany.Description.ToLower().Contains(criteria.Keywords))                                          
                                            || u.Title.ToLower().Contains(criteria.Keywords));
                    }

                    if (!string.IsNullOrEmpty(criteria.JobTitle))
                    {
                        query = query.Where(u => u.EmploymentHistories.Any(h => h.JobTitle.ToLower().Contains(criteria.JobTitle.ToLower())
                                                                                && (!criteria.IsCurrentPosition.HasValue || h.IsCurrent == criteria.IsCurrentPosition)
                                                                          ));
                    }

                    if (!string.IsNullOrEmpty(criteria.CompanyName))
                    {
                        query = query.Where(u => u.EmploymentHistories.Any(h => h.MasterCompany.Description.ToLower().Contains(criteria.CompanyName.ToLower())
                                                                             && (!criteria.IsCurrentPosition.HasValue || h.IsCurrent == criteria.IsCurrentPosition)
                                                                         ));
                    }

                    if (!string.IsNullOrEmpty(criteria.FirstName))
                    {
                        query = query.Where(u => u.FirstName.ToLower().Contains(criteria.FirstName.ToLower()));
                    }

                    if (!string.IsNullOrEmpty(criteria.LastName))
                    {
                        query = query.Where(u => u.LastName.ToLower().Contains(criteria.LastName.ToLower()));
                    }

                    if (criteria.IsProfileAvailable.HasValue)
                    {
                        query = query.Where(u => u.IsProfileAvailable == criteria.IsProfileAvailable.Value);
                    }

                    if (!string.IsNullOrEmpty(criteria.SchoolName))
                    {
                        query = query.Where(u => u.EducationHistories.Any(e => e.Institution.ToLower().Contains(criteria.SchoolName.ToLower())));
                    }

                    if (!string.IsNullOrEmpty(criteria.Skill))
                    {

                        query = query.Where(u => 
                            u.EmploymentHistories.Any(h => h.UserEmploymentSkills.Any(skl=>skl.MasterSkill.Description.ToLower().Contains(criteria.Skill.ToLower())))
                            || u.UserSkills.Any(skl => skl.MasterSkill.Description.ToLower().Contains(criteria.Skill.ToLower())));

                    }

                    if (!string.IsNullOrEmpty(criteria.Title))
                    {
                        query = query.Where(u => u.Title.ToLower().Contains(criteria.Title.ToLower()));
                    }

                    if (criteria.CountryId.HasValue)
                    {
                        query = query.Where(u => u.CountryID == criteria.CountryId);
                    }
                    if(criteria.Industryid>0)
                    {
                        query = query.Where(u => u.EmploymentHistories.Any(h => h.IndustryId == criteria.Industryid));
                    }
                    if(criteria.Interest>0)
                    {
                        query = query.Where(u => u.UserInterests.Any(h => h.MasterInterestId == criteria.Interest));
                    }
                  
                    if (criteria.ExperienceFrom.HasValue && criteria.ExperienceTo.HasValue)
                    {
                        query = query.Where(u => u.TotalExperienceInYears >= criteria.ExperienceFrom.Value && u.TotalExperienceInYears <= criteria.ExperienceTo.Value);
                    }
                }

                totalRecordsCount = query.Count();

                // Taking the page count records only.
                if (recordsCount.HasValue && pageIndex.HasValue)
                {
                    query = query.OrderByDescending(u=>u.CreatedDateTime).Skip(recordsCount.Value * (pageIndex.Value - 1)).Take(recordsCount.Value);
                }

                LoggingManager.Debug("Search query is " + query.ToTraceString());

                query.ToList().ForEach(u =>
                {
                    u.ProfileSearchResultCount++;
                    if (loginUserId.HasValue)
                    {
                        u.IsUserFollowing = !context.PreferredFeedUserUsers.Any(f => f.UserId == loginUserId && f.FollowingUserId == u.Id);
                    }
                    u.CurrentPosition = u.EmploymentHistories.Where(e => e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
                    u.CurrentCompany = u.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(e => e.MasterCompany.Description).FirstOrDefault();
                    u.PastPositions = string.Join("<br/>", u.EmploymentHistories.Where(e => !e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle));
                    u.PastPosition = u.EmploymentHistories.Where(e => (e.IsCurrent == false) && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
                    u.PastCompany = u.EmploymentHistories.Where(e => (e.IsCurrent == false) && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.MasterCompany.Description).FirstOrDefault();
                    u.ProfileVisitedCount = u.UserProfileVisitedHistories.Count();
                });

                if (updateProfileVisitedCount)
                {
                    context.SaveChanges();
                }

                LoggingManager.Debug("Search for users exit from user manager.");

                return query.OrderByDescending(u=>u.CreatedDateTime).ToList();
            }
        }

        public static List<User> SearchUsersSp(UserSearchCriteria criteria, out int totalRecordsCount, int recordsCount, int pageIndex, int? loginUserId = null)
        {
            LoggingManager.Debug("Entering SearchUsersSp - UserManager");
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["HuntableDb"].ConnectionString))
            {
                using (
                    var command = new SqlCommand(StoredProcedures.SearchUsers, con) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    if (!string.IsNullOrEmpty(criteria.Keywords))
                    {
                        command.Parameters.AddWithValue("@keywords", criteria.Keywords);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@keywords", DBNull.Value);
                    }
                    if (!string.IsNullOrEmpty(criteria.FirstName))
                    {
                        command.Parameters.AddWithValue("@firstName", criteria.FirstName);
                    }
                    if (!string.IsNullOrEmpty(criteria.LastName))
                    {
                        command.Parameters.AddWithValue("@lastName", criteria.LastName);
                    }
                    if (!string.IsNullOrEmpty(criteria.IsProfileAvailable.ToString()))
                    {
                        command.Parameters.AddWithValue("@IsProfileAvailable", criteria.IsProfileAvailable);
                    }
                    if (!string.IsNullOrEmpty(criteria.SchoolName))
                    {
                        command.Parameters.AddWithValue("@SchoolName", criteria.SchoolName);
                    }
                    if (!string.IsNullOrEmpty(criteria.Industry))
                    {
                        command.Parameters.AddWithValue("@Industry", criteria.Industry);
                    }
                    if (!string.IsNullOrEmpty(criteria.Skill))
                    {
                        command.Parameters.AddWithValue("@Skills", criteria.Skill);
                    }
                    if (criteria.CountryId.HasValue)
                    {
                        command.Parameters.AddWithValue("@CountryId", criteria.CountryId);
                    }
                    if (criteria.TotalExp.HasValue)
                    {
                        command.Parameters.AddWithValue("@TotalExp", criteria.TotalExp);
                    }

                    command.Parameters.AddWithValue("@UserId", loginUserId);
                    command.Parameters.AddWithValue("@PageIndex", pageIndex);
                    command.Parameters.AddWithValue("@PageSize", recordsCount);
                    command.Parameters.Add("@TotalRecordsCount", System.Data.SqlDbType.Int);
                    command.Parameters["@TotalRecordsCount"].Direction = System.Data.ParameterDirection.Output;

                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    var users = new List<User>();
                    while (dr.Read())
                    {
                        var user = new User
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                FirstName = dr["FirstName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                PersonalLogoFileStoreId =
                                    new Utility().ConvertToInt(dr["PersonalLogoFileStoreId"]),
                                CountryID = new Utility().ConvertToInt(dr["CountryID"]),
                                CurrentCompany = Convert.ToString(dr["CurrentCompany"]),
                                CurrentPosition = Convert.ToString(dr["CurrentRole"]),
                                IsUserFollowing = Convert.ToInt32(dr["FollowupCount"]) <= 0,
                                IsJobFollowing = Convert.ToInt32(dr["FollowupJobCount"]) <= 0,

                            };
                        
                        users.Add(user);
                    }

                    dr.Close();

                    totalRecordsCount = Convert.ToInt32(command.Parameters["@TotalRecordsCount"].Value);
                    LoggingManager.Debug("Exiting SearchUsersSp - UserManager");
                    return users;
                }
            }

        }

        public void SaveUserPaymentInfo(UserPaymentInfo paymentInfo)
        {
            LoggingManager.Debug("Entering SaveUserPaymentInfo - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                context.AddToUserPaymentInfoes(paymentInfo);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting SaveUserPaymentInfo - UserManager");
        }

        public EmploymentHistory GetUserEmpDetails(int userid)
        {
            LoggingManager.Debug("Entering GetUserEmpDetails - UserManager");
            EmploymentHistory user;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                user = context.EmploymentHistories.
                    Include("MasterCompany")
                    .FirstOrDefault(u => u.UserId == userid);
            }
            LoggingManager.Debug("Exiting GetUserEmpDetails - UserManager");
            return user;
        }
        public void SavePremiumUser(int userId)
        {
            LoggingManager.Debug("Entering SavePremiumUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var currentUser = context.Users.FirstOrDefault(u => u.Id == userId);

                if (currentUser != null)
                {
                    if (currentUser.IsPremiumAccount == true)
                    {
                        return;
                    }

                    currentUser.IsPremiumAccount = true;
                    if (currentUser.ReferralId.HasValue)
                    {
                        var referrer = context.Users.FirstOrDefault(u => u.Id == currentUser.ReferralId);
                        if (referrer != null)
                        {
                            var level1 = referrer.LevelOnePremiumCount;
                            referrer.LevelOnePremiumCount = level1.HasValue ? level1 + 1 : 1;

                            if (referrer.ReferralId.HasValue)
                            {
                                var parentuser = context.Users.FirstOrDefault(u => u.Id == referrer.ReferralId);

                                if (parentuser != null)
                                {
                                    parentuser.LevelTwoPremiumCount = parentuser.LevelTwoPremiumCount.HasValue
                                                                          ? parentuser.LevelTwoPremiumCount + 1
                                                                          : 1;
                                    if (parentuser.ReferralId.HasValue)
                                    {
                                        var superparentuser =
                                            context.Users.FirstOrDefault(u => u.Id == parentuser.ReferralId);
                                        if (superparentuser != null)
                                        {
                                            superparentuser.LevelThreePremiumCount =
                                                superparentuser.LevelThreePremiumCount.HasValue
                                                    ? superparentuser.LevelThreePremiumCount + 1
                                                    : 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    context.SaveChanges();
                    // Sending the email notifications.
                    SendEmailToReferalUsers(context, currentUser);
                }
            }
            LoggingManager.Debug("Exiting SavePremiumUser - UserManager");
        }

        public static void SendEmailToReferalUsers(huntableEntities context, User joinedUser)
        {
            LoggingManager.Debug("Entering SendEmailToReferalUsers - UserManager");
            if (joinedUser.ReferralId.HasValue)
            {
                var refferedUser = context.Users.First(u => u.Id == joinedUser.ReferralId);
                string baseUrl = Common.GetApplicationBaseUrl();
                var invManager = new InvitationManager();
                var level1Count = refferedUser.LevelOneInvitedCount.HasValue ? refferedUser.LevelOneInvitedCount : 0;
                var level1JCount = refferedUser.LevelOnePremiumCount.HasValue ? refferedUser.LevelOnePremiumCount : 0;
                var level2JCount = refferedUser.LevelTwoPremiumCount.HasValue ? refferedUser.LevelTwoPremiumCount : 0;
                var level3JCount = refferedUser.LevelThreePremiumCount.HasValue ? refferedUser.LevelThreePremiumCount : 0;
                string joinedUserProfilePicturePath = Path.Combine(baseUrl, joinedUser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                string joinedUserProfilePath = Path.Combine(baseUrl, "ViewUserProfile.aspx?UserId=" + joinedUser.Id);
                var firstLevelEmailNotificationData = Common.GetuserEmailPref(refferedUser.Id);
                if (firstLevelEmailNotificationData == null || firstLevelEmailNotificationData.WhenFriendsJoin)
                {
                    // Sending first level notification.
                    var firstLevelTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.FirstConnectionJoined);
                    var friendsInvitations = invManager.GetFriendsInvitationsAsHtmlString(refferedUser.Id);
                    var valuesList = new Hashtable
                                         {
                                             {"Name", refferedUser.Name},
                                             {"Joined User Name", joinedUser.Name},
                                             {"Joined User Role", joinedUser.CurrentPosition},
                                             {"Joined User Company", joinedUser.CurrentCompany},
                                             {"Joined User Description", joinedUser.Summary},
                                             {"Total Credit", refferedUser.AffliateAmountAsText},
                                             {"Server Url", baseUrl},
                                             {"JoinedUserProfilePicturePath", joinedUserProfilePicturePath},
                                             {"Your Account Details", Path.Combine(baseUrl, "MyAccount.aspx")},
                                             {"Invite More Friends", Path.Combine(baseUrl, "SendInvitations.aspx")},
                                             {
                                                 "Dont Want To Receive Email",
                                                 Path.Combine(baseUrl, "UserEmailNotification.aspx")
                                                 },
                                             {"Joined User Profile Link", joinedUserProfilePath},
                                             {"TotalInvitations",level1Count},
                                             {"TotalFirstConnections",level1JCount},
                                             {"TotalSecondConnections",level2JCount},
                                             {"TotalThirdConnections",level3JCount},
                                             {"TotalAffilateEarning",refferedUser.AffliateAmountAsText},
                                             {"FriendsInvitations",friendsInvitations}
                                         };

                    string body = SnovaUtil.LoadTemplate(firstLevelTemplate.TemplateText, valuesList);
                    SnovaUtil.SendEmail(firstLevelTemplate.Subject, body, refferedUser.EmailAddress);
                }
                if (refferedUser.ReferralId.HasValue)
                {
                    var secondLevelUser = context.Users.First(u => u.Id == refferedUser.ReferralId);
                    var slevel1Count = secondLevelUser.LevelOneInvitedCount.HasValue ? secondLevelUser.LevelOneInvitedCount : 0;
                    var slevel2Count = secondLevelUser.LevelTwoInvitedCount.HasValue ? secondLevelUser.LevelTwoInvitedCount : 0;
                    var slevel3Count = secondLevelUser.LevelThreeInvitedCount.HasValue ? secondLevelUser.LevelThreeInvitedCount : 0;
                    int scount = Convert.ToInt32(slevel1Count + slevel2Count + slevel3Count);
                    var slevel1JCount = secondLevelUser.LevelOnePremiumCount.HasValue ? secondLevelUser.LevelOnePremiumCount : 0;
                    var slevel2JCount = secondLevelUser.LevelTwoPremiumCount.HasValue ? secondLevelUser.LevelTwoPremiumCount : 0;
                    var slevel3JCount = secondLevelUser.LevelThreePremiumCount.HasValue ? secondLevelUser.LevelThreePremiumCount : 0;
                    int sJcount = Convert.ToInt32(slevel1JCount + slevel2JCount + slevel3JCount);
                    var secondLevelEmailNotificationData = Common.GetuserEmailPref(secondLevelUser.Id);
                    if (secondLevelEmailNotificationData == null || secondLevelEmailNotificationData.WhenFriendsFriendsJoin)
                    {
                        // Sending the second level notification.
                        var secondLevelTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.FriendFriendJoined);
                        var friendsInvitations = invManager.GetFriendsInvitationsAsHtmlString(secondLevelUser.Id);
                        var secondValuesList = new Hashtable
                                                   {
                                                       {"Name", secondLevelUser.Name},
                                                       {"Joined User Name", joinedUser.Name},
                                                       {"Joined User Role", joinedUser.CurrentPosition},
                                                       {"Joined User Company", joinedUser.CurrentCompany},
                                                       {"Joined User Description", joinedUser.Summary},
                                                       {"Total Credit", secondLevelUser.AffliateAmountAsText},
                                                       {"Your Friend Name", refferedUser.Name},
                                                       {"Your Friend Role", refferedUser.CurrentPosition},
                                                       {"Your Friend Company", refferedUser.CurrentCompany},
                                                       {"Server Url", baseUrl},
                                                       {"JoinedUserProfilePicturePath", joinedUserProfilePicturePath},
                                                       {"Joined User Profile Link", joinedUserProfilePath},
                                                       {"Your Account Details", Path.Combine(baseUrl, "MyAccount.aspx")},
                                                       {
                                                           "Invite More Friends",
                                                           Path.Combine(baseUrl, "SendInvitations.aspx")
                                                           },
                                                       {
                                                           "Dont Want To Receive Email",
                                                           Path.Combine(baseUrl, "UserEmailNotification.aspx")
                                                           },
                                                           {"TotalInvitations",scount},
                                             {"FriendsJoined",sJcount},
                                             {"TotalFirstConnections",slevel1JCount},
                                             {"TotalSecondConnections",slevel2JCount},
                                             {"TotalThirdConnections",slevel3JCount},
                                             {"TotalAffilateEarning",secondLevelUser.AffliateAmountAsText},
                                              {"FriendsInvitations",friendsInvitations}
                                                   };

                        string secondBody = SnovaUtil.LoadTemplate(secondLevelTemplate.TemplateText, secondValuesList);
                        SnovaUtil.SendEmail(secondLevelTemplate.Subject, secondBody, secondLevelUser.EmailAddress);
                    }
                    if (secondLevelUser.ReferralId.HasValue)
                    {
                        var thirdLevelEmailNotificationData = Common.GetuserEmailPref(secondLevelUser.ReferralId.Value);
                        if (thirdLevelEmailNotificationData == null || thirdLevelEmailNotificationData.WhenThirdConnectionJoins)
                        {
                            var thirdUser = context.Users.First(u => u.Id == secondLevelUser.ReferralId);
                            var tlevel1Count = thirdUser.LevelOneInvitedCount.HasValue ? thirdUser.LevelOneInvitedCount : 0;
                            var tlevel2Count = thirdUser.LevelTwoInvitedCount.HasValue ? thirdUser.LevelTwoInvitedCount : 0;
                            var tlevel3Count = thirdUser.LevelThreeInvitedCount.HasValue ? thirdUser.LevelThreeInvitedCount : 0;
                            int tcount = Convert.ToInt32(tlevel1Count + tlevel2Count + tlevel3Count);
                            var tlevel1JCount = thirdUser.LevelOnePremiumCount.HasValue ? thirdUser.LevelOnePremiumCount : 0;
                            var tlevel2JCount = thirdUser.LevelTwoPremiumCount.HasValue ? thirdUser.LevelTwoPremiumCount : 0;
                            var tlevel3JCount = thirdUser.LevelThreePremiumCount.HasValue ? thirdUser.LevelThreePremiumCount : 0;
                            int jcount = Convert.ToInt32(tlevel1JCount + tlevel2JCount + tlevel3JCount);
                            // Sending third level notification.
                            var thirdLevelTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.ThirdConnectionJoined);
                            var upgradeTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.Upgrade);
                            var friendsInvitations = invManager.GetFriendsInvitationsAsHtmlString(thirdUser.Id);
                            var thirdValuesList = new Hashtable
                                                      {
                                                          {"Name",thirdUser.Name},
                                                          {"Joined User Name", joinedUser.Name},
                                                          {"Joined User Role", joinedUser.CurrentPosition},
                                                          {"Joined User Company", joinedUser.CurrentCompany},
                                                          {"Joined User Description", joinedUser.Summary},
                                                          {"Your Friend Name", refferedUser.Name},
                                                          {"Your Friend Role", refferedUser.CurrentPosition},
                                                          {"Your Friend Company", refferedUser.CurrentCompany},
                                                          {"Friend Friend Name", secondLevelUser.Name},
                                                          {"Friend Friend Role", secondLevelUser.CurrentPosition},
                                                          {"Friend Friend Company", secondLevelUser.CurrentCompany},
                                                          {"Total Credit", thirdUser.AffliateAmountAsText},
                                                          {"Server Url", baseUrl},
                                                          {"TotalInvitations",tcount},
                                             {"FriendsJoined",jcount},
                                             {"TotalFirstConnections",tlevel1JCount},
                                             {"TotalSecondConnections",tlevel2JCount},
                                             {"TotalThirdConnections",tlevel3JCount},
                                             {"TotalAffilateEarning",thirdUser.AffliateAmountAsText},
                                              {"FriendsInvitations",friendsInvitations}
                                                      };
                            var upgradeList = new Hashtable
                                                  {
                                                      {"Name",thirdUser.Name},
                                                      {"FirstInvited",tlevel1Count},
                                                      {"FirstJoined",tlevel1JCount},
                                                      {"FirstEarnings",tlevel1JCount*5},
                                                      {"SecondInvited",tlevel2Count},
                                                      {"SecondJoined",tlevel2JCount},
                                                      {"SecondEarnings",tlevel2JCount*1},
                                                      {"ThirdInvited",tlevel3Count},
                                                      {"ThirdJoined",tlevel3JCount},
                                                      {"ThirdEarnings",tlevel3JCount*0.5}
                                                  };
                            string yourFriendUserProfilePath = Path.Combine(baseUrl, "ViewUserProfile.aspx?UserId=" + refferedUser.Id);
                            thirdValuesList.Add("Your Friend Profile Link", yourFriendUserProfilePath);

                            string friendFriendUserProfilePath = Path.Combine(baseUrl, "ViewUserProfile.aspx?UserId=" + secondLevelUser.Id);
                            thirdValuesList.Add("Friend Friend Profile Link", friendFriendUserProfilePath);


                            thirdValuesList.Add("JoinedUserProfilePicturePath", joinedUserProfilePicturePath);
                            thirdValuesList.Add("Joined User Profile Link", joinedUserProfilePath);

                            thirdValuesList.Add("Your Account Details", Path.Combine(baseUrl, "MyAccount.aspx"));
                            thirdValuesList.Add("Invite More Friends", Path.Combine(baseUrl, "SendInvitations.aspx"));
                            thirdValuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));

                            string thirdBody = SnovaUtil.LoadTemplate(thirdLevelTemplate.TemplateText, thirdValuesList);
                            string upgradeBody = SnovaUtil.LoadTemplate(upgradeTemplate.TemplateText, upgradeList);
                            SnovaUtil.SendEmail(thirdLevelTemplate.Subject, thirdBody, thirdUser.EmailAddress);
                            if (thirdUser.IsPremiumAccount == null)
                            {
                                SnovaUtil.SendEmail(upgradeTemplate.Subject, upgradeBody, thirdUser.EmailAddress);
                            }
                        }
                    }
                }
            }
            LoggingManager.Debug("Exiting SendEmailToReferalUsers - UserManager");
        }
      
        public static List<MasterCompany> GetFeaturedUserCompanies(int userId)
        {
            return new List<MasterCompany>();
        }

        public static List<FeaturedUserCompany> GetFeaturedUserPCompanies(string letterfilter)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.FeaturedUserCompanies.Take(8).ToList();
            }
        }

        public List<PreferredUserCompany> GetFeaturedCompanies(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.PreferredUserCompanies.Where(x => x.UserId == userId).Take(8).ToList();
            }
        }

        public List<PreferredUserCompany> GetFeaturedCompanie(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.PreferredUserCompanies.Where(x => x.UserId == userId).ToList();
            }
        }

        public List<PreferredUserCompany> GetFeaturedCompaniesDefault()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.PreferredUserCompanies.Take(8).ToList();
            }
        }


        public List<UserVideo> GetVideos(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.UserVideos.Where(x => x.UserId == userId).ToList();
            }
        }
        public List<UserVideo> GetRecentVideos(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.UserVideos.Where(x => x.UserId == userId&&x.AddedDateTime==DateTime.Today).ToList();
            }
        }
        public List<UserPortfolio> GetPictures(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.UserPortfolios.Where(x => x.UserId == userId).ToList();
            }
        }

        public List<CompanyPortfolio> GetPics(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //return context.UserPortfolios.Where(x => x.UserId == userId).ToList();
                return context.CompanyPortfolios.Where(x => x.CompanyId == userId).ToList();
            }
        }
        public List<CompanyVideo> GetMyvideos(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.CompanyVideos.Where(x => x.CompanyId == userId && x.VideoUrl != "Video url").ToList();
            }
        }



        public static List<User> GetUsersAndFriends(int userId, int pageIndex, int pageSize, out int totalRecords)
        {
            LoggingManager.Debug("Entering GetUsersAndFriends - UserManager");
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["HuntableDb"].ConnectionString);
            var command = new SqlCommand(StoredProcedures.SearchUsersToFollow, con) { CommandType = System.Data.CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@PageIndex", pageIndex);
            command.Parameters.AddWithValue("@PageSize", pageSize);
            command.Parameters.Add("@TotalRecordsCount", System.Data.SqlDbType.Int);
            command.Parameters["@TotalRecordsCount"].Direction = System.Data.ParameterDirection.Output;
            con.Open();
            SqlDataReader dr = command.ExecuteReader();
            var users = new List<User>();
            while (dr.Read())
            {
                var user = new User
                               {
                                   Id = Convert.ToInt32(dr["Id"]),
                                   FirstName = dr["FirstName"].ToString(),
                                   LastName = dr["LastName"].ToString(),
                                   PersonalLogoFileStoreId = (dr["PersonalLogoFileStoreId"] == null || dr["PersonalLogoFileStoreId"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["PersonalLogoFileStoreId"]),
                                   CountryID = new Utility().ConvertToInt(dr["CountryID"] as string),
                                   CurrentCompany = Convert.ToString(dr["CurrentCompany"]),
                                   CurrentPosition = Convert.ToString(dr["CurrentRole"]),
                                   IsUserFollowing = Convert.ToInt32(dr["FollowupCount"]) <= 0,
                                   IsJobFollowing = Convert.ToInt32(dr["FollowupJobCount"]) <= 0
                               };


                users.Add(user);
            }
            dr.Close();
            totalRecords = Convert.ToInt32(command.Parameters["@TotalRecordsCount"].Value);
            LoggingManager.Debug("Exiting GetUsersAndFriends - UserManager");
            return users;
        }

        public static List<PreferredFeedUserUser> FollowingUser(int loginUserId, int userIdToFollow)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return
                    context.PreferredFeedUserUsers.Where(
                        x => x.UserId == loginUserId && x.FollowingUserId == userIdToFollow).ToList();
            }

        }
        public static void FollowCompany(int loginUserId, int userIdFollow)
        {
            LoggingManager.Debug("Entering FollowCompany - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var followup = new PreferredJobUserCompany { UserId = loginUserId, MasterCompanyId = userIdFollow };
                context.AddToPreferredJobUserCompanies((followup));
                context.SaveChanges();
                var socialManager = new SocialShareManager();
                var cmpny = context.MasterCompanies.FirstOrDefault(c => c.Id == userIdFollow);
                var msg = "[UserName]" + " " + " is following " + " " + cmpny.Description+" "+"in Huntable";
                socialManager.ShareOnFacebook(loginUserId, msg, "");

            }
            LoggingManager.Debug("Exiting FollowCompany - UserManager");
        }

        public static void FollowJobUser(int loginUserId, int userIdToFollow)
        {
            LoggingManager.Debug("Entering FollowJobUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var followup = new PreferredJobUserUser { UserId = loginUserId, FollowingUserId = userIdToFollow };
                 new  UserMessageManager().FollowUser(userIdToFollow,loginUserId);
                context.AddToPreferredJobUserUsers(followup);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting FollowJobUser - UserManager");
        }

        public static void AddSkillToUser(int loginUserId, int skillId, int category, string howlong)
        {
            LoggingManager.Debug("Entering AddSkillToUser - UserManager");
            
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var skillToSave = new UserSkill { UserId = loginUserId, MasterSkillId = skillId, SkillCategory = category, HowLong = howlong };
                var skillname = context.MasterSkills.FirstOrDefault(x => x.Id == skillToSave.MasterSkillId);
                context.AddToUserSkills(skillToSave);
                context.SaveChanges();
                var socialManager = new SocialShareManager();
                var msg = "[UserName]" + " " + "has added - "+skillname.Description+" "+ "to his skills in Huntable";
                socialManager.ShareOnFacebook(loginUserId, msg, "");
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Skill, loginUserId, skillId, null);
            }
            LoggingManager.Debug("Exiting AddSkillToUser - UserManager");
        }

        public static void AddSkillToUser(int loginUserId, string skill, int category, string howlong)
        {
            LoggingManager.Debug("Entering AddSkillToUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var skillExistOrNot = context.MasterSkills.FirstOrDefault(s => s.Description.ToLower() == skill.ToLower());
              
                if (skillExistOrNot!=null)
                {
                     int  skilltoSave = skillExistOrNot.Id;
                     AddSkillToUser(loginUserId, Convert.ToInt32(skilltoSave), (int)category, howlong);
                    return;
                }
                var masterskillToSave = new MasterSkill { Description = skill };
                context.AddToMasterSkills(masterskillToSave);
                context.SaveChanges();

                var skillToSave = new UserSkill { UserId = loginUserId, MasterSkillId = masterskillToSave.Id, SkillCategory = category, HowLong = howlong };
                var skillname = context.MasterSkills.FirstOrDefault(x => x.Id == skillToSave.MasterSkillId);
                context.AddToUserSkills(skillToSave);
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Skill, loginUserId, masterskillToSave.Id, null);
                context.SaveChanges();
                var socialManager = new SocialShareManager();
                var msg = "[UserName]" + " " + "has added - " + skillname.Description +" " +"to his skills in Huntable";
                socialManager.ShareOnFacebook(loginUserId, msg, "");                
            }
            LoggingManager.Debug("Exiting AddSkillToUser - UserManager");
        }

        public static void AddLanguageToUser(int loginUserId, string language, int? level)
        {
            LoggingManager.Debug("Entering AddLanguageToUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var masterlanguageToSave = new MasterLanguage { Description = language };
                context.AddToMasterLanguages(masterlanguageToSave);
                context.SaveChanges();

                var languageToSave = new UserLanguage { UserId = loginUserId, MasterLanguageId = masterlanguageToSave.Id, Level = level };
                context.AddToUserLanguages(languageToSave);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting AddLanguageToUser - UserManager");
        }

        public static void AddLanguageToUser(int loginUserId, int languageId, int? level)
        {
            LoggingManager.Debug("Entering AddLanguageToUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var languageToSave = new UserLanguage { UserId = loginUserId, MasterLanguageId = languageId, Level = level };
                context.AddToUserLanguages(languageToSave);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting AddLanguageToUser - UserManager");
        }

        public static void AddInterestToUser(int loginUserId, string interest)
        {
            LoggingManager.Debug("Entering AddInterestToUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var masterInterestToSave = new MasterInterest { Description = interest };
                context.AddToMasterInterests(masterInterestToSave);
                context.SaveChanges();

                var interestToSave = new UserInterest { UserId = loginUserId, MasterInterestId = masterInterestToSave.Id };
                context.AddToUserInterests(interestToSave);
                context.SaveChanges();
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Interest, loginUserId, interestToSave.MasterInterestId, null);
            }
            LoggingManager.Debug("Exiting AddInterestToUser - UserManager");
        }

        public static void AddInterestToUser(int loginUserId, int interestId)
        {
            LoggingManager.Debug("Entering AddInterestToUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var interestToSave = new UserInterest { UserId = loginUserId, MasterInterestId = interestId };
                var interestname = context.MasterInterests.FirstOrDefault(x => x.Id == interestToSave.MasterInterestId);
                context.AddToUserInterests(interestToSave);
                context.SaveChanges();
                var socialManager = new SocialShareManager();
                var msg = "[UserName]" + " " + "has updated interest in Huntable to - "+interestname.Description;
                socialManager.ShareOnFacebook(loginUserId, msg, "");
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Interest, loginUserId, interestId, null);
            }
            LoggingManager.Debug("Exiting AddInterestToUser - UserManager");
        }

        public void AddUserPortfolio(int loginid, int pictureid, string description)
        {
            LoggingManager.Debug("Entering AddUserPortfolio - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var picturestosave = new UserPortfolio { UserId = loginid, PictureId = pictureid, PictureDescription = description, AddedDateTime = DateTime.Now };
                context.UserPortfolios.AddObject(picturestosave);
                context.SaveChanges();
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Photo,loginid,picturestosave.Id,null);
            }
            LoggingManager.Debug("Exiting AddUserPortfolio - UserManager");
        }
        public void AddUserVideo(int loginid, string videotitle, string videourl)
        {
            LoggingManager.Debug("Entering AddUserVideo - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var videotosave = new UserVideo
                                      {
                                          UserId = loginid,
                                          VideoTitle = videotitle,
                                          VideoUrl = videourl,
                                          AddedDateTime = DateTime.Now
                                      };
                context.UserVideos.AddObject(videotosave);
                context.SaveChanges();
                FeedManager.addFeedNotification(FeedManager.FeedType.User_Video, loginid,videotosave.Id,null);
            }
            LoggingManager.Debug("Exiting AddUserVideo - UserManager");
        }

        public List<CompanyVideo> GetCompanyVideos(int frontEndUserCompanyId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.CompanyVideos.Where(x => x.CompanyId == frontEndUserCompanyId).ToList();

                           }
        }

        public static void FollowCompanybyUser(int companyId, int userIdToFollow)
        {
            LoggingManager.Debug("Entering FollowCompanybyUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var preferredFeedUserCompaniesFollwer = new PreferredFeedUserCompaniesFollwer { CompanyID = companyId, FollowingUserId = userIdToFollow };

                context.AddToPreferredFeedUserCompaniesFollwers(preferredFeedUserCompaniesFollwer);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting FollowCompanybyUser - UserManager");
        }
        
        public static bool CheckCompanyUser(int userCompanyId, int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.PreferredFeedUserCompaniesFollwers.Any(x => x.FollowingUserId == userId && x.CompanyID == userCompanyId);
            }
        }
        public int GetCompaniesCountfollowedByUser(int companyId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.PreferredFeedUserCompaniesFollwers.Count(x => x.CompanyID == companyId);
            }

        }
        
        
        public List<EmploymentHistory> Getemployeedetails(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.EmploymentHistories.Where(x => x.UserId == userId).ToList();
            }
        }


        public static void FollowCompanyuserbyUser(int userId, int userIdToFollow)
        {
            LoggingManager.Debug("Entering FollowCompanyuserbyUser - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var preferredJobUserUser = new PreferredJobUserUser { UserId = userId, FollowingUserId = userIdToFollow };

                context.AddToPreferredJobUserUsers(preferredJobUserUser);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting FollowCompanyuserbyUser - UserManager");
        }
        public static bool CheckUserbyUser(int userId, int followingUserId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.PreferredJobUserUsers.Any(x => x.UserId == userId && x.FollowingUserId == followingUserId);
            }
        }
        public static void FollowUser1(int loginUserId, int folowingid)
        {
            LoggingManager.Debug("Entering FollowUser1 - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var followup = new PreferredFeedUserUser { UserId = loginUserId, FollowingUserId = folowingid };
                context.PreferredFeedUserUsers.AddObject(followup);
                context.SaveChanges();
              
            }
            LoggingManager.Debug("Exiting FollowUser1 - UserManager");
        }
        public string GetUserName(int userid)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var usrname = context.Users.FirstOrDefault(x => x.Id == userid);
                return usrname.FirstName;
            }
        }
        public static void ProfileUpdatedOn(int userid)
        {
            LoggingManager.Debug("Entering ProfileUpdatedOn - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var usr = context.Users.FirstOrDefault(x => x.Id == userid);
                if (usr != null) usr.LastProfileUpdatedOn = DateTime.Now;
                context.SaveChanges();                
            }
            LoggingManager.Debug("Exiting ProfileUpdatedOn - UserManager");
        }

        public static List<User> GetPeopleYouMayKnow(int loginuserid)
        {
            List<PreferredFeedUserUser> firstLevelFollowers = null; List<PreferredFeedUserUser> firstLevelFollowers01 = new List<PreferredFeedUserUser>();
            List<PreferredFeedUserUser> finalList = new List<PreferredFeedUserUser>();
            List<User> usersYouMayKnow = new List<User>();
            LoggingManager.Debug("Entering GetPeopleYouMayKnow - UserManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int commonfollowers = 0;
               List<PreferredFeedUserUser> users= context.PreferredFeedUserUsers.Where(x =>x.UserId == loginuserid).ToList(); // i am following
                //List<User>
                foreach (PreferredFeedUserUser preferredFeedUserUser in users)
                {
                     firstLevelFollowers =
                        context.PreferredFeedUserUsers.Where(x => x.UserId == preferredFeedUserUser.FollowingUserId) // 
                               .ToList();
                    foreach (PreferredFeedUserUser firstLevelFollower in firstLevelFollowers)
                    {
                         firstLevelFollowers01.Add(firstLevelFollower);
                    }
                    //foreach (var feedUserUser in firstLevelFollowers)
                    //{
                    //    var secondLevelFollowers =
                    //        context.PreferredFeedUserUsers.Where(x => x.UserId == feedUserUser.FollowingUserId).ToList();
                    //    //foreach (var secondLevelFollower in secondLevelFollowers)
                    //    //{
                    //    //    //secondLevelFollower.UserId;
                    //    //}
                    //}
                }
                foreach (PreferredFeedUserUser feeduser in firstLevelFollowers01)
                {
                    foreach (PreferredFeedUserUser innerfeed in users)
                    {
                        var feed =
                            context.PreferredFeedUserUsers.Where(
                                h => h.FollowingUserId == feeduser.FollowingUserId && h.UserId == innerfeed.FollowingUserId)
                                   .ToList();
                        if (feed.Count>0)
                        {
                            commonfollowers++;
                        }
                    }
                    if (commonfollowers>=2)
                    {
                        
                        finalList.Add(feeduser);
                        commonfollowers = 0;
                    }
                }
                
                if (finalList.Count>0)
                {
                    
                    foreach (PreferredFeedUserUser eachfinallist in finalList)
                    {
                       User UsersYouMay = context.Users.FirstOrDefault(h => h.Id == eachfinallist.UserId);
                       usersYouMayKnow.Add(UsersYouMay);
                    }
                    int c = usersYouMayKnow.Count();
                }
                //return finalList ;
            }
            return usersYouMayKnow.Distinct().ToList();
        }

        
    }
}
