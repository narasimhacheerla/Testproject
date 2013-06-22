using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Mail;
using System.Collections;
using System.IO;
using Snovaspace.Util.Logging;

namespace Huntable.Business
{
    public class UserMessageManager
    {
        public User GetUserbyUserId(huntableEntities context, int userId)
        {
            return context.Users.First(u => u.Id == userId);
        }
        public List<Company> GetCompaniesbyUser(huntableEntities context, int userId)
        {
            LoggingManager.Debug("Entering GetCompaniesbyUser - UserMessageManager");
            List<Company> companiesbyuser = context.Companies.ToList().Where(u => u.Userid == userId).ToList();
            LoggingManager.Debug("Exiting GetCompaniesbyUser - UserMessageManager");
            return companiesbyuser;
        }
        public List<EmploymentHistory> GetEmploymentUserDetails(huntableEntities context, int userId)
        {
            LoggingManager.Debug("Entering GetEmploymentUserDetails - UserMessageManager");
            List<EmploymentHistory> employmentUserDetails = context.EmploymentHistories.ToList().Where(u => u.Id == userId).ToList();
            LoggingManager.Debug("Exiting GetEmploymentUserDetails - UserMessageManager");
            return employmentUserDetails;
        }
        public string GetIndustryNamebyCompanyIndustryId(huntableEntities context, int IndustryId)
        {
            LoggingManager.Debug("Entering GetIndustryNamebyCompanyIndustryId - UserMessageManager");
            var companyindustryName = context.MasterIndustries.First(u => u.Id == IndustryId).Description;
            LoggingManager.Debug("Exiting GetIndustryNamebyCompanyIndustryId - UserMessageManager");
            return companyindustryName;
        }
       
        public List<UserMessage> GetUserMessages(huntableEntities context, int userId, out int totalRecordsCount, int? recordsCount = null, int? pageIndex = null)
        {
            LoggingManager.Debug("Entering GetUserMessages - UserMessageManager");
            IQueryable<UserMessage> query = context.UserMessages.Where(u => u.SentTo == userId && u.IsActive);
            totalRecordsCount = query.Count();
            if (recordsCount.HasValue && pageIndex.HasValue)
            {
                query = query.OrderByDescending(u => u.SentDate).Skip(recordsCount.Value * (pageIndex.Value - 1)).Take(recordsCount.Value);
            }
            else
            {
                query = query.OrderByDescending(u => u.SentDate);
            }
            LoggingManager.Debug("Exiting GetUserMessages - UserMessageManager");
            return query.ToList();
        }

        public List<UserMessage> GetUserSentMessages(huntableEntities context, int userId, out int totalRecordsCount, int? recordsCount = null, int? pageIndex = null)
        {
            LoggingManager.Debug("Entering GetUserSentMessages - UserMessageManager");
            IQueryable<UserMessage> query = context.UserMessages.Where(u => u.SentBy == userId && u.SentIsActive);
            totalRecordsCount = query.Count();
            if (recordsCount.HasValue && pageIndex.HasValue)
            {
                query = query.OrderByDescending(u => u.SentDate).Skip(recordsCount.Value * (pageIndex.Value - 1)).Take(recordsCount.Value);
            }
            LoggingManager.Debug("Exiting GetUserSentMessages - UserMessageManager");
            return query.ToList();
            //return context.UserMessages.Where(u => u.SentTo == userId && u.IsActive).OrderBy(u => u.SentDate).ToList();
        }
        public List<UserMessage> GetMessageDetails(int id)
        {
            
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.UserMessages.Where(x => x.Id == id&&x.IsActive==true).ToList();
            }
           

        }
        public int SaveMessage(huntableEntities context, UserMessage objUserMessage, bool isEndosement = false)
        {
            LoggingManager.Debug("Entering SaveMessage - UserMessageManager");
            context.UserMessages.AddObject(objUserMessage);
            int savedRowCount = context.SaveChanges();

            var toUser = context.Users.First(u => u.Id == objUserMessage.SentTo);
            var fromUser = context.Users.First(u => u.Id == objUserMessage.SentBy);
            if (isEndosement)
            {
                SendEndosementEmailToUser(toUser, fromUser,objUserMessage.Body);
            }
            else
            {
                SendEmailToUser(objUserMessage.Subject, objUserMessage.Body, toUser, fromUser);
            }
            LoggingManager.Debug("Exiting SaveMessage - UserMessageManager");
            return savedRowCount;
        }
        public void SendPassworToUser(int userid,string password,Guid code)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(x => x.Id == userid);
                var template = EmailTemplateManager.GetTemplate(EmailTemplates.PasswordChange);
                string baseUrl = Common.GetApplicationBaseUrl();
                string resetLink = Path.Combine(baseUrl, "ConfirmEmailAddress.aspx?Code=" + code);
                var date = DateTime.Now.ToString("g", new CultureInfo("en-US"));
                var valuesList = new Hashtable
                                     {
                                         {"Name", user.Name},
                                         {"Password", password},
                                         {"Code", resetLink},
                                         {"Server Url", baseUrl},
                                         {
                                             "Dont Want To Receive Email",
                                             Path.Combine(baseUrl, "UserEmailNotification.aspx")
                                         },
                                         {"Date",date}
                                     };

