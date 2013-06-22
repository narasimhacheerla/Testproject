using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using System.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompaniesIndustry : System.Web.UI.Page
    {
        protected string LetterFilter
        {
            get
            {
                return Session["Companies_letterFilter"] as string;
            }
            set
            {
                Session["Companies_letterFilter"] = value == "All" ? null : value;
            }
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Prerender - CompaniesIndustry");

            DisplayData();

            LoggingManager.Debug("Exiting Page_Prerender - CompaniesIndustry");
        }

        private void DisplayData()
        {

            LoggingManager.Debug("Entering DisplayData - CompaniesIndustry");

            IList<int> countryIds = (from RepeaterItem repeaterItem in rspCountry.Items select repeaterItem.FindControl("chbcountry") as CheckBox into countryCheckbox where countryCheckbox != null && countryCheckbox.Checked select MasterDataManager.AllCountries.First(x => x.Description == countryCheckbox.Text).Id).ToList();

            UpdateCompanies(countryIds);
            LettersBind();

            LoggingManager.Debug("Exiting DisplayData - CompaniesIndustry");

        }

        private void LettersBind()
        {
            LoggingManager.Debug("Entering LettersBind - CompaniesIndustry");

            // Declares a variable that will store a referance to the DataTable we are 
            //  going to bind the repeater control to.
            DataTable dt;

            //------------------------------------------------------------------------
            // Get the appropriate set of records to view/edit
            if (Session[ToString() + "_LettersData"] == null)
            {

                string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", 
                             "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", 
                             "U", "V", "W", "X", "Y", "Z", "All"};

                // Create a new data table
                dt = new DataTable();

                // Create the scheme of the table
                dt.Columns.Add(new DataColumn("Letter", typeof(string)));

                // Populate the data table with the letter data
                foreach (string t in letters)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = t;
                    dt.Rows.Add(dr);
                }

                // Store a referance to the newly create data tabel in the session for 
                //  use on post back.
                Session[ToString() + "_LettersData"] = dt;
            }
            else
                dt = (DataTable)Session[ToString() + "_LettersData"];

            //------------------------------------------------------------------------
            // Bind the data's default view to the grid
            lvLetters.DataSource = dt.DefaultView;
            lvLetters.DataBind();

            LoggingManager.Debug("Exiting LettersBind - CompaniesIndustry");
        }

        protected void LettersItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            LoggingManager.Debug("Entering LettersItemDataBound - CompaniesIndustry");

            var data = (DataRowView)eventArgs.Item.DataItem;

            if ((string)data[0] == LetterFilter || (LetterFilter == null && (string)data[0] == "All"))
            {
                var lnkletter = (LinkButton)eventArgs.Item.FindControl("lnkletter");
                lnkletter.Enabled = false;
            }
            LoggingManager.Debug("Exiting LettersItemDataBound - CompaniesIndustry");

        }

        protected void LettersItemCommand(object source, CommandEventArgs commandEventArgs)
        {
            LoggingManager.Debug("Entering LettersItemCommand - CompaniesIndustry");
            if (commandEventArgs.CommandName == "Filter")
            {
                LetterFilter = (string) commandEventArgs.CommandArgument;
            }
            LoggingManager.Debug("Exiting LettersItemCommand - CompaniesIndustry");
        }

        protected void BtnCompaniesSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompaniesSearchClick - CompaniesIndustry");

            string url = string.Format("~/companiessearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
            new Utility().RedirectUrl(Response, url);

            LoggingManager.Debug("Exiting BtnCompaniesSearchClick - CompaniesIndustry");

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompaniesIndustry");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                hfCountry.Value = "20";
                var countries = context.MasterIndustries.ToList().Take(Convert.ToInt32(hfCountry.Value));
                rspCountry.DataSource = countries;
                rspCountry.DataBind();
            }
            var cmpMgr = new CompanyManager();
            rspdata.DataSource = cmpMgr.GetCompaniesCountryList(0);
            rspdata.DataBind();
            var cbId = ((CheckBox)rspCountry.FindControl("chbcountry"));

            LoggingManager.Debug("Exiting Page_Load - CompaniesIndustry");
        }
        private void UpdateCompanies(IList<int> countryIds)
        {
            LoggingManager.Debug("Entering UpdateCompanies - CompaniesIndustry");
            var cmpMgr = new CompanyManager();

            rspdata.DataSource = cmpMgr.GetCompaniesCountryList(LoginUserId,LetterFilter, countryIds);
            rspdata.DataBind();

            LoggingManager.Debug("Exiting UpdateCompanies - CompaniesIndustry");
        }
        protected void Showmore(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Showmore - CompaniesIndustry");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var countries = context.MasterIndustries.ToList();
                rspCountry.DataSource = countries;
                rspCountry.DataBind();
                lnkshow.Visible = false;
            }
            LoggingManager.Debug("Exiting Showmore - CompaniesIndustry");
        }

        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompaniesIndustry");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var photo = result.CompanyLogoId;

            LoggingManager.Debug("Exiting Picture - CompaniesIndustry");

                return new FileStoreService().GetDownloadUrl(photo);
            }

        }
        public int Followers(object id)
        {
            LoggingManager.Debug("Entering Followers - CompaniesIndustry");

            int s = Int32.Parse(id.ToString());
            var cmpMgr2 = new CompanyManager();

            LoggingManager.Debug("Exiting Followers - CompaniesIndustry");

            return cmpMgr2.GetcompanyFollowers(s);


        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - CompaniesIndustry");

            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Message sent succesfully')", true);
            }


            LoggingManager.Debug("Exiting FollowupClick - CompaniesIndustry");
        }
        protected void UnfollowCompanyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnfollowClick-CompaniesIndustry");
            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);

            }
            LoggingManager.Debug("Exiting Unfollowupclick-CompaniesIndustry");
        }
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompaniesFollowers");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompaniesFollowers");

                return 0;
            }
        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - CompaniesIndustry");

            int p = Int32.Parse(id.ToString());
            var cmpMgr1 = new CompanyManager();

            LoggingManager.Debug("Exiting Jobs - CompaniesIndustry");

            return cmpMgr1.GetJobsPostedByCompany(p);
            

        }


    }
}