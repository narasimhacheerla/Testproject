using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Business;
using Huntable.Data;
using Huntable.Entities.SearchCriteria;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class JobControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    Common.FillDropDownBox(ddlJobType, MasterDataManager.AllJobTypes.ToList().Select(x => new KeyValuePair<string, int>(x.Description, x.Id)).ToList());
                    Common.FillDropDownBox(ddlCountry, MasterDataManager.AllCountries.ToList().Select(x => new KeyValuePair<string, int>(x.Description, x.Id)).ToList());
                    Common.FillDropDownBox(ddlIndustry, MasterDataManager.AllIndustries.ToList().Select(x => new KeyValuePair<string, int>(x.Description, x.Id)).ToList());

                    SetControls();

                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var maxSalary = context.MasterMaximumSalaries.ToList();
                        var result = from mms in context.MasterMaximumSalaries
                                     select new
                                     {
                                         mms.MaximumSalary
                                     };
                        int count= result.Count();
                        foreach (var n in result)
                        {
                            //string[] maxsal = n.ToString() + "$";
                        }
                        ddlSalary.DataSource = maxSalary;
                        ddlSalary.DataTextField = "MaximumSalary";
                        ddlSalary.DataValueField = "Id";
                        ddlSalary.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
        }

        private void SetControls()
        {
            var js = (JobSearchCriteria)Session[SessionNames.SearchCriteria];
            if (js != null)
            {
                if (!string.IsNullOrWhiteSpace(js.JobTitle)) txtjobTitle.Text = js.JobTitle;
                else txtjobTitle.Text = "Job Title";
                if (!string.IsNullOrWhiteSpace(js.Keywords)) txtKeywords.Text = js.Keywords;
                else txtKeywords.Text = "Keywords";
                ddlCountry.SelectedValue = Convert.ToString(js.Country);
                ddlExperience.SelectedValue = Convert.ToString(js.Experience);
                ddlJobType.SelectedValue = Convert.ToString(js.JobType);
                FillSalary();
                ddlIndustry.SelectedValue = Convert.ToString(js.Industry);
               if(! string.IsNullOrWhiteSpace(js.Location)) txtLocation.Text =  js.Location;
               else txtLocation.Text = "Location";
               if (!string.IsNullOrWhiteSpace(js.Company)) txtCompany.Text = js.Company;
               else txtCompany.Text = "Company";
                ddlSalary.SelectedValue = Convert.ToString(js.Salary);
                if (!string.IsNullOrWhiteSpace(js.SkillText)) txtSkill.Text = js.SkillText;
                else txtSkill.Text = "Skill";
            }
        }

        protected void DdlJobTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillSalary();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
        }

        private void FillSalary()
        {
            var jobmanager = new JobsManager();
            int jobTypeId = Convert.ToInt32(ddlJobType.SelectedItem.Value);
            ddlSalary.DataSource = jobmanager.GetSalaryByJobtype(jobTypeId);
            ddlSalary.DataTextField = "Salary";
            ddlSalary.DataValueField = "Id";
            ddlSalary.DataBind();
        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            try
            {
                var jobSearchCriteria = new JobSearchCriteria
                {
                    Country =  Convert.ToInt16(ddlCountry.SelectedItem.Value),
                    Experience = Convert.ToInt16(ddlExperience.SelectedItem.Value),
                    Industry = Convert.ToInt16(ddlIndustry.SelectedItem.Value),
                    JobTitle = (txtjobTitle.Text == "Job Title") ? string.Empty : txtjobTitle.Text.Trim(),
                    JobType = Convert.ToInt16(ddlJobType.SelectedItem.Value),
                    Keywords = (txtKeywords.Text == "Keywords")?string.Empty:txtKeywords.Text.Trim(),
                    Location =  (txtLocation.Text == "Location")?string.Empty:txtLocation.Text,
                    Salary = Convert.ToInt16(ddlSalary.SelectedItem.Value),
                    SkillText = txtSkill.Text == "Skill" ? string.Empty : txtSkill.Text,
                    Company =(txtCompany.Text == "Company")?string.Empty:txtCompany.Text.Trim()
                };

                Session[SessionNames.SearchCriteria] = jobSearchCriteria;
                Response.Redirect("~/JobSearch.aspx", false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
        }
    }
}