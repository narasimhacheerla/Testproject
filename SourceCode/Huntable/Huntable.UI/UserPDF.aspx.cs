using System;
using System.Linq;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.Logging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Text;

namespace Huntable.UI
{
    public partial class UserPDF : System.Web.UI.Page
    {
        private int? OtherUserId
        {
            get
            {
                LoggingManager.Debug("Entering OtherUserId - UserPDF.aspx");
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting OtherUserId - UserPDF.aspx");
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - UserPDF.aspx");

            if (!IsPostBack)
            {
                LoadProfile();
                }
            LoggingManager.Debug("Exiting Page_Load - UserPDF.aspx");

        }
        private void LoadProfile()
        {
            LoggingManager.Debug("Entering LoadProfile - UserPDF.aspx");

            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    User user = null;
                    Int16 id = Convert.ToInt16(Request.QueryString["UserId"]);
                    user = context.Users.First(u => u.Id == id );
                    if (user != null)
                    {
                        DisplayUserDetails(user);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting LoadProfile - UserPDF.aspx");
        }

        private void DisplayUserDetails(User user)
        {
            LoggingManager.Debug("Entering DisplayUserDetails - UserPDF.aspx");
            try
            {
                imgProfile.ImageUrl = user.UserProfilePictureDisplayUrl;
                lblName.Text = user.Name;
                lblLocation.Text = string.Format("{0}, {1}", user.County, user.CountryName);
                lblTown.Text = string.Format("{0}", user.City);


                var currentEmployment = user.EmploymentHistories.FirstOrDefault(emp => emp.IsCurrent);
                if (currentEmployment != null)
                {
                    lblCurrentRole.Text = string.Format("{0} at {1}", currentEmployment.JobTitle, currentEmployment.MasterCompany != null ? currentEmployment.MasterCompany.Description : string.Empty);
                    lblCurrentPosition.Text = lblCurrentRole.Text;
                    lblSkills.Text = string.Join(",", currentEmployment.UserEmploymentSkills.Select(s => s.MasterSkill.Description));
                    lblSkillsDetail.Text = lblSkills.Text;
                }

                lblPastPosition.Text = string.Join("<br/>", user.EmploymentHistories.Where(h => h.IsCurrent == false).Select(h => string.Format("{0} at {1}", h.JobTitle, h.MasterCompany != null ? h.MasterCompany.Description : string.Empty)));
                lblEducation.Text = string.Join("<br/>", user.EducationHistories.Where(edu => !string.IsNullOrEmpty(edu.Degree)).Select(education => string.Format("{0} in {1}", education.Degree, education.Description)));
                lblSummary.Text = user.Summary;

                rptrExperience.DataSource = user.EmploymentHistories.Select(emp => new { emp.JobTitle, Company = emp.MasterCompany != null ? emp.MasterCompany.Description : string.Empty, Location = emp.MasterCountry != null ? emp.MasterCountry.Description : string.Empty, town = emp.Town != null ? emp.Town : string.Empty, Period = GetEmploymentPeriod(emp), emp.Description });
                rptrExperience.DataBind();

                rpEducations.DataSource = user.EducationHistories.Select(edu => new { edu.Institution, Course = edu.Degree, Period = GetEducationPeriod(edu) });
                rpEducations.DataBind();

                    lblPhoneNumber.Text = user.PhoneNumber;
                    lblAddress.Text = user.HomeAddress;
                    lblCity.Text = user.City;
                    lblCountry.Text = user.CountryName;
                    if (user.DOB.HasValue)
                        lblBirthDay.Text = user.DOB.Value.ToShortDateString();
                    lblMaritalStatus.Text = user.IsMarried ? "Married" : "Single";
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DisplayUserDetails - UserPDF.aspx");
        }

        private string GetEducationPeriod(EducationHistory history)
        {
            LoggingManager.Debug("Entering GetEducationPeriod - UserPDF.aspx");

            string period = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                period = string.Format("{0} {1} - {2} {3}", history.MasterMonth.Description, history.MasterYear1.Description, history.MasterMonth1.Description, history.MasterYear.Description);
            }
            LoggingManager.Debug("Exiting GetEducationPeriod - UserPDF.aspx");

            return period;
        }

        private string GetEmploymentPeriod(EmploymentHistory history)
        {
            LoggingManager.Debug("Entering GetEmploymentPeriod - UserPDF.aspx");

            string period = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                period = string.Format("{0} {1} - {2} {3}", history.MasterMonth.Description, history.MasterYear1.Description, history.MasterMonth1.Description, history.MasterYear.Description);
            }
            LoggingManager.Debug("Exiting GetEmploymentPeriod - UserPDF.aspx");    
            return period;
        }

    }
}