using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompanyJobs : System.Web.UI.Page
    {

        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompanyView");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompanyView");

                return 0;
            }
        }

        protected void LnkJobsClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkJobsClick - HeaderAfterLoggingIn.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            if (loggedInUserId != null)
            {
                var result = jobManager.GetUserDetails(loggedInUserId.Value);

                if (result.IsPremiumAccount == false || result.IsPremiumAccount == null && result.CreditsLeft == null && result.FreeCredits == true)
                {
                    Response.Redirect("WhatIsHuntableUpgrade.aspx");
                }
                else if (result.CreditsLeft == null && result.CreditsLeft == null || result.CreditsLeft == 0 && result.FreeCredits == false)
                {
                    Response.Redirect("BuyCredit.aspx");
                }
                else if (result.CreditsLeft == 0 && result.FreeCredits == true && result.IsPremiumAccount == null || result.IsPremiumAccount == false)
                {
                    Response.Redirect("WhatIsHuntableUpgrade.aspx");
                }
                else if (result.CreditsLeft == 0 && result.FreeCredits == true && result.IsPremiumAccount != null ||
                         result.IsPremiumAccount == true)
                {
                    Response.Redirect("PostJob.aspx");
                }

                else
                {
                    Response.Redirect("PostJob.aspx");
                }
            }
            LoggingManager.Debug("Exiting lnkJobsClick - HeaderAfterLoggingIn.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Entering Page_Load - CompanyJobs");
                var comp = context.Users.FirstOrDefault(x => x.Id == LoginUserId && x.IsCompany == true);
                var companyid = 0;
                if (comp != null)
                {
                    var firstOrDefault = context.Companies.FirstOrDefault(x => x.Userid == comp.Id);
                    if (firstOrDefault != null)
                        companyid = firstOrDefault.Id;
                }
                if (OtherComId == companyid)
                {
                    btn_follow.Visible = false;
                    btn_following.Visible = false;
                }
                if (!IsPostBack)
                {
                    if (OtherComId.HasValue)
                    {
                        var complist = new CompanyManager();
                        var result = complist.GetCmpny(OtherComId.Value).FirstOrDefault();
                        if (result != null) lbl_compName.Text = result.CompanyName;
                    }

                    if (OtherComId != null) a_comP_view.HRef = new UrlGenerator().CompanyUrlGenerator(OtherComId.Value);

                    Displayjobs();
                    var followResul = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                    if (OtherComId == companyid)
                    {
                        btn_follow.Visible = false;
                        btn_following.Visible = false;
                    }
                    else
                    {

                        if (followResul != null && IsThisUserFollowingCompany(followResul.Userid))
                            btn_following.Visible = true;
                        else
                            btn_follow.Visible = true;
                    }
                }

            }


            overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(OtherComId));
            activity.HRef = "businessactivity.aspx?Id=" + OtherComId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + OtherComId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + OtherComId;
            careers.HRef = "companyjobs.aspx?Id=" + OtherComId;
            article.HRef = "article.aspx?Id=" + OtherComId;
            a_Comp_carrer.HRef = "companycareers.aspx?Id=" + OtherComId;
            cj.HRef = "companyjobs.aspx?Id=" + OtherComId;
            a_compInfo.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(OtherComId));

            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
            LoggingManager.Debug("Exiting Page_Load - CompanyJobs");



        }
        public void Displayjobs()
        {
            LoggingManager.Debug("Entering Displayjobs - CompanyJobs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var companydetails = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                if (companydetails != null)
                {
                    imCompanyImage.ImageUrl = new FileStoreService().GetDownloadUrl(companydetails.CompanyLogoId);
                    var d1 = DateTime.Now.AddDays(-30);
                    List<Job> jobs = context.Jobs.Where(x => x.UserId == companydetails.Userid && x.CreatedDateTime > d1).OrderByDescending(x => x.CreatedDateTime).ToList();

                    foreach (var job in jobs)
                    {
                        job.ProfileImagePath = job.IsCompanyLogo
                                                   ? job.User.CompanyLogoPictureDisplayUrl
                                                   : job.User.UserProfilePictureDisplayUrl;
                        job.IsUserAlreadyToThisJob = new JobsManager().IsUserAlreadyToThisJob(context, job.Id);

                    }
                    if (jobs.Count > 0)
                    {
                        lvJobs.DataSource = jobs;
                        lvJobs.DataBind();
                    }
                    else
                    {
                        dv.Attributes.Add("style", "height:739px");
                        njbs.Visible = true;
                    }
                }

            }
            LoggingManager.Debug("Exiting Displayjobs - CompanyJobs");
        }
        private int? OtherComId
        {

            get
            {
                LoggingManager.Debug("Entering OtherComId - CompanyJobs");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting OtherComId - CompanyJobs");
                return null;
            }
        }
        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - CompanyJobs");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var userTo = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == userTo && y.FollowingUserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                LoggingManager.Debug("Exiting IsThisUserFollowingCompany - CompanyJobs");
                return false;

            }



        }
        protected void LvJobsItemCommand(object sender, ListViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering LvJobsItemCommand - CompanyJobs");
            if (e.CommandName == "Apply")
            {
                int jobId = Convert.ToInt32(e.CommandArgument);
                var tx = e.Item.FindControl("txtapply") as TextBox;
                using (huntableEntities.GetEntitiesWithNoLock())
                {
                    int userId = Convert.ToInt32(Common.GetLoggedInUserId(Session));


                    if (tx != null)
                    {
                        string userComments = tx.Text;

                        var details = new JobApplication
                            {
                                UserId = userId,
                                JobId = jobId,
                                AppliedDateTime = DateTime.Now,
                                UserComments = userComments
                            };
                        new JobsManager().SaveJobApplication(details, Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                                                             jobId);
                    }
                    Displayjobs();
                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function",
                                                            "overlay('Job application sent succesfully')", true);
                }
            }

            LoggingManager.Debug("Exiting LvJobsItemCommand - CompanyJobs");
        }

        protected void lvjobs_apply(object sender, ListViewItemEventArgs e)
        {
            LoggingManager.Debug("Entering lvjobs_apply - CompanyJobs");
            var user = Common.GetLoggedInUser();
            if (user != null && user.IsCompany == true)
            {

                //LinkButton lblAssId = (LinkButton)e.Item.FindControl("LinkButton1");
                //lblAssId.Visible = false;
                var lblfoll = (Label)e.Item.FindControl("Following");
                lblfoll.Visible = false;
            }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (e.Item.DataItem != null)
                {
                    var tb = (TextBox)e.Item.FindControl("txtapply");
                    var usr = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                    if (usr != null)
                        if (tb != null)
                            tb.Text = @"Hi,

My name is " + usr.FirstName + @" " + usr.LastName +
                                      @", I would like to apply for the job posted by you.  Please check my profile, achievements, portfolio & endorsements for more details. 
Look forward to hear from you.

Regards
" + usr.FirstName;
                }
            }
            LoggingManager.Debug("Exiting lvjobs_apply - CompanyJobs");
        }
        protected void Follow(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Follow - CompanyJobs");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherComId != null)
                {
                    if (btn_follow.Text == @"Follow")
                        if (loginUserId != null)
                        {
                            CompanyManager.FollowCompany(loginUserId.Value, OtherComId.Value);
                            btn_following.Visible = true;
                            btn_follow.Visible = false;
                            Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('You are now following')", true);
                        }
                }
            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting Follow - CompanyJobs");
        }
        protected void Following(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Following - CompanyJobs");

            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherComId != null)
                {

                    if (loginUserId != null)
                    {
                        CompanyManager.UnfollowCompany(loginUserId.Value, OtherComId.Value);
                        btn_following.Visible = false;
                        btn_follow.Visible = true;
                        Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                    }
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting Following - CompanyJobs");

        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering Following - CompanyJobs");
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            LoggingManager.Debug("Exiting Following - CompanyJobs");
            return null;
        }
    }
}