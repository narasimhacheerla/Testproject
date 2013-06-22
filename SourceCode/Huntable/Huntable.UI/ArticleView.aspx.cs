using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI
{
    public partial class ArticleView : System.Web.UI.Page
    {
        public int? Atid
        {

            get
            {
                LoggingManager.Debug("Entwering Atid - ArticleView");
                int otherUserId;
                if (int.TryParse(Request.QueryString["ATId"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting Atid - ArticleView");

                return null;
            }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ArticleView");

            var complist = new CompanyManager();
            if (CompId.HasValue)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    LoggingManager.Debug("Exiting GetCmpny1  - CompanyManager.cs");

                    string result = context.CompanyArticles.Where(x => x.CompanyId == CompId.Value && x.Id == Atid.Value).FirstOrDefault().ArticleDescription;

                    if (Atid != null)
                    {
                        //var result = complist.GetCmpny1(CompId.Value, Atid.Value);
                        var result1 = complist.GetCmpny(CompId.Value).FirstOrDefault();
                        var result2 = complist.GetCmpny2(CompId.Value);
                        if (result1 != null)
                        {
                            Label1.Text = result1.CompanyName;
                            lbl_comp_name.Text = result1.CompanyName;
                        }
                        // dl2.DataSource = result1;
                        //dl2.DataBind();
                        articledesc.Text = result.Replace("\n", "<br/>");
                    }
                }

            }


            a_overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(CompId));
            a_Activity.HRef = "businessactivity.aspx?Id=" + CompId;
            a_Products.HRef = "companyproducts.aspx?Id=" + CompId;
            a_careers.HRef = "companyjobs.aspx?Id=" + CompId;
            a_Business.HRef = "company-blogs-popular.aspx?Id=" + CompId;
            a_Article.HRef = "article.aspx?Id=" + CompId;
            a_comp_name.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(CompId));


            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var followResul = context.Companies.FirstOrDefault(x => x.Id == CompId);
                if (followResul != null && followResul.Userid == LoginUserId)
                {

                    div_follow.Visible = false;
                    div_following.Visible = false;
                }
                else if (followResul != null && IsThisUserFollowingCompany(followResul.Userid))
                {

                    div_following.Visible = true;
                    div_follow.Visible = false;
                }
                else if (followResul != null && !IsThisUserFollowingCompany(followResul.Userid))
                {

                    div_follow.Visible = true;
                }
            }
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();


            LoggingManager.Debug("Exiting Page_Load - ArticleView");

        }

        private int? compId = -1;
        private int? CompId
        {
            get
            {
                if (compId == -1)
                {
                    compId = OtherUserId();
                }
                return compId;
            }
        }

        private int? OtherUserId()
        {
            LoggingManager.Debug("Entering compId - ArticleView");
            int otherUserId;
            if (int.TryParse(Request.QueryString["Id"], out otherUserId))
            {
                return otherUserId;
            }
            LoggingManager.Debug("Exiting compId - ArticleView");

            return null;
        }

        public string Picture(object id)
        {

            LoggingManager.Debug("Entering Picture - ArticleView");

            if (id != null)
            {
                int p = Int32.Parse(id.ToString());
                return new FileStoreService().GetDownloadUrl(p);
            }
            else
            {
                LoggingManager.Debug("Exiting Picture - ArticleView");
                return new FileStoreService().GetDownloadUrl(null);
            }

        }
        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - CompanyView");

            var user_to = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == user_to && y.FollowingUserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                else
                    return false;

            }


        }
        protected void Follow(object sender, EventArgs e)
        {

            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (CompId != null)
                {
                    if (btn_follow.Text == "Follow")
                        if (loginUserId != null && CompanyManager.FollowCompany(loginUserId.Value, CompId.Value))
                            btn_follow.Text = "following";
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
                if (CompId != null)
                {

                    if (loginUserId != null)
                    {
                        CompanyManager.UnfollowCompany(loginUserId.Value, CompId.Value);
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

        }
    }
}