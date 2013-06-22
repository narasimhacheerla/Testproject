using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Huntable.Data;
using Huntable.Data.Enums;
using System.Collections;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.Business.BatchJobs
{
    public class RememberEmail
    {
        public void Run()
        {
            LoggingManager.Debug("Entering into Remember email");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                try
                {

             
                var emailtemplate = EmailTemplateManager.GetTemplate(EmailTemplates.RememberEmail);
                var date = DateTime.Now.AddDays(-30);
                var lastlogin = DateTime.Now.Date.AddDays(-29);
                var lastlogin1 = DateTime.Now.Date.AddDays(-43);
                var users = context.Users.Where(x => x.LastLoginTime == lastlogin).Select(y => y.EmailAddress).ToList();
                var userdetails = context.Users.Where(x => ((EntityFunctions.TruncateTime(x.LastLoginTime) == lastlogin || EntityFunctions.TruncateTime(x.LastLoginTime) == lastlogin1)) && x.IsCompany == null && x.IsAdmin == null).ToList();
                int newregisteredcompanies = context.Companies.Count(x => x.CreatedDateTime >= lastlogin);
                foreach (var usrdtls in userdetails)
                {
                    const int newjobs = 500;
                    int matchingskill = context.Users.Count(x => x.ExpectedSkill == usrdtls.ExpectedSkill && x.Id != usrdtls.Id);
                    var valuesList = new Hashtable
                                               {
                                                  { "new jobs posted" , newjobs},
                                                  {"name" ,usrdtls.FirstName},
                                                  {"companies" , newregisteredcompanies},
                                                  {"skillmatching" , matchingskill}

                                               };
                    string baseUrl = "https://huntable.co.uk";
                    valuesList.Add("Server Url", baseUrl);
                    var body = SnovaUtil.LoadTemplate(emailtemplate.TemplateText, valuesList);
                    SnovaUtil.SendEmail(emailtemplate.Subject, body, new List<string> { usrdtls.EmailAddress }.ToArray(), null, null);

                }
                }
                catch (Exception exception)
                {
                    LoggingManager.Debug("Exiting from Remember email"+exception);
                    throw;
                }
            }
        }
    }
}
