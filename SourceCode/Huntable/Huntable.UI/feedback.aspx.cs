using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Text;
using Snovaspace.Util.Mail;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - feedback.aspx");


            LoggingManager.Debug("Exiting Page_Load - feedback.aspx");
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnSendMail_Click - feedback.aspx");

            StringBuilder body = new StringBuilder()
            .Append("Subject : ")
            .Append(txtSubject.Text)
            .AppendLine()
            .Append("<br/>")
            .Append("Message Content : ")
            .Append(txtSub.Text.Replace("\n" ,"<br/>"))
            .AppendLine()
            .Append("<br/>")
            .Append("Name : ")
            .Append(txtName.Text)
            .AppendLine()
            .Append("<br/>")
            .Append("Email : ")
            .Append(txtEmail.Text)
            .AppendLine();
            
            string sub = txtSubject.Text;
            string body1 = body.ToString();
            string email = "support@huntable.co.uk";

            SnovaUtil.SendEmail(sub, body1, email);
            txtSubject.Text = "";
            txtSub.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            lblSuccess.Text = "Message sent successfully";
            lblSuccess.ForeColor = System.Drawing.Color.Green;
            lblSuccess.Visible = true;

            LoggingManager.Debug("Exiting btnSendMail_Click - feedback.aspx");

        }

        //public static void SendEmail(string sub, string subject, string body, string toEmail)
        //{
        //    StringBuilder body1 = new StringBuilder()
        //    .Append("You entered the following comments")
        //    .AppendLine()
        //    .Append(body)
        //    .AppendLine()
        //    .Append("Thank you for giving us valuable suggestions.we started working upon that");
        //    var msg = new MailMessage();
        //    string userName = ConfigurationManager.AppSettings["FromEmail"];
        //    string password = ConfigurationManager.AppSettings["FromEmailPassword"];
        //    msg.From = new MailAddress(userName, ConfigurationManager.AppSettings["FromUserName"]);
        //    msg.To.Add(toEmail);
        //    msg.Subject =subject + " Your comments reached Huntable";
        //    msg.Body = body1.ToString();
        //    msg.IsBodyHtml = true;
        //    string smtpAddress = ConfigurationManager.AppSettings["SMTPAddress"];
        //    int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
        //    var smtp = new SmtpClient(smtpAddress, smtpPort) { EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]), Credentials = new NetworkCredential(userName, password) };
        //    smtp.Send(msg);


        //    StringBuilder bodyAdmin = new StringBuilder()
        //    .Append("Name:")
        //    .AppendLine()
        //    .AppendLine(subject)
        //    .AppendLine()
        //    .Append("Email Id:")
        //    .AppendLine()
        //    .AppendLine(toEmail)
        //    .AppendLine()
        //    .Append("Comments:")
        //    .AppendLine()
        //    .Append(body);

        //    var msgAdmin = new MailMessage();
        //    string from = ConfigurationManager.AppSettings["FromEmail"];
        //    string pwd = ConfigurationManager.AppSettings["FromEmailPassword"];
        //    msgAdmin.From = new MailAddress(userName, ConfigurationManager.AppSettings["FromUserName"]);
        //    msgAdmin.To.Add(ConfigurationManager.AppSettings["FromEmail"]);
        //    msgAdmin.Subject = "Comments entered by user";
        //    msgAdmin.Body = bodyAdmin.ToString();
        //    msgAdmin.IsBodyHtml = true;
        //    smtp.Send(msgAdmin);


        //}

        protected void lnkRP_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkRP_Click - feedback.aspx");
            txtSubject.Text = "";
            txtSubject.Text = "Report a Problem";
            LoggingManager.Debug("Exiting lnkRP_Click - feedback.aspx");

        }
        protected void lnkQ_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkQ_Click - feedback.aspx");
            txtSubject.Text = "";
            txtSubject.Text = "User has a Question";

            LoggingManager.Debug("Exiting lnkQ_Click - feedback.aspx");
        }
        protected void lnkSugg_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkSugg_Click - feedback.aspx");
            txtSubject.Text = "";
            txtSubject.Text = "User sent a Suggestion";
            LoggingManager.Debug("Exiting lnkSugg_Click - feedback.aspx");

        }
        protected void lnkApp_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkApp_Click - feedback.aspx");
            txtSubject.Text = "";
            txtSubject.Text = "Appreciated";
            LoggingManager.Debug("Exiting lnkApp_Click - feedback.aspx");
        }
        protected void lnkAny_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkAny_Click - feedback.aspx");
            txtSubject.Text = "";
            txtSubject.Text = "User Comments";
            LoggingManager.Debug("Exiting lnkAny_Click - feedback.aspx");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnSend_Click - feedback.aspx");

            string sub = "User sent a contact request";
            string bodyC = "Name:" + txtNames.Value + "Email:" + txtEmails.Value + "Message:" + txtMessage.Value;
            string emailC = "support@huntable.co.uk";
            SnovaUtil.SendEmail(sub, bodyC, emailC);
            txtNames.Value = "";
            txtEmails.Value = "";
            txtMessage.Value = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "alert('Your request sent successfully');", true);
            LoggingManager.Debug("Exiting btnSend_Click - feedback.aspx");
        }
    }
}