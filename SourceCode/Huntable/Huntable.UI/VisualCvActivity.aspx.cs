using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using OAuthUtility;
using Snovaspace.Util;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Huntable.Data;
using Huntable.Business;
using System.Web.UI.HtmlControls;
namespace Huntable.UI
{

    public partial class VisualCvActivity : Page
    {
        private int? UserId
        {
            get
            {
                LoggingManager.Debug("Entering UserId - visualCvActivity.aspx");

                int userId;
                if (int.TryParse(Request.QueryString["UserId"], out userId))
                {
                    return userId;
                }
                if (Page.RouteData.Values["ID"] != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
                }
                if (int.TryParse(Session[SessionNames.LoggedInUserId].ToString(), out userId))
                {
                    return userId;
                       
                }
                LoggingManager.Debug("Exiting UserId - visualCvActivity.aspx");
                return null;


            }
        }
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - visualCvActivity.aspx");
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                LoggingManager.Debug("Exiting LoginUserId - visualCvActivity.aspx");
                return 0;
            }
        }

        public string CalculateChatAuthHash()
        {
            var loginUser = Common.GetLoggedInUser();
            if (OtherUserId != null && loginUser != null)
            {
                var result = Common.GetChatUrl(loginUser.Id.ToString(), OtherUserId.Value.ToString(), loginUser.Name, loginUser.UserProfilePictureDisplayUrl);
                return result;
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                LoadUsersData();

                hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (FeedManager.GetIfProfileLiked(loginUserId ?? 0, OtherUserId ?? loginUserId ?? 0, FeedManager.FeedType.Like_User_Profile.ToString()))
                {
                    hypLikeProfile.HRef = "javascript:" + "MarkDirectUnlike(0, '" + FeedManager.FeedType.Like_User_Profile.ToString() + "', " + (OtherUserId ?? loginUserId ?? 0).ToString() + ")";
                    hypLikeProfile.InnerHtml = "liked my profile";
                }
                else
                {
                    hypLikeProfile.HRef = "javascript:" + "MarkDirectLike(0, '" + FeedManager.FeedType.Like_User_Profile.ToString() + "', " + (OtherUserId ?? loginUserId ?? 0).ToString() + ")";
                    hypLikeProfile.InnerHtml = "like my profile";
                }
                

            }

            hdnOtherUserId.Value = CalculateChatAuthHash();

            if (UserId == LoginUserId)
            {
                showProfileImage12.HRef = "pictureupload.aspx";
            }
            LoggingManager.Debug("Entering Page_Load - VisualCvActivity.aspx");
            UserFeedList1.profileUserId = Convert.ToString(UserId);
            UserPhotoLikes_Right1.profileUserId = Convert.ToString(UserId);
            LoggingManager.Debug("Exiting Page_Load - visualCvActivity.aspx");

        }

        public void LoadUsersData()
        {
            LoggingManager.Debug("Entering LoadUsersData - visualCvActivity.aspx");
            
           
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
            LoggingManager.Debug("Entering Page_Load - VisualCvActivity.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                var blockedUser =
                            context.UserBlockLists.FirstOrDefault(
                                e => e.UserId == OtherUserId.Value && e.BlockedUserId == loggedInUserId);
                if (blockedUser != null)
                 {
                     UcMessage.Visible = false;
                     a_chat.Visible = false;
                 }
                if(OtherUserId.HasValue&&OtherUserId.Value!=loggedInUserId)
                {

                   
                    tellthe.Visible = false;
                    divaftretell.Style.Add("margin-top","40px");
                    a_addpictures.Visible = false;
                    a_addvideos.Visible = false;
                }
                else
                {
                    divaftretell.Style.Add("margin-top", "174px");
                }
                var userdtls = context.Users.FirstOrDefault(x => x.Id == UserId);
                if (userdtls != null)
                {
                    imgProfile.Src = userdtls.UserProfilePictureDisplayUrl;

                    lblName.Text = userdtls.Name;
                    LoadUsercontrolsData();
                    var currentEmployment = userdtls.EmploymentHistories.FirstOrDefault(emp => emp.IsCurrent);
                    if (currentEmployment != null)
                        lblCurrentRoleProfileSection.Text = string.Format("{0}", currentEmployment.JobTitle);
                    if (userdtls.LastActivityDate.HasValue && userdtls.LastActivityDate.Value > DateTime.Now.AddMinutes(-ConfigurationManagerHelper.GetAppsettingByKey<int>(Constants.LastActivityMinutesKey)))
                    {
                        userOnline.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOnlineImagePathKey);
                        onlineinfo.Text = string.Format("Online Now");

                    }
                    else
                    {
                        userOnline.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOfflineImagePathKey);
                        onlineinfo.Text = string.Format("Offline");

                    }
                }
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherUserId.HasValue)
                {
                    var userp = context.Users.FirstOrDefault(x => x.Id == OtherUserId.Value);
                   
                    if (userp != null)
                    {
                        showProfileImage12.HRef = userp.UserProfilePictureDisplayUrl;
                        imgHasCompany.Visible = userp.UserHasCompany;
                    }
                    vc.HRef = new UrlGenerator().UserUrlGenerator(OtherUserId.Value);
                    vt.HRef = new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                    vca.HRef = new UrlGenerator().UserActivityUrlGenerator(OtherUserId.Value);
                    // Logging the profile visited activity.


                }
                else
                {
                    showProfileImage12.HRef = userdtls.UserProfilePictureDisplayUrl;
                    imgHasCompany.Visible = userdtls.UserHasCompany;
                    vc.HRef = "visualcv.aspx";
                    vt.HRef = "viewuserprofile.aspx";
                    vca.HRef = "visualcvactivity.aspx";
                    divOnlineNoewChatWithMe.Visible = false;
                    messageboxdiv.Visible = false;

                }
                if (OtherUserId != null)
                {
                    LoagUserProfileVisited(context, OtherUserId.Value, loginUserId);
                    var following =
                        context.PreferredFeedUserUsers.FirstOrDefault(
                            x => x.UserId == OtherUserId.Value && x.FollowingUserId == loginUserId.Value);
                   
                    if (following != null)
                    {
                        flwngr.Visible = true;
                        flwr.Visible = false;
                    }
                    else
                    {
                        flwngr.Visible = false;
                        flwr.Visible = true;
                    }
                }
               
            }
            if (hdnUserId.Value == "")
            {
                a_addpictures.Visible = false;
                a_addvideos.Visible = false;
                p_msg_like.Visible = false;
                flwr.Visible = false;
                flwngr.Visible = false;
                a_chat.Visible = false;
                userOnline.Visible = false;
                onlineinfo.Visible = false;
               
                
            }
            //var loginUserId1 = Common.GetLoggedInUserId(Session);
            //if (FeedManager.GetIfProfileLiked(loginUserId1 ?? 0, OtherUserId ?? loginUserId1 ?? 0, FeedManager.FeedType.Like_User_Profile.ToString()))
            //{
            //    hypLikeProfile.HRef = "javascript:" + "MarkDirectUnlike(0, '" + FeedManager.FeedType.Like_User_Profile.ToString() + "', " + (OtherUserId ?? loginUserId1 ?? 0).ToString() + ")";
            //    hypLikeProfile.InnerHtml = "liked my profile";
            //}
            //else
            //{
            //    hypLikeProfile.HRef = "javascript:" + "MarkDirectLike(0, '" + FeedManager.FeedType.Like_User_Profile.ToString() + "', " + (OtherUserId ?? loginUserId1 ?? 0).ToString() + ")";
            //    hypLikeProfile.InnerHtml = "like my profile";
            //}
            LoggingManager.Debug("Exiting LoadUsersData-visualcvactivity.aspx");
        }
        public void  LoadUsercontrolsData()
        {
            LoggingManager.Debug("Entering LoadUsercontrolsData - visualCvActivity.aspx");
            if (UserId != null)
            {
                var userresult = new UserManager();
                    var resulkt = userresult.GetUserfollower((int)UserId);

                List<User> userFollowings = userresult.GetUserFollowings((int) UserId);
                var photos=UserProfileManager.GetPictures((int) UserId);
                photos.Reverse();
                dlpictures.DataSource = photos.Take(4);
                dlpictures.DataBind();
                var videos = UserProfileManager.GetVideos((int) UserId);
                videos.Reverse();
                dlvideos.DataSource = videos.Take(4);
                dlvideos.DataBind();
                var companies = UserProfileManager.GetcompaniesFollowers((int) UserId);
                var cmpnies = companies.Select(x => x.CompanyID).Distinct().ToList();
                cmpnies.Reverse();
                dlComapnies.DataSource = cmpnies.Take(4);
                dlComapnies.DataBind();
                var following = UserProfileManager.GetUserFollowings((int) UserId);
                var flwing = following.Where(x=>x.UserId!= UserId).Select(x => x.UserId).Distinct().ToList();
                if (flwing.Count() != 0)
                {

                    dlfollowers.DataSource = flwing.Take(4);
                    dlfollowers.DataBind();
                }
                else
                {
                    //dlfollowers.DataSource = resulkt.Distinct().Take(4);
                    //dlfollowers.DataBind();
                    followersdiv.Visible = false;
                    followingdiv.Visible = true;
                }
                pictureslink.HRef = "Pictures.aspx?UserId=" + UserId;
                pictureslinktop.HRef = "pictures.aspx?UserId=" + UserId;
                lblpicturesCount.Text = photos.Count().ToString(CultureInfo.InvariantCulture);
                companylinktop.HRef = "Following.aspx?UserId=" + UserId;
                lblCompanycount.Text = cmpnies.Count().ToString(CultureInfo.InvariantCulture);
                followingtoplink.HRef = "Following.aspx?UserId=" + UserId;
                followerstoplink.HRef = "Followers.aspx?UserId=" + UserId;
                followbottom.HRef = "Following.aspx?UserId=" + UserId;
                lblCompanyBottomClick.HRef = "Following.aspx?UserId=" + UserId;
                lblfollowing.Text = flwing.Count().ToString(CultureInfo.InvariantCulture);
                lblFolowers.Text = resulkt.Count().ToString(CultureInfo.InvariantCulture);
                videotoplink.HRef = "Videos.aspx?UserId=" + UserId;
                videobottom.HRef = "Videos.aspx?UserId=" + UserId;
                var vid = UserProfileManager.GetVideos((int)UserId);
                vid.Reverse();
                dlvideos.DataSource = vid.Take(4);
                dlvideos.DataBind();
            }
            LoggingManager.Debug("Exiting LoadUsercontrolsData - visualCvActivity.aspx");
        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - visualCvActivity.aspx");
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());


                return new FileStoreService().GetDownloadUrl(p);
            }
            LoggingManager.Debug("Exiting Picture - visualCvActivity.aspx");
            return new FileStoreService().GetDownloadUrl(null);
        }

        public string ProfilePicture(object id)
        {
            LoggingManager.Debug("Entering ProfilePicture - visualCvActivity.aspx");
            if(id!=null)
            {
                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var photo = context.Users.FirstOrDefault(x => x.Id == p);
                    if (photo != null) return new FileStoreService().GetDownloadUrl(photo.PersonalLogoFileStoreId);
                }
            }
            LoggingManager.Debug("Exiting ProfilePicture - visualCvActivity.aspx");
            return new FileStoreService().GetDownloadUrl(null);
        }
        public string Picturec(object id)
        {
            LoggingManager.Debug("Entering Picture - VisualCV.aspx");
            if (id != null)
            {

                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {


                    LoggingManager.Debug("Exiting Picture - VisualCV.aspx");
                    return new FileStoreService().GetDownloadUrl(p);

                }

            }
            return "~/HuntableImages/nomore.jpg";
        }

        protected void Btnattachclick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnattachClick - visualCvActivity.aspx");
            //int count = 1;
            //    txtbox.Text = txtbox.Text;
            //    string strr = txtbox.Text;
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strr + "','blank' + new Date().getTime(),'menubar=no') </script>");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

           
                int imageid = Convert.ToInt32(new FileStoreService().LoadFileFromFileUpload(Constants.PortFolioImages, fp));
                //var user = im.UserId;
                 const string defaultmsg = "Got something to say, ask, post, share…";
                if (UserId != null)
                {
                    var userport = new UserPortfolio
                        {
                            UserId = (int) UserId,
                            PictureId = imageid,
                            AddedDateTime = DateTime.Now,
                            PictureDescription = ((txtTellworld.Text.Trim() != string.Empty && txtTellworld.Text.Trim() != defaultmsg))?txtTellworld.Text.Trim():string.Empty

                        };
                    context.UserPortfolios.AddObject(userport);
                    context.SaveChanges();
                    txtTellworld.Text = "";
                    FeedManager.addFeedNotification(FeedManager.FeedType.Wall_Picture, (int)UserId, userport.Id, null);
                    var socialManager = new SocialShareManager();
                    var msg = "https://huntable.co.uk/LoadFile.ashx?id=" + imageid;    
                    socialManager.ShareOnFacebook((int)UserId,"[UserName] shared a picture via Huntable",msg);
                }
            }
            LoggingManager.Debug("Exiting BtnattachClick - visualCvActivity.aspx");
        }
        protected void BtnTellWorldClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnTellWorldClick - VisualCVActivity.aspx");
            var sessionid = Session[SessionNames.LoggedInUserId];
            const string defaultmsg = "Got something to say, ask, post, share…";
            if (sessionid != null)
            {
                if (txtTellworld.Text.Trim() != string.Empty && txtTellworld.Text.Trim() != defaultmsg) 
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
                            socialManager.ShareLinkOnFacebook(userId, "", "[UserName] has shared a link in Huntable", "", "", link, "https://huntable.co.uk/images/banner-img1.png");
                        }
                        else
                        {
                            socialManager.ShareOnFacebook(userId, txtTellworld.Text, "");
                        }

                    }
                    if (chkLinkedIn.Checked)
                        socialManager.ShareOnLinkedIn(userId, txtTellworld.Text, "");
                    if (chkTwitter.Checked)
                        socialManager.ShareOnTwitter(userId, txtTellworld.Text);

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
                    //update.Update();

                    //const string url = "http://www.huntable.co.uk";

                    //txtTellworld.Text = txtTellworld.Text;
                    //int count = 1;
                    //string strl = "http://www.linkedin.com/shareArticle?mini=true&url=" + txtTellworld.Text +
                    //              "&summary=" +
                    //              txtTellworld.Text;
                    //string str = "https://twitter.com/share?url=" + txtTellworld.Text + "&summary=" + txtTellworld.Text;
                    //const string strf = "http://www.facebook.com/sharer.php?u=" + url;
                    //const string strg = "https://plus.google.com/share?url=" + url;
                    //if (chkTwitter.Checked)
                    //    Page.ClientScript.RegisterStartupScript(GetType(),
                    //                                            " _blank" + count.ToString(CultureInfo.InvariantCulture),
                    //                                            "<script language=javascript>window.open('" + str +
                    //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                    //count = count + 1;
                    //if (chkLinkedIn.Checked)
                    //    Page.ClientScript.RegisterStartupScript(GetType(),
                    //                                            "_blank" + count.ToString(CultureInfo.InvariantCulture),
                    //                                            "<script language=javascript>window.open('" + strl +
                    //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                    //count = count + 1;
                    //if (chkFacebook.Checked)
                    //    Page.ClientScript.RegisterStartupScript(GetType(),
                    //                                            " _blank" + count.ToString(CultureInfo.InvariantCulture),
                    //                                            "<script language=javascript>window.open('" + strf +
                    //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                    //count = count + 1;
                    //if (chkgoogle.Checked)
                    //    Page.ClientScript.RegisterStartupScript(GetType(),
                    //                                            " _blank" + count.ToString(CultureInfo.InvariantCulture),
                    //                                            "<script language=javascript>window.open('" + strg +
                    //                                            "','blank' + new Date().getTime(),'menubar=no') </script>");
                }
                else
                {
                   
                    Response.Write(
                    "<script language='javascript'>alert('Oops! You Forgot to write something');</script>");
                }
            }
            else
            {
                Response.Write(
                    "<script language='javascript'>alert('Please Enter Message or check logged in ');</script>");
            }

            LoggingManager.Debug("Exiting BtnTellWorldClick -VisualCVActivity.aspx");

        }
        protected void UserCompaniesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UserCompaniesClick - visualCvActivity.aspx");
            int id = 0;
            if (OtherUserId.HasValue)
            {

                id = OtherUserId.Value;
            }
            else if (UserId.HasValue)
            {
                id = UserId.Value;

            }
            if (id != 0)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usercompaniesList = from uc in context.UserCompanies
                                            join c in context.Companies on uc.CompanyId equals c.Id
                                            where uc.UserId == id && c.IsVerified == true
                                            select new
                                            {
                                                c.Id,
                                                c.CompanyName,
                                                c.CompanyWebsite,
                                                c.CompanyEmail,
                                                c.CompanyLogoId
                                            };



                    dlListOfCompanies.DataSource = usercompaniesList;
                    dlListOfCompanies.DataBind();
                }
            }

            //ScriptManager.RegisterStartupScript(this, GetType(), "Click", "rowaction22();", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "rowAction22()", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "Companies", "rowAction22();", true);
            LoggingManager.Debug("Exiting UserCompaniesClick - visualCvActivity.aspx");


        }
        public string ComapnyPicture(object id)
        {
            LoggingManager.Debug("Entering ComapnyPicture - visualCvActivity.aspx");
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var photo = context.Companies.FirstOrDefault(x => x.Id == p);
                    if (photo != null) return new FileStoreService().GetDownloadUrl(photo.CompanyLogoId);
                }
            }
            LoggingManager.Debug("Exiting ComapnyPicture - visualCvActivity.aspx");
            return new FileStoreService().GetDownloadUrl(null);
        }

        public string CompanyPicture(object id)
        {
            LoggingManager.Debug("Entering ComapnyPicture - visualCvActivity.aspx");
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var photo = context.Companies.FirstOrDefault(x => x.Id == p);
                    if (photo != null) return new FileStoreService().GetDownloadUrl(photo.CompanyLogoId);
                }
            }
            LoggingManager.Debug("Exiting ComapnyPicture - visualCvActivity.aspx");
            return new FileStoreService().GetDownloadUrl(null);
        }

        protected void FollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering  FollowClick - visualCvActivity.aspx");
            using (huntableEntities.GetEntitiesWithNoLock())
            {
                if (OtherUserId.HasValue)
                {
                    var loginUserId = Common.GetLoggedInUserId(Session);

                    if (loginUserId != null) UserManager.FollowUser(loginUserId.Value, OtherUserId.Value);
                    flwr.Visible = false;
                    flwngr.Visible = true;
                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('You are now following')", true);
                }
            }
            LoggingManager.Debug("Exiting  FollowClick - visualCvActivity.aspx");
        }

        protected void UnfollowClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering UnfollowClick - visualCvActivity.aspx");
            var loginUserId = Common.GetLoggedInUserId(Session);
            var usrmngr = new UserManager();
            if (OtherUserId != null) if (loginUserId != null)
                {
                 
                usrmngr.Unfollow(OtherUserId.Value, loginUserId.Value);
                Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true); 
            } flwr.Visible = true;
            flwngr.Visible = false;
            LoggingManager.Debug("Exiting UnfollowClick - visualCvActivity.aspx");
        }
        private void LoagUserProfileVisited(huntableEntities context, int userId, int? loginUserId)
        {
            LoggingManager.Debug("Entering LoagUserProfileVisited - VisualCvActivity.aspx");

            var profileVisitedHistory = new UserProfileVisitedHistory
            {
                Date = DateTime.Now,
                UserId = userId,
                VisitorUserId = loginUserId,
                IPAddress = GetIpAddress()
            };
            context.UserProfileVisitedHistories.AddObject(profileVisitedHistory);
            context.SaveChanges();
            LoggingManager.Debug("Exiting LoagUserProfileVisited - VisualCvActivity.aspx");
        }
        private string GetIpAddress()
        {
            LoggingManager.Debug("Entering GetIpAddress - VisualCvActivity.aspx");

            string strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                                  Request.ServerVariables["REMOTE_ADDR"];

            LoggingManager.Debug("Exiting GetIpAddress - visualCvActivity.aspx");
            return strIpAddress;
        }
        private int? OtherUserId
        {
            get
            {
                LoggingManager.Debug("Entering OtherUserId - visualCvActivity.aspx");
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                if (Page.RouteData.Values["ID"] != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
                }

                LoggingManager.Debug("Exiting OtherUserId - visualCvActivity.aspx");
                return null;
            }
        }
        protected void Itemflwrsbound(object sender, DataListItemEventArgs e)
        {

            LoggingManager.Debug("Entering Itemflwrsbound - visualCvActivity.aspx");
            var anchor = (HtmlAnchor)e.Item.FindControl("anchor2");
           
            
            if (e.Item.DataItem != null)
            {
                int userid = Convert.ToInt32(e.Item.DataItem.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == userid && x.IsCompany == null);
                    if (usr != null)
                    {
                        anchor.HRef = "~/"+ new UrlGenerator().UserUrlGenerator(usr.Id);
                    }
                    else
                    {
                        var firstOrDefault = context.Companies.FirstOrDefault(x => x.Userid == userid);
                        if (firstOrDefault != null)
                        {
                            int cmid = firstOrDefault.Id;
                            anchor.HRef = "~/" + new UrlGenerator().CompanyUrlGenerator(cmid);
                        }
                    }
                }
                
            }
            LoggingManager.Debug("Exiting Itemflwrsbound - visualCvActivity.aspx");

        }
        protected void Itemflwngbound(object sender, DataListItemEventArgs e)
        {
            LoggingManager.Debug("Entering temflwngbound - visualCvActivity.aspx");
            
            var anchor = (HtmlAnchor)e.Item.FindControl("anchor1");

            if (e.Item.DataItem != null)
            {
                int userid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"UserId").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == userid && x.IsCompany == null);
                    if (usr != null)
                    {
                        anchor.HRef = "~/" + new UrlGenerator().UserUrlGenerator(userid);
                    }
                    else
                    {
                        var firstOrDefault = context.Companies.FirstOrDefault(x => x.Userid == userid);
                        if (firstOrDefault != null)
                        {
                            int cmid = firstOrDefault.Id;
                            anchor.HRef = "~/" + new UrlGenerator().CompanyUrlGenerator(cmid);
                        }
                    }
                }

            }
            LoggingManager.Debug("Exiting temflwngbound - visualCvActivity.aspx");
        }


        protected void chkTwitter_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkTwitter_CheckedChanged - visualCvActivity.aspx");
            if (chkTwitter.Checked)
                CheckSocialShare("twitter");
            LoggingManager.Debug("EXiting chkTwitter_CheckedChanged - visualCvActivity.aspx");
        }

        protected void chkLinkedIn_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkLinkedIn_CheckedChanged - visualCvActivity.aspx");
            if (chkLinkedIn.Checked)
                CheckSocialShare("linkedin");
            LoggingManager.Debug("EXiting chkLinkedIn_CheckedChanged - visualCvActivity.aspx");
        }

        protected void chkFacebook_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkFacebook_CheckedChanged - visualCvActivity.aspx");
            if (chkFacebook.Checked)
                CheckSocialShare("facebook");
            LoggingManager.Debug("EXiting chkFacebook_CheckedChanged - visualCvActivity.aspx");
        }

        private void CheckSocialShare(string provider)
        {
            LoggingManager.Debug("Entering CheckSocialShare - visualCvActivity.aspx");
            var user = Common.GetLoggedInUser();
            if (user == null) return;
            var check = user.OAuthTokens.Any(o => o.Provider == provider);
            if (check) return;
            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var callbackuri = baseUrl + "oauth.aspx";
            Session["oauthmode"] = "visualactivity";
            OAuthWebSecurity.RequestAuthentication(provider, callbackuri);
            LoggingManager.Debug("Exiting CheckSocialShare - visualCvActivity.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - visualCvActivity.aspx");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return "~/" + new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - visualCvActivity.aspx");
                return null;
            }

        }
        public string CompanyUrlGenerator(object id)
        {
            LoggingManager.Debug("Entering CompanyUrlGenerator - UserSearchCriteria .aspx");
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return "~/" + new UrlGenerator().CompanyUrlGenerator(userid);
            }
            else
            {
                LoggingManager.Debug("Exiting CompanyUrlGenerator - UserSearchCriteria .aspx");
                return null;
            }

        }
    }
}
