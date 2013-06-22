using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snovaspace.Util.Mail
{
    public interface IMailManager
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="subject">Email Subject</param>
        /// <param name="body">Email body</param>
        /// <param name="toList">To Address list</param>
        /// <param name="ccList">cc Address list</param>
        /// <param name="bccList">bcc address list</param>
        /// <param name="htmlBody">indicates whether the body is html or not (default is false)</param>
        /// <param name="emailAccountId">Email account Id (pass null to use default account is exists)</param>
        void SendEmail(string subject, string body, string[] toList, string[] attachments = null, string[] ccList = null, string[] bccList = null, bool htmlBody = false, int? emailAccountId = null);
    }
}
