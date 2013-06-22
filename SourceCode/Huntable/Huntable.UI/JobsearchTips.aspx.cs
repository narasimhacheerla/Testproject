using System;
using System.Globalization;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class JobSearchTips : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - JobSearchTips.aspx");

            if (!IsPostBack)
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                if (loggedInUserId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var objMessageManager = new UserMessageManager();
                        var jobManager = new JobsManager();
                        var result = jobManager.GetUserEmployment(loggedInUserId.Value,3);
                        dtLstJobs.DataSource = result;
                        dtLstJobs.DataBind();
                        User userDetails = objMessageManager.GetUserbyUserId(context, loggedInUserId.Value);
                        lblUName.Text = userDetails.Name;
                        imgProfile.ImageUrl = userDetails.UserProfilePictureDisplayUrl;
                        var percentCompleted = UserManager.GetProfilePercentCompleted(loggedInUserId.Value);
                        lblPComplete.Text = (percentCompleted / 100).ToString("00%");
                        int value = Convert.ToInt32(percentCompleted);
                        ProgressBar2.Value = value;
                    }
                }
            }

            LoggingManager.Debug("Exiting Page_Load - JobSearchTips.aspx");
        }



        protected void btnJobsSearch_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnJobsSearch_Click - JobSearchTips.aspx");
            try
            {
                string url = string.Format("~/JobSearch.aspx?keyword={0}", (txtJobsSearchKeyword.Text != @"e.g: Job Title,Keywords, or Company name") ? txtJobsSearchKeyword.Text : string.Empty);
                Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting btnJobsSearch_Click - JobSearchTips.aspx");
        }

        protected void BtnJobShowCLick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnJobShowCLick - JobSearchTips.aspx");

            var loggedInUserId = Common.GetLoggedInUserId();
            if (loggedInUserId != null)
            {
                var jobManager = new JobsManager();
               
                if (hJobfield.Value == string.Empty)
                    hJobfield.Value = "3";
                hJobfield.Value = (Convert.ToInt32(hJobfield.Value) + 6).ToString(CultureInfo.InvariantCulture);
                var result = jobManager.GetUserEmployment(loggedInUserId.Value, Convert.ToInt32(hJobfield.Value));
                dtLstJobs.DataSource = result;
                dtLstJobs.DataBind();
            }
            LoggingManager.Debug("Exiting BtnJobShowCLick - JobSearchTips.aspx");

        }
        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - JobSearchTips.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            if (loggedInUserId != null)
            {
                var result = jobManager.GetUserDetails(loggedInUserId.Value);
                if (result.IsPremiumAccount == false || result.IsPremiumAccount == null && result.CreditsLeft == null && result.FreeCredits == true)
                {
                    Response.Redirect("WhatIsHuntableUpgrade.aspx");
                }
                else if (result.CreditsLeft == null && result.CreditsLeft == null || result.CreditsLeft == 0 && result.FreeCredits == false)
                {
                    Response.Redirect("BuyCredit.aspx");
                }
                else if (result.CreditsLeft == 0 && result.FreeCredits == true && result.IsPremiumAccount == null || result.IsPremiumAccount == false)
                {
                    Response.Redirect("WhatIsHuntableUpgrade.aspx");
                }
                else if (result.CreditsLeft == 0 && result.FreeCredits == true && result.IsPremiumAccount != null ||
                         result.IsPremiumAccount == true)
                {
                    Response.Redirect("PostJob.aspx");
                }

                else
                {
                    Response.Redirect("PostJob.aspx");
                }
            }

            LoggingManager.Debug("Exiting BtnPostOpportunityClick - JobSearchTips.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - JobSearchTips.aspx");
        
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            LoggingManager.Debug("Exiting UrlGenerator - JobSearchTips.aspx");
        
            return null;
        }
    }
}