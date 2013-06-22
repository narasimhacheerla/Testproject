using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Snovaspace.Util.Logging;

namespace Snovaspace.Util.Mail
{
    public class SnovaUtil
    {
        public static void SendEmail(string subject, string body, string toEmail, string cc = null)
        {
            new Utility().RunAsTask(() => SendEmail(subject, body, new List<string> { toEmail }.ToArray(), null, cc));
        }

        public static void SendEmail(string subject, string body, string[] toEmails, string attachment = null, string cc = null)
        {
          

            try
            {
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

                foreach (var toEmail in toEmails) msg.To.Add(toEmail);
                msg.Bcc.Add("manoj@snovaspace.com");

                if (cc != null) msg.CC.Add(cc);

                if (attachment != null) msg.Attachments.Add(new Attachment(attachment, "application/pdf"));

                var smtp = new SmtpClient(smtpAddress, smtpPort) { EnableSsl = enableSsl, Credentials = new NetworkCredential(userName, password) };
                smtp.Send(msg);

                LoggingManager.Debug("Email Sending Completed");
            }
            catch (Exception exception)
            {
                LoggingManager.Error(exception);
            }
        }

        public static string LoadTemplate(string body, Hashtable data)
        {
            LoggingManager.Debug("Request for loading template: " + body);
            var loadedTemplate = data.Cast<DictionaryEntry>().Aggregate(body, (current, entry) => current.Replace("[[DynamicValue=#" + entry.Key + "#]]", Convert.ToString(entry.Value)));
            return HttpUtility.HtmlDecode(loadedTemplate);
        }
    }
}
