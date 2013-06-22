using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using System.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI
{
    public partial class CompanyProducts : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompanyProducts");

            var objInvManager = new InvitationManager();
            var complist = new CompanyManager();



            if (CompId.HasValue)
            {





                var imageresult = objInvManager.GetCompanyImages(CompId.Value);
                //dlimages.DataSource = imageresult;
                //dlimages.DataBind();
                var compresult = complist.GetCmpny(CompId.Value).ToList();
                dlcompname.DataSource = compresult;
                dlcompname.DataBind();
                var comp_name = compresult.FirstOrDefault();
                Label1.Text = comp_name.CompanyName;
                Label2.Text = comp_name.CompanyName;
                //DataList1.DataSource = compresult.FirstOrDefault(); ;
                //DataList1.DataBind();



                int productId = 0;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var image1 = context.CompanyProducts.FirstOrDefault(x => x.ComapnyId == CompId);
                    CompanyProduct image =
                        context.CompanyProducts.Where(x => x.ComapnyId == compId).OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();


                    if (image != null)
                    {
                        prdtimg.ImageUrl = new FileStoreService().GetDownloadUrl(image.ProductImageId);
                        lblDescription.Text = image.ProductDescription.Replace("\n", "<br/>");
                        productId = image.Id;
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        prdtimg.Visible = false;
                        lblDescription.Visible = false;
                        prooverview.Visible = false;
                    }
                    //lblDescription.Text = image.ProductDescription;
                    var follow_resul = context.Companies.Where(x => x.Id == compId).FirstOrDefault();
                    if (follow_resul.Userid == LoginUserId)
                    {
                        //btn_follow.Visible = false;
                        //btn_following.Visible = false;
                        div_follow.Visible = false;
                        div_following.Visible = false;
                    }
                    else if (IsThisUserFollowingCompany(follow_resul.Userid))
                    {

                        //btn_following.Visible = true;
                        //btn_follow.Visible = false;
                        div_following.Visible = true;
                        div_follow.Visible = false;
                    }
                    else if (!IsThisUserFollowingCompany(follow_resul.Userid))
                    {
                        // btn_follow.Visible = true;
                        div_follow.Visible = true;
                    }

                }

                if (productId > 0)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        //int? userid =Common.GetLoggedInUserId();
                        var user = Common.GetLoginUser(Session);
                        if (user != null)
                        {
                            var photo = user.PersonalLogoFileStoreId;
                            hdnUserImage.Value = new FileStoreService().GetDownloadUrl(photo);
                        }
                        else
                            hdnUserImage.Value = "";
                    }
                    var mainfeed = FeedManager.getMainFeed(Huntable.Business.FeedManager.FeedType.Company_Product, productId);
                    if (mainfeed != null)
                    {
                        litLikeComment.Text = "<div id='feed_"+ mainfeed.Id +"'>"
                                + FeedContentManager.getLikeComment(mainfeed, LoginUserId)
                                + "<script type='text/javascript'>"
                                + "$(document).ready(function(){ GetComments(" + mainfeed.Id + ");DisplayComment(" + mainfeed.Id + ",'"+hdnUserImage.Value+"');});"
                                + "</script>"
                                +"</div>";
                    }
                }
            }

            overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(compId));
            activity.HRef = "businessactivity.aspx?Id=" + compId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + compId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + compId;
            careers.HRef = "companyjobs.aspx?Id=" + compId;
            article.HRef = "article.aspx?Id=" + compId;
            a_next.HRef = "companyjobs.aspx?Id=" + compId;
            LoggingManager.Debug("Exiting Page_Load - CompanyProducts");
            //using (var context = huntableEntities.GetEntitiesWithNoLock())
            //{
            //    var result = context.Companies.FirstOrDefault(x => x.Id == CompId);
            //    if (IsThisUserFollowingCompany(result.Userid) == true)
            //    {
            //        btn_follow.Visible = true;
            //    }
            //}
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();


        }
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
                    return false;

            }


        }
        private int? CompId
        {
            get
            {
                LoggingManager.Debug("Entering CompId - CompanyProducts");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting CompId - CompanyProducts");

                return null;
            }
        }

        public string FollowersPhoto(object id)
        {
            LoggingManager.Debug("Entering FollowersPhoto - CompanyProducts");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);
                var photo = result.PersonalLogoFileStoreId;

                LoggingManager.Debug("Exiting FollowersPhoto - CompanyProducts");

                return new FileStoreService().GetDownloadUrl(photo);
            }

        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompanyProducts");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.CompanyProducts.FirstOrDefault(x => x.ProductImageId == p);
                var photo = result.ProductImageId;

                LoggingManager.Debug("Exiting Picture - CompanyProducts");

                return new FileStoreService().GetDownloadUrl(photo);
            }

        }

        public string portfolio(object id)
        {
            LoggingManager.Debug("Entering portfolio - CompanyProducts");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.CompanyPortfolios.FirstOrDefault(x => x.PortfolioImageid == p);
                var photo = result.PortfolioImageid;

                LoggingManager.Debug("Exiting portfolio - CompanyProducts");

                return new FileStoreService().GetDownloadUrl(photo);
            }

        }
        private int? compId
        {
            get
            {
                LoggingManager.Debug("Entering compId - CompanyProducts");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting compId - CompanyProducts");
                return null;
            }
        }
        protected void Follow(object sender, EventArgs e)
        {

            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (compId != null)
                {
                    if (btn_follow.Text == "Follow")
                        if (loginUserId != null && CompanyManager.FollowCompany(loginUserId.Value, compId.Value))
                            btn_follow.Text = "following";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
                }
            }
            catch (Exception)
            {
                { }
                throw;
            }

        }
        protected void Following(object sender, EventArgs e)
        {

            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (compId != null)
                {

                    if (loginUserId != null)
                    {
                        CompanyManager.UnfollowCompany(loginUserId.Value, compId.Value);
                        div_following.Visible = false;
                        div_follow.Visible = true;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                    }
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            //using (var context = huntableEntities.GetEntitiesWithNoLock())
            //{
            //    var Queuser = context.Companies.FirstOrDefault(x => x.Id == compId);
            //    //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
            //    var result = context.PreferredFeedUserUsers.Where(y => y.UserId == Queuser.Userid && y.FollowingUserId == loginUserId).FirstOrDefault();

            //    context.DeleteObject(result);
            //    context.SaveChanges();
            //    var comp_foll = context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == compId && x.FollowingUserId == loginUserId).FirstOrDefault();
            //    context.DeleteObject(comp_foll);
            //    context.SaveChanges();

            //}
            //div_following.Visible = true;
            //div_follow.Visible = false;

        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - CompanyProducts");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - CompanyProducts");
                return null;
            }

        }

    }
}