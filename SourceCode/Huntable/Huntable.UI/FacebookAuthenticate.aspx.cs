using System;
using OAuthUtility;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class FacebookAuthenticate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - FacebookAuthenticate.aspx");

            LoggingManager.Debug("Exiting Page_Load - FacebookAuthenticate.aspx");
        }



        protected void lbtnConnect_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering lbtnConnect_Click - FacebookAuthenticate.aspx");

            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var callbackuri = baseUrl + "oauth.aspx";
            Session["oauthmode"] = "facebookconnect";
            OAuthWebSecurity.RequestAuthentication("Facebook", callbackuri);
            LoggingManager.Debug("Exiting lbtnConnect_Click - FacebookAuthenticate.aspx");
        }
    }
}