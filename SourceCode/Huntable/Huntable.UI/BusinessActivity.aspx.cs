using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class BusinessActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - BusinessActivity");

            overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(compId));
            activity.HRef = "businessactivity.aspx?Id=" + compId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + compId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + compId;
            careers.HRef = "companyjobs.aspx?Id=" + compId;
            article.HRef = "article.aspx?Id=" + compId;
            cmpvw.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(compId));
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var comp = context.Users.FirstOrDefault(x => x.Id == LoginUserId && x.IsCompany == true);
                var companyid = 0;
                if (comp != null)
                {
                    companyid = context.Companies.FirstOrDefault(x => x.Userid == comp.Id).Id;
                }
                    if (compId.Value == companyid)
                    {
                        follo.Visible = false;
                        Unfollo.Visible = false;
                        dvcmp.Visible = false;
                    }
                    var cmpny = context.Companies.FirstOrDefault(x => x.Id == compId);
                    if (cmpny != null)
                    {
                        lblcname.Text = cmpny.CompanyName;
                        lblcmpny.Text = cmpny.CompanyName;
                    }
                    var followcmny = context.PreferredFeedUserCompaniesFollwers.FirstOrDefault(x => x.CompanyID == compId.Value && x.FollowingUserId == LoginUserId);
                    if (followcmny != null)
                    {
                        follo.Visible = false;
                        Unfollo.Visible = true;
                    }
                    else
                    {
                        Unfollo.Visible = false;
                        follo.Visible = true;
                    }
                    if (cmpny.Userid != null)
                        UserFeedList1.profileUserId = cmpny.Userid.ToString();
                
                else
                {
                    
                    UserFeedList1.profileUserId = cmpny.Userid.ToString();
                }

            }
            eact.HRef = "employeeactivity.aspx?Id=" + compId;

            LoggingManager.Debug("Exiting Page_Load - BusinessActivity");
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();

        }
        private int? compId
        {


            get
            {
                LoggingManager.Debug("Entering compId - BusinessActivity");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting compId - BusinessActivity");

                return null;
            }
        }
        protected void follow_click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Follow_Click - Employeeactivity");
            //var button = sender as Button;
            if (compId.HasValue)
            {
                //int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, compId.Value);
                follo.Visible = false;
                Unfollo.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }
            LoggingManager.Debug("Exiting Follow_Click - Employeeactivity");
        }
        protected void unfollow_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnFollow_Click - Employeeactivity");
            //var button = sender as Button;
            if (compId.HasValue)
            {
                //int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, compId.Value);

                follo.Visible = true;
                Unfollo.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
            }
            LoggingManager.Debug("Exiting UnFollow_Click - Employeeactivity");
        }
        public int LoginUserId
        {


            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompaniesHome");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompaniesHome");

                return 0;
            }
        }

    }
}

