using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;
using System.Web.UI.HtmlControls;
using Subgurim.Controles;

namespace Huntable.UI
{
    public partial class Article : System.Web.UI.Page
    {

        public int _loginUserId = -1;
        public int LoginUserId
        {
            get
            {
                if (_loginUserId == -1)
                {
                    _loginUserId = GetLoggedInUserId();
                }
                return _loginUserId;
            }
        }

        private int GetLoggedInUserId()
        {
            LoggingManager.Debug("Entering LoginUserId - ArticleView");

            var loginUserId = Common.GetLoggedInUserId(Session);
            if (loginUserId != null) return loginUserId.Value;

            LoggingManager.Debug("Exiting LoginUserId - CompanyView");

            return 0;
        }

        public int? compId = -1;
        public int? OtherComId
        {
            get {
                if (compId == -1)
                {
                    compId = GetOtherCompId();
                }
                return compId;
            }
        }

        private int? GetOtherCompId()
        {
            LoggingManager.Debug("Entering OtherComId - Article");

            int otherUserId;
            if (int.TryParse(Request.QueryString["Id"], out otherUserId))
            {
                return otherUserId;
                //exit(1);
            }
            LoggingManager.Debug("Exiting OtherComId - Article");

            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Article");

            var complist = new CompanyManager();

            Company _result;
            if (OtherComId.HasValue)
            {
                var result = complist.GetCmpny(OtherComId.Value);
                var result1 = complist.GetCmpny1(OtherComId.Value);
                var result2 = complist.GetCmpny(OtherComId.Value);
                var result3 = complist.GetCmpny(OtherComId.Value);
                dl3.DataSource = result3;
                dl3.DataBind();
                _result = result.FirstOrDefault();
                if (_result == null) throw new ArgumentNullException("sender");
                img.ImageUrl = Picture(_result.CompanyLogoId);
                if (_result.CompanyIndustry > 0)
                {
                    Label4.Text = _result.MasterIndustry.Description;
                }

                lbl.Text = _result.CompanyWebsite;
                a_web.HRef = "http://" + _result.CompanyWebsite;
                lbl_ph.Text = _result.PhoneNo;
                lbl_add1.Text = _result.Address1;
                lbl_add2.Text = _result.Address2;
                lbl_town.Text = _result.TownCity;
                Label3.Text = _result.CompanyName;
                Label5.Text = _result.CompanyName;

                //dl.DataSource = result;
                //dl.DataBind();
                dl1.DataSource = result1;
                dl1.DataBind();
                dl2.DataSource = result2;
                dl2.DataBind();

            }

            overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(OtherComId));
            activity.HRef = "businessactivity.aspx?Id=" + OtherComId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + OtherComId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + OtherComId;
            careers.HRef = "companyjobs.aspx?Id=" + OtherComId;
            article.HRef = "article.aspx?Id=" + OtherComId;
            //pstjob.HRef = "postjob.aspx?Id=" + compId;


            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var company = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                string address = "54, whitby road, south harrow, HA2 8LH";

                if (company != null)
                {
                    address = company.Address1 + "," + company.Address2 + "," +
                              company.TownCity + "," +
                              company.MasterCountry.Code + "," +
                              company.Postcode;
                }

                GeoCode geocode = GMap.geoCodeRequest(address, string.Empty);
                new GLatLng(geocode.Placemark.coordinates.lat, geocode.Placemark.coordinates.lng);

                //GMap1.Height = 460; GMap1.Width = 425;

                //GMap1.enableDoubleClickZoom = true;
                //GMap1.enableRotation = true;
                //GMap1.enableHookMouseWheelToZoom = true;
                //GMap1.ShowMapTypeControl = true;
                //GMap1.mapType = GMapType.GTypes.Normal;

                //GMap1.setCenter(latlng, 4);

                //GLatLng relativo = new GLatLng(2, -6);
                //GMarker marker1 = new GMarker(latlng + relativo);
                //GMarker marker2 = new GMarker(latlng - relativo);
                //GMarker marker3 = new GMarker(latlng + relativo + relativo);
                //GMap1.setCenter(latlng, 4, GMapType.GTypes.MapMaker_Hybrid);
                //GInfoWindow window = new GInfoWindow(latlng, geocode.Placemark.address);
                //GInfoWindowOptions options1 = new GInfoWindowOptions();
                //window.options = options1;
                //GMap1.Add(window);

