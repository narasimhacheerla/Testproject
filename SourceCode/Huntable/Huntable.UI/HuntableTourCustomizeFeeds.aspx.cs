using System;
using System.Linq;
using Huntable.Data;
using Snovaspace.Util.Logging;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace Huntable.UI
{
    public partial class CustomizeFeeds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HuntableTourCustomizeFeeds.aspx");

            UserControl uch = Page.Master.FindControl("headerAfterLoggingIn") as UserControl;
            HtmlGenericControl DivMenu = (HtmlGenericControl)uch.FindControl("menu");
            HtmlGenericControl DivMsg = (HtmlGenericControl)uch.FindControl("Div1");
            HtmlGenericControl DivMenu2 = (HtmlGenericControl)uch.FindControl("menu2");
            DivMenu.Visible = false;
            //DivMsg.Visible = false;
            DivMenu2.Visible = false;

            LoggingManager.Debug("Exiting Page_Load - HuntableTourCustomizeFeeds.aspx");

        }

        protected void btnIGotIt_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnIGotIt_Click - HuntableTourCustomizeFeeds.aspx");
            try
            {
                if (Session["LoggedInUserId"] != null)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var userId = Convert.ToInt32(Session["LoggedInUserId"]);
                        var user = context.Users.First(u => u.Id == userId);
                        user.IsCustomizingYourFeedsAccepted = true;
                        context.SaveChanges();
                      
                    }
                }
                Response.Redirect("ProfileUploadPage.aspx", false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting btnIGotIt_Click - HuntableTourCustomizeFeeds.aspx");
        }
    }
}