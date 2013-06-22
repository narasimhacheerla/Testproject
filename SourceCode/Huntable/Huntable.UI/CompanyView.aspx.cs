using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;
using System.Web.UI;

namespace Huntable.UI
{
    public partial class CompanyView : System.Web.UI.Page
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
            LoggingManager.Debug("Entering LoginUserId - CompanyView");

            var loginUserId = Common.GetLoggedInUserId(Session);
            if (loginUserId != null) return loginUserId.Value;

            LoggingManager.Debug("Exiting LoginUserId - CompanyView");

            return 0;
        }


        public int? otherComId = -1;

        public int? OtherComId
        {
            get
            {
                if (otherComId == -1)
                {
                    otherComId = GetOtherCompanyId();    
                }

                return otherComId;
            }
        }

        private int? GetOtherCompanyId()
        {
            LoggingManager.Debug("Entering OtherComId - CompanyView");

            int otherUserId;
            if (int.TryParse(Request.QueryString["Id"], out otherUserId))
            {
                return otherUserId;
            }
            if (Page.RouteData.Values["ID"] != null)
            {
                string id = (Page.RouteData.Values["ID"]).ToString();
                string[] words = id.Split('-');
                int k = words.Length;
                string companyid = words[k - 1];
                return Convert.ToInt32(companyid);
            }
            LoggingManager.Debug("Exiting OtherComId - CompanyView");

            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompanyView");

            overview.HRef = "businessactivity.aspx?Id=" + OtherComId;
            activity.HRef = "businessactivity.aspx?Id=" + OtherComId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + OtherComId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + OtherComId;
            careers.HRef = "companyjobs.aspx?Id=" + OtherComId;
            article.HRef = "article.aspx?Id=" + OtherComId;
            addyourbusinesspage.HRef = "Company-registration.aspx";
            List<Company> cmpusr = new List<Company>();
            // first request
            if (!IsPostBack)
            {
                if (OtherComId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        LoggingManager.Debug("Entering viewers also viewed. with other company id:" + OtherComId);

                        #region  ViewersAlsoViewed
                        var loginUserId = Common.GetLoggedInUserId(Session);
                        var firstOrDefault = context.Companies.FirstOrDefault(x => x.Id == OtherComId.Value);
                        if (firstOrDefault != null)
                        {
                            int? cmpuserid = firstOrDefault.Userid;
                            if (loginUserId != OtherComId && loginUserId.HasValue)
                            {
                                LoagUserProfileVisited(context, Convert.ToInt32(cmpuserid), loginUserId);
                            }
                        }

                        #endregion

                        var company = context.Companies.FirstOrDefault(x => x.Id == OtherComId);

                        var lastOrDefault = context.CompanyPortfolios.Where(x => x.CompanyId == OtherComId).ToList().LastOrDefault();
                        if (lastOrDefault != null)
                        {
                            string portdesc = lastOrDefault.PortfolioDescription;
                            if (portdesc != "")
                            {
                                deslbl1.Text = portdesc.Replace("\n", "<br/>");
                            }
                        }
                        if (company != null)
                        {
                            lblCompanyName.Text = company.CompanyName;
                            lblCompanyName1.Text = company.CompanyName;
                            //lblCompanyName2.Text = company.CompanyName;
                            lblCompanyName3.Text = company.CompanyName;
                            Label1.Text = company.CompanyName;
                            lblCompanyName4.Text = company.CompanyName;
                            lblCompanyHeading.Text = company.CompanyHeading;
                            //   lblCompanyHeading1.Text = company.CompanyHeading;
                            lblCompanyDescription.Text = company.CompanyDescription;
                            if (company.MasterIndustry != null)
                                lblCompanyIndustry.Text = company.MasterIndustry.Description;
                            lblWebsite.Text = company.CompanyWebsite;
                            if (company.CompanyWebsite.ToLower().Contains("http://"))
                            {
                                a_comp.HRef = company.CompanyWebsite;
                            }
                            else
                            {
                                a_comp.HRef = "http://" + company.CompanyWebsite;
                            }
                            lblPhoneNo.Text = Convert.ToString(company.PhoneNo);
                            if (company.MasterEmployee != null)
                                lblNoofEmployees.Text = Convert.ToString(company.MasterEmployee.NoofEmployess) + " Employees";
                            lblAddress1.Text = company.Address1;
                            lblAddress2.Text = company.Address2;
                            lblTownCity.Text = company.TownCity;
                            lblcountry.Text = company.MasterCountry.Description;
                            deslbl1.Attributes.Add("style", "font-family:Georgia;font-size:14px");
                            companylogoimg.ImageUrl = new FileStoreService().GetDownloadUrl(company.CompanyLogoId);
                        }
                        else
                        {
                            deslbl1.Text = "No Text To Display";
                        }

                        try
                        {
                            var mcmpny = (from c in context.Companies
                                          join d in context.MasterCompanies on c.CompanyName equals d.Description
                                          where c.Id == OtherComId
                                          select d.Id).Single();

                            var mcp = Int32.Parse(mcmpny.ToString(CultureInfo.InvariantCulture));
                            var emplyuser = from a in context.EmploymentHistories
                                            join b in context.Users on a.UserId equals b.Id
                                            where a.CompanyId == mcp && b.Id != LoginUserId
                                            select new
                                            {
                                                b.FirstName,

                                                b.PersonalLogoFileStoreId,
                                                b.Password,
                                                b.Id,
                                                b.LastName,


                                            };
                            // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                            dlCompanyEmployees.DataSource = emplyuser.Distinct().Take(6);
                            dlCompanyEmployees.DataBind();
                        }
                        catch (Exception ex)
                        {
                            LoggingManager.Error(ex);
                        }

                        //var companyEmployessList = context.EmploymentHistories.Where(x => x.CompanyId == OtherComId);
                        //dlCompanyEmployees.DataSource = companyEmployessList.Distinct().Take(6);
                        //dlCompanyEmployees.DataBind();

                        // Empoyees You may want to follow  End


                        // Viewers also viewed-----

                        int? usrid = context.Companies.FirstOrDefault(x => x.Id == OtherComId).Userid;
                        var result =
                           context.UserProfileVisitedHistories.Where(x => x.UserId == usrid).ToList();
                        foreach (var res in result)
                        {
                            var viewlist =
                                from a in context.UserProfileVisitedHistories
                                where a.VisitorUserId == res.VisitorUserId
                                select new
                                    {
                                        a.UserId,
                                        a.Date
                                    };

                            foreach (var vwlis in viewlist)
                            {
                                var cmpur = context.Companies.FirstOrDefault(x => x.Userid == vwlis.UserId && x.Id != OtherComId);
                                if (cmpur != null)
                                {
                                    cmpusr.Add(cmpur);
                                }
                            }
                        }
                        if (cmpusr.Count != 0)
                        {
                            cmpusr.Reverse();
                            dlview.DataSource = cmpusr.Distinct().Take(4);
                            dlview.DataBind();
                        }
                        //if (result != null)
                        //{
                        //    int vCompId = Int32.Parse(result.VisitorCompanyId.ToString());
                        //    var compResult = from cv in context.CompanyProfileVisitedHistories
                        //                     join c in context.Companies on cv.CompanyId equals c.Id
                        //                     where cv.VisitorCompanyId == vCompId && cv.CompanyId != OtherComId
                        //                     select new
                        //                                {
                        //                                    c.CompanyLogoId,
                        //                                    c.Id
                        //                                };
                        //    var comp_result_ = compResult.Distinct().Take(4);
                        //if (cmpusr.Count != 0)
                        //{
                        //    dlview.DataSource = cmpusr.Distinct().Take(4);
                        //    dlview.DataBind();
                        //} //}


                        // viewers also viewed End----



                        var portfolioimage = context.CompanyPortfolios.Where(x => x.CompanyId == OtherComId).ToList();
                        portfolioimage.Reverse();
                        //var portimg = cmgr.GetCmpny2(OtherComId.Value);
                        dlr.DataSource = portfolioimage;
                        dlr.DataBind();
                        dlPortfolioDescription.DataSource = portfolioimage;
                        dlPortfolioDescription.DataBind();

                        var companyFollow = CompanyManager.FollowingCompany(LoginUserId, OtherComId.Value);
                        if (companyFollow.Count > 0)
                        {
                            //btnFollow.Text = string.Format("following", "ARG0");

                        }
                        var followResul = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                        if (followResul != null && followResul.Userid == LoginUserId)
                        {
                            //btn_follow.Visible = false;
                            //btn_following.Visible = false;
                            div_follow.Visible = false;
                            div_following.Visible = false;
                        }
                        var followcmny = context.PreferredFeedUserCompaniesFollwers.FirstOrDefault(x => x.CompanyID == OtherComId.Value && x.FollowingUserId == LoginUserId);
                        if (followcmny != null && followResul.Userid != LoginUserId)
                        {
                            div_following.Visible = true;
                            div_follow.Visible = false;
                        }
                        else if (followcmny == null && followResul.Userid != LoginUserId)
                        {
                            div_following.Visible = false;
                            div_follow.Visible = true;
                            //btn_following.Visible = true;
                            //btn_follow.Visible = false;

                        }

                    }
                }
                SetProfileLink();
            }
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();

            LoggingManager.Debug("Exiting Page_Load - CompanyView");
        }

        private void SetProfileLink()
        {
            LoggingManager.Debug("Entering SetProfileLink - CompanyView");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                var company = context.Companies.FirstOrDefault(x => x.Id == OtherComId);
                if (company != null)
                {
                    int? otherUserId = company.Userid;
                    var likeComment = FeedManager.GetLikeAndCommentCount(FeedManager.FeedType.Like_Company_Profile, (otherUserId ?? loginUserId ?? 0), loginUserId ?? 0);
                    hypLikeProfileCount.HRef = "javascript:OpenLikePopup('https://huntable.co.uk/ajax-like.aspx?FeedId=0&FeedType="
                        + FeedManager.FeedType.Like_Company_Profile.ToString() + "&RefRecordId=" + (otherUserId ?? loginUserId ?? 0).ToString() + "')";
                    if (likeComment.TotalLikes > 0)
                    {
                        lblLikeProfileCount.InnerHtml = likeComment.TotalLikes.ToString();
                    }
                    else
                    {
                        hypLikeProfileCount.Style["display"] = "none";
                    }
                    if (likeComment.IsLikedByCurrentUser)
                    {
                        hypLikeProfile.HRef = "javascript:" + "MarkDirectUnlike(0, '" + FeedManager.FeedType.Like_Company_Profile.ToString() + "', " + (otherUserId ?? loginUserId ?? 0).ToString() + ")";
                        hypLikeProfile.InnerHtml = "liked this company";
                    }
                    else
                    {
                        hypLikeProfile.HRef = "javascript:" + "MarkDirectLike(0, '" + FeedManager.FeedType.Like_Company_Profile.ToString() + "', " + (otherUserId ?? loginUserId ?? 0).ToString() + ")";
                        hypLikeProfile.InnerHtml = "like this company";
                    }
                }
                else
                {
                    divLikeProfile.Visible = false;
                }
            }
            LoggingManager.Debug("Eing SetProfileLink - CompanyView");
        }
        //protected void FollowupCompaniesClick(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering FollowupCompaniesClick - CompanyView");
        //    try
        //    {
        //        int? loginUserId = Common.GetLoggedInUserId(Session);
        //        if (OtherComId != null)
        //        {
        //            if(btnFollow.Text == "Follow")
        //                if (loginUserId != null && CompanyManager.FollowCompany(loginUserId.Value, OtherComId.Value))                        
        //                    btnFollow.Text = "following";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        { }
        //        throw;
        //    }
        //    LoggingManager.Debug("Exiting FollowupCompaniesClick - CompanyView");
        //}
        protected void seemore_click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering seemore_click - CompanyView");

            if (hfemplyee.Value == string.Empty)
                hfemplyee.Value = "6";
            hfemplyee.Value = (Convert.ToInt32(hfemplyee.Value) + 6).ToString();
            using (var context = new huntableEntities())
            {
                try
                {
                    var mcmpny = (from c in context.Companies
                                  join d in context.MasterCompanies on c.CompanyName equals d.Description
                                  where c.Id == OtherComId
                                  select d.Id).Single();

                    var mcp = Int32.Parse(mcmpny.ToString());
                    var emplyuser = from a in context.EmploymentHistories
                                    join b in context.Users on a.UserId equals b.Id
                                    where a.CompanyId == mcp
                                    select new
                                    {
                                        b.FirstName,

                                        b.PersonalLogoFileStoreId,
                                        b.Password,
                                        b.Id,
                                        b.LastName,




                                    };
                    // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                    dlCompanyEmployees.DataSource = emplyuser.Distinct().Take(Convert.ToInt32(hfemplyee.Value));
                    dlCompanyEmployees.DataBind();

                }
                catch (Exception ex)
                {
                    LoggingManager.Error(ex);
                }
                //var companyEmployessList = context.EmploymentHistories.Where(x => x.CompanyId == OtherComId);
                //dlCompanyEmployees.DataSource = companyEmployessList.Distinct.Take(Convert.ToInt32(hfemplyee.Value));
                //dlCompanyEmployees.DataBind();
            }
            LoggingManager.Debug("EXiting seemore_click - CompanyView");
        }

        protected void click_seemore(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering click_seemore - CompanyView");
            List<Company> cmpusr = new List<Company>();
            if (Hidden_Field.Value == string.Empty)
                Hidden_Field.Value = "4";
            Hidden_Field.Value = (Convert.ToInt32(Hidden_Field.Value) + 4).ToString();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? usrid = context.Companies.FirstOrDefault(x => x.Id == OtherComId).Userid;
                var result =
                           context.UserProfileVisitedHistories.Where(x => x.UserId == usrid).ToList();
                foreach (var res in result)
                {
                    var viewlist =
                                from a in context.UserProfileVisitedHistories
                                where a.VisitorUserId == res.VisitorUserId
                                select new
                                {
                                    a.UserId,
                                    a.Date
                                };
                    viewlist.OrderByDescending(x => x.Date);
                    foreach (var vwlis in viewlist)
                    {
                        var cmpur = context.Companies.FirstOrDefault(x => x.Userid == vwlis.UserId && x.Id != OtherComId);
                        if (cmpur != null)
                        {
                            cmpusr.Add(cmpur);
                        }
                    }
                }
                if (cmpusr.Count != 0)
                {
                    cmpusr.Reverse();
                    dlview.DataSource = cmpusr.Distinct().Take(8);
                    dlview.DataBind();
                }


            }
            LoggingManager.Debug("Entering click_seemore - CompanyView");

        }


        protected void CommandCompanyEmployeeFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering CommandCompanyEmployeeFollow_Click - CompanyView");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);

                var btnCompanyEmployeeFollow = sender as LinkButton;
                if (btnCompanyEmployeeFollow != null)
                {
                    int companyEmployeeUserId = Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument);
                }
                if (loginUserId != null)
                {
                    UserManager.FollowUser(loginUserId.Value, Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument));
                    btnCompanyEmployeeFollow.Text = "Following";
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var mcmpny = (from c in context.Companies
                                      join d in context.MasterCompanies on c.CompanyName equals d.Description
                                      where c.Id == OtherComId
                                      select d.Id).Single();

                        var mcp = Int32.Parse(mcmpny.ToString(CultureInfo.InvariantCulture));
                        var emplyuser = from a in context.EmploymentHistories
                                        join b in context.Users on a.UserId equals b.Id
                                        where a.CompanyId == mcp && b.Id != LoginUserId
                                        select new
                                        {
                                            b.FirstName,

                                            b.PersonalLogoFileStoreId,
                                            b.Password,
                                            b.Id,
                                            b.LastName,


                                        };
                        // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                        dlCompanyEmployees.DataSource = emplyuser.Distinct().Take(6);
                        dlCompanyEmployees.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "overlay('You are now following')", true);
                    }
                }


            }
            catch (Exception)
            {
                { }
                throw;
            }

            LoggingManager.Debug("Exiting CommandCompanyEmployeeFollow_Click - CompanyView");
        }

        protected void CommandCompanyEmployeeUnFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering CommandCompanyEmployeeFollowClick - CompanyView");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);

                var btnCompanyEmployeeFollow = sender as LinkButton;
                if (btnCompanyEmployeeFollow != null)
                {
                    int companyEmployeeUserId = Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument);
                }


                var usrmgr = new UserManager();

                if (loginUserId != null)
                {
                    usrmgr.Unfollow(Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument), loginUserId.Value);

                    btnCompanyEmployeeFollow.Text = "Follow";
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var mcmpny = (from c in context.Companies
                                      join d in context.MasterCompanies on c.CompanyName equals d.Description
                                      where c.Id == OtherComId
                                      select d.Id).Single();

                        var mcp = Int32.Parse(mcmpny.ToString(CultureInfo.InvariantCulture));
                        var emplyuser = from a in context.EmploymentHistories
                                        join b in context.Users on a.UserId equals b.Id
                                        where a.CompanyId == mcp && b.Id != LoginUserId
                                        select new
                                        {
                                            b.FirstName,

                                            b.PersonalLogoFileStoreId,
                                            b.Password,
                                            b.Id,
                                            b.LastName,


                                        };
                        // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                        dlCompanyEmployees.DataSource = emplyuser.Distinct().Take(6);
                        dlCompanyEmployees.DataBind();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting CommandCompanyEmployeeFollowClick - CompanyView");
        }


        public string UserPicture(object id)
        {
            LoggingManager.Debug("Entering UserPicture - CompanyView");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.PersonalLogoFileStoreId == p);
                var photo = result.PersonalLogoFileStoreId;

                LoggingManager.Debug("Exiting UserPicture - CompanyView");

                return new FileStoreService().GetDownloadUrl(photo);
            }

        }


        //public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        //{
        //    LoggingManager.Debug("Entering IsThisUserFollowingCompany - CompanyView");

        //    LoggingManager.Debug("Exiting IsThisUserFollowingCompany - CompanyView");

        //    return UserManager.FollowingUser(LoginUserId,Convert.ToInt32(useridFollowedByCompany)).Any();


        //}
        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - CompanyView");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var user_to = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == user_to && y.FollowingUserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                else
                    LoggingManager.Debug("Exiting IsThisUserFollowingCompany - CompanyView");
                return false;

            }



        }

        //public string CompanyPicture(object imageLogId)
        //{
        //    LoggingManager.Debug("Entering CompanyPicture - CompanyView");

        //    LoggingManager.Debug("Exiting CompanyPicture - CompanyView");

        //    return new FileStoreService().GetDownloadUrl(Convert.ToInt32(imageLogId));



        //}
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompanyView");
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());


                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    return new FileStoreService().GetDownloadUrl(p);
                }

            }
            else
            {
                LoggingManager.Debug("Exiting Picture - CompanyView");
                return new FileStoreService().GetDownloadUrl(null);
            }




        }

        public string CompanyPortfolioPicture(object id)
        {
            LoggingManager.Debug("Entering CompanyPortfolioPicture - CompanyView");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.CompanyPortfolios.FirstOrDefault(x => x.PortfolioImageid == p);
                var photo = result.PortfolioImageid;

                LoggingManager.Debug("Exiting CompanyPortfolioPicture - CompanyView");

                return new FileStoreService().GetDownloadUrl(photo);
            }

        }
        protected void Follow(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Follow - CompanyView");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherComId != null)
                {
                    if (btn_follow.Text == "Follow")
                        if (loginUserId != null)
                        {
                            CompanyManager.FollowCompany(loginUserId.Value, OtherComId.Value);
                            div_following.Visible = true;
                            div_follow.Visible = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
                        }
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting Follow - CompanyView");

        }
        protected void Following(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Following - CompanyView");

            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherComId != null)
                {

                    if (loginUserId != null)
                    {
                        CompanyManager.UnfollowCompany(loginUserId.Value, OtherComId.Value);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                        div_following.Visible = false;
                        div_follow.Visible = true;
                    }
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting Following - CompanyView");
        }

        protected void dispic(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering dispic - CompanyView");
            var btn = sender as ImageButton;
            if (btn != null)
            {
                int id = Convert.ToInt32(btn.CommandArgument);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    string des = context.CompanyPortfolios.FirstOrDefault(x => x.Id == id).PortfolioDescription.Replace("\n", "<br/>");
                    deslbl.Attributes.Add("style", "font-family:Georgia;font-size:14px");
                    if (des != "")
                    {

                        deslbl.Text = des;
                    }
                    else
                    {
                        deslbl.Text = "No Text To Dispaly";
                    }
                    deslbl1.Visible = false;
                }
            }
            LoggingManager.Debug("Exiting dispic - CompanyView");
        }
        protected string JobTitle(object id)
        {
            LoggingManager.Debug("Entering JobTitle - CompanyView");
            if (id != null)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int emid = Convert.ToInt32(id.ToString());
                    return context.EmploymentHistories.Where(x => x.UserId == emid).ToList().LastOrDefault().JobTitle;
                }
            }
            else
            {
                LoggingManager.Debug("Exiting JobTitle - CompanyView");
                return null;
            }
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - CompanyView");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - CompanyView");
                return null;
            }


        }
        public string UserUrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - CompanyView");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return "~/" + new UrlGenerator().UserUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - CompanyView");
                return null;
            }


        }
        private void LoagUserProfileVisited(huntableEntities context, int userId, int? loginUserId)
        {
            LoggingManager.Debug("Entering LoagUserProfileVisited - VisualCV.aspx");
            DateTime dt = Convert.ToDateTime(Session["Datetm"].ToString());
            var profileVisitedHistory = new UserProfileVisitedHistory
            {

                UserId = userId,
                VisitorUserId = loginUserId,
                IPAddress = GetIpAddress(),
                Date = dt
            };
            context.UserProfileVisitedHistories.AddObject(profileVisitedHistory);
            context.SaveChanges();
            LoggingManager.Debug("Exiting LoagUserProfileVisited - VisualCV.aspx");
        }
        private string GetIpAddress()
        {
            LoggingManager.Debug("Entering GetIpAddress - VisualCV.aspx");

            string strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                                  Request.ServerVariables["REMOTE_ADDR"];

            LoggingManager.Debug("Exiting GetIpAddress - VisualCV.aspx");
            return strIpAddress;
        }
    }
}