using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Data.Enums;
using OAuthUtility;
using Snovaspace.Util.CSV;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.Business
{
    public class InvitationManager
    {
        public void UploadContactsFromFileUploadControl(Page page, FileUpload fuInvitationFriends)
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

                    new InvitationManager().InviteEmailFriends(page, contacts,0);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting UploadContactsFromFileUploadControl - InvitationManager");
        }

        public void InviteEmailFriends(Page page, IList<Contact> emailcontacts, int customInvitationId)
        {
            LoggingManager.Debug("Entering InviteEmailFriends - InvitationManager");
            var objInvManager = new InvitationManager();
            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var userId = Common.GetLoggedInUserId(HttpContext.Current.Session);
            if (userId.HasValue)
            {
                var count = objInvManager.SendInvitations(userId.Value, baseUrl, emailcontacts, customInvitationId,false);

                if (count > 0)
                {
                    new Snovaspace.Util.Utility().DisplayMessageWithPostback(page, "Sent " + count + " Invitations.");
                }
                else
                {
                    new Snovaspace.Util.Utility().DisplayMessageWithPostback(page, "No invitations sent. All invited users have joined Huntable.");
                }
            }
            else new Snovaspace.Util.Utility().DisplayMessage(page, "Please login to see these datails.");
            LoggingManager.Debug("Exiting InviteEmailFriends - InvitationManager");
        }


        public void SendEmailInvitation(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                var email = user.EmailAddress;

                var provider = Snovaspace.Util.Utility.GetProviderFromEmailAddress(email);
                if(provider=="other")
                    return;
                var template = EmailTemplateManager.GetTemplate(EmailTemplates.EmailInvitation);
                var subject = template.Subject;
                var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
                var url = baseUrl + "contact-invitepage.aspx?ref=" + userId+"&provider="+provider;

                var valuesList = new Hashtable
                    {
                        {"UserName", user.Name},
                        {"Base Url", baseUrl},
                        {"Follow", url}
                    };
                var body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                SnovaUtil.SendEmail(subject, body, email);
            }
        }

        public int SendInvitations(int userId, string baseUrl, IList<Contact> contacts,int customInvitationId,bool? isComp)
        {
            LoggingManager.Debug("Entering SendInvitations - InvitationManager");
            var count = 0;

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);

                
                foreach (var contact in contacts)
                {
                    var existingRegisteredUser = context.Users.Where(x => x.EmailAddress.ToLower() == contact.Email.ToLower() && x.IsVerified.HasValue && x.IsVerified.Value).FirstOrDefault();

                    if (existingRegisteredUser != null) continue;

                    if (context.Invitations.Any(u => u.EmailAddress.ToLower() == contact.Email.ToLower() && u.UserId == userId))
                    {
                        var inv = context.Invitations.FirstOrDefault( u => u.EmailAddress.ToLower() == contact.Email.ToLower() && u.UserId == userId);
                        ResendInvitation(baseUrl,inv.Id,customInvitationId);
                        count++;
                        continue;
                    }

                    var invitationToSave = new Invitation();
                    context.Invitations.AddObject(invitationToSave);
                    invitationToSave.UserId = userId;
                    invitationToSave.InvitationTypeId = (int)InvitationType.Email;
                    invitationToSave.Name = contact.Name;
                    invitationToSave.UniqueId = contact.UniqueId;
                    invitationToSave.EmailAddress = contact.Email;
                    invitationToSave.TokenId = contact.TokenId;
                    invitationToSave.InvitationSentDateTime = DateTime.Now;
                    invitationToSave.IsJoined = false;
                    if(customInvitationId>0)
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
                        var template = EmailTemplateManager.GetTemplate(EmailTemplates.Invitation);
                        var subject = template.Subject;
                        var url = baseUrl + "Default.aspx?ref=" + invitationToSave.Id;

                        if (customInvitationId > 0)
                        {                          
                            url =(user.IsCompany!=null)? baseUrl + "CustomizedHomepage.aspx?ref=" + invitationToSave.Id+"&isComp=" + 1:
                                baseUrl+"CustomizedHomepage.aspx?ref=" + invitationToSave.Id;
                        }
                        var valuesList = new Hashtable
                                             {
                                                 {"Invited User Name", user.Name},
                                                 {"Invitation Link", url},
                                                 {"Server Url", baseUrl},
                                                 {
                                                     "Dont Want To Receive Email",
                                                     Path.Combine(baseUrl, "UserEmailNotification.aspx")
                                                     }
                                             };
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
            LoggingManager.Debug("Exiting SendInvitations - InvitationManager");
            return count;
        }

        public void ResendInvitation(string baseUrl, int id,int customInvitationId)
        {
            LoggingManager.Debug("Entering ResendInvitation - InvitationManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var inv = context.Invitations.FirstOrDefault(i => i.Id == id);
                var user = context.Users.First(u => u.Id == inv.UserId);
                var objEmailTemplate = new EmailTemplateManager();
                var template = objEmailTemplate.GetTemplate("Invitation");
                if (inv != null)
                {
                    var url = baseUrl + "Default.aspx?ref=" + inv.Id;
                    if (inv.CustomInvitationId.HasValue || customInvitationId != 0)
                        url = (user.IsCompany!=null)?baseUrl + "CustomizedHomepage.aspx?ref=" + inv.Id+"&isComp=" + 1:
                            baseUrl+"CustomizedHomepage.aspx?ref=" + inv.Id;

                    if (inv.InvitationTypeId == (int)InvitationType.Email)
                    {
                        var subject = template.Subject;
                        var valuesList = new Hashtable
                                             {
                                                 {"Invited User Name", user.Name},
                                                 {"Invitation Link", url},
                                                 {"Server Url", baseUrl},
                                                 {
                                                     "Dont Want To Receive Email",
                                                     Path.Combine(baseUrl, "UserEmailNotification.aspx")
                                                     }
                                             };
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
            LoggingManager.Debug("Exiting ResendInvitation - InvitationManager");
        }



        public int SaveInvitation(int userId, InvitationType type, string uid, string name, string imagePath, int customInvitationId,int tokenid)
        {
            LoggingManager.Debug("Entering SaveInvitation - InvitationManager");
            int id;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var typeInt = (int)type;
                var invitationToSave =
                    context.Invitations.FirstOrDefault(
                        i => i.InvitationTypeId == typeInt && i.UserId == userId && i.UniqueId == uid);
                if (invitationToSave != null)
                {
                    invitationToSave.InvitationSentDateTime = DateTime.Now;
                    if (customInvitationId > 0)
                        invitationToSave.CustomInvitationId = customInvitationId;
                }
                else
                {
                    invitationToSave = new Invitation();
                    context.Invitations.AddObject(invitationToSave);
                    invitationToSave.UserId = userId;
                    invitationToSave.Name = name;
                    invitationToSave.UniqueId = uid;
                    invitationToSave.EmailAddress = name;
                    invitationToSave.InvitationTypeId = typeInt;
                    invitationToSave.PhotoPath = imagePath;
                    invitationToSave.InvitationSentDateTime = DateTime.Now;
                    invitationToSave.IsJoined = false;
                    if (customInvitationId > 0)
                        invitationToSave.CustomInvitationId = customInvitationId;
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
                        if (currentUser.LevelOneInvitedCount == 3)
                        {
                            SendEmail(currentUser.Id);
                        }
                    }

                }
                invitationToSave.TokenId = tokenid;
                context.SaveChanges();
                id = invitationToSave.Id;
            }
            LoggingManager.Debug("Exiting SaveInvitation - InvitationManager");
            return id;

        }


        public Invitation UpdateInvitation(int id)
        {
            LoggingManager.Debug("Entering UpdateInvitation - InvitationManager");
            Invitation inv;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                inv = context.Invitations.FirstOrDefault(i => i.Id == id);
                if (inv != null)
                {
                    if (!inv.IsJoined)
                    {
                        inv.IsJoined = true;
                        inv.JoinedDateTime = DateTime.Now;
                    }

                    var userId = inv.UserId;

                    var currentUser = context.Users.FirstOrDefault(u => u.Id == userId);
                    if (currentUser != null)
                    {
                        var level1 = currentUser.LevelOneJoinedCount;
                        currentUser.LevelOneJoinedCount = level1.HasValue ? level1 + 1 : 1;
                        if (currentUser.ReferralId.HasValue)
                        {
                            var parentuser = context.Users.FirstOrDefault(u => u.Id == currentUser.ReferralId);

                            if (parentuser != null)
                            {
                                parentuser.LevelTwoJoinedCount = parentuser.LevelTwoJoinedCount.HasValue
                                                                     ? parentuser.LevelTwoJoinedCount + 1
                                                                     : 1;
                                if (parentuser.ReferralId.HasValue)
                                {
                                    var superparentuser =
                                        context.Users.FirstOrDefault(u => u.Id == parentuser.ReferralId);
                                    if (superparentuser != null)
                                    {
                                        superparentuser.LevelThreeJoinedCount =
                                            superparentuser.LevelThreeJoinedCount.HasValue
                                                ? superparentuser.LevelThreeJoinedCount + 1
                                                : 1;
                                        SendEmail(superparentuser.Id);
                                    }
                                }
                            }
                            if (level1 == 3)
                            {
                                SendEmail(currentUser.Id);
                            }
                        }
                    }
                }

                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting UpdateInvitation - InvitationManager");
            return inv;
        }

        public void SendEmail(int id)
        {
            LoggingManager.Debug("Entering SendEmail - InvitationManager");
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
            LoggingManager.Debug("Exiting SendEmail - InvitationManager");
        }

        public void UpdateUserIdInInvitaion(int invId, int userId)
        {
            LoggingManager.Debug("Entering UpdateUserIdInInvitaion - InvitationManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var inv = context.Invitations.FirstOrDefault(i => i.Id == invId);
                if (inv != null) inv.RegisteredUserID = userId;
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting UpdateUserIdInInvitaion - InvitationManager");
        }

        public void DeleteInvitation(int id)
        {
            LoggingManager.Debug("Entering DeleteInvitation - InvitationManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var inv = context.Invitations.FirstOrDefault(i => i.Id == id);
                context.Invitations.DeleteObject(inv);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting DeleteInvitation - InvitationManager");
        }
        public EmploymentHistory GetUsercurrentrole(int id)
        {
            LoggingManager.Debug("Entering GetUsercurrentrole - InvitationManager");
            EmploymentHistory emp;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                emp = context.EmploymentHistories.Include("MasterCompany")
                    .FirstOrDefault(
                    e => e.IsCurrent && e.UserId == id && !string.IsNullOrEmpty(e.JobTitle));

            }
            LoggingManager.Debug("Exiting GetUsercurrentrole - InvitationManager");
            return emp;
        }
        public List<JobCreditsPurchased> GetPurchases(int userid)
        {

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.JobCreditsPurchaseds.Where(u => u.UserId == userid).ToList();
            }
        }

        public List<UserEndorseList> GetUserEnorsements(int userid)
        {

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return
                    context.UserEndorseLists.Where(x => x.UserId == userid && x.EndorsedDateTime != null&&x.IsDeleted==null).OrderByDescending(u => u.EndorsedDateTime)
                        .ToList();
            }
        }

        public User GetUserDetails(int userId)
        {
            LoggingManager.Debug("Entering GetUserDetails - InvitationManager");
            User user;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                user = context.Users.FirstOrDefault(u => u.Id == userId);
            }
            LoggingManager.Debug("Exiting GetUserDetails - InvitationManager");
            return user;
        }

        public EmploymentHistory Getemploymentdetails(int userid)
        {
            LoggingManager.Debug("Entering Getemploymentdetails - InvitationManager");
            EmploymentHistory user;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                user = context.EmploymentHistories.FirstOrDefault(u => u.UserId == userid);
            }
            LoggingManager.Debug("Exiting Getemploymentdetails - InvitationManager");
            return user;
        }

        public Invoice GetUserInvoice(int userId)
        {
            LoggingManager.Debug("Entering GetUserInvoice - InvitationManager");
            Invoice invoice;
            using (var context = new huntableEntities())
            {


                invoice =
                    context.Invoices.Where(x => x.UserId.Equals(userId)).OrderByDescending(x => x.Id).FirstOrDefault();


            }
            LoggingManager.Debug("Exiting GetUserInvoice - InvitationManager");
            return invoice;
        }

        public List<CustomProfileVisitedHistory1> GetUserProfileVisitedHistory(int userid, int noOfRecords)
        {
            
            LoggingManager.Debug("Entering GetUserProfileVisitedHistory - InvitationManager");
            using (var context = new huntableEntities())
            {
                var cmpusr = new List<CustomProfileVisitedHistory1>();
                var firstOrDefault = context.Users.FirstOrDefault(x => x.Id == userid);
                if (firstOrDefault != null)
                {
                    int? usrid = firstOrDefault.Id;
                    var result =
                        context.UserProfileVisitedHistories.Where(x => x.UserId == usrid && x.VisitorUserId != usrid).Select(x=>x.VisitorUserId).ToList().Distinct();
                    foreach (var res in result)
                    {
                        var viewlist =
                            from a in context.UserProfileVisitedHistories
                            where a.VisitorUserId == res
                            select new
                                {
                                    a.UserId,
                                    a.Date
                                };
                        var viewlist1 = viewlist.OrderByDescending(x => x.Date).Where(x=>x.UserId != usrid).Select(x=>x.UserId).Distinct();
                        foreach (var vwlis in viewlist1)
                        {
                            var cmpur1 = from a in context.Users
                                         join b in context.EmploymentHistories on a.Id equals b.UserId
                                         where a.Id == vwlis && a.Id != usrid
                                         select new
                                             {
                                                 a.FirstName,
                                                 a.Id,
                                                 b.JobTitle,
                                                 b.MasterCompany.Description,
                                                 hid = b.Id
                                                 

                                             };
                        
                            var cmpur = cmpur1.Where(h=>h.JobTitle!=null).ToList().OrderByDescending(x => x.hid).FirstOrDefault();
                            if (cmpur == null) continue;
                            var cmpur2 = new CustomProfileVisitedHistory1
                                {
                                    ID = cmpur.Id,

                                    Name = cmpur.FirstName,
                                    MasterCompany = cmpur.Description,
                                    Jobtitle = cmpur.JobTitle

                                };


                            cmpusr.Add(cmpur2);
                        }
                    }
                }
                if (cmpusr.Count != 0)
                {
                    cmpusr.Reverse();
                    cmpusr.Distinct();
                    return cmpusr.Take(noOfRecords).ToList();
                    
                }
                return null;
                
                //var currentuser =
                //    context.UserProfileVisitedHistories.Where(x => x.UserId == userid && x.VisitorUserId != userid).OrderByDescending(
                //        x => x.Id).FirstOrDefault();
                //var userId = (currentuser != null) ? currentuser.VisitorUserId : userid;
                //var result = from visitorid in context.UserProfileVisitedHistories
                //              join id in context.Users on visitorid.UserId equals id.Id
                //              where visitorid.VisitorUserId == userId && visitorid.UserId != userid && visitorid.UserId != userId
                //              select new 
                //                  {
                //                      id.Id

                //                  };
                //IList<int> ili = new List<int>();
                //IList<object> resu = new List<object>();
                //foreach (var res in result)
                //{
                //    int p = res.Id;
                //    int iln;
                //    var il = context.EmploymentHistories.FirstOrDefault(x => x.UserId == p);
                    
                //    if(il != null)
                //    {
                //         iln = il.Id;
                //         ili.Add(iln);
                //    }
                   
                   
                //}
                //foreach (int ilis in ili)
                //{
                //   var  resul = from emphis in context.EmploymentHistories
                //               join usr in context.Users on emphis.UserId equals usr.Id
                //               where emphis.Id == ilis
                //               select new
                //               {
                //                   usr.FirstName,
                //                   emphis.JobTitle,
                //                   emphis.MasterCompany.Description,
                //                   usr.Id
                //               };
                //   foreach (object rsl in resul)
                //   {
                //       resu.Add(rsl);
                //   }
                //}
                
                              //join empid in context.EmploymentHistories on id.Id equals empid.UserId
                              //where visitorid.VisitorUserId == userId && visitorid.UserId != userid && visitorid.UserId != userId && empid.IsCurrent
                              //orderby visitorid.Date descending
                              //select new
                              //        {
                              //            id.FirstName,
                              //            empid.JobTitle,
                              //            empid.MasterCompany.Description,
                              //            id.Id
                              //        }).Distinct().Take(noOfRecords);
                LoggingManager.Debug("Exiting GetUserProfileVisitedHistory - InvitationManager");

                 //return   resu.Distinct().Take(3).ToList();
                 //return   resu.Select(
                 //       x =>
                 //      new CustomProfileVisitedHistory(x.FirstName, x.JobTitle, x.Description,
                 //                                       x.Id)).Distinct().ToList();

            }
        }

        public List<FeaturedUserCompany> GetFeaturedCompanies(int userId,string letterfilter=null)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.FeaturedUserCompanies
                    .Include("Mastercompany")
                    .Where(j => j.UserId == userId).ToList();
            }
        }

        public List<User> GetLevelOneConnects(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.Users.Where(u => u.ReferralId == userId && u.IsPremiumAccount == true).ToList();
            }
        }

        public List<User> GetUserFriends(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.Users.Where(u => u.ReferralId == userId).ToList();
            }
        }



        public List<CompanyPortfolio> GetCompanyImages(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.CompanyPortfolios.Where(x => x.CompanyId == userId).ToList();
            }

        }


        public List<User> GetFriendsInvitaions(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return
                    context.Users.Where(u => u.ReferralId == userId && u.IsPremiumAccount == true).OrderByDescending
                        (
                            u => u.LevelOnePremiumCount).ToList();
            }
        }

        public List<Invitation> GetInvitationList(int userId)
        {
            var context = huntableEntities.GetEntitiesWithNoLock();
            return context.Invitations.Where(u => u.UserId == userId).ToList();
        }

        public List<Invitation> GetJoinedList(int userId)
        {
            var context = huntableEntities.GetEntitiesWithNoLock();
            return context.Invitations.Where(u => u.UserId == userId && u.IsJoined).ToList();
        }

        public List<Invitation> GetNotJoinedList(int userId)
        {
            var context = huntableEntities.GetEntitiesWithNoLock();
            return context.Invitations.Where(u => u.UserId == userId && u.IsJoined == false).ToList();
        }

        public List<Invitation> GetInvitationList(int userId, InvitationType type)
        {
            LoggingManager.Debug("Entering GetInvitationList - InvitationManager");
            var context = huntableEntities.GetEntitiesWithNoLock();

            var typeInt = (int)type;
            LoggingManager.Debug("Exiting GetInvitationList - InvitationManager");
            return context.Invitations.Where(u => u.UserId == userId && u.InvitationTypeId == typeInt).ToList();
        }

        public List<User> GetTopRecommendators(int userid, int count)
        {
            var context = huntableEntities.GetEntitiesWithNoLock();

            return
                context.Users.Where(u => u.ReferralId == userid).OrderByDescending(u => u.LevelOneInvitedCount).
                    Take(count).ToList();
        }

        public DataTable GetYourFriendsInvitationsCount()
        {
            LoggingManager.Debug("Entering GetYourFriendsInvitationsCount - InvitationManager");
            var userId = Common.GetLoggedInUserId(HttpContext.Current.Session);
            var dt = new DataTable();
            dt.Columns.Add("count", typeof(int));
            dt.Columns.Add("url", typeof(string));
            dt.Columns.Add("userId", typeof(string));

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var currentUser = context.Users.FirstOrDefault(u => u.Id == userId);
                if (currentUser != null)
                {
                    List<User> friendsList = context.Users.Where(u => u.ReferralId == userId).Take(4).ToList();
                    foreach (User u in friendsList)
                    {
                        DataRow dr = dt.NewRow();
                        dr["url"] = u.UserProfilePictureDisplayUrl;
                        dr["count"] = (u.LevelOneInvitedCount ?? 0) +
                                      (u.LevelTwoInvitedCount ?? 0) +
                                      (u.LevelThreeInvitedCount ?? 0);
                        dr["userId"] = u.Id;
                        dt.Rows.Add(dr);

                    }
                }
            }
            LoggingManager.Debug("Exiting GetYourFriendsInvitationsCount - InvitationManager");
            return dt;
        }


        public string GetFriendsInvitationsAsHtmlString(int userId)
        {
            LoggingManager.Debug("Entering GetFriendsInvitationsAsHtmlString - InvitationManager");
            string body = string.Empty;
            var friendsInvitationsTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.FriendsInvitations);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var currentUser = context.Users.FirstOrDefault(u => u.Id == userId);
                if (currentUser != null)
                {
                    string baseUrl = Common.GetApplicationBaseUrl();
                    List<User> friendsList = context.Users.Where(u => u.ReferralId == userId).Take(4).ToList();
                    foreach (User u in friendsList)
                    {
                        var count = (u.LevelOneInvitedCount ?? 0) +
                                     (u.LevelTwoInvitedCount ?? 0) +
                                     (u.LevelThreeInvitedCount ?? 0);
                        var valuesList = new Hashtable
                                         {
                                             {"ImageUrl", Path.Combine(baseUrl, u.UserProfilePictureDisplayUrl.Replace("~/", string.Empty))},
                                             {"InvitationCount", count}
                                         };

                        body = body + SnovaUtil.LoadTemplate(friendsInvitationsTemplate.TemplateText, valuesList);
                    }
                }
            }
            LoggingManager.Debug("Exiting GetFriendsInvitationsAsHtmlString - InvitationManager");
            return body;
        }

    }

    public class CustomProfileVisitedHistory1
    {
       

        public string Name;
        public string Jobtitle;
        public string MasterCompany;
        public int ID;
    }
}
