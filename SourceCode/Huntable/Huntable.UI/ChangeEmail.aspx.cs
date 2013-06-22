using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;
using Huntable.Business;

namespace Huntable.UI
{
    public partial class ChangeEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
            LoggingManager.Debug("Entering Page_Load  ChangeEmail");
            try
            {
                bool userLoggedIn = Common.IsLoggedIn();
                var user = Common.GetLoggedInUser();

                if (userLoggedIn)
                {
                    lblemail.Text = user.EmailAddress;
                    lblpassword.Text = user.Password;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load  ChangeEmail");
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnChange_Click ChangeEmail");

            Response.Redirect("ChangeEmailSettings.aspx");

            LoggingManager.Debug("Exiting btnChange_Click ChangeEmail");
        }

        protected void btnPwd_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnPwd_Click ChangeEmail");

            Response.Redirect("ChangeEmailSettings.aspx");

            LoggingManager.Debug("Exiting btnPwd_Click ChangeEmail");

        }

        protected void showpassword(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering showpassword ChangeEmail");

            lblpassword.Visible = true;

            LoggingManager.Debug("Exiting showpassword ChangeEmail");
        }

      

        protected void shwpsd_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering shwpsd_Click ChangeEmail");
            //var user = Common.GetLoggedInUser();
            //if(user.TempPwd==null)
            lblpassword.Visible = true;
            //else
            //{
            //    lblpassword.Text = user.TempPwd;
            //    lblpassword.Visible = true;
            ////}
            dtpass.Visible = false;

            LoggingManager.Debug("Exiting shwpsd_Click ChangeEmail");
        }
    }
}