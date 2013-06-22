using System;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class EmailConfirmationSentSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - EmailConfirmationSentSuccess.aspx");
            try
            {
                if (!IsPostBack)
                {
                    var email = Request.QueryString["Id"];
                   
                    if (email != null)
                    {
                        lblEmail.Text = email;
                    }
                    else
                    {
                        lblEmail.Text = Session[SessionNames.UserEmailAddress].ToString();
                        LoggingManager.Info("User Emailaddress :" + lblEmail.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - EmailConfirmationSentSuccess.aspx");
        }

        protected void BtnResentEmailClick(object sender, EventArgs e)
        {
           
            LoggingManager.Debug("Entering btnResentEmail_Click - EmailConfirmationSentSuccess.aspx");
            var id = Request.QueryString["companyid"];
           
            var applicationBaseUrl = Common.GetApplicationBaseUrl();
            if (id != null & id != string.Empty)
            {
                var company = Common.GetCompanyByEmailId(lblEmail.Text);
                  var user = Common.GetUserById(company.Userid.ToString());
                new CompanyManager().SendVerificationEMail(company,user.FirstName);
            }
            else
            {
                var user = Common.GetUserByEmailId(lblEmail.Text);
                new UserManager().SendVerificationEMail(user, applicationBaseUrl);
            }
           
            lblcon.Visible = true;
            LoggingManager.Debug("Exiting btnResentEmail_Click - EmailConfirmationSentSuccess.aspx");

        }

        protected void BtnTakeTourClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering btnTakeTour_Click - EmailConfirmationSentSuccess.aspx");
            try
            {
                LoggingManager.Info("Redirecting to customiszefeeds.aspx");
                Response.Redirect("customiszefeeds.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting btnTakeTour_Click - EmailConfirmationSentSuccess.aspx");

            Response.Redirect("customiszefeeds.aspx", false);
        }

        protected void LinkButton1Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering LinkButton1Click - EmailConfirmationSentSuccess.aspx");

            Server.Transfer("ChangeEmail.aspx");

            LoggingManager.Debug("Exiting LinkButton1Click - EmailConfirmationSentSuccess.aspx");
        }
    }
}