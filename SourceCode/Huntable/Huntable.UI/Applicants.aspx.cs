using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;


namespace Huntable.UI
{
    public partial class Applicants : System.Web.UI.Page
    {
        readonly JobsManager _jobManager = new JobsManager();
        protected void Page_Load(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering Page_Load - Applicants");

            DisplayJobApplicants();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                if (user.IsCompany == true)
                {

                    ProfileHuntablediv.Visible = false;
                }

            }
            LoggingManager.Debug("Exiting Page_Load - Applicants");

        }
        public void DisplayJobApplicants()
        {
            var userId = Business.Common.GetLoggedInUserId(Session);

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            if (loggedInUserId != null)
            {
                var jobcount = new JobsManager().MyApplicants(loggedInUserId.Value);
                lblCount.Text = jobcount.Count.ToString();
            }
            {
                if (loggedInUserId != null)
                {
                    var result = jobManager.GetUserDetails(loggedInUserId.Value);
                    upgradelink.HRef = result.IsCompany == true ? "companyregistration2.aspx" : "Editprofilepage.aspx";
                }
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {


                    var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId.Value);
                    if (usri != null && usri.IsPremiumAccount == null)
                    {
                        bimage.Visible = true;
                        pimage.Visible = false;
                    }
                    else
                    {
                        bimage.Visible = false;
                        pimage.Visible = true;
                    }
                }
            }
            if (userId != null)
            {
                var jobapplicants = new JobsManager().MyApplicants(userId.Value);
                //var jobapplicants = new JobsManager().JobApplicants(userId.Value);
                if (jobapplicants.Count != 0)
                {
                    rspdata.DataSource = jobapplicants;
                    rspdata.DataBind();

                }
                else
                {
                    lblMessage.Text = "You have no Job Applicants to display";
                    lblMessage.Visible = true;
                }
            }
        }
        protected void LvJobsApplicantsItemCommand(object sender, ListViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering lvJobsAppliedItemCommand - JobsApplied.aspx");
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int? loggedInUserId = Common.GetLoggedInUserId();
                LoggingManager.Info("Row Index = " + rowIndex);
                if (e.CommandName == "jobDelete")
                {
                    LoggingManager.Info("Inside jobdelete command.");

                    int otheruserId = Convert.ToInt32(e.CommandArgument);

                    if (loggedInUserId != null)
                    {
                        _jobManager.DeleteJobApplicants(otheruserId, loggedInUserId.Value);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Job deletd succesfully ')", true);
                    } DisplayJobApplicants();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting lvJobsAppliedItemCommand -JobsApplied.aspx");
        }
        protected string GetUrl(object userId)
        {
            LoggingManager.Debug("Entering GetUrl - Applicants");

            int? loggedInUserId = Common.GetLoggedInUserId();

            if (loggedInUserId.HasValue && (Int32)userId != loggedInUserId.Value)
            {
                return "window.open('AjaxChat/MessengerWindow.aspx?init=1&target=" + userId + "', '" + userId + "', 'width=650,height=400,resizable=1,menubar=0,status=0,toolbar=0'); return false";


            }
            LoggingManager.Debug("Exiting GetUrl - Applicants");

            return null;


        }
        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - ViewUserProfile.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            if (loggedInUserId != null)
            {
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
            }

            LoggingManager.Debug("Exiting BtnPostOpportunityClick - ViewUserProfile.aspx");
        }
        private int LoginUserId
        {
            get
            {
                return Common.GetLoggedInUserId(Session).Value;
            }
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().UserUrlGenerator(userid);
            }
            else
            {
                return null;
            }

        }

    }
}