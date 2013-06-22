using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    using System.Collections;
    using System.IO;

    using Snovaspace.Util.Mail;
    using System.Configuration;

    public class ReSendInvitations : BatchJobBase
    {
        public ReSendInvitations()
            : base("ReSendInvitations")
        {

        }

        public override void Run(huntableEntities huntableEntities)
        {
            LoggingManager.Debug("Entering into the resend invitations");
            var invitationReminderEmailDays = ConfigurationManager.AppSettings["InvitationReminderEmailDays"];
            if (string.IsNullOrWhiteSpace(invitationReminderEmailDays))
            {
                throw new InvalidOperationException("invitationReminderEmailDays key is missing from web.config");
            }
            int invitationReminderEmailDaysCount = Convert.ToInt32(invitationReminderEmailDays);
            DateTime invitationReminderEmailDate = DateTime.Now.Date.AddDays(-invitationReminderEmailDaysCount);
            List<Invitation> allInvitationRemindersList = huntableEntities.Invitations.Where(i => i.JoinedDateTime == null && i.ReminderEmailSentDate == null && (EntityFunctions.TruncateTime(i.InvitationSentDateTime) == invitationReminderEmailDate)).ToList();

            // For each user, 
            foreach (Invitation initation in allInvitationRemindersList)
            {
                try
                {

           
                var user = huntableEntities.Users.First(u => u.Id == initation.UserId);

                var template = EmailTemplateManager.GetTemplate(EmailTemplates.Invitation);
                var subject = template.Subject;

                string baseUrl = ConfigurationManager.AppSettings["WebsiteURL"];
                var url = baseUrl + "Default.aspx?ref=" + initation.Id;

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
                SnovaUtil.SendEmail(subject, body, new List<string> { user.EmailAddress }.ToArray(), null, null);
                    LoggingManager.Debug("email has been sent to following emial "+user.EmailAddress);
                Invitation invitation = huntableEntities.Invitations.FirstOrDefault(i => i.Id == initation.Id);
                if (invitation != null) invitation.ReminderEmailSentDate = DateTime.Now;

                huntableEntities.SaveChanges();
                }
                catch (Exception exception)
                {
                    LoggingManager.Debug("exiting from resend invitations with exception" +exception);
                    throw;
                }
            }
        }
    }
}