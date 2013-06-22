using System;
using System.Linq;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using System.Threading;
using System.Text;

using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace Huntable.UI
{
    public partial class  HeaderAfterLoggingInWithoutLinks : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HeaderAfterLoggingInWithoutLinks.ascx");
            try
            {
                bool userLoggedIn = Common.IsLoggedIn();
                var fullName = Page.GetType().FullName;

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    if (userLoggedIn)
                    {
                        var user = Common.GetLoggedInUser(context);
                        if (user.IsCompany == true)
                        {
                            var comp = context.Companies.FirstOrDefault(x => x.Userid == user.Id);
                            _profile.HRef = new UrlGenerator().CompanyUrlGenerator(comp.Id);
                        }
                        else
                            _profile.HRef = "ViewUserProfile.aspx";
                
                        lblUserName.Text = user.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - HeaderAfterLoggingInWithoutLinks.ascx");
        }

        
        public void Flashmessage(string message)
        {
            LoggingManager.Debug("Entering Flashmessage - HeaderAfterLoggingInWithoutLinks.ascx");
            popupmessage2.Visible = true;
            string strScript = "HideCtrl('" + popupmessage2.ClientID + "','5000')";
            lblUserName.Text = message;
            
            Page.ClientScript.RegisterStartupScript(this.GetType(),Guid.NewGuid().ToString(),strScript,true);
            LoggingManager.Debug("Exiting Flashmessage - HeaderAfterLoggingInWithoutLinks.ascx");
        }
        public static void SendEmail(string subject, string body, params string[] toEmails)
        {
            LoggingManager.Debug("Entering SendEmail - HeaderAfterLoggingInWithoutLinks.ascx");
            var msg = new MailMessage();
            string userName = ConfigurationManager.AppSettings["FromEmail"];
            string password = ConfigurationManager.AppSettings["FromEmailPassword"];
            msg.From = new MailAddress(userName, ConfigurationManager.AppSettings["FromUserName"]);
            foreach (string toEmail in toEmails) msg.To.Add(toEmail);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            string smtpAddress = ConfigurationManager.AppSettings["SMTPAddress"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            var smtp = new SmtpClient(smtpAddress, smtpPort) { EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]), Credentials = new NetworkCredential(userName, password) };
            smtp.Send(msg);
            LoggingManager.Debug("Exiting SendEmail - HeaderAfterLoggingInWithoutLinks.ascx");
        }

        protected void lnkJobsClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkJobsClick - HeaderAfterLoggingInWithoutLinks.ascx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            var result = jobManager.GetUserDetails(loggedInUserId.Value);

            if (result.IsPremiumAccount == false || result.IsPremiumAccount == null)
            {
                Server.Transfer("WhatIsHuntableUpgrade.aspx");
            }
            else if (result.CreditsLeft == null || result.CreditsLeft == 0)
            {
                Server.Transfer("BuyCredit.aspx");
            }
            else
            {
                Response.Redirect("PostJob.aspx");
            }
            LoggingManager.Debug("Exiting lnkJobsClick - HeaderAfterLoggingInWithoutLinks.ascx");
        }

        protected void LogOutClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering LogOutClick - HeaderAfterLoggingInWithoutLinks.ascx");
            Session.Abandon();
            Session.Clear();
            Response.Redirect(PageNames.Home);
            LoggingManager.Debug("Exiting LogOutClick - HeaderAfterLoggingInWithoutLinks.ascx");
        }

        public void Flasmessage()
        {
            LoggingManager.Debug("Entering Flashmessage - HeaderAfterLoggingInWithoutLinks.ascx");
            popupmessage2.Visible = true;
            string strScript = "HideCtrl('" + popupmessage2.ClientID + "','15000')";

            Page.ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              strScript,
              true);
            LoggingManager.Debug("Exiting Flashmessage - HeaderAfterLoggingInWithoutLinks.ascx");
        }
        protected void PictureUploadClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering PictureUploadClick - HeaderAfterLoggingInWithoutLinks.ascx");

            LoggingManager.Debug("Exiting PictureUploadClick - HeaderAfterLoggingInWithoutLinks.ascx");
            Response.Redirect("PictureUpload.aspx", false);
        }
    }
}