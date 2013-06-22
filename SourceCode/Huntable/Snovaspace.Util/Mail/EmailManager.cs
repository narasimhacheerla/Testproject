using System;
using System.Linq;
using Snovaspace.Util.FileDataStore;
using System.IO;

namespace Snovaspace.Util.Mail
{
    public class EmailManager : IMailManager
    {
        private readonly string _applicationName;

        public EmailManager(string applicationName)
        {
            _applicationName = applicationName;
        }

        public void SendEmail(string subject, string body, string[] toList, string[] attachments = null, string[] ccList = null, string[] bccList = null, bool htmlBody = false, int? emailAccountId = null)
        {
            using (var dataEntities = new FileStoreEntities())
            {
                var email = new ApplicationEmail
                                {
                                    CreatedDate = DateTime.Now,
                                    Subject = subject,
                                    Body = body,
                                    HtmlBody = htmlBody,
                                    ToList = string.Join(";", toList)
                                };
                if (ccList != null && ccList.Length > 0)
                {
                    email.CCList = string.Join(";", ccList);
                }
                if (bccList != null && bccList.Length > 0)
                {
                    email.BCCList = string.Join(";", bccList);
                }
                if (emailAccountId.HasValue)
                {
                    email.ApplicationEmailAccountId = emailAccountId.Value;
                }
                else
                {
                    var defaultEmailAccountId = dataEntities.ApplicationEmailAccounts.Where(a => a.ApplicationDefinition.Name.ToLower() == _applicationName.ToLower()).Select(a => a.Id).FirstOrDefault();
                    if (defaultEmailAccountId > 0)
                    {
                        email.ApplicationEmailAccountId = defaultEmailAccountId;
                    }
                    else
                    {
                        throw new Exception("Default email account is not configured.");
                    }
                }
                if (attachments != null && attachments.Length > 0)
                {
                    foreach (var att in attachments)
                    {
                        var attachment = new ApplicationEmailAttachment
                                             {
                                                 FileName = Path.GetFileNameWithoutExtension(att),
                                                 FileContent = File.ReadAllBytes(att),
                                                 Extension = Path.GetExtension(att)
                                             };
                        email.ApplicationEmailAttachments.Add(attachment);
                    }
                }

                dataEntities.ApplicationEmails.AddObject(email);
                dataEntities.SaveChanges();
            }
        }
    }
}
