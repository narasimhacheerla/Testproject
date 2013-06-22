using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.Business.BatchJobs
{
    public class EmailInvites
    {
        public void Run()
        {
            LoggingManager.Debug("Entering Into Email Invites");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                try
                {
                    var template = EmailTemplateManager.GetTemplate(EmailTemplates.EmailInvitation);
                    var subject = template.Subject;                  
                     const string baseUrl = "https://huntable.co.uk/";
                    var date = DateTime.Now.Date.AddDays(-5);
                    var count = context.Users.FirstOrDefault(x => (x.IsDeleted == null || x.IsDeleted == false) && EntityFunctions.TruncateTime(x.CreatedDateTime) == date);
                    var users = context.Users.Where(x => (x.IsDeleted == null || x.IsDeleted == false)&&EntityFunctions.TruncateTime(x.CreatedDateTime) == date ).ToList();
                    foreach (var user in users)
                    {
                        var email = user.EmailAddress;

                        var provider = Snovaspace.Util.Utility.GetProviderFromEmailAddress(email);
                        if (provider == "other")
                            continue;

                        var url = baseUrl + "contact-invitepage.aspx?ref=" + user.Id + "&provider=" + provider;

                        var valuesList = new Hashtable
                            {
                                {"UserName", user.Name},
                                {"Base Url", baseUrl},
                                {"Follow", url}
                            };
                        var body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                        SnovaUtil.SendEmail(subject, body, new List<string> {email}.ToArray(), null, null);
                    }
                }
                catch (Exception exception)
                {
                    LoggingManager.Debug("Encountered Exception in Email invites"+exception);
                    throw;
                }



            }
        }
    }
}
