using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;
using System.Collections;
using Huntable.Data;

namespace Huntable.UI
{
    public partial class testingemailtemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - TopReferrers.aspx");
            LoggingManager.Debug("Exiting Page_Load - TopReferrers.aspx");

        }
        protected void TestEmailTemplate(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering TestEmailTemplate - TopReferrers.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var emailtemplate = EmailTemplateManager.GetTemplate(EmailTemplates.RememberEmail);
                var date = DateTime.Now.AddDays(-30);
                var lastlogin = DateTime.Now.AddDays(-28);
                var users = context.Users.Where(x => x.LastLoginTime >= lastlogin).Select(y => y.EmailAddress).ToList();
                var userdetails = context.Users.Where(x => x.LastLoginTime >= lastlogin && x.IsCompany == null && x.IsAdmin == null).ToList();
                int newregisteredcompanies = context.Companies.Where(x => x.CreatedDateTime >= lastlogin).ToList().Count();
                foreach (var usrdtls in userdetails)
                {
                    int newjobs = 500;
                    int matchingskill = context.Users.Where(x => x.ExpectedSkill == usrdtls.ExpectedSkill && x.Id != usrdtls.Id).ToList().Count();
                    var valuesList = new Hashtable
                                               {
                                                  { "new jobs posted" , newjobs},
                                                  {"name" ,usrdtls.FirstName},
                                                  {"companies" , newregisteredcompanies},
                                                  {"skillmatching" , matchingskill}

                                               };
                    string baseUrl = Common.GetApplicationBaseUrl();
                    valuesList.Add("Server Url", baseUrl);
                    var body = SnovaUtil.LoadTemplate(emailtemplate.TemplateText, valuesList);
                    SnovaUtil.SendEmail(emailtemplate.Subject, body, usrdtls.EmailAddress) ;

                }
                //int newjobs = 500;
                //var valuesList = new Hashtable
                //                               {
                //                                  { "new jobs posted" , newjobs}

                //                               };
                //string baseUrl = Common.GetApplicationBaseUrl();
                //valuesList.Add("Server Url", baseUrl);
                //var body = SnovaUtil.LoadTemplate(emailtemplate.TemplateText, valuesList);
                //string[] emails = users.ToArray();
                //var emails = new[] { "narasimhareddycheerla@gmail.com", "narasimha@snovaspace.com" };
                //SnovaUtil.SendEmail(emailtemplate.Subject, body, emails);
            }
            LoggingManager.Debug("Exiting TestEmailTemplate - TopReferrers.aspx");
        }
    }
}