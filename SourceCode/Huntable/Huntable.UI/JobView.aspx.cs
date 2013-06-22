using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using System.Linq;

namespace Huntable.UI
{
    public partial class JobView : System.Web.UI.Page
    {
        private int JobId
        {
            get
            {
                //int otherUserId;
                //if (int.TryParse(Request.QueryString["JobId"], out otherUserId))
                //{
                //    return otherUserId;
                //}
                string id = (Page.RouteData.Values["ID"]).ToString();
                string[] words = id.Split('-');
                int k = words.Length;
                string jobid = words[k - 1];
                return Convert.ToInt32(jobid);
            }
        }
       

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - JobView.aspx");

            Common.IsLoggedIn();
            var user = Common.GetLoggedInUser();

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                var id = context.Jobs.FirstOrDefault(j => j.Id == JobId);
                if (id != null)
                {
                    var views = id.TotalViews.HasValue ? id.TotalViews : 0;
                    id.TotalViews = views + 1;
                    if (id.IsRssJob == null)
                    {
                        //lblapplynowdiv.Visible = false;
                        lbApplyNowbtn.Visible = false;
                    }
                    else
                    {
                        lblapplynowdiv.Visible = false;
                        //lbApplyNowbtn.Visible = false;
                    }
                }
                context.SaveChanges();
                if (id != null)
                {

                    int regcmpny = id.UserId;
                    var iscmpny = context.Users.FirstOrDefault(x => x.Id == regcmpny && x.IsCompany == true);
                    var isuser = context.Users.FirstOrDefault(x => x.Id == regcmpny && x.IsCompany == null);
                    if (loginUserId != null && regcmpny == loginUserId.Value)
                    {
                        cmpnyfollow.Visible = false;


                        cmpnyunfollow.Visible = false;
                    }
                    else
                    {
                        if (iscmpny != null)
                        {
                            int cmpnyid = iscmpny.Id;
                            int cmpid = context.Companies.First(x => x.Userid == cmpnyid).Id;
                            var cnt =
                                context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == cmpid).Select(
                                    x => x.FollowingUserId).Distinct().ToList();
                            int coun = cnt.Count;
                            lblcount.Text = coun.ToString(CultureInfo.InvariantCulture);
                            var flwcmpnylabel =
                                context.PreferredFeedUserCompaniesFollwers.FirstOrDefault(
                                    x => x.CompanyID == cmpid && x.FollowingUserId == loginUserId);
                            if (flwcmpnylabel != null)
                            {

                                cmpnyfollow.Visible = false;
                                cmpnyunfollow.Visible = true;

                            }
                            else
                            {
                                cmpnyfollow.Visible = true;

                            }
                            Epp.HRef = "companyregistration2.aspx";
                        }

                        if (isuser != null)
                        {
                            int usrid = isuser.Id;
                            var cnt =
                                context.PreferredFeedUserUsers.Where(x => x.UserId == usrid).Select(
                                    x => x.FollowingUserId).Distinct().ToList();
                            int coun = cnt.Count;
                            lblcount.Text = coun.ToString(CultureInfo.InvariantCulture);
                            var flwusrlabel =
                                context.PreferredFeedUserUsers.FirstOrDefault(
                                    x => x.UserId == usrid && x.FollowingUserId == loginUserId);
                            if (flwusrlabel != null)
                            {
                                cmpnyfollow.Visible = false;
                                cmpnyunfollow.Visible = true;
                            }
                            else
                            {
                                cmpnyfollow.Visible = true;

                            }
                            Epp.HRef = "editprofilepage.aspx";


                        }
                    }


                }
                if (!IsPostBack)
                {
                    hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
                    var usr1 = context.Users.FirstOrDefault(x => x.Id == loginUserId);

                    if (usr1 != null)
                        txtapply.Text = @"Hi,

My name is " + usr1.FirstName + @" " + usr1.LastName +
                                        @", I would like to apply for the job posted by you.  Please check my profile, achievements, portfolio & endorsements for more details. 
Look forward to hear from you.

Regards
" + usr1.FirstName;
                }
            }

            try
            {

                if ((Page.RouteData.Values["ID"]) != null)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        PopulateJobDetails(context, JobId);
                    }

                    BindPeopleAppliedToSimilarJobsRepeater(JobId);
                }

                else
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var jobtitle = Request.QueryString["title"];
                        var jm = new JobsManager();
                        var job = jm.GetJobDetailsByTitle(jobtitle);
                        int jobId = job.Id;
                        PopulateJobDetails(context, jobId);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - JobView.aspx");

        }
        public void BindPeopleAppliedToSimilarJobsRepeater(int jobId)
        {
            LoggingManager.Debug("Entering BindPeopleAppliedToSimilarJobsRepeater - JobView.aspx");

            int numberOfJobsToShow;
            int.TryParse(ConfigurationManager.AppSettings["NumbersOfJobsToShowToWhichPeopleAlsoAppliedToSameJob"], out numberOfJobsToShow);
            var jm = new JobsManager();
           // var listOfJobs = jm.GetJobsToWhichPeopleAppliedToThisJob(numberOfJobsToShow, jobId);
            jm.Getfollowerslist(jobId);
            // lblcount.Text = count.Count.ToString(CultureInfo.InvariantCulture);
            //rpPeopleAppliedToSimilarJobs.DataSource = listOfJobs;
            //rpPeopleAppliedToSimilarJobs.DataBind();

            LoggingManager.Debug("Exiting BindPeopleAppliedToSimilarJobsRepeater - JobView.aspx");

        }
        public int LoginUserId
        {
            get
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                return 0;
            }
        }
        public void PopulateJobDetails(huntableEntities context, int jobId)
        {
            LoggingManager.Debug("Entering PopulateJobDetails - JobView.aspx");
            Common.IsLoggedIn();
            var user = Common.GetLoggedInUser();
            try
            {

                var jm = new JobsManager();
                var job = jm.GetJobDetailsById(jobId);


                lblJobTitle.Text = job.Title;
                LoggingManager.Info("Job Title:" + lblJobTitle.Text);
                lblCountry.Text = job.MasterCountry.Description;
                if (job.SalaryCurrencyId != null)
                {
                    lblSym.Text = job.MasterCurrencyType.Symbol;
                }

                lblSalary.Text = Convert.ToString(job.Salary);
                lblSalCurType.Text = job.CurrencyDescription;
                lblNumberOfNumberOfApplicationsOfThisJob.Text = jm.GetNumberOfApplicationsOfThisJOb(jobId).ToString(CultureInfo.InvariantCulture);
                lblJobTotalNumberOfviews.Text = (job.TotalViews != null) ? Convert.ToString(job.TotalViews) : "0";
                
                LoggingManager.Info("Job Type" + lblJobType.Text);
                lblJobPostedDate.Text = Convert.ToString(job.CreatedDateTime);
                lblJobDescription.Text = job.JobDescription.Replace("\n", "<br/>");
                lblreference.Text = job.ReferenceNumber;
                lblAbtComp.Text = job.AboutYourCompany.Replace("\n", "<br/>");
                lblcandProf.Text = job.DesiredCandidateProfile.Replace("\n", "<br/>");

               
                lblExperienceRequiredEssential.Text = Convert.ToString(job.MinExperience + "-" +job.MaxExperience);
               
                if (job.SkillId != null)
                {
                    lblSkillTypeEssential.Text = job.MasterSkill.Description;
                }                //lblCSym.Text = job.MasterCurrencyType.Symbol;
                lblSalaryEssential.Text = Convert.ToString(job.Salary);
                lblCurr.Text = job.CurrencyDescription;
                lblJobPostedOnEssential.Text = Convert.ToString(job.CreatedDateTime);
                var isUserAlreadyAppliedToThisJob = jm.IsUserAlreadyToThisJob(context, jobId);
                using (var cont = huntableEntities.GetEntitiesWithNoLock())
                {
                    if (job.IsRssJob == false||job.IsRssJob==null)
                    {
                        showProfileImage.SmallImageSource = job.IsCompanyLogo ? job.User.CompanyLogoPictureDisplayUrl : job.User.UserProfilePictureDisplayUrl;
                        imgUserCompany.ImageUrl = job.IsCompanyLogo ? job.User.CompanyLogoPictureDisplayUrl : job.User.UserProfilePictureDisplayUrl;
                        var cmid = cont.Companies.FirstOrDefault(x => x.Userid == job.User.Id);
                        var userid = cont.Users.FirstOrDefault(x => x.Id == job.UserId);
                        if (cmid != null)
                        {
                            aj.HRef = new UrlGenerator().CompanyUrlGenerator(cmid.Id);
                            followrs.HRef = "followers.aspx?UserId=" + cmid.Userid;
                        }
                        else
                        {
                            if (userid != null)
                            {
                                aj.HRef = new UrlGenerator().UserUrlGenerator(userid.Id);
                                followrs.HRef = "followers.aspx?UserId=" + userid.Id;
                            }
                        }


                        showProfileImage11.HRef = job.PhotoDisplayPath1;
                        shwProfileImage11.Src = job.PhotoThumbDisplayPath1;

                        showProfileImage12.HRef = job.PhotoDisplayPath2;
                        shwProfileImage12.Src = job.PhotoThumbDisplayPath2;

                        showProfileImage13.HRef = job.PhotoDisplayPath3;
                        shwProfileImage13.Src = job.PhotoThumbDisplayPath3;

                        showProfileImage14.HRef = job.PhotoDisplayPath4;
                        shwProfileImage14.Src = job.PhotoThumbDisplayPath4;

                        showProfileImage15.HRef = job.PhotoDisplayPath5;
                        shwProfileImage15.Src = job.PhotoThumbDisplayPath5;
                        lblJobType.Text = job.MasterJobType.Description ?? string.Empty;
                        lblJobTypeEssential.Text = job.MasterJobType.Description ?? string.Empty;
                        lblIndustryEssential.Text = job.MasterIndustry.Description;
                        lbAlreadyApplied.Visible = !isUserAlreadyAppliedToThisJob;
                        lblapplynowdiv.Visible = isUserAlreadyAppliedToThisJob;
                        ////
                    
                        //
                    }
                    else
                    {
                        lbAlreadyApplied.Visible = false;
                        lbApplyNowbtn.Visible = true;
                        lblapplynowdiv.Visible = false;
                      
                    }
                }
                //showProfileImage1.SmallImageSource = job.PhotoThumbDisplayPath1;
                //showProfileImage2.SmallImageSource = job.PhotoThumbDisplayPath2;
                //showProfileImage3.SmallImageSource = job.PhotoThumbDisplayPath3;
                //showProfileImage4.SmallImageSource = job.PhotoThumbDisplayPath4;
                //showProfileImage5.SmallImageSource = job.PhotoThumbDisplayPath5;

                //showProfileImage1.BigImageSource = job.PhotoDisplayPath1;
                //showProfileImage2.BigImageSource = job.PhotoDisplayPath2;
                //showProfileImage3.BigImageSource = job.PhotoDisplayPath3;
                //showProfileImage4.BigImageSource = job.PhotoDisplayPath4;
                //showProfileImage5.BigImageSource = job.PhotoDisplayPath5;
                if (user.IsCompany != null && user.IsCompany == true)
                {
                    lbAlreadyApplied.Visible = false;
                    lblapplynowdiv.Visible = false;
                }
              
                   
              
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting PopulateJobDetails - JobView.aspx");
        }

        protected void BtnApplyNowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnApplyNowClick - JobView.aspx");
             var sessionid = Session[SessionNames.LoggedInUserId];
              
            try
            {
                if (sessionid != null)
                {
                    using (var context = new huntableEntities())
                    {
                        var result = context.Jobs.FirstOrDefault(x => x.Id == JobId);
                        if (result != null && result.IsRssJob == null)
                        {
                            var jm = new JobsManager();
                            var loggedInUserId = Common.GetLoggedInUserId(Session);
                            if (loggedInUserId != null)
                            {
                                int userId = Convert.ToInt32(Common.GetLoggedInUserId(Session));


                                var details = new JobApplication
                                    {
                                        UserId = userId,
                                        JobId = JobId,
                                        AppliedDateTime = DateTime.Now,
                                        UserComments = txtapply.Text
                                    };
                                jm.SaveJobApplication(details, loggedInUserId.Value,
                                                      JobId);
                                Page.ClientScript.RegisterStartupScript(GetType(), "Call my function",
                                                                        "overlay('Job application sent succesfully')",
                                                                        true);

                            }
                            var isUserAlreadyAppliedToThisJob = jm.IsUserAlreadyToThisJob(context,
                                                                                          JobId);
                            //Response.Redirect("Jobview.aspx?JobId=" + jobid);
                            lbAlreadyApplied.Visible = !isUserAlreadyAppliedToThisJob;
                            lblapplynowdiv.Visible = isUserAlreadyAppliedToThisJob;
                        }
                        else
                        {
                            if (result != null)
                            {

                                string url = result.Url;
                                var sb = new StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append("window.open('");
                                sb.Append(url);
                                sb.Append("');");
                                sb.Append("</script>");
                                ClientScript.RegisterStartupScript(GetType(),
                                                                   "script", sb.ToString());
                            }
                        }

                    }
                }
                else
                {
                    Response.Write(
                        "<script language='javascript'>alert('You are Not logged in. Please Login First');</script>");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
             
            LoggingManager.Debug("Exiting BtnApplyNowClick - JobView.aspx");
        }

        protected void LbFollowCompanyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering LbFollowCompanyClick - JobView.aspx");
            var sessionid = Session[SessionNames.LoggedInUserId];
            try
            {
                if (sessionid != null)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var cmpny = context.Jobs.First(j => j.Id == JobId);
                        int regcmpny = cmpny.UserId;
                        var iscmpny = context.Users.FirstOrDefault(x => x.Id == regcmpny && x.IsCompany == true);
                        var isuser = context.Users.FirstOrDefault(x => x.Id == regcmpny && x.IsCompany == null);

                        if (iscmpny != null)
                        {
                            int cmpnyid = iscmpny.Id;
                            int cmpid = context.Companies.First(x => x.Userid == cmpnyid).Id;
                            CompanyManager.FollowCompany(LoginUserId, cmpid);
                            cmpnyunfollow.Visible = true;
                            cmpnyfollow.Visible = false;
                            Page.ClientScript.RegisterStartupScript(GetType(), "Call my function",
                                                                    "overlay('You are now following')", true);

                            // var cmpnyflwr = new PreferredFeedUserCompaniesFollwer { CompanyID = cmpnyid, FollowingUserId = loginUserId };
                            // context.PreferredFeedUserCompaniesFollwers.AddObject(cmpnyflwr);
                            //context.SaveChanges();
                        }
                        if (isuser != null)
                        {
                            UserManager.FollowUser(LoginUserId, cmpny.UserId);
                            cmpnyunfollow.Visible = true;
                            cmpnyfollow.Visible = false;
                            Page.ClientScript.RegisterStartupScript(GetType(), "Call my function",
                                                                    "overlay('You are now following')", true);
                        }

                    }
                }
                else
                {
                    Response.Write(
                        "<script language='javascript'>alert('You are Not logged in. Please Login First');</script>");
                }
                //Response.Redirect("jobview.aspx?JobId=" + JobId);
            }

            catch (Exception ex)
            {

                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LbFollowCompanyClick - JobView.aspx");

        }
        protected void Lblunfollow(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Lblunfollow - JobView.aspx");
            var sessionid = Session[SessionNames.LoggedInUserId];
            if (sessionid != null)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var cmpny = context.Jobs.First(j => j.Id == JobId);
                    int regcmpny = cmpny.UserId;
                    var iscmpny = context.Users.FirstOrDefault(x => x.Id == regcmpny && x.IsCompany == true);
                    var isuser = context.Users.FirstOrDefault(x => x.Id == regcmpny && x.IsCompany == null);

                    if (iscmpny != null)
                    {
                        int cmpnyid = iscmpny.Id;
                        int cmpid = context.Companies.First(x => x.Userid == cmpnyid).Id;
                        CompanyManager.UnfollowCompany(LoginUserId, cmpid);
                        cmpnyfollow.Visible = true;
                        cmpnyunfollow.Visible = false;
                        Page.ClientScript.RegisterStartupScript(GetType(), "Call my function",
                                                                "overlay('Succesfully unfollowed')", true);

                        // var cmpnyflwr = new PreferredFeedUserCompaniesFollwer { CompanyID = cmpnyid, FollowingUserId = loginUserId };
                        // context.PreferredFeedUserCompaniesFollwers.AddObject(cmpnyflwr);
                        //context.SaveChanges();
                    }
                    if (isuser != null)
                    {

                        int usrid = isuser.Id;
                        var usrmgr = new UserManager();
                        usrmgr.Unfollow(usrid, LoginUserId);
                        cmpnyfollow.Visible = true;
                        cmpnyunfollow.Visible = false;
                        Page.ClientScript.RegisterStartupScript(GetType(), "Call my function",
                                                                "overlay('Succesfully unfollowed')", true);
                        //var usrflwr = new PreferredFeedUserUser { UserId = LoginUserId, FollowingUserId = usrid };
                        //context.PreferredFeedUserUsers.AddObject(usrflwr);
                        //context.SaveChanges();
                    }
                }
            }
            else
            {
                Response.Write(
                    "<script language='javascript'>alert('You are Not logged in. Please Login First');</script>");
            }
            LoggingManager.Debug("Exiting Lblunfollow - JobView.aspx");
            //Response.Redirect("jobview.aspx?JobId=" + JobId);
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - JobView.aspx");
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - JobView.aspx");
                return null;
            }

        }

    }
}
