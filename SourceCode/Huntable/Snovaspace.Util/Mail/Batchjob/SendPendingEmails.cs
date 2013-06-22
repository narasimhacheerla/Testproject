using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snovaspace.Util.FileDataStore;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Snovaspace.Util.Mail.Batchjob
{
    public class SendPendingEmailManager
    {
        public static void SendEmails()
        {
            using (FileStoreEntities context = new FileStoreEntities())
            {
                List<int> allApplicationsToSendEmails = context.ApplicationEmails.Where(e => !e.SentSuccessfully.HasValue || e.ResendTriggered == true).Select(e => e.ApplicationEmailAccount.ApplicationDefinition.Id).Distinct().ToList();
                foreach (var appId in allApplicationsToSendEmails)
                {
                    var applicationEmailsToSend = context.ApplicationEmails.Where(e => (e.ApplicationEmailAccount.ApplicationDefinitionId == appId && !e.SentSuccessfully.HasValue) || e.ResendTriggered == true)
                        .ToList();
                    foreach (var emailToSend in applicationEmailsToSend)
                    {
                        try
                        {
                            SendEmail(emailToSend);
                            ApplicationEmailEvent emailEvent = new ApplicationEmailEvent();
                            emailEvent.ApplicationEmailId = emailToSend.Id;
                            emailEvent.Date = DateTime.Now;
                            if (!emailToSend.SentSuccessfully.HasValue || emailToSend.SentSuccessfully == false)
                            {
                                emailEvent.EventId = (int)EEmailEvent.EmailSent;
                                emailToSend.SentSuccessfully = true;
                                emailToSend.SentDate = DateTime.Now;
                            }
                            if (emailToSend.ResendTriggered.HasValue && emailToSend.ResendTriggered.Value)
                            {
                                emailToSend.ResendTriggered = false;
                                emailEvent.EventId = (int)EEmailEvent.EmailResendSuccess;
                                emailEvent.ResendRequestedDate = emailToSend.ResendTriggeredDate;
                            }
                            context.ApplicationEmailEvents.AddObject(emailEvent);
                        }
                        catch (Exception ex)
                        {
                            //emailToSend.ErrorMessage = ex.Message;
                            ApplicationEmailEvent emailEvent = new ApplicationEmailEvent();
                            emailEvent.ApplicationEmailId = emailToSend.Id;
                            emailEvent.Date = DateTime.Now;
                            emailEvent.ErrorMessage = ex.Message;
                            emailEvent.EventId = (int)EEmailEvent.EmailSendingFailed;
                            if (emailToSend.SentSuccessfully != true)
                            {
                                // Resending.
                                emailToSend.SentSuccessfully = false;
                                emailEvent.EventId = (int)EEmailEvent.EmailResendingFailed;                              
                            }
                            context.ApplicationEmailEvents.AddObject(emailEvent);
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        private static void SendEmail(ApplicationEmail email)
        {
            var msg = new MailMessage();

            string fromUserName = email.ApplicationEmailAccount.DisplayName;
            string userName = email.ApplicationEmailAccount.FromUserName;
            string password = email.ApplicationEmailAccount.FromPassword;
            string smtpAddress = email.ApplicationEmailAccount.SMTPAddress;
            int smtpPort = email.ApplicationEmailAccount.SMTPPort.Value;//Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SMTPPort"]);
            bool enableSsl = email.ApplicationEmailAccount.EnableSsl.Value;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSsl"]);

            msg.From = new MailAddress(userName, fromUserName);
            msg.Subject = email.Subject;
            msg.Body = email.Body;
            msg.IsBodyHtml = email.HtmlBody;

            foreach (var toEmail in email.ToList.Split(';')) msg.To.Add(toEmail);
            if (!string.IsNullOrEmpty(email.CCList ))
            {
                foreach (var ccEmail in email.CCList.Split(';')) msg.To.Add(ccEmail);
            }
            if (!string.IsNullOrEmpty(email.BCCList ))
            {
                foreach (var bccEmail in email.BCCList.Split(';')) msg.To.Add(bccEmail);
            }

            foreach (var attachment in email.ApplicationEmailAttachments)
            {
                Attachment att = new Attachment(new MemoryStream(attachment.FileContent), string.Format("{0}.{1}", attachment.FileName, attachment.Extension));
            }

            using (var smtp = new SmtpClient(smtpAddress, smtpPort) { EnableSsl = enableSsl, Credentials = new NetworkCredential(userName, password) })
            {
                smtp.Send(msg);
            }
        }
    }

    public enum EEmailEvent
    {
        EmailSent = 1,
        EmailResendSuccess = 2,
        EmailSendingFailed = 3,
        EmailResendingFailed = 4
    }
}
