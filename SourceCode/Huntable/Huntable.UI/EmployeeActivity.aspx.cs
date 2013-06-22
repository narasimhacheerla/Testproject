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
    public partial class EmployeeActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - EmployeeActivity.aspx");
            overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(compId));
            activity.HRef = "businessactivity.aspx?Id=" + compId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + compId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + compId;
            careers.HRef = "companyjobs.aspx?Id=" + compId;
            article.HRef = "article.aspx?Id=" + compId;
            cmpvw.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(compId));
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var cmpny = context.Companies.FirstOrDefault(x => x.Id == compId);
                var comp = context.Users.FirstOrDefault(x => x.Id == LoginUserId && x.IsCompany == true);
                var companyid = 0;
                if (comp != null)
                {
                    companyid = context.Companies.FirstOrDefault(x => x.Userid == comp.Id).Id;
                }
                //else
                //{
                //    companyid = compId.Value;
                //}
                
                    if (compId.Value == companyid)
                    {
                        follo.Visible = false;
                        Unfollo.Visible = false;
                        dvcmp.Visible = false;
                    }
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
                        UserFeedList1.profileUserId = LoginUserId.ToString();
                   
                if (cmpny != null)
                    UserFeedList1.profileUserId = cmpny.Userid.ToString();

            }
            bact.HRef = "businessactivity.aspx?Id=" + compId;
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
            LoggingManager.Debug("Exiting Page_Load - EmployeeActivity.aspx");


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
            LoggingManager.Debug("Entering Follow_Click - EmployeeActivity");
            //var button = sender as Button;
            if (compId.HasValue)
            {
                //int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, compId.Value);
                follo.Visible = false;
                Unfollo.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }
            LoggingManager.Debug("Exiting Follow_Click - EmployeeActivity");
        }
        protected void unfollow_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnFollow_Click - EmployeeActivity");
            //var button = sender as Button;
            if (compId.HasValue)
            {
                //int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, compId.Value);
                follo.Visible = true;
                Unfollo.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
            }
            LoggingManager.Debug("Exiting UnFollow_Click - EmployeeActivity");
        }
        public int LoginUserId
        {


            get
            {
                LoggingManager.Debug("Entering LoginUserId - EmployeeActivity");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - EmployeeActivity");

                return 0;
            }
        }
    }
}