                //companyAddressSmall.Attributes["src"] = companyAddressSmall.Attributes["src"].Replace("54,+whitby+road,+south+harrow,+HA2+8LH", address).Replace("54+Whitby+Rd,+Harrow+HA2+8LH,+United+Kingdom", address);
                var followResul = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                if (followResul != null && followResul.Userid == LoginUserId)
                {
                    //btn_follow.Visible = false;
                    //btn_following.Visible = false;
                    div_follow.Visible = false;
                    div_following.Visible = false;
                    Button1.Visible = false;
                    Button2.Visible = false;
                }
                else if (followResul != null && IsThisUserFollowingCompany(followResul.Userid))
                {

                    //btn_following.Visible = true;
                    //btn_follow.Visible = false;
                    div_following.Visible = true;
                    div_follow.Visible = false;
                    Button1.Visible = false;
                    Button2.Visible = true;
                }
                else if (followResul != null && !IsThisUserFollowingCompany(followResul.Userid))
                {
                    // btn_follow.Visible = true;
                    div_follow.Visible = true;
                    Button1.Visible = true;
                }

            }


            SetProfileLink();

            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
            LoggingManager.Debug("Exiting Page_Load - Article");



        }

        private void SetProfileLink()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                var company = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                if (company != null)
                {
                    int? otherUserId = company.Userid;
                    var likeComment = FeedManager.GetLikeAndCommentCount(FeedManager.FeedType.Like_Company_Profile, (otherUserId ?? loginUserId ?? 0), loginUserId ?? 0);
                    hypLikeProfileCount.HRef = "javascript:OpenLikePopup('ajax-like.aspx?FeedId=0&FeedType="
                        + FeedManager.FeedType.Like_Company_Profile.ToString() + "&RefRecordId=" + (otherUserId ?? loginUserId ?? 0).ToString(CultureInfo.InvariantCulture) + "')";
                    if (likeComment.TotalLikes > 0)
                    {
                        lblLikeProfileCount.InnerHtml = likeComment.TotalLikes.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        hypLikeProfileCount.Style["display"] = "none";
                    }
                    if (likeComment.IsLikedByCurrentUser)
                    {
                        hypLikeProfile.HRef = "javascript:" + "MarkDirectUnlike(0, '" + FeedManager.FeedType.Like_Company_Profile.ToString() + "', " + (otherUserId ?? loginUserId ?? 0).ToString(CultureInfo.InvariantCulture) + ")";
                        hypLikeProfile.InnerHtml = "liked this company";
                    }
                    else
                    {
                        hypLikeProfile.HRef = "javascript:" + "MarkDirectLike(0, '" + FeedManager.FeedType.Like_Company_Profile.ToString() + "', " + (otherUserId ?? loginUserId ?? 0).ToString(CultureInfo.InvariantCulture) + ")";
                        hypLikeProfile.InnerHtml = "like this company";
                    }
                }
                else
                {
                    divLikeProfile.Visible = false;
                }
            }
        }
        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {




            var userTo = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == userTo && y.FollowingUserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                return false;
            }


        }

        protected void post_Click(object sender, EventArgs e)
        {
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
        }
        protected void Itembound(object sender, DataListItemEventArgs e)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var firstOrDefault = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                if (firstOrDefault != null)
                {
                    string weblink = firstOrDefault.CompanyWebsite;
                    var a1 = (HtmlAnchor)e.Item.FindControl("a_comp");

                    a1.HRef = "http://" + weblink;
                }
            }
        }
        //protected void dl1_ItemBound(object sender, DataListItemEventArgs e)
        //{
        //    HtmlAnchor a_view = (HtmlAnchor)e.Item.FindControl("a_articleView");
        //    a_view.HRef = "articleview.aspx?Id=" + compId; 
        //}
        protected void Itembund(object sender, DataListItemEventArgs e)
        {
            var art = (HtmlAnchor)e.Item.FindControl("art");
            art.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(OtherComId));
        }
      
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - Article");

            if (id != null)
            {
                int p = Int32.Parse(id.ToString());
                return new FileStoreService().GetDownloadUrl(p);
            }

            LoggingManager.Debug("Exiting Picture - Article");

            return new FileStoreService().GetDownloadUrl(null);
        }

        protected void Follow(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Follow - Article");

            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherComId != null)
                {
                    if (btn_follow.Text == @"Follow")
                        if (loginUserId != null && CompanyManager.FollowCompany(loginUserId.Value, OtherComId.Value))
                        {

                            div_following.Visible = true;
                            div_follow.Visible = false;
                            Button1.Visible = false;
                            Button2.Visible = true;
                            Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('You are now following')", true);
                        }
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting Picture - Article");

        }

        protected void Following(object sender, EventArgs e)
        {
            LoggingManager.Debug("Exiting Following - Article");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherComId != null)
                {

                    if (loginUserId != null)
                    {
                        CompanyManager.UnfollowCompany(loginUserId.Value, OtherComId.Value);
                        div_following.Visible = false;
                        div_follow.Visible = true;
                        Button1.Visible = true;
                        Button2.Visible = false;
                        Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);


                    }
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting Following - Article");
        }
    }
}