using System;
using System.Linq;
using Huntable.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CustomizeJobs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HuntableTourCustomizejobs.aspx");

            UserControl uch = Page.Master.FindControl("headerAfterLoggingIn") as UserControl;
            HtmlGenericControl DivMenu = (HtmlGenericControl)uch.FindControl("menu");
            HtmlGenericControl DivMsg = (HtmlGenericControl)uch.FindControl("Div1");
            HtmlGenericControl DivMenu2 = (HtmlGenericControl)uch.FindControl("menu2");
            DivMenu.Visible = false;
            //DivMsg.Visible = false;
            DivMenu2.Visible = false;

            LoggingManager.Debug("Exiting Page_Load - HuntableTourCustomizejobs.aspx");
        }

        protected void BtnIGotItClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnIGotItClick - HuntableTourCustomizejobs.aspx");

            if (Session["LoggedInUserId"] != null)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var userId = Convert.ToInt32(Session["LoggedInUserId"]);
                    var user = context.Users.First(u => u.Id == userId);
                    user.IsCustomizingYourJobsAccepted = true;
                    context.SaveChanges();
                }
            }

            Response.Redirect("HuntableTourCustomizeFeeds.aspx", false);

            LoggingManager.Debug("Exiting BtnIGotItClick - HuntableTourCustomizejobs.aspx");
        }
    }
}