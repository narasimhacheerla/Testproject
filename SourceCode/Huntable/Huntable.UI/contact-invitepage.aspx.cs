using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using OAuthUtility;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class contact_invitepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - contact_invitepage ");

            if(Request["flag"]!=null)
            {
                TestInvitation();
            }
            LoggingManager.Debug("Exiting Page_Load - contact_invitepage ");

        }

        private void TestInvitation()
        {
            LoggingManager.Debug("Entering TestInvitation - contact_invitepage ");

            var userId = Common.GetLoggedInUserId(Session);
            if(userId!=null)
            {
                var invmMngr = new InvitationManager();
                invmMngr.SendEmailInvitation(userId.Value);
            }
            LoggingManager.Debug("Exiting TestInvitation - contact_invitepage ");

        }

        private string Provider
        {
            get { return Request["provider"] ?? ""; }
        }

        private int UserId
        {
            get
            {
                var result = 0;
                if (Request["ref"] != null)
                    int.TryParse(Request["ref"], out result);
                return result;
            }
        }

        protected void lbtnInvite_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnInvite_Click - contact_invitepage ");


            if (UserId != 0 && Provider != "")
            {
                var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
                var callbackuri = baseUrl + "oauth.aspx";
                Session["oauthmode"] = "email";

                Session["senderid"] = UserId;

                if (Provider == "live")
                    Response.Redirect("oauth.aspx?currpage=live", false);

                try
                {
                    OAuthWebSecurity.RequestAuthentication(Provider, callbackuri);
                }
                catch
                {

                }


            }

            LoggingManager.Debug("Exiting lbtnInvite_Click - contact_invitepage ");
        }

    }
}