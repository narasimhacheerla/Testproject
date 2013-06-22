using System;
using System.Linq;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Huntable.UI
{
    public partial class TourOfHuntable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HuntableTourFeatures.aspx");
            if (!IsPostBack)
            {
                UserControl uch = Page.Master.FindControl("HeaderAfterLoggingIn") as UserControl;
                var DivMenu = (HtmlGenericControl)uch.FindControl("menu");
                var DivMsg = (HtmlGenericControl)uch.FindControl("Div1");
                var DivMenu2 = (HtmlGenericControl)uch.FindControl("menu2");
                DivMenu.Visible = false;
                //DivMsg.Visible = false;
               DivMenu2.Visible=false;
            }
            LoggingManager.Debug("Exiting Page_Load - HuntableTourFeatures.aspx");
        }

        protected void BtnIGotItClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnIGotItClick - HuntableTourFeatures.aspx");
            try
            {
                if (Session["LoggedInUserId"] != null)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var userId = Convert.ToInt32(Session["LoggedInUserId"]);
                        var user = context.Users.First(u => u.Id == userId);
                        user.IsSearchingAndConnectingWithOtherUsersAccepted = true;
                        context.SaveChanges();
                    }
                }

                Response.Redirect("HuntableTourCustomizeJobs.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting BtnIGotItClick - HuntableTourFeatures.aspx");
        }
    }
}