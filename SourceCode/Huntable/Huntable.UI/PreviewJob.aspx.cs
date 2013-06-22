using System;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class PreviewJob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - PreviewJob.aspx");

            if (!IsPostBack)
            {
                LoadJobDetsFromPostJob();
            }

            LoggingManager.Debug("Exiting Page_Load - PreviewJob.aspx");
        }

        private void LoadJobDetsFromPostJob()
        {
            LoggingManager.Debug("Entering LoadJobDetsFromPostJob - PreviewJob.aspx");

            var tb = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtJobTitle");
            lblJobTitle.Text = tb.Text;

            var tbCompName = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtCompanyName");
            lblCompName.Text = tbCompName.Text;

            var tbSal = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtSalary");
            lblSalary.Text = tbSal.Text;

            var ddlCountry = (DropDownList)PreviousPage.Controls[0].FindControl("MainContent").FindControl("ddlCountry");
            lblCountry.Text = ddlCountry.SelectedItem.Text;
            hdnCountry.Value = ddlCountry.SelectedValue;

            var tbLocName = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtLocationName");
            lblJobLoc.Text = tbLocName.Text;

            var tbjobdesc = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtJobDesc");
            lblJobDescription.Text = tbjobdesc.Text;

            var txtMin = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtMin");
            var txtMax = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtMax");
            lblExperienceRequiredEssential.Text = int.Parse(txtMin.Text) + " - " + int.Parse(txtMax.Text);
            hdnMinExp.Value = txtMin.Text;
            hdnMaxExp.Value = txtMax.Text;

            var ddlJobType = (DropDownList)PreviousPage.Controls[0].FindControl("MainContent").FindControl("ddlJobType");
            lblJobTypeEssential.Text = ddlJobType.SelectedItem.Text;
            hdnJobType.Value = ddlJobType.SelectedValue;

            var ddlIndustry = (DropDownList)PreviousPage.Controls[0].FindControl("MainContent").FindControl("ddlIndustry");
            lblIndustryEssential.Text = ddlIndustry.SelectedItem.Text;
            hdnIndustry.Value = ddlIndustry.SelectedValue;
           var refrence= ((TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtreference"));
            referencenumber.Value = refrence.Text;
            lblrefNumber.Text = referencenumber.Value;
            //var ddlSector = (DropDownList)PreviousPage.Controls[0].FindControl("MainContent").FindControl("ddlSector");
            //lblSector.Text = ddlSector.SelectedItem.Text;
            //hdnSector.Value = ddlSector.SelectedValue;

            var txtSkill = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtSkill");
            lblSkillTypeEssential.Text = txtSkill.Text;
            hdnSkill.Value = txtSkill.Text;

            var ddlSalaryType = (DropDownList)PreviousPage.Controls[0].FindControl("MainContent").FindControl("ddlSalaryType");
            var txtSalary = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtSalary");
            lblSalaryEssential.Text = txtSalary.Text;
            lblCurr.Text = ddlSalaryType.SelectedItem.Text;

            lblSalCurType.Text = ddlSalaryType.SelectedItem.Text;
            hdnSalary.Value = tbSal.Text;
            hdnSalaryType.Value = ddlSalaryType.SelectedItem.Value;

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                lblCurrSymbol.Text = lblSym.Text = (from s in context.MasterCurrencyTypes
                                                    where s.Description == ddlSalaryType.SelectedItem.Text
                                                    select s.Symbol).FirstOrDefault();
            }

            var tbCanprofile = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtCanprofile");
            lblcandProf.Text = tbCanprofile.Text;

            var tbAboutCompany = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtAboutCompany");
            lblAbtComp.Text = tbAboutCompany.Text;

            var tbApplicants = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtApplicants");
            lblRecAppl.Value = tbApplicants.Text;

            var tbExternalApplicant = (TextBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("txtExternalApplicant");
            lblExtSiteAppl.Value = tbExternalApplicant.Text;

            var cbCompany = (CheckBox)PreviousPage.Controls[0].FindControl("MainContent").FindControl("cbCompany");
            chkcbCompany = cbCompany;

            var fuPhoto1 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto1");
            Session["photo1"] = fuPhoto1;

            var fuPhoto2 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto2");
            Session["photo2"] = fuPhoto2;

            var fuPhoto3 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto3");
            Session["photo3"] = fuPhoto3;

            var fuPhoto4 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto4");
            Session["photo4"] = fuPhoto4;

            var fuPhoto5 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto5");
            Session["photo5"] = fuPhoto5;

            var hfPhoto1 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfThumbPhoto1");
            phfPhoto1.Value = hfPhoto1.Value;

            var hfPhoto2 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfThumbPhoto2");
            phfPhoto2.Value = hfPhoto2.Value;

            var hfPhoto3 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfThumbPhoto3");
            phfPhoto3.Value = hfPhoto3.Value;

            var hfPhoto4 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfThumbPhoto4");
            phfPhoto4.Value = hfPhoto4.Value;

            var hfPhoto5 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfThumbPhoto5");
            phfPhoto5.Value = hfPhoto5.Value;

            phfPhoto1Full.Value = ((HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto1")).Value;
            phfPhoto2Full.Value = ((HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto2")).Value;
            phfPhoto3Full.Value = ((HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto3")).Value;
            phfPhoto4Full.Value = ((HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto4")).Value;
            phfPhoto5Full.Value = ((HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hfPhoto5")).Value;

            if (!string.IsNullOrEmpty(hfPhoto1.Value))
                shwProfileImage11.Src = new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto1.Value));
            if (!string.IsNullOrEmpty(hfPhoto2.Value))
                shwProfileImage12.Src = new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto2.Value));
            if (!string.IsNullOrEmpty(hfPhoto3.Value))
                shwProfileImage13.Src = new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto3.Value));
            if (!string.IsNullOrEmpty(hfPhoto4.Value))
                shwProfileImage14.Src = new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto4.Value));
            if (!string.IsNullOrEmpty(hfPhoto5.Value))
                shwProfileImage15.Src = new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto5.Value));

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            if (loggedInUserId != null)
            {
                var result = jobManager.GetUserDetails(loggedInUserId.Value);
                showProfileImage.SmallImageSource = result.UserProfilePictureDisplayUrl;
            }

            LoggingManager.Debug("Exiting LoadJobDetsFromPostJob - PreviewJob.aspx");
        }

        private void UpdateJob(int jobID)
        {
            LoggingManager.Debug("Entering UpdateJob - PreviewJob.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var job = context.Jobs.First(x => x.Id == jobID);
                    UploadPhotos(job);
                    context.SaveChanges();                   
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting UpdateJob - PreviewJob.aspx");
        }

        private void UploadPhotos(Job job)
        {
            LoggingManager.Debug("Entering UploadPhotos - PreviewJob.aspx");
            try
            {
                var pfuPhoto1 = (HiddenField)Session["photo1"];
                
                if (Convert.ToInt32((pfuPhoto1.Value).ToString()) >  0)
                {
                    job.PhotoPath1 = Convert.ToInt32((pfuPhoto1.Value).ToString());
                    job.PhotoThumbPath1 = Convert.ToInt32((phfPhoto1.Value).ToString());
                }
                else
                {

                    job.PhotoPath1 = new Snovaspace.Util.Utility().ConvertToInt(phfPhoto1.Value);
                }

                job.PhotoThumbPath1 = job.PhotoThumbPath1;

                var pfuPhoto2 = (HiddenField)Session["photo2"];
                if (Convert.ToInt32((pfuPhoto2.Value).ToString()) > 0)
                {
                    job.PhotoPath2 = Convert.ToInt32((pfuPhoto2.Value).ToString());
                    job.PhotoThumbPath2 = Convert.ToInt32((phfPhoto2.Value).ToString());
                }
                else
                {
                    job.PhotoPath2 = new Snovaspace.Util.Utility().ConvertToInt(phfPhoto2.Value);
                }

                job.PhotoThumbPath2 = job.PhotoThumbPath2;

                var pfuPhoto3 = (HiddenField)Session["photo3"];
                if (Convert.ToInt32((pfuPhoto3.Value).ToString()) > 0)
                {
                    job.PhotoPath3 = Convert.ToInt32((pfuPhoto3.Value).ToString());
                    job.PhotoThumbPath3 = Convert.ToInt32((phfPhoto3.Value).ToString());
                }
                else
                {
                    job.PhotoPath3 = new Snovaspace.Util.Utility().ConvertToInt(phfPhoto3.Value);
                }

                job.PhotoThumbPath3 = job.PhotoThumbPath3;

                var pfuPhoto4 = (HiddenField)Session["photo4"];
                if (Convert.ToInt32((pfuPhoto4.Value).ToString()) > 0)
                {
                    job.PhotoPath4 = Convert.ToInt32((pfuPhoto4.Value).ToString());
                    job.PhotoThumbPath4 = Convert.ToInt32((phfPhoto4.Value).ToString());
                }
                else
                {
                    job.PhotoPath4 = new Snovaspace.Util.Utility().ConvertToInt(phfPhoto4.Value);
                }

                job.PhotoThumbPath4 = job.PhotoThumbPath4;

                var pfuPhoto5 = (HiddenField)Session["photo5"];
                if (Convert.ToInt32((pfuPhoto5.Value).ToString()) > 0)
                {
                    job.PhotoPath5 = Convert.ToInt32((pfuPhoto5.Value).ToString());
                    job.PhotoThumbPath5 = Convert.ToInt32((phfPhoto5.Value).ToString());
                }
                else
                {
                    job.PhotoPath5 = new Snovaspace.Util.Utility().ConvertToInt(phfPhoto5.Value);
                }

                job.PhotoThumbPath5 = job.PhotoThumbPath5;
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting UploadPhotos - PreviewJob.aspx");
        }

        private Job LoadJobFromUI()
        {
            LoggingManager.Debug("Entering LoadJobFromUI - PreviewJob.aspx");

            var job = new Job
             {
                 Id = 0,
                 Title = lblJobTitle.Text,
                 CompanyName = lblCompName.Text,
                 Salary = int.Parse(hdnSalary.Value),
                 CountryId = int.Parse(hdnCountry.Value),
                 LocationName = lblJobLoc.Text,
                 JobDescription = lblJobDescription.Text,
                 MinExperience = int.Parse(hdnMinExp.Value),
                 MaxExperience = int.Parse(hdnMaxExp.Value),
                 JobTypeId = int.Parse(hdnJobType.Value),
                 IndustryId = int.Parse(hdnIndustry.Value),
                 //SectorId = int.Parse(hdnSector.Value),
                 SkillId = MasterDataManager.GetSkillId(hdnSkill.Value),
                 DesiredCandidateProfile = lblcandProf.Text,
                 AboutYourCompany = lblAbtComp.Text,
                 UserId = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                 ReceiveApplicants = lblRecAppl.Value,
                 ExternalSiteApplicant = lblExtSiteAppl.Value,
                 IsCompanyLogo = chkcbCompany.Checked,
                 SalaryCurrencyId = int.Parse(hdnSalaryType.Value),
                 ReferenceNumber = referencenumber.Value
             };
            LoggingManager.Debug("Exiting LoadJobFromUI - PreviewJob.aspx");

            return job;

        }
        private int? GetSkillId()
        {
            LoggingManager.Debug("Entering GetSkillId - PreviewJob.aspx");
            var skill = MasterDataManager.AllSkills.Where(s => s.Description.ToLower() == hdnSkill.Value.ToLower()).FirstOrDefault();
            if (skill == null)
            {
                return null;
            }
            else
            {
                LoggingManager.Debug("Exiting GetSkillId - PreviewJob.aspx");
                return skill.Id;
            }

        }

        protected void LbPostNowCLick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering LbPostNowCLick - PreviewJob.aspx");
            try
            {
                LoggingManager.Info("Inside Job Posting");
                var loggedInUserId = Common.GetLoggedInUserId(Session);

                if (loggedInUserId != null)
                {
                    var jobManager = new JobsManager();
                    var usrdeatils = new InvitationManager();
                    var result = usrdeatils.GetUserDetails(loggedInUserId.Value);
                    int? credit = result.CreditsLeft;
                    if (credit > 0)
                    {
                        int jobID = jobManager.SaveJob(LoadJobFromUI());
                        LoggingManager.Info("inserted/updated Job ID = " + jobID);
                        UpdateJob(jobID);
                        Response.Write(
                            "<script language='javascript'>alert('Job Posted Successfully');document.location='MyPostedJobs.aspx';</script>");
                        LoggingManager.Info("Job Insert/update is done and redirecting to My Posted Jobs page.");
                    }
                    else
                    {
                        Response.Write(
                           "<script language='javascript'>alert('You dont have enough Credis.Please buy credits');document.location='MyPostedJobs.aspx';</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LbPostNowCLick - PreviewJob.aspx");
        }
    }
}