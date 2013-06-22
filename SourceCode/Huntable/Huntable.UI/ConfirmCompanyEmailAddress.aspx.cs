using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.UI
{
    public partial class ConfirmCompanyEmailAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ConfirmCompanyEmailAddress");

           
            try
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                    {
                        var id = Request.QueryString["Id"];
                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                        {
                            id = Server.UrlDecode(id);
                            var companyId = Convert.ToInt32(id);
                            var company = context.Companies.FirstOrDefault(x=>x.Userid == companyId);
                            var logins = context.Logins.FirstOrDefault(x => x.CompanyId == companyId);
                            if (company != null && (company.IsVerified.HasValue && company.IsVerified.Value))
                            {
                                pnlAlreadyConfirmed.Visible = true;
                            }
                            else
                            {
                                if (company != null) company.IsVerified = true;
                                if (logins != null) logins.IsVerified = true;
                                context.SaveChanges();
                                var welcomeEmailTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.WelcometoHuntable);
                                var applicationBaseUrl = Common.GetApplicationBaseUrl();
                                if (company != null)
                                {
                                    var welcomeEmailValues = new Hashtable { { "Server Url", applicationBaseUrl }, { "Name", company.CompanyName } };
                                    string welcomeEmailBody = SnovaUtil.LoadTemplate(welcomeEmailTemplate.TemplateText, welcomeEmailValues);
                                    SnovaUtil.SendEmail(welcomeEmailTemplate.Subject, welcomeEmailBody, company.CompanyEmail, "registration@huntable.co.uk");
                                }
                                pnlSuccess.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - ConfirmCompanyEmailAddress");
        }
    }
}