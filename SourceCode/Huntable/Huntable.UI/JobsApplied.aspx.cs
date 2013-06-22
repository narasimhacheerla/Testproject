using System;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Entities;
using System.Collections.Generic;
using Snovaspace.Util.Logging;
using Huntable.Data;
using System.Linq;


namespace Huntable.UI
{
    public partial class JobsApplied : System.Web.UI.Page
    {
        readonly JobsManager _jobManager = new JobsManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering page_Load - JobsApplied.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            try
            {
                if (!IsPostBack)
                {
                    BindUserAppliedJobs();
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {

                        if (loggedInUserId != null)
                        {


                            var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
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
                        else
                        {
                            bimage.Visible = true;
                            pimage.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting page_Load - JobsApplied.aspx");
        }

        private void BindUserAppliedJobs()
        {
            LoggingManager.Debug("Entering BindUserAppliedJobs - JobsApplied.aspx");

            IList<Job> appliedJobs = _jobManager.AppliedJobs(Convert.ToInt32(Common.GetLoggedInUserId(Session)));
            lvJobsApplied.DataSource = appliedJobs;
            lvJobsApplied.DataBind();

            LoggingManager.Debug("Exiting BindUserAppliedJobs - JobsApplied.aspx");
        }
     
        protected void LvJobsAppliedItemCommand(object sender, ListViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering LvJobsAppliedItemCommand - JobsApplied.aspx");
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                LoggingManager.Info("Row Index = " + rowIndex);
                if (e.CommandName == "jobDelete")
                {
                    LoggingManager.Info("Inside jobdelete command.");

                    int jobId = Convert.ToInt32(e.CommandArgument);

                    _jobManager.DeleteAppliedJob(jobId, Convert.ToInt32(Common.GetLoggedInUserId(Session)));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Job deleted successfully ')", true);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting LvJobsAppliedItemCommand -JobsApplied.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_PreRender - JobsApplied.aspx");
            // Workaround to prevent clicking twice on the pager to have results displayed properly
            BindUserAppliedJobs();

            LoggingManager.Debug("Exiting Page_PreRender - JobsApplied.aspx");
        }
        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - JobsApplied.aspx");

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

            LoggingManager.Debug("Exiting BtnPostOpportunityClick - JobsApplied.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - JobsApplied.aspx");
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - JobsApplied.aspx");
                return null;
            }

        }

        //protected void BtnSearch(object sender, EventArgs e)
        //{
        //}
    }
}