using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using OAuthUtility;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using System.Linq;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI
{
    public partial class HomePageAfterLoggingIn : Page
    {
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - HomePageAfterLoggingIn");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - HomePageAfterLoggingIn");
                return 0;


            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HomePageAfterLoggingIn.aspx");
            try
            {
                bool userLoggedIn = Common.IsLoggedIn();
                var user = Common.GetLoggedInUser();
                if (!IsPostBack)
                {
                    LoadProfilePercentCompleted();
                    PopulateJobFeeds();
                    
                }
                if (userLoggedIn)
                {
                    opp1.Visible = true;
                    cmpimage.Visible = false;
                    rightDiv.Visible = true;
                    DateTime dt = user.LastLoginTime;
                    string s = dt.ToString("@HH:mm tt");
                    lblUserName.Text = user.Name;
                    lblLogDate.Text = user.LastLoginTime.ToString("MM'/'dd'/'yyyy");
                    lblLogin.Text = s;
                    lblProfile.Text = user.LastProfileUpdatedOn.ToString("MM'/'dd'/'yyyy");
                    imgProfile.ImageUrl = user.UserProfilePictureDisplayUrl;
                    lblAcccount.Text = user.IsPremiumAccount.HasValue ? "premium" : "Basic";
                }
                else
                {
                    tell.Visible = false;
                    opp1.Visible = false;
                    imgProfile.Visible = false;
                    rightDiv.Visible = false;
                }


                LoggingManager.Debug("Exiting Page_Load - HomePageAfterLoggingIn.aspx");
                if (user.IsCompany == true)
                {
                    DateTime dt = user.LastLoginTime;
                    string s = dt.ToString("@HH:mm tt");
                    //lbllastlogin.Text = user.LastLoginTime.ToString("MM'/'dd'/'yyyy");
                    //lbllog.Text = s;
                    //lblpupdate.Text = user.LastProfileUpdatedOn.ToString("MM'/'dd'/'yyyy");
                    //tell.Visible = false;
                    //tell1.Visible = true;
                    divTellTheWorld.Style["margin-left"] = "88px";
                    divTellTheWorld.Style["margin-top"] = "0px";
                    spnLoginTime.Style["margin-left"] = "88px";
                    opp1.Visible = false;
                    cmpimage.ImageUrl = user.UserProfilePictureDisplayUrl;
                    div_comp.Visible = true;
                    cmpimage.Visible = true;
                    rightDiv.Visible = false;
                    imgProfile.Visible = false;
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var firstOrDefault = context.Companies.FirstOrDefault(x => x.Userid == LoginUserId);
                        if (firstOrDefault != null)
                        {
                            int cmpid = firstOrDefault.Id;
                            AA1.HRef = new UrlGenerator().CompanyUrlGenerator(cmpid);
                        }
                    }
                }
                else
                {
                    AA1.HRef = "viewuserprofile.aspx";
                }
              

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - HomePageAfterLoggingIn.aspx");
        }

        protected void PagePrerender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering PagePrerender - HomePageAfterLoggingIn.aspx");
            try
            {
                //PopulateUserFeeds();

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting PagePrerender - HomePageAfterLoggingIn.aspx");

        }

        protected void BtnJobShowCLick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnJobShowCLick - HomePageAfterLoggingIn.aspx");

            var loggedInUserId = Common.GetLoggedInUserId();
            if (loggedInUserId != null)
            {
                if (hJobfield.Value == string.Empty)
                    hJobfield.Value = "6";
                hJobfield.Value = (Convert.ToInt32(hJobfield.Value) + 6).ToString();
                PopulateJobFeeds();
            }
            LoggingManager.Debug("Exiting BtnJobShowCLick - HomePageAfterLoggingIn.aspx");

        }

        private void PopulateJobFeeds()
        {
            LoggingManager.Debug("Entering PopulateJobFeeds - HomePageAfterLoggingIn.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var loggedInUserId = Common.GetLoggedInUserId();

                if (loggedInUserId != null)
                {
                    if (hJobfield.Value == string.Empty)
                        hJobfield.Value = "6";

                    List<Job> jobs = context.JobFeeds.Where(x => x.UserId == loggedInUserId).Select(x => x.Job).OrderByDescending(y => y.CreatedDateTime).Take(Convert.ToInt32(hJobfield.Value)).ToList();
                    foreach (var job in jobs)
                    {
                        job.ProfileImagePath = job.IsCompanyLogo ? job.User.CompanyLogoPictureDisplayUrl : job.User.UserProfilePictureDisplayUrl;
                        job.IsUserAlreadyToThisJob = new JobsManager().IsUserAlreadyToThisJob(context, job.Id);

                    }

                    lvJobs.DataSource = jobs;
                    lvJobs.DataBind();
                }
            }
            LoggingManager.Debug("Exiting PopulateJobFeeds - HomePageAfterLoggingIn.aspx");
        }

        //private void PopulateUserFeeds()
        //{
        //    LoggingManager.Debug("Entering PopulateUserFeeds - HomePageAfterLoggingIn.aspx");
        //    var feedManager = new UserFeedManager();
        //    var loggedInUserId = Common.GetLoggedInUserId();

        //    if (loggedInUserId != null)
        //    {
        //        if (hField.Value == string.Empty) hField.Value = "12";

        //        var result = feedManager.GetUserFeeds(loggedInUserId.Value, Convert.ToInt32(hField.Value));

        //        gvUserFeeds.GridDetails.DataSource = result;
        //        gvUserFeeds.GridDetails.DataBind();
        //    }
        //    LoggingManager.Debug("Exiting PopulateUserFeeds - HomePageAfterLoggingIn.aspx");

        //}

        protected void BtnUserSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnUserSearchClick - HomePageAfterLoggingIn.aspx");
            try
            {
                string url = string.Format("~/UserSearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
                new Utility().RedirectUrl(Response, url);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting btnUserSearchClick - HomePageAfterLoggingIn.aspx");
        }

        //protected void btnShowMoreFeedsClick(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering btnShowMoreFeedsClick - HomePageAfterLoggingIn.aspx");
        //    var loggedInUserId = Common.GetLoggedInUserId();
        //    if (loggedInUserId != null)
        //    {
        //        if (hField.Value == string.Empty)
        //            hField.Value = "12";
        //        hField.Value = (Convert.ToInt32(hField.Value) + 6).ToString();
        //    }
        //    LoggingManager.Debug("Exiting btnShowMoreFeedsClick - HomePageAfterLoggingIn.aspx");
        //}
        protected void BtnTellWorldClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnTellWorldClick - HomePageAfterLoggingIn.aspx");
            const string defaultmsg = "Got something to say, ask, post, share…";
            if ((txtTellworld.Text.Trim() != string.Empty && txtTellworld.Text.Trim() != defaultmsg))
            {
                var feedManager = new UserFeedManager();
                var userId = Convert.ToInt32(Common.GetLoggedInUserId(Session));
                feedManager.SaveUserFeed(userId, "WhatsonMind",
                                         txtTellworld.Text.Trim());
                var socialManager = new SocialShareManager();
                if (chkFacebook.Checked)
                {
                    if (txtTellworld.Text.StartsWith("http://") || txtTellworld.Text.StartsWith("www"))
                    {
                        var text = txtTellworld.Text.Trim();
                        var result = text.Split(' ');
                        var link = result[0];
                        var context = new huntableEntities();
                        var user = context.Users.FirstOrDefault(u => u.Id == userId);
                        if (user != null && user.PersonalLogoFileStoreId != null)
                        {
                            socialManager.ShareLinkOnFacebook(userId, "", "[UserName] has shared a link in Huntable", "",
                                                              "", link, "http://huntable.co.uk/loadfile.ashx?id=" + user.PersonalLogoFileStoreId);
                        }
                        else
                            socialManager.ShareLinkOnFacebook(userId, "", "[UserName] has shared a link in Huntable", "",
                                                                   "", link, "");
                    }
                    else{
                        socialManager.ShareOnFacebook(userId, txtTellworld.Text, "");
                    }
                    
                }
                   
                if (chkLinkedIn.Checked)
                    socialManager.ShareOnLinkedIn(userId, txtTellworld.Text, "");
                if(chkTwitter.Checked)
                    socialManager.ShareOnTwitter(userId,txtTellworld.Text);


                if (!chkFacebook.Checked)
                {
                    if (txtTellworld.Text.StartsWith("http://") || txtTellworld.Text.StartsWith("www"))
                    {
                        var text = txtTellworld.Text.Trim();
                        var result = text.Split(' ');
                        var link = result[0];
                        socialManager.ShareLinkOnFacebook(userId, "", "[UserName] has shared a link in Huntable", "", "", link, "");
                    }
                }

                txtTellworld.Text = "";
                //update.Update();ss

                //var url = "http://www.huntable.co.uk";

                //txtTellworld.Text = txtTellworld.Text;
                //int count = 1;
                //string strl = "http://www.linkedin.com/shareArticle?mini=true&url=" + txtTellworld.Text + "&summary=" +
                //              txtTellworld.Text;
                //string str = "https://twitter.com/share?url=" + txtTellworld.Text + "&summary=" + txtTellworld.Text;
                //string strf = "http://www.facebook.com/sharer.php?u=" + url;
                //string strg = "https://plus.google.com/share?url=" + url;
                //if (chkTwitter.Checked)
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), " _blank" + count.ToString(),
                //                                            "<script language=javascript>window.open('" + str +
                //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                //count = count + 1;
                //if (chkLinkedIn.Checked)
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "_blank" + count.ToString(),
                //                                            "<script language=javascript>window.open('" + strl +
                //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                //count = count + 1;
                //if (chkFacebook.Checked)
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), " _blank" + count.ToString(),
                //                                            "<script language=javascript>window.open('" + strf +
                //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                //count = count + 1;
                //if (chkgoogle.Checked)
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), " _blank" + count.ToString(),
                //                                            "<script language=javascript>window.open('" + strg +
                //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");

                LoggingManager.Debug("Exiting BtnTellWorldClick - HomePageAfterLoggingIn.aspx");
            }
            else
            {
                Response.Write(
                "<script language='javascript'>alert('Oops! You Forgot to write something');</script>");
            }
         
        }

        private void LoadProfilePercentCompleted()
        {
            LoggingManager.Debug("Entering LoadProfilePercentCompleted - HomePageAfterLoggingIn.aspx");
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    var percentCompleted = UserManager.GetProfilePercentCompleted(userId.Value);
                    lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                    int value = Convert.ToInt32(percentCompleted);
                    ProgressBar2.Value = value;
                    LoggingManager.Info("Percent Completed:" + lblPercentCompleted.Text);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadProfilePercentCompleted - HomePageAfterLoggingIn.aspx");

        }

        protected void BtnJoinClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnJoinClick - HomePageAfterLoggingIn.aspx");

            LoggingManager.Debug("Exiting BtnJoinClick - HomePageAfterLoggingIn.aspx");


        }

        protected void LvJobsItemCommand(object sender, ListViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering LvJobsItemCommand - HomePageAfterLoggingIn.aspx");
            if (e.CommandName == "Apply")
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int jobId = Convert.ToInt32(jobid.Value);
                    TextBox tx = e.Item.FindControl("txtapply") as TextBox;
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
                        PopulateJobFeeds();
                        //ScriptManager.RegisterStartupScript(this, GetType(), "Call my function",
                        //                                   "fdout()", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "Call my function",
                                                            "overlay('Job application sent succesfully')", true);
                    }
                }
                LoggingManager.Debug("Exiting LvJobsItemCommand - HomePageAfterLoggingIn.aspx");
            }
        }

        //protected void btnTellWorldclick(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering btnattach_Click - HomePageAfterLoggingIn.aspx");
        //    //int count = 1;
        //    //    txtbox.Text = txtbox.Text;
        //    //    string strr = txtbox.Text;
        //    //    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strr + "','blank' + new Date().getTime(),'menubar=no') </script>");
        //    using (var context = huntableEntities.GetEntitiesWithNoLock())
        //    {

        //        var cmpMgr = new CompanyManager();
        //        int imageid = Convert.ToInt32(new FileStoreService().LoadFileFromFileUpload(Constants.PortFolioImages, fp));
        //        var im = context.UserPortfolios.FirstOrDefault(x => x.UserId == LoginUserId);
        //        //var user = im.UserId;

        //        var userport = new UserPortfolio
        //        {
        //            UserId = LoginUserId,
        //            PictureId = imageid,
        //            AddedDateTime = DateTime.Now

        //        };
        //        context.UserPortfolios.AddObject(userport);
        //        context.SaveChanges();
        //        FeedManager.addFeedNotification(FeedManager.FeedType.User_Photo, LoginUserId, userport.Id, txtTellworld.Text);
        //    }
        //    LoggingManager.Debug("Exiting btnattach_Click - HomePageAfterLoggingIn.aspx");
        //}

        protected void Btnattachclick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnattachClick - HomePageAfterLoggingIn.aspx");
            //int count = 1;
            //    txtbox.Text = txtbox.Text;
            //    string strr = txtbox.Text;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strr + "','blank' + new Date().getTime(),'menubar=no') </script>");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                var cmpMgr = new CompanyManager();
                int imageid = Convert.ToInt32(new FileStoreService().LoadFileFromFileUpload(Constants.PortFolioImages, fp));
                var im = context.UserPortfolios.FirstOrDefault(x => x.UserId == LoginUserId);
                //var user = im.UserId;
                const string defaultmsg = "Got something to say, ask, post, share…";
                var userport = new UserPortfolio
                {
                    UserId = LoginUserId,
                    PictureId = imageid,
                    AddedDateTime = DateTime.Now,
                    PictureDescription =  ((txtTellworld.Text.Trim() != string.Empty && txtTellworld.Text.Trim() != defaultmsg))?txtTellworld.Text.Trim():string.Empty
                };
                context.UserPortfolios.AddObject(userport);
                context.SaveChanges();
                txtTellworld.Text = "";
                FeedManager.addFeedNotification(FeedManager.FeedType.Wall_Picture, LoginUserId, userport.Id, null);
                var socialManager = new SocialShareManager();
                var msg = "https://huntable.co.uk/LoadFile.ashx?id="+imageid;              
                socialManager.ShareOnFacebook(LoginUserId,"[UserName] shared a picture via Huntable",msg);

              //  socialManager.ShareOnFacebook(LoginUserId, "[UserName] has attached a picture",  Server.MapPath("/images/signout2.jpg"));

            }
            LoggingManager.Debug("Exiting BtnattachClick - HomePageAfterLoggingIn.aspx");
        }
        protected void ApplyJob(object sender, EventArgs e)
        {
            //var button = sender as Button;

            //int jobId = Convert.ToInt32(button.CommandArgument);
            ////int jobId = Convert.ToInt32(e.CommandArgument);
            //int userId = Convert.ToInt32(Common.GetLoggedInUserId(Session));
            //string userComments = txtapply.Text;
            //var details = new JobApplication { UserId = userId, JobId = jobId, AppliedDateTime = DateTime.Now  , UserComments = userComments};
            //using (var context = huntableEntities.GetEntitiesWithNoLock())
            //{
            //    context.JobApplications.AddObject(details);
            //    context.SaveChanges();
            //}

            //new JobsManager().SaveJobApplication(Convert.ToInt32(Common.GetLoggedInUserId(Session)), jobId);
            //Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "fdout()", true);
        }
        protected void LvItemound(object sender, ListViewItemEventArgs e)
        {
            LoggingManager.Debug("Entering LvItemound - HomePageAfterLoggingIn.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (e.Item.DataItem != null)
                {
                    TextBox TB = (TextBox) e.Item.FindControl("txtapply");
                    var usr = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                    if (usr != null)
                        if (TB != null)
                            TB.Text = @"Hi,

My name is " + usr.FirstName + @" " + usr.LastName +
                                      @", I would like to apply for the job posted by you.  Please check my profile, achievements, portfolio & endorsements for more details. 
Look forward to hear from you.

Regards
" + usr.FirstName;
                }
            }
            LoggingManager.Debug("Exiting LvItemound - HomePageAfterLoggingIn.aspx");
        }

        protected void chkTwitter_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkTwitter_CheckedChanged - HomePageAfterLoggingIn.aspx");
            if(chkTwitter.Checked)
                CheckSocialShare("twitter");
            LoggingManager.Debug("Exiting chkTwitter_CheckedChanged - HomePageAfterLoggingIn.aspx");
        }

        protected void chkLinkedIn_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkLinkedIn_CheckedChanged - HomePageAfterLoggingIn.aspx");
            if(chkLinkedIn.Checked)
                CheckSocialShare("linkedin");
            LoggingManager.Debug("Exiting chkLinkedIn_CheckedChanged - HomePageAfterLoggingIn.aspx");
        }

        protected void chkFacebook_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkFacebook_CheckedChanged - HomePageAfterLoggingIn.aspx");
            if (chkFacebook.Checked)
                CheckSocialShare("facebook");
            LoggingManager.Debug("Exiting chkFacebook_CheckedChanged - HomePageAfterLoggingIn.aspx");
        }

        private void CheckSocialShare(string provider)
        {
            LoggingManager.Debug("Entering CheckSocialShare - HomePageAfterLoggingIn.aspx");
            var user = Common.GetLoggedInUser();
            if (user == null) return;
            var check = user.OAuthTokens.Any(o => o.Provider == provider);
            if (check) return;
            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var callbackuri = baseUrl + "oauth.aspx";
            Session["oauthmode"] = "socialshare";
            OAuthWebSecurity.RequestAuthentication(provider, callbackuri);
            LoggingManager.Debug("Exiting CheckSocialShare - HomePageAfterLoggingIn.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - HomePageAfterLoggingIn.aspx");

            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - HomePageAfterLoggingIn.aspx");
                return null;
            }

        }
    }
}
