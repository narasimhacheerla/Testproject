using System;
using Huntable.Business;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class UserPassiveJobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool userLoggedIn = Common.IsLoggedIn();
            LoggingManager.Debug("Entering Page_Load - UserPassiveJobs.aspx");
            try
            {
                if (!IsPostBack)
                {
                    var jobmanager = new JobsManager();
                    empllstvw.DataSource = jobmanager.GetPasiveJobs();
                    empllstvw.DataBind();
                    postDiv.Visible = userLoggedIn;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - UserPassiveJobs.aspx");
        }

        protected void DataPagerProductsPreRender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DataPagerProductsPreRender - UserPassiveJobs.aspx");
            try
            {

                var jobmanager = new JobsManager();
                empllstvw.DataSource = jobmanager.GetPasiveJobs();
                empllstvw.DataBind();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DataPagerProductsPreRender - UserPassiveJobs.aspx");
        }
         protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - ViewUserProfile.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            var result = jobManager.GetUserDetails(loggedInUserId.Value);
            string credit = (result.CreditsLeft).ToString();

            if (result.IsPremiumAccount == false || result.IsPremiumAccount == null)
            {
                Server.Transfer("WhatIsHuntableUpgrade.aspx");
            }
            else if (result.CreditsLeft == null || result.CreditsLeft == 0)
            {
                Server.Transfer("BuyCredit.aspx");
            }
            else
            {
                Server.Transfer("PostJob.aspx");
            }

            LoggingManager.Debug("EXiting BtnPostOpportunityClick - ViewUserProfile.aspx");
        }
    
    }
}