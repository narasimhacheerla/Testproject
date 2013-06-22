using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class ImportContacts : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void IbtnFacebookClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("oauth.aspx?currpage=facebook", false);
        }
        protected void IbtnLinkedInClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("oauth.aspx?currpage=linkedin", false);
        }
        protected void IbtnTwitterClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("oauth.aspx?currpage=twitter", false);
        }
        protected void IbtnGoogleClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnGoogleClick - InviteFriends.aspx");
            try
            {
                Response.Redirect("oauth.aspx?currpage=gmail", false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnGoogleClick - InviteFriends.aspx");
        }
        protected void IbtnYahooClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnYahooClick - InviteFriends.aspx");
            try
            {
                Response.Redirect("oauth.aspx?currpage=yahoo");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnYahooClick - InviteFriends.aspx");
        }
        protected void IbtnLiveClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("oauth.aspx?currpage=live", false);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Server.Transfer("InviteFriends.aspx");
        }
    }
}