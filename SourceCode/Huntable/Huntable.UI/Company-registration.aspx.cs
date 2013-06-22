using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class Company_registration : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Company_registration");
            var recuiter = Request.QueryString["Id"];
            if (!Page.IsPostBack)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var countries = context.MasterCountries.ToList();
                    ddlcountry.DataSource = countries;
                    ddlcountry.DataTextField = "Description";
                    ddlcountry.DataValueField = "Id";
                    ddlcountry.DataBind();
                }
            }

            LoggingManager.Debug("Exiting Page_Load - Company_registration");
        }
        public int CountryId
        {
            get
            {
                LoggingManager.Debug("Entering CountryId - Company_registration");

                int id;
                return Int32.TryParse(ddlcountry.SelectedValue, out id) ? id : -1;
            }
            set
            {
                if (ddlcountry.Items.FindByValue(value.ToString()) != null)
                    ddlcountry.SelectedValue = value.ToString();
            }


        }
        protected void CompanystartClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering CompanystartClick - Company_registration");
            var recuiter = Request.QueryString["Id"];
            if (Page.IsValid)
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var companynm = context.Companies.FirstOrDefault(x => x.CompanyName.ToLower() == txtCompanyName.Text.ToLower());
                        if (companynm == null)
                        {
                            var user = Common.GetLoggedInUser(context);
                            //if (user.IsPremiumAccount == true)
                            //{
                            string companywebsite = txtCompanywebsite.Text;
                            string companyname = txtCompanyName.Text;
                            string comapnyemail = txtComapnyEmail.Text;
                            //int countryid = CountryId;
                            int countryid = Convert.ToInt32(ddlcountry.SelectedItem.Value);
                            string cnfrmPassword = txtConfrmPassword.Text;
                            bool rec = recuiter != null;
                            var email = txtComapnyEmail.Text;
                            var companyemailtest =
                                context.Companies.FirstOrDefault(x => x.CompanyEmail == txtComapnyEmail.Text);
                            if (companyemailtest == null)
                            {
                                var cmMgr = new CompanyManager();
                              
                                 cmMgr.CompanyFirstRegistrayion(companyname, companywebsite, countryid,
                                                                                 comapnyemail,
                                                                                 loginUserId.Value, cnfrmPassword, rec);
                             
                                lblMessage.Visible = true;
                                lblMessage.Text =
                                    "Company has been created successfully. Please confirm your email address";
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                txtComapnyEmail.Text = "";
                                txtCompanyName.Text = "";
                                txtCompanywebsite.Text = "";
                                txtConfrmPassword.Text = "";
                                txtPassword.Text = "";
                               
                                Response.Redirect("EmailConfirmationSentSuccess.aspx?Id=" + email+"&companyid="+1);
                            }
                            else
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "Email has been already registered with us";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }

                            //}
                            //else
                            //{
                            //    lblMessage.Visible = true;
                            //    lblMessage.Text = "Only Premium members can start a Company page. Upgrade and Try again";
                            //    lblMessage.ForeColor = System.Drawing.Color.Red;
                            //}
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Companies", "alert('Company name is already taken. Choose another name');", true);
                        }
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please login to register for recruiter";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            LoggingManager.Debug("Exiting CompanystartClick - Company_registration");
        }

    }
}