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
    public partial class HeaderAfterLoggingIn : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HeaderAfterLoggingIn.ascx");
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
                            liprofile.Visible = false;
                            aprofile.Visible = false;
                            aviewprofile.Visible = true;
                            aviewprofile.HRef = new UrlGenerator().CompanyUrlGenerator(comp.Id);
                            _profile.HRef = new UrlGenerator().CompanyUrlGenerator(comp.Id);
                            _Editprofile.Visible = true;
                            FindLabel.Text = "Find Followers";
                            findhref.HRef = "companiesfindfollowers.aspx";
                        }
                        else
                        {
                            _profile.HRef = "ViewUserProfile.aspx";
                            FindLabel.Text = "Find Friends";
                            findhref.HRef = "InviteFriends.aspx";
                        }
                        adminDiv.Visible = user.IsAdmin.HasValue && user.IsAdmin.Value;
                        if (user.Homepagetour == null||user.Homepagetour==false)
                        {
                            Homepageafterlogin.HRef = "HomePageTour.aspx";
                            HomeForall.HRef = "HomePageTour.aspx";

                        }
                        else
                        {
                            Homepageafterlogin.HRef = "HomePageAfterLoggingin.aspx";
                            HomeForall.HRef = "HomePageAfterLoggingin.aspx";
                        }
                        lblUserName.Text = user.Name;
                        //buycredits.HRef = user.IsPremiumAccount==true ? "Buycredit.aspx" : "Whatishuntableupgrade.aspx";
                        int count = Common.GetUnreadMessagesCount(context, user);
                        if (count > 0)
                        {
                            lblMessagesCount.Text = Common.GetUnreadMessagesCount(context, user).ToString();
                            lblMessagesCount.Visible = true;
                            hypMessageCount.Visible = true;
                        }
                        else
                        {
                            lblMessagesCount.Visible = false;
                            hypMessageCount.Visible = false;
                        }
                        //else
                        //{
                        //    string pageName = fullName.Substring(4);
                        //    if (pageName == "huntabletourfeatures_aspx" || pageName == "huntabletourcustomizefeeds_aspx" || pageName == "huntabletourcustomizejobs_aspx" || pageName == "profileuploadpage_aspx")
                        //    {
                        //        Div1.Visible = false;
                        //        Div2.Visible = false;

                        //    }
                        //    else
                        //    {
                        //        Div1.Visible = true;
                        //        message.Visible = false;
                        //        Div2.Visible = true;

                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - HeaderAfterLoggingIn.aspx");
        }


       
        
        
        public void Flashmessage(string message)
        {
            LoggingManager.Debug("Entering Flashmessage - HeaderAfterLoggingIn.aspx");
            popupmessage2.Visible = true;
            string strScript = "HideCtrl('" + popupmessage2.ClientID + "','5000')";
            lblUserName.Text = message;
            
            Page.ClientScript.RegisterStartupScript(this.GetType(),Guid.NewGuid().ToString(),strScript,true);
            LoggingManager.Debug("Exiting Flashmessage - HeaderAfterLoggingIn.ascx");
        }
        public static void SendEmail(string subject, string body, params string[] toEmails)
        {
            LoggingManager.Debug("Entering SendEmail - HeaderAfterLoggingIn.aspx");
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
            LoggingManager.Debug("Exiting SendEmail - HeaderAfterLoggingIn.aspx");
        }

        protected void lnkJobsClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkJobsClick - HeaderAfterLoggingIn.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            var result = jobManager.GetUserDetails(loggedInUserId.Value);
            string credit = (result.CreditsLeft).ToString();

            if (result.IsPremiumAccount == false || result.IsPremiumAccount == null && result.CreditsLeft == null&& result.FreeCredits == true)
            {
                Response.Redirect("~/WhatIsHuntableUpgrade.aspx");
            }
            else if (result.CreditsLeft == null&& result.CreditsLeft == null||result.CreditsLeft==0 && result.FreeCredits == false)
            {
                Response.Redirect("~/BuyCredit.aspx");
            }  
            else if (result.CreditsLeft == 0 && result.FreeCredits == true&&result.IsPremiumAccount==null||result.IsPremiumAccount==false)
            {
                Response.Redirect("~/WhatIsHuntableUpgrade.aspx");
            }
            else if (result.CreditsLeft == 0 && result.FreeCredits == true && result.IsPremiumAccount != null ||
                     result.IsPremiumAccount == true)
            {
                Response.Redirect("~/PostJob.aspx");
            }

            else
            {
                Response.Redirect("~/PostJob.aspx");
            }
            LoggingManager.Debug("Exiting lnkJobsClick - HeaderAfterLoggingIn.aspx");
        }
        protected void lnkBuyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkBuyClick - HeaderAfterLoggingIn.aspx");
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                var jobManager = new InvitationManager();
                var result = jobManager.GetUserDetails(loggedInUserId.Value);
                if (result.CreditsLeft == null&& result.FreeCredits == true||result.IsPremiumAccount==false)
                {
                    Response.Redirect("~/WhatIsHuntableUpgrade.aspx");
                }    
                else if (result.CreditsLeft == null&& result.FreeCredits == false)
                {
                    Response.Redirect("~/BuyCredit.aspx");
                }
                else if (result.CreditsLeft == 0 && result.FreeCredits == true&&result.IsPremiumAccount==null||result.IsPremiumAccount==false)
                {
                    Response.Redirect("~/WhatIsHuntableUpgrade.aspx");
                }
                else if (result.CreditsLeft == 0 && result.FreeCredits == true && result.IsPremiumAccount != null ||
                         result.IsPremiumAccount == true)
                {
                    Response.Redirect("~/BuyCredit.aspx");
                }
                else
                {
                    Response.Redirect("~/BuyCredit.aspx");
                }
                LoggingManager.Debug("Exiting lnkBuyClick - HeaderAfterLoggingIn.aspx");
        }
        //protected void tx tSharebyEmail_Click(object sender, EventArgs e)
        //{
        //    SendEmail("test", txtMessage.Text.Trim(), txtTo.Text);
        //}
        //protected void d_clip_button_Click(object sender, EventArgs e)
        //{
        //    Thread cbThread = new Thread(new ThreadStart(CopyToClipboard));
        //    cbThread.ApartmentState = ApartmentState.STA;
        //    cbThread.Start();
        //    cbThread.Join();
        //    StringBuilder strScript = new StringBuilder();
        //    //hdnpopup.Value = "0";
        //}
        //[STAThread]
        //protected void CopyToClipboard()
        //{
        //    DataObject m_data = new DataObject();
        //    m_data.SetData(DataFormats.Text, true, fe_text.Text);
        //    Clipboard.SetDataObject(m_data, true);
        //}
        //protected void btnShare_Click(object sender, EventArgs e)
        //{
        //    txtShareMessage.Text = txtShareMessage.Text;
        //    int count = 1;

        //    string strl = "http://www.linkedin.com/shareArticle?mini=true&url=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
        //    string str = "https://twitter.com/share?url=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
        //    string strf = "http://www.facebook.com/sharer.php?u=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;

        //    if (chkTwitter.Checked)
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + str + "','blank' + new Date().getTime(),'menubar=no') </script>");
        //    count = count + 1;
        //    if (chkLinkedIn.Checked)
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strl + "','blank' + new Date().getTime(),'menubar=no') </script>");
        //    count = count + 1;
        //    if (chkFacebook.Checked)
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strf + "','blank' + new Date().getTime(),'menubar=no') </script>");
        //}
        protected void BtnSearchForUsersClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchForUsersClick - HeaderAfterLoggingIn.aspx");
            try
            {
                string url = string.Format("~/UserSearch.aspx?keyword={0}", txtSearch.Value);
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);

            }
            LoggingManager.Debug("Exiting BtnSearchForUsersClick - HeaderAfterLoggingIn.aspx");
        }

        //protected void BtnSearchForJobsClick(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering BtnSearchForJobsClick - Default.aspx");
        //    try
        //    {
        //        string url = string.Format("~/JobSearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
        //        Response.Redirect(url, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingManager.Error(ex);

        //    }
        //    LoggingManager.Debug("Exiting BtnSearchForJobsClick - Default.aspx");
        //}

        protected void LogOutClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering LogOutClick - HeaderAfterLoggingIn.aspx");
            Session.Abandon();
            Session.Clear();
            Response.Redirect(PageNames.Home);
            LoggingManager.Debug("Exiting LogOutClick - HeaderAfterLoggingIn.aspx");
        }

        public void Flasmessage()
        {
            LoggingManager.Debug("Entering Flashmessage - HeaderAfterLoggingIn.aspx");
            popupmessage2.Visible = true;
            string strScript = "HideCtrl('" + popupmessage2.ClientID + "','15000')";

            Page.ClientScript.RegisterStartupScript(
              this.GetType(),
              Guid.NewGuid().ToString(),
              strScript,
              true);
            LoggingManager.Debug("Exiting Flashmessage - HeaderAfterLoggingIn.aspx");
        }
        protected void PictureUploadClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering PictureUploadClick - HeaderAfterLoggingIn.aspx");

            LoggingManager.Debug("Exiting PictureUploadClick - HeaderAfterLoggingIn.aspx");
            Response.Redirect("PictureUpload.aspx", false);
        }
    }
}