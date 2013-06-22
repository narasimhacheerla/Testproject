using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using System.Web.UI.WebControls;

namespace Huntable.UI
{
    public partial class PostJob : Page
    {
        public const string PicturesStateViewStateKey = "Pictures";
        public const string PicturesStateViewStateKey1 = "Picturesm";
        public static IList<int?> Zi = new List<int?>();
        public static IList<int?> Ji = new List<int?>();
        public static IList<int?> Se = new List<int?>();
        public static IList<DateTime> Dt = new List<DateTime>();
        public string Companyname;
        public IDictionary<string, int> Pictures
        {
            get
            {
                LoggingManager.Debug("Entering Pictures - PostJob");
                LoggingManager.Debug("Entering Pictures - PostJob");


                return (IDictionary<string, int>)(ViewState[PicturesStateViewStateKey] ?? new Dictionary<string, int>());
            }
            set
            {
                ViewState[PicturesStateViewStateKey] = value;
            }
        }
        public IDictionary<string, int> Picturesm
        {
            get
            {
                LoggingManager.Debug("Entering Picturesm - PostJob");
                LoggingManager.Debug("Entering Picturesm - PostJob");

                return (IDictionary<string, int>)(ViewState[PicturesStateViewStateKey1] ?? new Dictionary<string, int>());
            }
            set
            {
                ViewState[PicturesStateViewStateKey1] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering page_Load - PostJob");

            try
            {
                if (!IsPostBack)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var loggedInUserId = Common.GetLoggedInUserId(Session);
                        var jobManager = new InvitationManager();
                        if (loggedInUserId != null)
                        {
                            var result = jobManager.GetUserDetails(loggedInUserId.Value);
                            int? credit = result.CreditsLeft;
                            lblCredit.Text = ((credit >= 0) ? credit : 0).ToString();
                            imgJob.ImageUrl = result.UserProfilePictureDisplayUrl;
                            Pictures = new Dictionary<string, int>(Pictures);
                            Picturesm = new Dictionary<string, int>(Picturesm);
                        }

                        var usr = context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
                        if (usr != null && usr.IsCompany == true)
                        {
                            var firstOrDefault = context.Companies.FirstOrDefault(x => x.Userid == usr.Id);
                            if (firstOrDefault != null)
                            {
                                string cmpname = firstOrDefault.CompanyName;
                                txtCompanyName.Text = cmpname;
                            }
                            Companyname = txtCompanyName.Text;
                            txtCompanyName.Attributes.Add("readonly", "true");

                        }
                        else
                        {
                            Companyname = txtCompanyName.Text;
                        }
                    }

                    {
                        BindCountries();
                        BindJobType();
                        BindIndustry();
                        BindSalaryType();
                        GetUserJobApplications();
                        if (Request.Url.Query.Contains("id"))
                            PopulateJob(Convert.ToInt32(Request.QueryString["id"]));
                    }

                    if (Request.QueryString["from"] == "Preview")
                    {
                        var tb = (Label)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblJobTitle");
                        txtJobTitle.Text = tb.Text;

                        var tbCompName = (Label)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblCompName");

                        txtCompanyName.Text = tbCompName.Text;

                        Label tbSal = (Label)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblSalary");
                        txtSalary.Text = tbSal.Text;

                        HiddenField hdnCountry = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hdnCountry");
                        ddlCountry.SelectedValue = hdnCountry.Value;

                        Label tbLocName = (Label)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblJobLoc");
                        txtLocationName.Text = tbLocName.Text;

                        Label tbjobdesc = (Label)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblJobDescription");
                        txtJobDesc.Text = tbjobdesc.Text;

                        HiddenField hdnMin = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hdnMinExp");
                        txtMin.Text = hdnMin.Value;

                        HiddenField hdnMax = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hdnMaxExp");
                        txtMax.Text = hdnMax.Value;

                        HiddenField hdnJobType = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hdnJobType");
                        ddlJobType.SelectedValue = hdnJobType.Value;

                        HiddenField hdnIndustry = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hdnIndustry");
                        ddlIndustry.SelectedValue = hdnIndustry.Value;

                        HiddenField hdnSkill = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hdnSkill");
                        txtSkill.Text = hdnSkill.Value;

                        HiddenField hdnSalaryType = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("hdnSalaryType");
                        ddlSalaryType.SelectedValue = hdnSalaryType.Value;

                        HiddenField hdnrefNumber = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("referencenumber");
                        txtreference.Text = hdnrefNumber.Value;

                        Label lblcandProf = (Label)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblcandProf");
                        txtCanprofile.Text = lblcandProf.Text;

                        Label lblAbtComp = (Label)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblAbtComp");
                        txtAboutCompany.Text = lblAbtComp.Text;

                        var lblRecAppl = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblRecAppl");
                        txtApplicants.Text = lblRecAppl.Value;

                        var lblExtSiteAppl = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("lblExtSiteAppl");
                        txtExternalApplicant.Text = lblExtSiteAppl.Value;

                        var fuPhoto1 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto1");
                        var fuPhoto2 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto2");
                        var fuPhoto3 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto3");
                        var fuPhoto4 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto4");
                        var fuPhoto5 = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto5");

                        var fuPhoto1Full = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto1Full");
                        var fuPhoto2Full = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto2Full");
                        var fuPhoto3Full = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto3Full");
                        var fuPhoto4Full = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto4Full");
                        var fuPhoto5Full = (HiddenField)PreviousPage.Controls[0].FindControl("MainContent").FindControl("phfPhoto5Full");

                     
                        if (!string.IsNullOrEmpty(fuPhoto5.Value))
                        {
                            Pictures.Add(fuPhoto1Full.Value.ToString(), Convert.ToInt32(fuPhoto1Full.Value.ToString()));
                            Picturesm.Add(fuPhoto1.Value.ToString(), Convert.ToInt32(fuPhoto1.Value.ToString()));
                            Pictures.Add(fuPhoto2Full.Value.ToString(), Convert.ToInt32(fuPhoto2Full.Value.ToString()));
                            Picturesm.Add(fuPhoto2.Value.ToString(), Convert.ToInt32(fuPhoto2.Value.ToString()));
                            Pictures.Add(fuPhoto3Full.Value.ToString(), Convert.ToInt32(fuPhoto3Full.Value.ToString()));
                            Picturesm.Add(fuPhoto3.Value.ToString(), Convert.ToInt32(fuPhoto3.Value.ToString()));
                            Pictures.Add(fuPhoto4Full.Value.ToString(), Convert.ToInt32(fuPhoto4Full.Value.ToString()));
                            Picturesm.Add(fuPhoto4.Value.ToString(), Convert.ToInt32(fuPhoto4.Value.ToString()));
                            Pictures.Add(fuPhoto5Full.Value.ToString(), Convert.ToInt32(fuPhoto5Full.Value.ToString()));
                            Picturesm.Add(fuPhoto5.Value.ToString(), Convert.ToInt32(fuPhoto5.Value.ToString()));
                            //hfPhoto5.Value = fuPhoto5Full.Value;
                            //hfThumbPhoto5.Value = fuPhoto5.Value;
                        }
                        else if (!string.IsNullOrEmpty(fuPhoto4.Value) && string.IsNullOrEmpty(fuPhoto5.Value))
                        {
                            Pictures.Add(fuPhoto1Full.Value.ToString(), Convert.ToInt32(fuPhoto1Full.Value.ToString()));
                            Picturesm.Add(fuPhoto1.Value.ToString(), Convert.ToInt32(fuPhoto1.Value.ToString()));
                            Pictures.Add(fuPhoto2Full.Value.ToString(), Convert.ToInt32(fuPhoto2Full.Value.ToString()));
                            Picturesm.Add(fuPhoto2.Value.ToString(), Convert.ToInt32(fuPhoto2.Value.ToString()));
                            Pictures.Add(fuPhoto3Full.Value.ToString(), Convert.ToInt32(fuPhoto3Full.Value.ToString()));
                            Picturesm.Add(fuPhoto3.Value.ToString(), Convert.ToInt32(fuPhoto3.Value.ToString()));
                            Pictures.Add(fuPhoto4Full.Value.ToString(), Convert.ToInt32(fuPhoto4Full.Value.ToString()));
                            Picturesm.Add(fuPhoto4.Value.ToString(), Convert.ToInt32(fuPhoto4.Value.ToString()));
                            //hfPhoto4.Value = fuPhoto4Full.Value;
                            //hfThumbPhoto4.Value = fuPhoto4.Value;
                        }
                        else if (!string.IsNullOrEmpty(fuPhoto3.Value) && string.IsNullOrEmpty(fuPhoto4.Value) && string.IsNullOrEmpty(fuPhoto5.Value))
                        {
                            Pictures.Add(fuPhoto1Full.Value.ToString(), Convert.ToInt32(fuPhoto1Full.Value.ToString()));
                            Picturesm.Add(fuPhoto1.Value.ToString(), Convert.ToInt32(fuPhoto1.Value.ToString()));
                            Pictures.Add(fuPhoto2Full.Value.ToString(), Convert.ToInt32(fuPhoto2Full.Value.ToString()));
                            Picturesm.Add(fuPhoto2.Value.ToString(), Convert.ToInt32(fuPhoto2.Value.ToString()));
                            Pictures.Add(fuPhoto3Full.Value.ToString(), Convert.ToInt32(fuPhoto3Full.Value.ToString()));
                            Picturesm.Add(fuPhoto3.Value.ToString(), Convert.ToInt32(fuPhoto3.Value.ToString()));
                            //hfPhoto3.Value = fuPhoto3Full.Value;
                            //hfThumbPhoto3.Value = fuPhoto3.Value;
                        }
                        else if (!string.IsNullOrEmpty(fuPhoto2.Value) && string.IsNullOrEmpty(fuPhoto3.Value) && string.IsNullOrEmpty(fuPhoto4.Value) && string.IsNullOrEmpty(fuPhoto5.Value))
                        {
                            Pictures.Add(fuPhoto1Full.Value.ToString(), Convert.ToInt32(fuPhoto1Full.Value.ToString()));
                            Picturesm.Add(fuPhoto1.Value.ToString(), Convert.ToInt32(fuPhoto1.Value.ToString()));
                            Pictures.Add(fuPhoto2Full.Value.ToString(), Convert.ToInt32(fuPhoto2Full.Value.ToString()));
                            Picturesm.Add(fuPhoto2.Value.ToString(), Convert.ToInt32(fuPhoto2.Value.ToString()));
                            //hfPhoto2.Value = fuPhoto2Full.Value;
                            //hfThumbPhoto2.Value = fuPhoto2.Value;
                        }
                        else if (!string.IsNullOrEmpty(fuPhoto1.Value) && string.IsNullOrEmpty(fuPhoto2.Value) && string.IsNullOrEmpty(fuPhoto3.Value) && string.IsNullOrEmpty(fuPhoto4.Value) && string.IsNullOrEmpty(fuPhoto5.Value))
                        {
                            Pictures.Add(fuPhoto1Full.Value.ToString(), Convert.ToInt32(fuPhoto1Full.Value.ToString()));
                            Picturesm.Add(fuPhoto1.Value.ToString(), Convert.ToInt32(fuPhoto1.Value.ToString()));
                            //hfPhoto1.Value = fuPhoto1Full.Value;
                            //hfThumbPhoto1.Value = fuPhoto1.Value;
                        }
                     

                      
                     

                        //ResetImages();
                        rpic.DataSource = Pictures.Values;
                        rpic.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting page load - Post Job");
        }

        private void GetUserJobApplications()
        {
            LoggingManager.Debug("Entering GetUserJobApplications - PostJob");
            try
            {
                var jobManager = new JobsManager();
                lblPostedJobs.Text = "My Posted jobs: " + jobManager.GetUserJobs(Convert.ToInt32(Common.GetLoggedInUserId(Session)));
                var count = new JobsManager().MyApplicants(Convert.ToInt32(Common.GetLoggedInUserId(Session)));
                lblMyApplications.Text = "     My Applicants: " + count.Count;
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting GetUserJobApplications - PostJob");

        }
     
        private void BindCountries()
        {
            LoggingManager.Debug("Entering BindCountries - PostJob");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var masterCountriesList = context.MasterCountries.ToList();
                    ddlCountry.DataTextField = "Description";
                    ddlCountry.DataValueField = "Id";
                    ddlCountry.DataSource = masterCountriesList;


                    ddlCountry.DataBind();

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindCountries - PostJob");
        }


        private void BindJobType()
        {
            LoggingManager.Debug("Entering BindJobType - PostJob");
            try
            {

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var masterJobTypeList = context.MasterJobTypes.ToList();
                    ddlJobType.DataTextField = "Description";
                    ddlJobType.DataValueField = "Id";
                    ddlJobType.DataSource = masterJobTypeList;


                    ddlJobType.DataBind();

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindJobType - PostJob");
        }

        private void BindIndustry()
        {
            LoggingManager.Debug("Entering BindIndustry - PostJob");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var masterIndustriesList = context.MasterIndustries.ToList();
                    ddlIndustry.DataTextField = "Description";
                    ddlIndustry.DataValueField = "Id";
                    ddlIndustry.DataSource = masterIndustriesList;


                    ddlIndustry.DataBind();

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindIndustry - PostJob");

        }

        private void BindSalaryType()
        {
            LoggingManager.Debug("Entering BindSalaryType - PostJob");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var masterCurrencyType = context.MasterCurrencyTypes.ToList();
                    ddlSalaryType.DataTextField = "Description";
                    ddlSalaryType.DataValueField = "Id";
                    ddlSalaryType.DataSource = masterCurrencyType;

                    ddlSalaryType.DataBind();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindSalaryType - PostJob");
        }

        protected void BtnPostNowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnPostNowClick - Post Job");
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
            LoggingManager.Debug("Exiting btnPostNowClick - PostJob");
        }

        private Job LoadJobFromUI()
        {
            LoggingManager.Debug("Entering LoadJobFromUI - PostJob");


            Companyname = txtCompanyName.Text;

            var job = new Job
            {
                Id = Request.Url.Query.Contains("id") ? Convert.ToInt32(Request.QueryString["id"]) : 0,
                Title = txtJobTitle.Text,
                CompanyName = Companyname,
                Salary = int.Parse(txtSalary.Text),
                CountryId = int.Parse(ddlCountry.SelectedValue),
                LocationName = txtLocationName.Text,
                JobDescription = txtJobDesc.Text,
                MinExperience = int.Parse(txtMin.Text),
                MaxExperience = int.Parse(txtMax.Text),
                JobTypeId = int.Parse(ddlJobType.SelectedValue),
                IndustryId = int.Parse(ddlIndustry.SelectedValue),
                //SectorId = int.Parse(ddlSector.SelectedValue),
                SkillId = MasterDataManager.GetSkillId(txtSkill.Text),
                DesiredCandidateProfile = txtCanprofile.Text,
                AboutYourCompany = txtAboutCompany.Text,
                UserId = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                ReceiveApplicants = txtApplicants.Text,
                ExternalSiteApplicant = txtExternalApplicant.Text,
                //IsCompanyLogo = cbCompany.Checked,
                SalaryCurrencyId = int.Parse(ddlSalaryType.SelectedValue),
                ReferenceNumber = txtreference.Text
            };
            LoggingManager.Debug("Exiting LoadJobFromUI - PostJob");

            return job;

        }


        private void PopulateJob(int jobID)
        {
            LoggingManager.Debug("Entering PopulateJob - PostJob");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    Job data = context.Jobs.SingleOrDefault(x => x.Id == jobID);
                    if (data != null)
                    {
                        txtJobTitle.Text = data.Title;
                        txtCompanyName.Text = data.CompanyName;
                        txtSalary.Text = Convert.ToString(data.Salary);
                        ddlCountry.SelectedValue = Convert.ToString(data.CountryId);
                        txtLocationName.Text = data.LocationName;
                        txtJobDesc.Text = data.JobDescription;
                        txtMin.Text = Convert.ToString(data.MinExperience);
                        txtMax.Text = Convert.ToString(data.MaxExperience);
                        ddlJobType.SelectedValue = Convert.ToString(data.JobTypeId);
                        ddlIndustry.SelectedValue = Convert.ToString(data.IndustryId);
                        //ddlSector.SelectedValue = Convert.ToString(data.SectorId);
                        txtSkill.Text = data.MasterSkill.Description;
                        txtCanprofile.Text = data.DesiredCandidateProfile;
                        txtAboutCompany.Text = data.AboutYourCompany;
                        //cbCompany.Checked = data.IsCompanyLogo;
                        txtApplicants.Text = data.ReceiveApplicants;
                        txtExternalApplicant.Text = data.ExternalSiteApplicant;
                        //hfPhoto1.Value = Convert.ToString(data.PhotoPath1);
                        //hfThumbPhoto1.Value = Convert.ToString(data.PhotoThumbPath1);
                        //hfPhoto2.Value = Convert.ToString(data.PhotoPath2);
                        //hfThumbPhoto2.Value = Convert.ToString(data.PhotoThumbPath2);
                        //hfPhoto3.Value = Convert.ToString(data.PhotoPath3);
                        //hfThumbPhoto3.Value = Convert.ToString(data.PhotoThumbPath3);
                        //hfPhoto4.Value = Convert.ToString(data.PhotoPath4);
                        //hfThumbPhoto4.Value = Convert.ToString(data.PhotoThumbPath4);
                        //hfPhoto5.Value = Convert.ToString(data.PhotoPath5);
                        //hfThumbPhoto5.Value = Convert.ToString(data.PhotoThumbPath5);
                        txtreference.Text =Convert.ToString(data.ReferenceNumber);
                        //ResetImages();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting PopulateJob - PostJob");

        }

        private void UpdateJob(int jobID)
        {
            LoggingManager.Debug("Entering UpdateJob - PostJob");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var job = context.Jobs.First(x => x.Id == jobID);
                    Uplot(job);
                    //UploadPhotos(job);
                    context.SaveChanges();                    
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting UpdateJob - PostJob");

        }

        protected void TempPhotosUpload(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering TempPhotosUpload - PostJob");

        //    hfThumbPhoto1.Value = fuPhoto1.HasFile ? Convert.ToString(new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto1, 65, 57)) : hfThumbPhoto1.Value;
        //    hfThumbPhoto2.Value = fuPhoto2.HasFile ? Convert.ToString(new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto2, 65, 57)) : hfThumbPhoto2.Value;
        //    hfThumbPhoto3.Value = fuPhoto3.HasFile ? Convert.ToString(new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto3, 65, 57)) : hfThumbPhoto3.Value;
        //    hfThumbPhoto4.Value = fuPhoto4.HasFile ? Convert.ToString(new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto4, 65, 57)) : hfThumbPhoto4.Value;
        //    hfThumbPhoto5.Value = fuPhoto5.HasFile ? Convert.ToString(new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto5, 65, 57)) : hfThumbPhoto5.Value;

        //    hfPhoto1.Value = fuPhoto1.HasFile ? Convert.ToString(new FileStoreService().LoadImageAndResize("JobImages", fuPhoto1)) : hfPhoto1.Value;
        //    hfPhoto2.Value = fuPhoto2.HasFile ? Convert.ToString(new FileStoreService().LoadImageAndResize("JobImages", fuPhoto2)) : hfPhoto2.Value;
        //    hfPhoto3.Value = fuPhoto3.HasFile ? Convert.ToString(new FileStoreService().LoadImageAndResize("JobImages", fuPhoto3)) : hfPhoto3.Value;
        //    hfPhoto4.Value = fuPhoto4.HasFile ? Convert.ToString(new FileStoreService().LoadImageAndResize("JobImages", fuPhoto4)) : hfPhoto4.Value;
        //    hfPhoto5.Value = fuPhoto5.HasFile ? Convert.ToString(new FileStoreService().LoadImageAndResize("JobImages", fuPhoto5)) : hfPhoto5.Value;
            if (Pictures.Count == 5)
            {

                hfPhoto1.Value = Convert.ToString(Pictures.ElementAt(0).Value);
                hfThumbPhoto1.Value = Convert.ToString(Picturesm.ElementAt(0).Value);

                hfPhoto2.Value = Convert.ToString(Pictures.ElementAt(1).Value);
                hfThumbPhoto2.Value = Convert.ToString(Picturesm.ElementAt(1).Value);

                hfPhoto3.Value = Convert.ToString(Pictures.ElementAt(2).Value);
                hfThumbPhoto3.Value = Convert.ToString(Picturesm.ElementAt(2).Value);

                hfPhoto4.Value = Convert.ToString(Pictures.ElementAt(3).Value);
                hfThumbPhoto4.Value = Convert.ToString(Picturesm.ElementAt(3).Value);

                hfPhoto5.Value = Convert.ToString(Pictures.ElementAt(4).Value);
                hfThumbPhoto5.Value = Convert.ToString(Picturesm.ElementAt(4).Value);

            }
            else if (Pictures.Count == 4)
            {
                hfPhoto1.Value = Convert.ToString(Pictures.ElementAt(0).Value);
                hfThumbPhoto1.Value = Convert.ToString(Picturesm.ElementAt(0).Value);

                hfPhoto2.Value = Convert.ToString(Pictures.ElementAt(1).Value);
                hfThumbPhoto2.Value = Convert.ToString(Picturesm.ElementAt(1).Value);

                hfPhoto3.Value = Convert.ToString(Pictures.ElementAt(2).Value);
                hfThumbPhoto3.Value = Convert.ToString(Picturesm.ElementAt(2).Value);

                hfPhoto4.Value = Convert.ToString(Pictures.ElementAt(3).Value);
                hfThumbPhoto4.Value = Convert.ToString(Picturesm.ElementAt(3).Value);
            }
            else if (Pictures.Count == 3)
            {
                hfPhoto1.Value = Convert.ToString(Pictures.ElementAt(0).Value);
                hfThumbPhoto1.Value = Convert.ToString(Picturesm.ElementAt(0).Value);

                hfPhoto2.Value = Convert.ToString(Pictures.ElementAt(1).Value);
                hfThumbPhoto2.Value = Convert.ToString(Picturesm.ElementAt(1).Value);

                hfPhoto3.Value = Convert.ToString(Pictures.ElementAt(2).Value);
                hfThumbPhoto3.Value = Convert.ToString(Picturesm.ElementAt(2).Value);
            }
            else if (Pictures.Count == 2)
            {
                hfPhoto1.Value = Convert.ToString(Pictures.ElementAt(0).Value);
                hfThumbPhoto1.Value = Convert.ToString(Picturesm.ElementAt(0).Value);

                hfPhoto2.Value = Convert.ToString(Pictures.ElementAt(1).Value);
                hfThumbPhoto2.Value = Convert.ToString(Picturesm.ElementAt(1).Value);
            }
            else if (Pictures.Count == 1)
            {
                hfPhoto1.Value = Convert.ToString(Pictures.ElementAt(0).Value);
                hfThumbPhoto1.Value = Convert.ToString(Picturesm.ElementAt(0).Value);
            }
            LoggingManager.Debug("Exiting TempPhotosUpload - PostJob");
        }

        //private void UploadPhotos(Job job)
        //{
        //    LoggingManager.Debug("Entering UploadPhotos - Post Job");
        //    try
        //    {
        //        if (fuPhoto1.FileName.Length > 0)
        //        {
        //            job.PhotoPath1 = new FileStoreService().LoadFileFromFileUpload("JobImages", fuPhoto1);
        //            job.PhotoThumbPath1 = new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto1, 65, 57);

        //            hfPhoto1.Value = Convert.ToString(job.PhotoPath1);
        //            hfThumbPhoto1.Value = Convert.ToString(job.PhotoThumbPath1);
        //        }
        //        else
        //        {
        //            job.PhotoThumbPath1 = new Snovaspace.Util.Utility().ConvertToInt(hfThumbPhoto1.Value);
        //            job.PhotoPath1 = new Snovaspace.Util.Utility().ConvertToInt(hfPhoto1.Value);
        //        }

        //        if (fuPhoto2.FileName.Length > 0)
        //        {
        //            job.PhotoPath2 = new FileStoreService().LoadFileFromFileUpload("JobImages", fuPhoto2);
        //            job.PhotoThumbPath2 = new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto2, 65, 57);

        //            hfPhoto2.Value = Convert.ToString(job.PhotoPath2);
        //            hfThumbPhoto2.Value = Convert.ToString(job.PhotoThumbPath2);
        //        }
        //        else
        //        {
        //            job.PhotoThumbPath2 = new Snovaspace.Util.Utility().ConvertToInt(hfThumbPhoto2.Value);
        //            job.PhotoPath2 = new Snovaspace.Util.Utility().ConvertToInt(hfPhoto2.Value);
        //        }

        //        if (fuPhoto3.FileName.Length > 0)
        //        {
        //            job.PhotoPath3 = new FileStoreService().LoadFileFromFileUpload("JobImages", fuPhoto3);
        //            job.PhotoThumbPath3 = new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto3, 65, 57);

        //            hfPhoto3.Value = Convert.ToString(job.PhotoPath3);
        //            hfThumbPhoto3.Value = Convert.ToString(job.PhotoThumbPath3);
        //        }
        //        else
        //        {
        //            job.PhotoThumbPath3 = new Snovaspace.Util.Utility().ConvertToInt(hfThumbPhoto3.Value);
        //            job.PhotoPath3 = new Snovaspace.Util.Utility().ConvertToInt(hfPhoto3.Value);
        //        }

        //        if (fuPhoto4.FileName.Length > 0)
        //        {
        //            job.PhotoPath4 = new FileStoreService().LoadFileFromFileUpload("JobImages", fuPhoto4);
        //            job.PhotoThumbPath4 = new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto4, 65, 57);

        //            hfPhoto4.Value = Convert.ToString(job.PhotoPath4);
        //            hfThumbPhoto4.Value = Convert.ToString(job.PhotoThumbPath4);
        //        }
        //        else
        //        {
        //            job.PhotoThumbPath4 = new Snovaspace.Util.Utility().ConvertToInt(hfThumbPhoto4.Value);
        //            job.PhotoPath4 = new Snovaspace.Util.Utility().ConvertToInt(hfPhoto4.Value);
        //        }

        //        if (fuPhoto5.FileName.Length > 0)
        //        {
        //            job.PhotoPath5 = new FileStoreService().LoadFileFromFileUpload("JobImages", fuPhoto5);
        //            job.PhotoThumbPath5 = new FileStoreService().LoadImageFromFileUpload("JobImages", fuPhoto5, 65, 57);

        //            hfPhoto5.Value = Convert.ToString(job.PhotoPath5);
        //            hfThumbPhoto5.Value = Convert.ToString(job.PhotoThumbPath5);
        //        }
        //        else
        //        {
        //            job.PhotoThumbPath5 = new Snovaspace.Util.Utility().ConvertToInt(hfThumbPhoto4.Value);
        //            job.PhotoPath5 = new Snovaspace.Util.Utility().ConvertToInt(hfPhoto5.Value);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingManager.Error(ex);
        //    }
        //    LoggingManager.Debug("Exiting UploadPhotos - Post Job");

        //}

        protected void DeleteIcon1Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering DeleteIcon1Click - PostJob");
            //hfPhoto1.Value = null;
            //hfThumbPhoto1.Value = null;
            //ResetImages();
            LoggingManager.Debug("Exiting DeleteIcon1Click - PostJob");

        }

        //private void ResetImages()
        //{
        //    LoggingManager.Debug("Entering ResetImages - Post Job");

        //    try
        //    {
        //        deleteIcon1.Visible = !string.IsNullOrEmpty(hfPhoto1.Value);
        //        deleteIcon2.Visible = !string.IsNullOrEmpty(hfPhoto2.Value);
        //        deleteIcon3.Visible = !string.IsNullOrEmpty(hfPhoto3.Value);
        //        deleteIcon4.Visible = !string.IsNullOrEmpty(hfPhoto4.Value);
        //        deleteIcon5.Visible = !string.IsNullOrEmpty(hfPhoto5.Value);

        //        fuPhoto1.Visible = string.IsNullOrEmpty(hfPhoto1.Value);
        //        fuPhoto2.Visible = string.IsNullOrEmpty(hfPhoto2.Value);
        //        fuPhoto3.Visible = string.IsNullOrEmpty(hfPhoto3.Value);
        //        fuPhoto4.Visible = string.IsNullOrEmpty(hfPhoto4.Value);
        //        fuPhoto5.Visible = string.IsNullOrEmpty(hfPhoto5.Value);

        //        phAddExample1.Controls.Clear();
        //        phAddExample2.Controls.Clear();
        //        phAddExample3.Controls.Clear();
        //        phAddExample4.Controls.Clear();
        //        phAddExample5.Controls.Clear();

        //        if (!string.IsNullOrWhiteSpace(hfPhoto1.Value) && !string.IsNullOrWhiteSpace(hfThumbPhoto1.Value))
        //        {
        //            string strExample1 = "<a id=\"example1\" href=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto1.Value)) + "\"><img alt=\"example1\" src=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfThumbPhoto1.Value)) + "\" /></a>";
        //            phAddExample1.Controls.Add(new LiteralControl(strExample1));
        //        }

        //        if (!string.IsNullOrWhiteSpace(hfPhoto2.Value) && !string.IsNullOrWhiteSpace(hfThumbPhoto2.Value))
        //        {
        //            string strExample2 = "<a id=\"example2\" href=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto2.Value)) + "\"><img alt=\"example2\" src=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfThumbPhoto2.Value)) + "\" /></a>";
        //            phAddExample2.Controls.Add(new LiteralControl(strExample2));
        //        }

        //        if (!string.IsNullOrWhiteSpace(hfPhoto3.Value) && !string.IsNullOrWhiteSpace(hfThumbPhoto3.Value))
        //        {
        //            string strExample3 = "<a id=\"example3\" href=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto3.Value)) + "\"><img alt=\"example3\" src=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfThumbPhoto3.Value)) + "\" /></a>";
        //            phAddExample3.Controls.Add(new LiteralControl(strExample3));

        //        }

        //        if (!string.IsNullOrWhiteSpace(hfPhoto4.Value) && !string.IsNullOrWhiteSpace(hfThumbPhoto4.Value))
        //        {
        //            string strExample4 = "<a id=\"example4\" href=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto4.Value)) + "\"><img alt=\"example4\" src=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfThumbPhoto4.Value)) + "\" /></a>";
        //            phAddExample4.Controls.Add(new LiteralControl(strExample4));

        //        }

        //        if (!string.IsNullOrWhiteSpace(hfPhoto5.Value) && !string.IsNullOrWhiteSpace(hfThumbPhoto5.Value))
        //        {
        //            string strExample5 = "<a id=\"example5\" href=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfPhoto5.Value)) + "\"><img alt=\"example5\" src=\"" + new FileStoreService().GetDownloadUrl(Convert.ToInt32(hfThumbPhoto5.Value)) + "\" /></a>";
        //            phAddExample5.Controls.Add(new LiteralControl(strExample5));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingManager.Error(ex);
        //    }
        //    LoggingManager.Debug("Exiting ResetImages - Post Job");
        //}

        //protected void DeleteIcon2Click(object sender, ImageClickEventArgs e)
        //{
        //    LoggingManager.Debug("Entering DeleteIcon2Click - Post Job");
        //    try
        //    {
        //        //hfPhoto2.Value = null;
        //        //hfThumbPhoto2.Value = null;
        //        ResetImages();
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingManager.Error(ex);
        //    }
        //    LoggingManager.Debug("Exiting DeleteIcon2Click - Post Job");

        //}
        protected void DeleteIcon3Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering DeleteIcon3Click - PostJob");
            try
            {
                //hfPhoto3.Value = null;
                //hfThumbPhoto3.Value = null;
                //ResetImages();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteIcon3Click - PostJob");

        }
        protected void DeleteIcon4Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering DeleteIcon4Click - PostJob");
            try
            {
                //hfPhoto4.Value = null;
                //hfThumbPhoto4.Value = null;
                //ResetImages();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteIcon4Click - PostJob");
        }
        protected void DeleteIcon5Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering DeleteIcon5Click - PostJob");
            try
            {
                //hfPhoto5.Value = null;
                //hfThumbPhoto5.Value = null;
                //ResetImages();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteIcon5Click - PostJob");

        }

        protected void LnkcreditClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering LnkcreditClick - PostJob");
            try
            {
                LoggingManager.Info("Redirecting to Buy Credit page.");
                Response.Redirect("BuyCredit.aspx", false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LnkcreditClick - PostJob");
        }

        protected void BtnCandidateSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCandidateSearchClick - PostJob");
            try
            {
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
                        Response.Write("<script language='javascript'>alert('Job Posted Successfully');document.location='MyPostedJobs.aspx';</script>");
                        //Response.Redirect("UserSearch.aspx?keyword=" + txtSkill.Text, false);
                        Response.Redirect("UserSearch.aspx?Skill=" + txtSkill.Text + "&Industry=" + ddlIndustry.SelectedItem.Text + "&Country=" + ddlCountry.SelectedItem.Text, false);
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
            LoggingManager.Debug("Exiting BtnCandidateSearchClick - Post Job");
        }

        protected void BtnBuyCreidtsClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnBuyCreidtsClick - PostJob");
            try
            {
                LoggingManager.Info("Redirecting to Buy Credit page.");
                Response.Redirect("BuyCredit.aspx", false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnBuyCreidtsClick - PostJob");
        }

        protected void lbPostNowCLick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbPostNowCLick - PostJob");
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
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Job posted succesfully ')", true);
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
                LoggingManager.Info("Job Insert/update is done and redirecting to My Posted Jobs page.");
                //Response.Redirect("MyPostedJobs.aspx", false);

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting lbPostNowCLick - PostJob");
        }
        protected void Addpic(object sender, EventArgs e)
        {
            if (fp.HasFile && Pictures.Count <= 4)
            {
                LoggingManager.Debug("Entering Addpic - PostJob");

                var filestore = new FileStoreService();
                int? imId = filestore.LoadImageAndResize("picture", fp);
                int? smId = new FileStoreService().LoadImageFromFileUpload("JobImages", fp, 65, 57);
                Pictures.Add(imId.ToString(), (int)imId);
                Picturesm.Add(smId.ToString(), (int)smId);
                //string des = txd.Text;
                DateTime dat = DateTime.Now;
                Dt.Add(dat);
                Se.Add(smId);
                Zi.Add(imId);

                rpic.DataSource = Pictures.Values;
                rpic.DataBind();
                LoggingManager.Debug("Exiting Addpic - PostJob");

                //txd.Text = "";


            }

        }
        protected void Uplot(Job job)
        {
            LoggingManager.Debug("Entering Uplot - PostJob");
            if (Pictures.Count == 5)
            {
                job.PhotoPath1 = Pictures.ElementAt(0).Value;
                job.PhotoThumbPath1 = Picturesm.ElementAt(0).Value;
                hfPhoto1.Value = Convert.ToString(job.PhotoPath1);
                hfThumbPhoto1.Value = Convert.ToString(job.PhotoThumbPath1);
                job.PhotoPath2 = Pictures.ElementAt(1).Value;
                job.PhotoThumbPath2 = Picturesm.ElementAt(1).Value;
                hfPhoto2.Value = Convert.ToString(job.PhotoPath2);
                hfThumbPhoto2.Value = Convert.ToString(job.PhotoThumbPath2);
                job.PhotoPath3 = Pictures.ElementAt(2).Value;
                job.PhotoThumbPath3 = Picturesm.ElementAt(2).Value;
                hfPhoto3.Value = Convert.ToString(job.PhotoPath3);
                hfThumbPhoto3.Value = Convert.ToString(job.PhotoThumbPath3);
                job.PhotoPath4 = Pictures.ElementAt(3).Value;
                job.PhotoThumbPath4 = Picturesm.ElementAt(3).Value;
                hfPhoto4.Value = Convert.ToString(job.PhotoPath4);
                hfThumbPhoto4.Value = Convert.ToString(job.PhotoThumbPath4);
                job.PhotoPath5 = Pictures.ElementAt(4).Value;
                job.PhotoThumbPath5 = Picturesm.ElementAt(4).Value;
                hfPhoto5.Value = Convert.ToString(job.PhotoPath5);
                hfThumbPhoto5.Value = Convert.ToString(job.PhotoThumbPath5);


            }
            else if (Pictures.Count == 4)
            {
                job.PhotoPath1 = Pictures.ElementAt(0).Value;
                job.PhotoThumbPath1 = Picturesm.ElementAt(0).Value;
                hfPhoto1.Value = Convert.ToString(job.PhotoPath1);
                hfThumbPhoto1.Value = Convert.ToString(job.PhotoThumbPath1);
                job.PhotoPath2 = Pictures.ElementAt(1).Value;
                job.PhotoThumbPath2 = Picturesm.ElementAt(1).Value;
                hfPhoto2.Value = Convert.ToString(job.PhotoPath2);
                hfThumbPhoto2.Value = Convert.ToString(job.PhotoThumbPath2);
                job.PhotoPath3 = Pictures.ElementAt(2).Value;
                job.PhotoThumbPath3 = Picturesm.ElementAt(2).Value;
                hfPhoto3.Value = Convert.ToString(job.PhotoPath3);
                hfThumbPhoto3.Value = Convert.ToString(job.PhotoThumbPath3);
                job.PhotoPath4 = Pictures.ElementAt(3).Value;
                job.PhotoThumbPath4 = Picturesm.ElementAt(3).Value;
                hfPhoto4.Value = Convert.ToString(job.PhotoPath4);
                hfThumbPhoto4.Value = Convert.ToString(job.PhotoThumbPath4);
            }
            else if (Pictures.Count == 3)
            {
                job.PhotoPath1 = Pictures.ElementAt(0).Value;
                job.PhotoThumbPath1 = Picturesm.ElementAt(0).Value;
                hfPhoto1.Value = Convert.ToString(job.PhotoPath1);
                hfThumbPhoto1.Value = Convert.ToString(job.PhotoThumbPath1);
                job.PhotoPath2 = Pictures.ElementAt(1).Value;
                job.PhotoThumbPath2 = Picturesm.ElementAt(1).Value;
                hfPhoto2.Value = Convert.ToString(job.PhotoPath2);
                hfThumbPhoto2.Value = Convert.ToString(job.PhotoThumbPath2);
                job.PhotoPath3 = Pictures.ElementAt(2).Value;
                job.PhotoThumbPath3 = Picturesm.ElementAt(2).Value;
                hfPhoto3.Value = Convert.ToString(job.PhotoPath3);
                hfThumbPhoto3.Value = Convert.ToString(job.PhotoThumbPath3);
            }
            else if (Pictures.Count == 2)
            {
                job.PhotoPath1 = Pictures.ElementAt(0).Value;
                job.PhotoThumbPath1 = Picturesm.ElementAt(0).Value;
                hfPhoto1.Value = Convert.ToString(job.PhotoPath1);
                hfThumbPhoto1.Value = Convert.ToString(job.PhotoThumbPath1);
                job.PhotoPath2 = Pictures.ElementAt(1).Value;
                job.PhotoThumbPath2 = Picturesm.ElementAt(1).Value;
                hfPhoto2.Value = Convert.ToString(job.PhotoPath2);
                hfThumbPhoto2.Value = Convert.ToString(job.PhotoThumbPath2);
            }
            else if (Pictures.Count == 1)
            {
                job.PhotoPath1 = Pictures.ElementAt(0).Value;
                job.PhotoThumbPath1 = Picturesm.ElementAt(0).Value;
                hfPhoto1.Value = Convert.ToString(job.PhotoPath1);
                hfThumbPhoto1.Value = Convert.ToString(job.PhotoThumbPath1);
            }

            //for (int index = 0; index < Pictures.Count; index++)
            //{
            //    using (var context = huntableEntities.GetEntitiesWithNoLock())
            //    {
            //        var pictureid = Pictures.ElementAt(index);
            //        var userport = new UserPortfolio { UserId = LoginUserId, PictureId = pictureid.Value, PictureDescription = pictureid.Key, AddedDateTime = DateTime.Now };
            //        context.AddToUserPortfolios(userport);
            //        context.SaveChanges();
            //        FeedManager.addFeedNotification(FeedManager.FeedType.User_Photo, LoginUserId, userport.Id, null);



            //    }

            //}
            //if (Zi.Count == 1)
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Picture added succesfully')", true);
            //}
            //if (Zi.Count > 1)
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Pictures added succesfully')", true);
            //}

            Zi = new List<int?>();
            Se = new List<int?>();
            Pictures.Clear();
            rpic.DataSource = null;
            rpic.DataBind();
            LoggingManager.Debug("Exiting Addpic - PostJob");
       
        }
        protected void DeleteClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DeleteClick - Pictures.aspx");
            var button = sender as ImageButton;
            if (button != null)
            {
                int pId = Convert.ToInt32(button.CommandArgument);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int i = 0;
                    foreach (var pic in Pictures )
                    {
                        i++;
                       if (pic.Value == pId)
                       {

                           var picsm = Picturesm.ElementAt(i-1);
                           Picturesm.Remove(picsm);
                           Pictures.Remove(pic);
                           rpic.DataSource = Pictures.Values;
                           rpic.DataBind();
                           return;
                       }
                    }
                }

            }

            LoggingManager.Debug("Exiting DeleteClick - Pictures.aspx");
        }
         
    }
   
}
