using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetOpenAuth.ApplicationBlock;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.CSV;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;
using OAuthUtility;
using Contact = OAuthUtility.Contact;

namespace Huntable.Business
{
    public class CompanyMessageManager
    {
        public static void SendInvitation(int invitedUserId, int loginUserId)
        {
            LoggingManager.Debug("Entering SendInvitations - CompanyMessageManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == invitedUserId);
                var loguser = context.Users.FirstOrDefault(x => x.Id == loginUserId && x.IsCompany == true);
                var company = context.Companies.FirstOrDefault(x => x.Userid == loguser.Id);

                var companyFollowers = new UserManager().GetUserfollower((int) loguser.Id);
                var companyFollowing = new UserManager().GetUserFollowings((int) loguser.Id);
                var invitationToSave = new Invitation
                    {
                        UserId = loginUserId,
                        InvitationSentDateTime = DateTime.Now,
                        IsJoined = false,
                        InvitedId = invitedUserId
                    };
                context.Invitations.AddObject(invitationToSave);
                context.SaveChanges();
                var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
                string followLink = Path.Combine(baseUrl,
                                                 "CompanyFollowConfirmation.aspx?Cmpid=" + company.Id + "&UserId=" +
                                                 invitedUserId);
                var url = baseUrl + "CustomizedHomepage.aspx?ref=" + invitationToSave.Id;
                var template = EmailTemplateManager.GetTemplate(EmailTemplates.CompanyInvitation);
                var subject = template.Subject;
                var valuesList = new Hashtable
                    {
                        {"UserName", user.Name},
                        {"ComapanyShortDescription", company.CompanyHeading},
                        {"ComapanyDescription", company.CompanyDescription.Replace("\n", "<br/>")},
                        {"Base Url", baseUrl},
                        {"ComapanyName", company.CompanyName},
                        {"ComapanyFollowingCount", companyFollowing.Count},
                        {"ComapanyFollowersCount", companyFollowers.Count},
                        {"Follow", followLink}
                    };
                string userProfilePicturePath = Path.Combine(baseUrl,
                                                             loguser.UserProfilePictureDisplayUrl.Replace("~/",
                                                                                                          string
                                                                                                              .Empty));
                valuesList.Add("ComapanyLogo", userProfilePicturePath);

                string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                SnovaUtil.SendEmail(subject, body, user.EmailAddress);
               var socialManager = new SocialShareManager();
                var msg = "[UserName]" + " " + "sent invitation successfully";
                    socialManager.ShareOnFacebook(loginUserId, msg, "");
                LoggingManager.Debug("Exiting SendInvitations - CompanyMessageManager");
            }
        }


        public void SendInvitationByEmail(Page page, int loginuserid, IList<Contact> emailcontacts,bool? isCompany)
        {
            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var userId = Common.GetLoggedInUserId(HttpContext.Current.Session);
            if (userId.HasValue)
            {
                var count = SendInvitations(userId.Value, baseUrl, emailcontacts,0,isCompany);
                new Snovaspace.Util.Utility().DisplayMessageWithPostback(page, "Sent " + count + " Invitations");
            }
            else new Snovaspace.Util.Utility().DisplayMessage(page, "Please login to see these datails.");
        }

