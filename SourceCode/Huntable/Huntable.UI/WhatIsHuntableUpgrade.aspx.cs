using System;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Entities;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class page24 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - WhatIsHuntableUpgrade.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            if (loggedInUserId != null)
            {
                var result = jobManager.GetUserDetails(loggedInUserId.Value);
                accountstat.Text = result.IsPremiumAccount==true ? "Premium" : "Basic";
                if (result.IsPremiumAccount==true)
                {
                    btnupgrade1.Visible = false;
                    btnupgrade2.Visible = false;
                    UpgradeBox.Visible = false;
                    up1.Visible = false;
                }
            }
            LoggingManager.Debug("Exiting Page_Load - WhatIsHuntableUpgrade.aspx");
        }

        protected void BtnbasicClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnbasicClick - WhatIsHuntableUpgrade.aspx");
            var featiredSele = new FeaturedSelections();
            featiredSele.PremiumPackage = 10;
            Session["FeaturedSelections"] = featiredSele;
            Response.Redirect("securecheckout.aspx?amt=10&SuccessUrl=WhatIsHuntableUpgrade.aspx&FailureUrl=WhatIsHuntableUpgrade.aspx", false);

            LoggingManager.Debug("Exiting BtnbasicClick - WhatIsHuntableUpgrade.aspx");
        }
        protected void CheckBoxRequiredServerValidate(object sender, ServerValidateEventArgs e)
        {
            LoggingManager.Debug("Entering CheckBoxRequiredServerValidate - WhatIsHuntableUpgrade.aspx");
            e.IsValid = chck.Checked;
            LoggingManager.Debug("Exiting CheckBoxRequiredServerValidate - WhatIsHuntableUpgrade.aspx");
        }

        protected void Btnupgrade1Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Btnupgrade1_Click - WhatIsHuntableUpgrade.aspx");
            Response.Redirect("securecheckout.aspx?amt=10&SuccessUrl=WhatIsHuntableUpgrade.aspx&FailureUrl=WhatIsHuntableUpgrade.aspx");
            LoggingManager.Debug("Exiting Btnupgrade1Click - WhatIsHuntableUpgrade.aspx");
        }
       
        protected void lnkTerms_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkTerms_Click - Site.Master");

            Server.Transfer("Terms.aspx");

            LoggingManager.Debug("Exiting lnkTerms_Click - Site.Master");
        }

        protected void lnkPrivacy_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkPrivacy_Click - Site.Master");

            Server.Transfer("PrivacyPolicy.aspx");

            LoggingManager.Debug("Exiting lnkPrivacy_Click - Site.Master");
        }
       
    }
}