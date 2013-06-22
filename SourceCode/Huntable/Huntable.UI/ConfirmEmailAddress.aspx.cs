using System;
using System.Linq;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;
using Huntable.Business;
using Huntable.Data.Enums;
using System.Collections;

namespace Huntable.UI
{
    public partial class ConfirmEmailAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ConfirmEmailAddress");

            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                    {
                        var id = Request.QueryString["Id"];
                      
                            id = Server.UrlDecode(id);
                            var userId = Convert.ToInt32(id);
                            var user = context.Users.FirstOrDefault(u =>u.Id == userId);
                            var logins = context.Logins.FirstOrDefault(x =>x.UserId==userId);
                            if (user != null && (user.IsVerified.HasValue && user.IsVerified.Value))
                            {
                                pnlAlreadyConfirmed.Visible = true;
                            }
                            else
                            {
                                if (user != null) user.IsVerified = true;
                                if (logins != null) logins.IsVerified = true;
                                context.SaveChanges();
                                var welcomeEmailTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.WelcometoHuntable);
                                var applicationBaseUrl = Common.GetApplicationBaseUrl();
                                if (user != null)
                                {
                                    var welcomeEmailValues = new Hashtable { { "Server Url", applicationBaseUrl }, { "Name", user.Name } };
                                    string welcomeEmailBody = SnovaUtil.LoadTemplate(welcomeEmailTemplate.TemplateText, welcomeEmailValues);
                                    SnovaUtil.SendEmail(welcomeEmailTemplate.Subject, welcomeEmailBody, user.EmailAddress, "registration@huntable.co.uk");
                                }
                                pnlSuccess.Visible = true;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                    {
                        var code = Request.QueryString["code"];
                        var guidcode=new Guid(code);
                        var user = context.Users.FirstOrDefault(x => x.Code == guidcode);
                      var login = context.Logins.FirstOrDefault(x => x.UserId == user.Id||x.CompanyId==user.Id);
                        if (user != null && (user.IsVerified.HasValue && user.IsVerified.Value))
                        {
                            
                             user.Password = user.TempPwd;
                            if (login != null) login.Password = user.TempPwd;
                            divPassword.Visible = true;
                            context.SaveChanges();
                        }
                        else
                        {
                            divalreadyConfirmed.Visible = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - ConfirmEmailAddress");
        }
    }
}