        public static int SendInvitations(int userId, string baseUrl, IList<Contact> contacts, int customInvitationId,bool? isCompany)
        {
            LoggingManager.Debug("Entering SendInvitations - CompanyMessageManager");
            var count = 0;

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                var loguser = context.Users.FirstOrDefault(x => x.Id == userId && x.IsCompany == true);
                var company = context.Companies.FirstOrDefault(x => x.Userid == loguser.Id);
                var companyFollowers = new UserManager().GetUserfollower((int)loguser.Id);
                var companyFollowing = new UserManager().GetUserFollowings((int)loguser.Id);
                foreach (var contact in contacts)
                {
                    if (
                        context.Invitations.Any(
                            u => u.EmailAddress.ToLower() == contact.Email.ToLower() && u.UserId == userId))
                    {
                        var inv =
                            context.Invitations.FirstOrDefault(
                                u => u.EmailAddress.ToLower() == contact.Email.ToLower() && u.UserId == userId);
                        ResendInvitation(baseUrl, inv.Id, customInvitationId);
                        count++;
                        continue;
                    }
                    var invitationToSave = new Invitation();
                    context.Invitations.AddObject(invitationToSave);
                    invitationToSave.UserId = userId;
                    invitationToSave.InvitationTypeId = (int) InvitationType.Email;
                    invitationToSave.Name = contact.Name;                   
                    invitationToSave.EmailAddress = contact.Email;                    
                    invitationToSave.InvitationSentDateTime = DateTime.Now;
                    invitationToSave.IsJoined = false;
                    if (customInvitationId > 0)
                    {
                        invitationToSave.CustomInvitationId = customInvitationId;
                    }
                    var currentUser = context.Users.FirstOrDefault(u => u.Id == userId);

                    if (currentUser != null)
                    {
                        var level1 = currentUser.LevelOneInvitedCount;
                        currentUser.LevelOneInvitedCount = level1.HasValue ? level1 + 1 : 1;
                        if (currentUser.ReferralId.HasValue)
                        {
                            var parentuser = context.Users.FirstOrDefault(u => u.Id == currentUser.ReferralId);

                            if (parentuser != null)
                            {
                                parentuser.LevelTwoInvitedCount = parentuser.LevelTwoInvitedCount.HasValue
                                                                      ? parentuser.LevelTwoInvitedCount + 1
                                                                      : 1;
                                if (parentuser.ReferralId.HasValue)
                                {
                                    var superparentuser =
                                        context.Users.FirstOrDefault(u => u.Id == parentuser.ReferralId);
                                    if (superparentuser != null)
                                        superparentuser.LevelThreeInvitedCount =
                                            superparentuser.LevelThreeInvitedCount.HasValue
                                                ? superparentuser.LevelThreeInvitedCount + 1
                                                : 1;
                                }
                            }
                        }


                        context.SaveChanges();
                        var template = EmailTemplateManager.GetTemplate(EmailTemplates.CompanyInvitation);
                        var subject = template.Subject;
                        var url = (isCompany != null)
                                      ? baseUrl + "Default.aspx?ref=" + invitationToSave.Id + "&isComp=" + 1
                                      : "Default.aspx?ref=" + invitationToSave.Id;
                      

                        if (customInvitationId > 0)
                        {
                             url = (isCompany != null)
                                         ? baseUrl + "CustomizedHomepage.aspx?ref=" + invitationToSave.Id + "&isComp=" + 1
                                         : baseUrl+"CustomizedHomepage.aspx?ref=" + invitationToSave.Id;
                            //url = baseUrl + "CustomizedHomepage.aspx?ref=" + invitationToSave.Id;
                        }
                        var valuesList = new Hashtable
                            {
                                {"UserName",contact.Email},
                                {"ComapanyShortDescription", company.CompanyHeading},
                                {"ComapanyDescription", company.CompanyDescription.Replace("\n", "<br/>")},
                                {"Base Url", baseUrl},
                                {"ComapanyName", company.CompanyName},
                                {"ComapanyFollowingCount", companyFollowing.Count},
                                {"ComapanyFollowersCount", companyFollowers.Count},
                                {"Follow", url}
                            };
                        string userProfilePicturePath = Path.Combine(baseUrl,
                                                            loguser.UserProfilePictureDisplayUrl.Replace("~/",
                                                                                                         string
                                                                                                             .Empty));
                        valuesList.Add("ComapanyLogo", userProfilePicturePath);
                        string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                        SnovaUtil.SendEmail(subject, body, contact.Email);

                        count++;
                        if (currentUser.LevelOneInvitedCount == 3)
                        {
                            SendEmail(currentUser.Id);
                        }
                    }
                }

            }
            LoggingManager.Debug("Exiting SendInvitations - CompanyMessageManager");
            return count;
        }

