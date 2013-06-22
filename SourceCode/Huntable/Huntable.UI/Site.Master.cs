using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Huntable.Business;
using Huntable.Data;
using System.Web.UI.HtmlControls;
using Snovaspace.Util.Logging;
using Snovaspace.Util.UICommon;

namespace Huntable.UI
{
    public partial class SiteMaster : SnovaMaster
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();    
            LoggingManager.Debug("Entering Page_Load - Site.Master : " + Page.Request.RawUrl);
            try
            {
                new Snovaspace.Util.Utility().RunAsTask(() => Common.LogLastActivityDate(Session));

                bool userLoggedIn = Common.IsLoggedIn();
                Session["Uid"] = userLoggedIn;
                if (Session["Uid"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RegisterChatUser();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                bool isProfilePage = Page.Request.RawUrl.ToLower().Contains("profileuploadpage")
                    || Page.Request.RawUrl.ToLower().Contains("huntabletour");

                headerAfterLoggingIn.Visible = userLoggedIn & !isProfilePage;
                headerBeforeLoggingIn.Visible = !userLoggedIn;
                headerAfterLoggingInWithouthinks.Visible = userLoggedIn & isProfilePage;

                var fullName = MainContent.Page.GetType().FullName;
                string pageName = fullName.Substring(4);
                GetMeta(pageName);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting Page_Load - Site.Master : " + Page.Request.RawUrl);
        }
        private int _startTime;
        protected void Page_Init(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering Page_Init - Site.Master");

            _startTime = Environment.TickCount;

            LoggingManager.Debug("Exiting Page_Init - Site.Master");
        }

        private void GetMeta(string pageName)
        {
            LoggingManager.Debug("Entering GetMeta - Site.Master");

            var jm = new JobsManager();
            SeoInfo info = jm.GetMeta(pageName);
            if (info != null)
            {
                var tag = new HtmlMeta { Name = "Title", Content = info.Title };
                HeadContent.Controls.Add(tag);

                tag = new HtmlMeta { Name = "Keywords", Content = info.Keyword };
                HeadContent.Controls.Add(tag);

                tag = new HtmlMeta { Name = "Description", Content = info.Description };
                HeadContent.Controls.Add(tag);
            }

            LoggingManager.Debug("Exiting GetMeta - Site.Master");
        }

        protected void lnkJobs_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkJobs_Click - Site.Master");

            Server.Transfer("~/UserPassiveJobs.aspx");

            LoggingManager.Debug("Exiting lnkJobs_Click - Site.Master");
        }

        protected void lnkHelp_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkHelp_Click - Site.Master");

            Response.Redirect("~/Aboutus.aspx");

            LoggingManager.Debug("Exitng lnkHelp_Click - Site.Master");

        }

        protected void lnkFaq_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkFaq_Click - Site.Master");

            Response.Redirect("~/faq.aspx");

            LoggingManager.Debug("Exiting lnkFaq_Click - Site.Master");
        }

        protected void lnkFeedBack_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkFeedBack_Click - Site.Master");

            Response.Redirect("~/feedback.aspx");

            LoggingManager.Debug("Exiting lnkFeedBack_Click - Site.Master");
        }
        protected void lnkUpGrade_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkUpGrade_Click - Site.Master");

            Server.Transfer("~/WhatIsHuntableUpgrade.aspx");

            LoggingManager.Debug("Exiting lnkUpGrade_Click - Site.Master");
        }


        protected void lnkContact_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkContact_Click - Site.Master");

            Response.Redirect("~/ContactUs.aspx");

            LoggingManager.Debug("Exiting lnkContact_Click - Site.Master");
        }
        protected void lnkAffliate_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkAffliate_Click - Site.Master");

            Server.Transfer("~/AffliatePartner.aspx");

            LoggingManager.Debug("Exiting lnkAffliate_Click - Site.Master");
        }
        protected void lnkTerms_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkTerms_Click - Site.Master");

            Response.Redirect("~/Terms.aspx");

            LoggingManager.Debug("Exiting lnkTerms_Click - Site.Master");
        }

        protected void lnkPrivacy_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkPrivacy_Click - Site.Master");

            Response.Redirect("~/PrivacyPolicy.aspx");

            LoggingManager.Debug("Exiting lnkPrivacy_Click - Site.Master");
        }
        protected void lnkcookie_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkPrivacy_Click - Site.Master");

            Response.Redirect("~/cookiespolicy.aspx");

            LoggingManager.Debug("Exiting lnkPrivacy_Click - Site.Master");
        }

      

        private void RegisterChatUser()
        {
            bool userLoggedIn = Common.IsLoggedIn();
            if (userLoggedIn)
            {
                var user = Common.GetLoggedInUser();
                if(user != null)
                {
                    var timestamp = DateTime.Now.ToString("yyMMddhhmmss");
                    var chaturl = Common.GetApplicationBaseUrl()+"Chat";
                    Page.ClientScript.RegisterStartupScript(GetType(), "InitializeMessenger",
                                                    String.Format(
                                                        "Messenger.initialize('{0}', '{1}', '{2}', '{3}', {4},'{5}','{6}');",
                                                        chaturl, user.Id, timestamp,
                                                        Common.CalculateChatAuthHash(user.Id.ToString(), String.Empty,
                                                                              timestamp),
                                                        5 * 1000,user.Name,user.UserProfilePictureDisplayUrl.Replace("~","")),
                                                    true);
                }
            }

         
        }

        //protected void lnkContact_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AboutUs.aspx");
        //}


        //protected void LoadCountry()
        //{
        //    ddlCountry.DataSource = context.MasterCountries;
        //    ddlCountry.DataTextField = "Description";
        //    ddlCountry.DataValueField = "Id";
        //    ddlCountry.DataBind();
        //}
    }

}
