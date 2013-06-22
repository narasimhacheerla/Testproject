using System;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Entities;
using System.Collections.Generic;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class MyPostedJobs : System.Web.UI.Page
    {
        readonly JobsManager _jobManager = new JobsManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering page_load - MyPostedJobs.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var user = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                    if (user.IsCompany == true)
                    {
                        isyour.HRef = "companyregistration2.aspx";
                        ProfileHuntablediv.Visible = false;
                    }
                    else
                    {
                        isyour.HRef = "editprofilepage.aspx";
                    }
                }
                if (!IsPostBack)
                {
                    BindUserPostedJobs();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting page_load -  MyPostedJobs.aspx");
        }

        private void BindUserPostedJobs()
        {
            LoggingManager.Debug("Entering BindUserPostedJobs -  MyPostedJobs.aspx");

            IList<Job> postedJobs = _jobManager.PostedJobs(Convert.ToInt32(Common.GetLoggedInUserId(Session)));
            jobCount.Text =(postedJobs.Count!=0)?Convert.ToString(postedJobs.Count):"0";
                lvPostedJobs.DataSource = postedJobs;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    foreach (Job j in postedJobs.ToList())
                    {
                        j.Symbol = (from c in context.MasterCurrencyTypes
                                    where c.Description == j.CurrencyDescription
                                    select c.Symbol).FirstOrDefault();
                    }
                }

                lvPostedJobs.DataBind();

                LoggingManager.Debug("Exiting BindUserPostedJobs -  MyPostedJobs.aspx");
        }
        protected void LvPostedJobsItemCommand(object sender, ListViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering  LvPostedJobsItemCommand -  MyPostedJobs.aspx");
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                LoggingManager.Info("Row Index = " + rowIndex);

                if (e.CommandName == "jobDelete")
                {
                    LoggingManager.Info("Inside jobdelete command.");
                   
                    int jobId = Convert.ToInt32(e.CommandArgument);
                            _jobManager.DeletePostedJob(jobId, Convert.ToInt32(Common.GetLoggedInUserId(Session)));
                    BindUserPostedJobs();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting  LvPostedJobsItemCommand -  MyPostedJobs.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering  Page_PreRender -  MyPostedJobs.aspx");

            // Workaround to prevent clicking twice on the pager to have results displayed properly
            BindUserPostedJobs();

            LoggingManager.Debug("Exiting  Page_PreRender -  MyPostedJobs.aspx");
        }
        private int LoginUserId
        {
            get
            {
                return Common.GetLoggedInUserId(Session).Value;
            }
        }
        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - MyPostedJobs.aspx");

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

            LoggingManager.Debug("Exiting BtnPostOpportunityClick - MyPostedJobs.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - MyPostedJobs.aspx");
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - MyPostedJobs.aspx");
                return null;
            }

        }
    }
}