        public static void ResendInvitation(string baseUrl, int id, int customInvitationId)
        {
            LoggingManager.Debug("Entering ResendInvitation - CompanyMessageManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var inv = context.Invitations.FirstOrDefault(i => i.Id == id);
                var user = context.Users.First(u => u.Id == inv.UserId);
                var loguser = context.Users.FirstOrDefault(x => x.Id == inv.UserId && x.IsCompany == true);
                var company = context.Companies.FirstOrDefault(x => x.Userid == loguser.Id);
                var companyFollowers = new UserManager().GetUserfollower((int)loguser.Id);
                var companyFollowing = new UserManager().GetUserFollowings((int)loguser.Id);
                var objEmailTemplate = new EmailTemplateManager();
                var template = EmailTemplateManager.GetTemplate(EmailTemplates.CompanyInvitation);
                if (inv != null)
                {
                    var url = baseUrl + "Default.aspx?ref=" + inv.Id;
                    if (inv.CustomInvitationId.HasValue || customInvitationId != 0)
                    {
                        url = (loguser.IsCompany!=null)?baseUrl + "CustomizedHomepage.aspx?ref=" + inv.Id+ "&isComp=" + 1:
                            baseUrl+"CustomizedHomepage.aspx?ref=" + inv.Id; 
                    }
                    if (inv.InvitationTypeId == (int) InvitationType.Email)
                    {
                        var subject = template.Subject;
                        var valuesList = new Hashtable
                            {
                                 {"UserName", user.Name},
                                {"ComapanyShortDescription", company.CompanyHeading},
                                {"ComapanyDescription", company.CompanyDescription.Replace("\n", "<br/>")},
                                {"Base Url", baseUrl},
                                {"ComapanyName", company.CompanyName},
                                {"ComapanyFollowingCount", companyFollowing.Count},
                                {"ComapanyFollowersCount", companyFollowers.Count},
                                {"Follow", url}
                            };
                        string userProfilePicturePath = Path.Combine(baseUrl,
                                                            loguser.UserProfilePictureDisplayUrl.Replace("~/",
                                                                                                         string
                                                                                                             .Empty));
                        valuesList.Add("ComapanyLogo", userProfilePicturePath);
                        string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                        SnovaUtil.SendEmail(subject, body, inv.EmailAddress);
                    }
                    if (customInvitationId > 0)
                    {
                        inv.CustomInvitationId = customInvitationId;
                    }
                    inv.InvitationSentDateTime = DateTime.Now;
                }

                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting ResendInvitation - CompanyMessageManager");
        }

        public static void SendEmail(int id)
        {
            LoggingManager.Debug("Entering SendEmail - CompanyMessageManager");
            using (var context = new huntableEntities())
            {

                var thirdUser = context.Users.FirstOrDefault(u => u.Id == id);
                if (thirdUser != null)
                {


                    var tlevel1Count = thirdUser.LevelOneInvitedCount.HasValue
                                           ? thirdUser.LevelOneInvitedCount
                                           : 0;
                    var tlevel2Count = thirdUser.LevelTwoInvitedCount.HasValue
                                           ? thirdUser.LevelTwoInvitedCount
                                           : 0;
                    var tlevel3Count = thirdUser.LevelThreeInvitedCount.HasValue
                                           ? thirdUser.LevelThreeInvitedCount
                                           : 0;
                    int tcount = Convert.ToInt32(tlevel1Count + tlevel2Count + tlevel3Count);
                    var tlevel1JCount = thirdUser.LevelOnePremiumCount.HasValue
                                            ? thirdUser.LevelOnePremiumCount
                                            : 0;
                    var tlevel2JCount = thirdUser.LevelTwoPremiumCount.HasValue
                                            ? thirdUser.LevelTwoPremiumCount
                                            : 0;
                    var tlevel3JCount = thirdUser.LevelThreePremiumCount.HasValue
                                            ? thirdUser.LevelThreePremiumCount
                                            : 0;
                    int TJcount = Convert.ToInt32(tlevel1JCount + tlevel2JCount + tlevel3JCount);
                    var upgradeTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.Upgrade);
                    var upgradeList = new Hashtable
                                      {
                                          {"Name", thirdUser.Name},
                                          {"FirstInvited", tlevel1Count},
                                          {"FirstJoined", tlevel1JCount},
                                          {"FirstEarnings", tlevel1JCount*5},
                                          {"SecondInvited", tlevel2Count},
                                          {"SecondJoined", tlevel2JCount},
                                          {"SecondEarnings", tlevel2JCount*1},
                                          {"ThirdInvited", tlevel3Count},
                                          {"ThirdJoined", tlevel3JCount},
                                          {"ThirdEarnings", tlevel3JCount*0.5}
                                      };
                    string upgradeBody = SnovaUtil.LoadTemplate(upgradeTemplate.TemplateText, upgradeList);
                    if (thirdUser.IsPremiumAccount == null)
                    {
                        SnovaUtil.SendEmail(upgradeTemplate.Subject, upgradeBody, thirdUser.EmailAddress);
                    }
                }
            }
            LoggingManager.Debug("Exiting SendEmail - CompanyMessageManager");
        }
        public void UploadContactsFromFileUploadControl(Page page, FileUpload fuInvitationFriends ,int loginuserid)
        {
            LoggingManager.Debug("Entering UploadContactsFromFileUploadControl - InvitationManager");
            try
            {
                var fileStoreService = new FileStoreService();

                int? fileId = fileStoreService.LoadFileFromFileUpload("InviteFriends", fuInvitationFriends);

                if (fileId.HasValue)
                {
                    Stream fileStream = fileStoreService.GetFileFromId(fileId.Value);

                    IList<Contact> contacts = new List<Contact>();

                    using (var reader = new CsvReader(fileStream))
                    {
                        var actualTable = reader.CreateDataTable(true);

                        foreach (DataRow row in actualTable.Rows)
                        {
                            if (row[1] as string != null)
                            {
                                contacts.Add(new Contact { Name = row[0] as string, Email = row[1] as string });
                            }
                        }
                    }

                  SendInvitationByEmail(page,  loginuserid ,contacts,true);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting UploadContactsFromFileUploadControl - InvitationManager");
        }
 

    }
}
