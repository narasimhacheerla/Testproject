using System;
using System.Linq;
using System.Web.UI;
using Huntable.Data;
using System.Net.Mail;
using System.Net;
using Huntable.Business;
using Huntable.Data.Enums;
using Snovaspace.Util.Mail;
using System.Collections;
using System.IO;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class ForgotPassword : Page
    {
        public SmtpClient Client = new SmtpClient();
        public MailMessage Msg = new MailMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ForgotPassword.aspx");

            LoggingManager.Debug("Exiting Page_Load - ForgotPassword.aspx");

        }

        protected void Button1Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Button1Click - ForgotPassword.aspx");
            string code;
            code = Guid.NewGuid().ToString();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (context.Users.Any(u => u.EmailAddress.ToLower() == txtemail.Text.ToLower()))
                {
                    var user = context.Users.First(u => u.EmailAddress.ToLower() == txtemail.Text.ToLower());
                    var resetPasswordTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.ResetPassword);
                    string applicationBaseUrl = Common.GetApplicationBaseUrl();
                    string resetLink = Path.Combine(applicationBaseUrl, "ResetPassword.aspx?email=" + txtemail.Text + "&code=" + code);
                    var valuesList = new Hashtable
                                         {
                                             {"Server Url", applicationBaseUrl},
                                             {"Name", user.Name},
                                             {"ResetLink", resetLink},
                                             {
                                                 "Dont Want To Receive Email",
                                                 Path.Combine(applicationBaseUrl, "UserEmailNotification.aspx")
                                             }
                                         };
                    string body = SnovaUtil.LoadTemplate(resetPasswordTemplate.TemplateText, valuesList);
                    SnovaUtil.SendEmail(resetPasswordTemplate.Subject, body, txtemail.Text);
                    lblemail.Text = "Email has been sent to mail id";
                    lblemail.ForeColor = System.Drawing.Color.Green;
                    lblemail.Visible = true;
                }
                else
                {
                    lblemail.Text = "Email has not been registered with us";
                    lblemail.Visible = true;
                    lblemail.ForeColor = System.Drawing.Color.Red;
                }


            }
            LoggingManager.Debug("Exiting Button1Click - ForgotPassword.aspx");
        }
        public static void SendEmail(string subject, string body, string toEmail)
        {
            LoggingManager.Debug("Entering SendEmail - ForgotPassword.aspx");
            var msg = new MailMessage();

            string fromUserName = System.Configuration.ConfigurationManager.AppSettings["FromUserName"];
            string userName = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
            string password = System.Configuration.ConfigurationManager.AppSettings["FromEmailPassword"];
            string smtpAddress = System.Configuration.ConfigurationManager.AppSettings["SMTPAddress"];
            int smtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
            bool enableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSsl"]);

            msg.From = new MailAddress(userName, fromUserName);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            msg.To.Add(toEmail);

            var smtp = new SmtpClient(smtpAddress, smtpPort) { EnableSsl = enableSsl, Credentials = new NetworkCredential(userName, password) };
            smtp.Send(msg);
            LoggingManager.Debug("Exiting SendEmail - ForgotPassword.aspx");
        }
    }
}