using System;
using System.Linq;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void BtnResetPasswordClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnResetPasswordClick - ResetPassword.aspx");

            string email = Request.QueryString["email"];

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var firstOrDefault = context.Users.FirstOrDefault(u => u.EmailAddress.Equals(email));
                User user = context.Users.SingleOrDefault(u => u.EmailAddress == firstOrDefault.EmailAddress);
                Login logins = context.Logins.FirstOrDefault(u => u.EmailAddress == email);
                if (user != null) user.Password = txtCnfrmpwd.Text;
                if (logins != null) logins.Password = txtCnfrmpwd.Text;
                context.SaveChanges();
                lblpasswd.Text = "Password has been changed";
                lblpasswd.ForeColor = System.Drawing.Color.Green;
                lblpasswd.Visible = true;
            }

            LoggingManager.Debug("Exiting BtnResetPasswordClick - ResetPassword.aspx");
        }
    }
}