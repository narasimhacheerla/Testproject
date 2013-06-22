using System;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Entities.SearchCriteria;
using Snovaspace.Util.Logging;
using System.Collections.Generic;

namespace Huntable.UI
{
    public partial class JobSearch : System.Web.UI.Page
    {
        public delegate void DelPopulateSearchUsers(int pageIndex);
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - JobSearch.aspx");

            try
            {
                if (!Page.IsPostBack)
                {
                    JobControl1.LoadControl("JobControl.ascx");

                    if (Request.QueryString["keyword"] != null)
                    {
                        var criteria = new JobSearchCriteria { Keywords = Request.QueryString["keyword"] };
                       
                        Session[SessionNames.SearchCriteria] = criteria;
                    }
                    SearchJobs(1);
                }
                else
                {
                    var delPopulate = new DelPopulateSearchUsers(SearchJobs);
                    pagerUsers.UpdatePageIndex = delPopulate;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - JobSearch.aspx");
        }

        protected void EmpllstvwItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            LoggingManager.Debug("Entering EmpllstvwItemCommand - JobSearch.aspx");
            try
            {
                if (e.CommandName == "ApplyNow")
                {
                    LoggingManager.Debug("Entering jobs listview applyNow command - JobSearch.aspx");
                    try
                    {
                        var jm = new JobsManager();
                        int jobId = Convert.ToInt32(e.CommandArgument);
                        int userId = Convert.ToInt32(Common.GetLoggedInUserId(Session));
                        var details = new JobApplication { UserId = userId, JobId = jobId, AppliedDateTime = DateTime.Now, UserComments = string.Empty };
                        jm.SaveJobApplication(details,Convert.ToInt32(Common.GetLoggedInUserId(Session)), Convert.ToInt32(e.CommandArgument));
                    }
                    catch (Exception ex)
                    {
                        LoggingManager.Error(ex);
                    }
                    LoggingManager.Debug("Exiting jobs listview applyNow command - JobSearch.aspx");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting EmpllstvwItemCommand - JobSearch.aspx");
        }

        private void SearchJobs(int pageIndex)
        {
            try
            {
                LoggingManager.Debug("Entering SearchJobs - SearchJobs.aspx");
                var js = (JobSearchCriteria)Session[SessionNames.SearchCriteria];
                  int totalRecords = 0;
                var datalayer = new JobsManager();
                List<Job> jobSearchResults;
                if (Request.QueryString["keyword"] != null)
                {
                    //jobSearchResults = datalayer.GetJobBySearchCriteria(Request.QueryString["keyword"],out  totalRecords,
                    //                                                    pagerUsers.RecordsPerPage, 1 );
                    jobSearchResults = datalayer.GetJobBySearchCriteria(js.JobTitle, Request.QueryString["keyword"], (Int16)js.Country,
                                                                       (Int16)js.Experience, (Int16)js.JobType,
                                                                       (Int16)js.Industry, js.Location,
                                                                       (Int16)js.Salary, (Int16)js.Skill, js.Company,
                                                                       js.SkillText, out  totalRecords,
                                                                       pagerUsers.RecordsPerPage, pageIndex);
                }
                else
                {
                    jobSearchResults = datalayer.GetJobBySearchCriteria(js.JobTitle, js.Keywords, (Int16)js.Country,
                                                                        (Int16)js.Experience, (Int16)js.JobType,
                                                                        (Int16)js.Industry, js.Location,
                                                                        (Int16)js.Salary, (Int16)js.Skill, js.Company,
                                                                        js.SkillText,out  totalRecords,
                                                                        pagerUsers.RecordsPerPage, pageIndex);
                }
                pagerUsers.TotalRecords = totalRecords;


                lblNoOfJobs.InnerText = Convert.ToString(totalRecords);
                
                empllstvw.DataSource = jobSearchResults;
                empllstvw.DataBind();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting SearchJobs - SearchJobs.aspx");
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Prerender - JobSearch.aspx");
            try
            {
                //SearchJobs(pageIndex);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Prerender - JobSearch.aspx");
        }

        protected void EmpllstvwItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering EmpllstvwItemDataBound - JobSearch.aspx");
            try
            {
                var rowView = (Job)e.Item.DataItem;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.Separator)
                {
                    var userAlreadyAppliedToThisJob = (LinkButton)e.Item.FindControl("lbUserAlreadyApplied");
                    var userNotAppliedToJob = (LinkButton)e.Item.FindControl("lbUserNotAppliedToJob");

                    if (rowView != null)
                    {
                        userAlreadyAppliedToThisJob.Visible = (bool)rowView.IsUserAlreadyToThisJob;
                        userNotAppliedToJob.Visible = !(bool)rowView.IsUserAlreadyToThisJob;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting EmpllstvwItemDataBound - JobSearch.aspx");

        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            else
            {
                return null;
            }
           
        }
    }
}