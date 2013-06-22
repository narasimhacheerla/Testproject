using System;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class ProfileFree : System.Web.UI.Page
    {
        protected void Button1Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Button1Click - ProfileFree.aspx");
            Response.Redirect("Profile-premium.aspx", false);
            LoggingManager.Debug("Exiting Button1Click - ProfileFree.aspx");
        }

        protected void LnkHomeClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering LnkHomeClick - ProfileFree.aspx");
            try
            {
                LoggingManager.Info("Redirecting to Profile-premium.aspx");
                Response.Redirect("Profile-premium.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LnkHomeClick - ProfileFree.aspx");

            Response.Redirect("Default.aspx", false);

        }

        protected void LnkProfileClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering LnkProfileClick - ProfileFree.aspx");
            try
            {
                LoggingManager.Info("Redirecting to Default.aspx"); 
                Server.Transfer("Default.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LnkProfileClick - ProfileFree.aspx");

            Response.Redirect("Profile-premium.aspx", false);
            
        }

           



        protected void lnkProfile_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkProfile_Click - ProfileFree.aspx");
            try
            {
                LoggingManager.Info("Redirecting to Profile-premium.aspx");
                Server.Transfer("Profile-premium.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting lnkProfile_Click - ProfileFree.aspx");

        }

        protected void LnkJobsClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering LnkJobsClick - ProfileFree.aspx");
            try
            {
                LoggingManager.Info("Redirecting to JobSearch.aspx");
                Server.Transfer("JobSearch.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LnkJobsClick - ProfileFree.aspx");

            Response.Redirect("JobSearch.aspx", false);

        }
    }
}