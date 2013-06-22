using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Threading;
using Huntable.Business;
using Huntable.Data;
using OAuthUtility;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class ShareMail : System.Web.UI.Page
    {
        private int? OtherUserId
        {

            get
            {
                LoggingManager.Debug("Entering OtherUserId - ShareMail.aspx");
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                if (Page.RouteData.Values["ID"] != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
                }
                LoggingManager.Debug("Exiting OtherUserId - ShareMail.aspx");
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ShareMail.aspx");
            if (!IsPostBack)
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherUserId.HasValue)
                {
                    if (loginUserId.HasValue && loginUserId.Value == OtherUserId.Value)
                    {
                        txtShareMessage.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                        txtMessage.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                        fe_text.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                    }
                    txtShareMessage.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                    txtMessage.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                    fe_text.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                }
                else
                {
                    txtShareMessage.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                    txtMessage.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                    fe_text.Text = "https://huntable.co.uk/" +new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                }
            }
            LoggingManager.Debug("Exiting Page_Load - ShareMail.aspx");

        }

        public static void SendEmail(string subject, StringBuilder body, params string[] toEmails)
        {
            LoggingManager.Debug("Entering SendEmail - ShareMail.aspx");

            var msg = new MailMessage();
            string userName = ConfigurationManager.AppSettings["FromEmail"];
            string password = ConfigurationManager.AppSettings["FromEmailPassword"];
            msg.From = new MailAddress(userName, ConfigurationManager.AppSettings["FromUserName"]);
            foreach (string toEmail in toEmails) msg.To.Add(toEmail);
            msg.Subject = subject;
            msg.Body = body.ToString();
            msg.IsBodyHtml = true;
            string smtpAddress = ConfigurationManager.AppSettings["SMTPAddress"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            var smtp = new SmtpClient(smtpAddress, smtpPort)
                {
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                    Credentials = new NetworkCredential(userName, password)
                };
            smtp.Send(msg);

            LoggingManager.Debug("Exiting SendEmail - ShareMail.aspx");
        }
        private int LoginUserId
        {
            get { return Common.GetLoggedInUserId(Session).Value; }
        }
        protected void txtSharebyEmail_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering txtSharebyEmail_Click - ShareMail.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = Common.GetLoggedInUser(context);
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                string message = string.Empty;

                const string url1 = "?UserId";
                const string url2 = "VisualCV.aspx";
                const string url3 = "viewuserprofile.aspx";
                const string url4 = "visualcvactivity.aspx";
                if (txtMessage.Text.ToLower().Contains(url1.ToLower()))
                {
                    if (loggedInUserId != null)
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(user.Name)
                            .Append(",has shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text.Trim())
                            .AppendLine()
                            .Append("Hope you like it")
                            .AppendLine();

                        SendEmail(user.Name + " " + "shared a link", body, txtTo.Text);
                    }
                    else
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")                         
                            .Append("Shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text.Trim())
                            .AppendLine()
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail("Shared a link", body, txtTo.Text);
                    }
                }
                else if (txtMessage.Text.ToLower().Contains(url2.ToLower()) ||
                         txtMessage.Text.ToLower().Contains(url3.ToLower()) ||
                         txtMessage.Text.ToLower().Contains(url4.ToLower()))
                {
                    var message1 = txtMessage.Text + "?UserId=" + user.Id;
                    if (loggedInUserId != null)
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(user.Name)
                            .Append(",has shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(message1)
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();

                        SendEmail(user.Name + " " + "shared a link", body, txtTo.Text);
                    }
                    else
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")                           
                            .Append("Shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(message1)
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail("Shared a link", body, txtTo.Text);
                    }
                }
                else
                {
                    if (loggedInUserId != null)
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(user.Name)
                            .Append(",has shared a message with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text.Trim())
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();

                        SendEmail(user.Name + " " + "shared a link", body, txtTo.Text);
                    }
                    else
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")                           
                            .Append("Shared a message with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text.Trim())
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail("Shared a link", body, txtTo.Text);
                    }
                }

            }
            LoggingManager.Debug("Exiting txtSharebyEmail_Click - ShareMail.aspx");
        }

        

        protected void btnShare_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnShare_Click - ShareMail.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            if (loggedInUserId != null)
            {
                var socialManager = new SocialShareManager();
                txtShareMessage.Text = txtShareMessage.Text;                
                if (chkTwitter.Checked)
                {
                    socialManager.ShareOnTwitter(LoginUserId, txtShareMessage.Text);
                }
                if (chkFacebook.Checked)
                {
                    var text = txtShareMessage.Text.Trim();
                    var result = text.Split(' ');
                    var link = result[0];
                    var context = new huntableEntities();
                    var user = context.Users.FirstOrDefault(u => u.Id == LoginUserId);
                    if (user != null && user.PersonalLogoFileStoreId != null)
                    {
                        socialManager.ShareLinkOnFacebook(LoginUserId, "", "[UserName] has shared a link in Huntable",
                                                          "", "", link, "http://huntable.co.uk/loadfile.ashx?id=" + user.PersonalLogoFileStoreId);
                    }
                    else
                    {
                        socialManager.ShareLinkOnFacebook(LoginUserId, "", "[UserName] has shared a link in Huntable",
                                                          "", "", link, "");
                    }
                }
                if (chkLinkedIn.Checked)
                {
                    socialManager.ShareOnLinkedIn(LoginUserId, txtShareMessage.Text, "");
                }
            }            else
            {

                txtShareMessage.Text = txtShareMessage.Text;
                int count = 1;
                string strl = "http://www.linkedin.com/shareArticle?mini=true&url=" + txtShareMessage.Text + "&summary=" +
                              txtShareMessage.Text;
                string str = "https://twitter.com/share?url=" + txtShareMessage.Text + "&summary=" +
                             txtShareMessage.Text;
                string strf = "http://www.facebook.com/sharer.php?u=" + txtShareMessage.Text + "&summary=" +
                              txtShareMessage.Text;
                if (chkTwitter.Checked)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(),
                                                            "<script language=javascript>window.open('" + str +
                                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                count = count + 1;
                if (chkLinkedIn.Checked)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(),
                                                            "<script language=javascript>window.open('" + strl +
                                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                count = count + 1;
                if (chkFacebook.Checked)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(),
                                                            "<script language=javascript>window.open('" + strf +
                                                            "','blank' + new Date().getTime(),'menubar=no') </script>");

                LoggingManager.Debug("Exiting btnShare_Click - ShareMail.aspx");
            }
        }
        private void CheckSocialShare(string provider)
        {
            LoggingManager.Debug("Entering CheckSocialShare - ViewUserProfile.aspx");
            var user = Common.GetLoggedInUser();
            if (user == null) return;
            var check = user.OAuthTokens.Any(o => o.Provider == provider);
            if (check) return;
            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var callbackuri = baseUrl + "oauth.aspx";
            Session["oauthmode"] = "socialshare";
            OAuthWebSecurity.RequestAuthentication(provider, callbackuri);
            LoggingManager.Debug("Exiting CheckSocialShare - ViewUserProfile.aspx");
        }
        protected void chkTwitter_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkTwitter_CheckedChanged - ShareMail.aspx");
            if (chkTwitter.Checked)
                CheckSocialShare("twitter");
            LoggingManager.Debug("Exiting chkTwitter_CheckedChanged - ShareMail.aspx");
        }
        protected void chkLinkedIn_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkLinkedIn_CheckedChanged - ShareMail.aspx");
            if (chkLinkedIn.Checked)
                CheckSocialShare("linkedin");
            LoggingManager.Debug("Exiting chkLinkedIn_CheckedChanged - ShareMail.aspx");
        }
        protected void chkFacebook_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkFacebook_CheckedChanged - ShareMail.aspx");
            if (chkFacebook.Checked)
                CheckSocialShare("facebook");
            LoggingManager.Debug("Exiting chkFacebook_CheckedChanged - ShareMail.aspx");
        }
    }
}


    