                string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                SnovaUtil.SendEmail(template.Subject, body, user.EmailAddress);

            }
        }
        public void FollowUser(int otheruserid, int loginuserid)
        {
            LoggingManager.Debug("Entering FollowUser - UserMessageManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var touser = context.Users.First(x => x.Id == otheruserid);
                var actuser = context.Users.First(x => x.Id == loginuserid);
                var following = context.PreferredFeedUserUsers.Where(x => x.UserId == loginuserid).ToList();
                var followed = context.PreferredFeedUserUsers.Where(x => x.FollowingUserId == loginuserid).ToList();
                var template = EmailTemplateManager.GetTemplate(Data.Enums.EmailTemplates.FollowUser);
                if(actuser.IsCompany==true)
                {
                    var comp = context.Companies.FirstOrDefault(x => x.Userid == loginuserid);
                    if (comp != null)
                    {
                        var valuesList = new Hashtable
                                             {
                                                 {"Name", actuser.Name},
                                                 {"Actual User Name", touser.Name},
                                                 {"Actual User Role", comp.CompanyName},
                                                 {"Actual User Company", actuser.CurrentCompany},
                                                 {"Description",comp.CompanyDescription},
                                                 {"Following", (following.Count != 0) ? following.Count : 0},
                                                 {"Followed", (followed.Count != 0) ? followed.Count : 0}
                                             };
                        string baseUrl = Common.GetApplicationBaseUrl();
                        string userProfilePath = Path.Combine(baseUrl, "companyview.aspx?Id=" + comp.Id);
                        valuesList.Add("Actual User Profile Link", userProfilePath);
                        string userProfilePicturePath = Path.Combine(baseUrl, actuser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                        valuesList.Add("User Profile Picture", userProfilePicturePath);
                        valuesList.Add("Server Url", baseUrl);
                        valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
                        valuesList.Add("FollowBackUser", baseUrl + "default.aspx?" + "ReturnUrl=ViewUserProfile.aspx?UserId=" + actuser.Id);
                        string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                        SnovaUtil.SendEmail(template.Subject, body, touser.EmailAddress);
                        var socialManager = new SocialShareManager();                       
                        var msg = "[UserName]" + " " + "is following" + " " + touser.FirstName + " " +touser.LastName+" "+ "in Huntable";
                        socialManager.ShareOnFacebook(loginuserid, msg, "");
                    }
                }
                else
                {

                    var valuesList = new Hashtable
                                     {
                                         {"Name", actuser.Name},
                                         {"Actual User Name", touser.Name},
                                         {"Actual User Role", actuser.CurrentPosition},
                                         {"Actual User Company", actuser.CurrentCompany},
                                         {"Description",actuser.Summary},
                                         {"Following", (following.Count != 0) ? following.Count : 0},
                                         {"Followed", (followed.Count != 0) ? followed.Count : 0}
                                     };
                    string baseUrl = Common.GetApplicationBaseUrl();
                    var url = new UrlGenerator().UserUrlGenerator(actuser.Id);
                    string userProfilePath = Path.Combine(baseUrl, url);
                    valuesList.Add("Actual User Profile Link", userProfilePath);
                    string userProfilePicturePath = Path.Combine(baseUrl, actuser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                    valuesList.Add("User Profile Picture", userProfilePicturePath);
                    valuesList.Add("Server Url", baseUrl);
                    valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
                    valuesList.Add("FollowBackUser", baseUrl + "default.aspx?" + "ReturnUrl=ViewUserProfile.aspx?UserId=" + actuser.Id);
                    string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                    SnovaUtil.SendEmail(template.Subject, body, touser.EmailAddress);
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "is following" + " " + touser.FirstName + " " + touser.LastName + " "+"in Huntable";
                    socialManager.ShareOnFacebook(loginuserid, msg, "");
                }
                
            }
            LoggingManager.Debug("Exiting FollowUser - UserMessageManager");
        }
        private static void SendEmailToUser(string subject, string message, User toUser, User fromUser)
        {
            LoggingManager.Debug("Entering SendEmailToUser - UserMessageManager");
            var userEmailNotificationData = Common.GetuserEmailPref(toUser.Id);
            if (userEmailNotificationData == null || userEmailNotificationData.WhenUserSendAMessage)
            {
                // Sending the email.
                var template = EmailTemplateManager.GetTemplate(Data.Enums.EmailTemplates.NewUserMessage);
                var valuesList = new Hashtable
                                     {
                                         {"Name", toUser.Name},
                                         {"Actual User Name", fromUser.Name},
                                         {"Actual User Role", fromUser.CurrentPosition},
                                         {"Actual User Message", message.Replace("\n" ,"<br/>")},
                                         {"UserSummary",fromUser.Summary.Replace("\n" ,"<br/>")},
                                         {"Actual User Company", fromUser.CurrentCompany}
                                     };

                string baseUrl = Common.GetApplicationBaseUrl();
                var url = new UrlGenerator().UserUrlGenerator(fromUser.Id);
                string userProfilePath = Path.Combine(baseUrl, url);
                valuesList.Add("Actual User Profile Link", userProfilePath);
                string userProfilePicturePath = Path.Combine(baseUrl, fromUser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                valuesList.Add("User Profile Picture", userProfilePicturePath);
                valuesList.Add("Server Url", baseUrl);
                valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
                string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                SnovaUtil.SendEmail(subject, body, toUser.EmailAddress);
            }
            LoggingManager.Debug("Exiting SendEmailToUser - UserMessageManager");
        }

        private static void SendEndosementEmailToUser(User toUser, User fromUser,string enodrsementBody=null)
        {
            LoggingManager.Debug("Entering SendEndosementEmailToUser - UserMessageManager");
            var userEmailNotificationData = Common.GetuserEmailPref(toUser.Id);
            if (userEmailNotificationData == null || userEmailNotificationData.EndorsementRequest)
            {
                // Sending the email.
                var template = EmailTemplateManager.GetTemplate(Data.Enums.EmailTemplates.EndorsementRequest);
                var valuesList = new Hashtable
                                     {
                                         {"Name", toUser.Name},
                                         {"Requested User Name", fromUser.Name},
                                         {"Requested User Role", fromUser.CurrentPosition},
                                         {"Requested User Location", fromUser.LocationToDisplay},
                                         {"Requested User Company", fromUser.CurrentCompany},
                                         {"Requested User Description", fromUser.Summary},
                                         {"Endorsement Message",enodrsementBody}
                                     };
                string baseUrl = Common.GetApplicationBaseUrl();
                valuesList.Add("Server Url", baseUrl);
                string userProfilePicturePath = Path.Combine(baseUrl, fromUser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                valuesList.Add("Requested User Picture", userProfilePicturePath);
                var url = new UrlGenerator().UserUrlGenerator(fromUser.Id);
                string userProfilePath = Path.Combine(baseUrl, url);
                valuesList.Add("Requested User Profile", userProfilePath);
                valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
                string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                SnovaUtil.SendEmail(template.Subject, body, toUser.EmailAddress);
            }
            LoggingManager.Debug("Exiting SendEndosementEmailToUser - UserMessageManager");
        }


        public int DeleteMessage(huntableEntities context, int messageId)
        {
            LoggingManager.Debug("Entering DeleteMessage - UserMessageManager");
            UserMessage objUserMasssage = context.UserMessages.FirstOrDefault(u => u.Id == messageId);
            if (objUserMasssage != null) objUserMasssage.IsActive = false;
            LoggingManager.Debug("Exiting DeleteMessage - UserMessageManager");
            return context.SaveChanges();
        }
        public int DeleteSentMessage(huntableEntities context, int messageId)
        {
            LoggingManager.Debug("Entering DeleteSentMessage - UserMessageManager");
            UserMessage objUserMasssage = context.UserMessages.FirstOrDefault(u => u.Id == messageId);
            if (objUserMasssage != null) objUserMasssage.SentIsActive = false;
            LoggingManager.Debug("Exiting DeleteSentMessage - UserMessageManager");
            return context.SaveChanges();
        }

        public int DeleteMutipleSentMessages(huntableEntities context, List<int> messageIdstoDelete)
        {
            LoggingManager.Debug("Entering DeleteMutipleSentMessages - UserMessageManager");
            List<UserMessage> lstUserMessage = context.UserMessages.Where(m => messageIdstoDelete.Contains(m.Id)).ToList();
            foreach (var userMessage in lstUserMessage)
            {
                userMessage.SentIsActive = false;
            }
            LoggingManager.Debug("Exiting DeleteMutipleSentMessages - UserMessageManager");
            return context.SaveChanges();
        }

        public int AlterMutipleMessages(huntableEntities context, List<int> messageIdstoDelete, string action)
        {
            LoggingManager.Debug("Entering AlterMutipleMessages - UserMessageManager");
            bool delete = action == "Delete";
            List<UserMessage> lstUserMessage = context.UserMessages.Where(m => messageIdstoDelete.Contains(m.Id)).ToList();
            foreach (var userMessage in lstUserMessage)
            {
                if (delete)
                    userMessage.IsActive = false;
                else
                    userMessage.IsRead = false;
            }
            LoggingManager.Debug("Exiting AlterMutipleMessages - UserMessageManager");
            return context.SaveChanges();
        }
        public int MarkasRead(huntableEntities context, int messageId)
        {
            LoggingManager.Debug("Entering MarkasRead - UserMessageManager");
            UserMessage objUserMasssage = context.UserMessages.FirstOrDefault(u => u.Id == messageId);
            if (objUserMasssage != null) objUserMasssage.IsRead = true;
            LoggingManager.Debug("Exiting MarkasRead - UserMessageManager");
            return context.SaveChanges();
        }

        public List<User> GetAllUserExcludingLoginUser(huntableEntities context, int loggedInUserId)
        {
            return context.Users.Where(u => !u.UserBlockLists1.Any(b => b.UserId == loggedInUserId && b.BlockedUserId == u.Id) && u.Id != loggedInUserId && u.IsVerified == true).ToList();
        }

        public int EndorseUser(huntableEntities context, int userId, int endorsedUserId, string comments)
        {
            LoggingManager.Debug("Entering EndorseUser - UserMessageManager");
            var endorseUser = context.UserEndorseLists.FirstOrDefault(e => e.UserId == userId && e.EndorsedUserId == endorsedUserId);
            if (endorseUser != null)
            {
                endorseUser.Comments = comments;
            }
            else
            {
                endorseUser = new UserEndorseList { UserId = userId, EndorsedUserId = endorsedUserId, Comments = comments };
                context.AddToUserEndorseLists(endorseUser);
            }
            LoggingManager.Debug("Exiting EndorseUser - UserMessageManager");
            return context.SaveChanges();
        }

        public string BlockUser(huntableEntities context, int userId, int blockedUserId)
        {
            LoggingManager.Debug("Entering BlockUser - UserMessageManager");
            string message;
            var blockedUser = context.UserBlockLists.FirstOrDefault(e => e.UserId == userId && e.BlockedUserId == blockedUserId);
         
                if (blockedUser != null)
                {
                    message = "User already blocked";
                }
                else
                {
                    blockedUser = new UserBlockList { UserId = userId, BlockedUserId = blockedUserId };
                    context.AddToUserBlockLists(blockedUser);
                    message = "User blocked successfully";
                }
            
            context.SaveChanges();
            LoggingManager.Debug("Exiting BlockUser - UserMessageManager");
            return message;
        }

        public bool GetUserMessageStatus(huntableEntities context, int loggedInUserId, int sentByUserId)
        {
            return context.UserBlockLists.Any(b => b.UserId == loggedInUserId && b.BlockedUserId == sentByUserId);
        }

        public User GetUserByName(huntableEntities context, string eMail) 
        {
            return context.Users.FirstOrDefault(u => u.FirstName + (u.LastName ?? "") == eMail);
        }

        public void SendAutomaticEmailtouser(int userid,string email,Hashtable valuesList)
        {
            var template = EmailTemplateManager.GetTemplate(Data.Enums.EmailTemplates.AutomaticInvoice);
            string baseUrl = Common.GetApplicationBaseUrl();          
            valuesList.Add("Server Url", baseUrl);
            valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
            string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
            SnovaUtil.SendEmail(template.Subject, body, email, "registration@huntable.co.uk");
        }
        public void SendEmailToPremiumusers(string email, string username)
        {
            var template = EmailTemplateManager.GetTemplate(EmailTemplates.PremiumUser);
            var valuesList = new Hashtable
                                         {
                                             {"Name", username},
                                         };

            string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
            SnovaUtil.SendEmail(template.Subject, body, email, "registration@huntable.co.uk");
        }
        public void NewEmailAddress(int userid)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(x => x.Id == userid);
                var template = EmailTemplateManager.GetTemplate(EmailTemplates.NewEmailAddress);
                string baseUrl = Common.GetApplicationBaseUrl();
               
                DateTime date = DateTime.Now;
                int dformat = date.Day;
                string mforamt = date.ToString("MMM");
                int yformat = date.Year;
                string hformat = date.ToString("HH:mm");

               
                var valuesList = new Hashtable
                                     {
                                         {"Name", user.Name},
                                        
                                         {"Server Url", baseUrl},

                                         {"date1" , dformat},

                                         {"month" , mforamt},

                                         {"year1" , yformat},

                                         {"hours" , hformat}
                                        
                                     };
                string profilePicturePath = Path.Combine(baseUrl, user.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                valuesList.Add("ProfilePicturePath", profilePicturePath);
                string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                SnovaUtil.SendEmail(template.Subject, body, user.EmailAddress);

            }
        }
    }
}
