using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using System.Web.UI;

namespace Huntable.UI
{
    public partial class CompaniesCountry : System.Web.UI.Page
    {
        public int LoginUserId
        {

          get
            {
                LoggingManager.Debug("Entering LoginUserId - CompaniesHome");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompaniesHome");

                return 0;
            }
        }
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
            LoggingManager.Debug("Entering Page_Prerender - CompaniesCountry");

            DisplayData();

            LoggingManager.Debug("Exiting Page_Prerender - CompaniesCountry");
        }

        private void DisplayData()
        {

            LoggingManager.Debug("Entering DisplayData - CompaniesCountry");

            IList<int> countryIds = (from RepeaterItem repeaterItem in rspCountry.Items select repeaterItem.FindControl("chbcountry") as CheckBox into countryCheckbox where countryCheckbox != null && countryCheckbox.Checked select MasterDataManager.AllCountries.First(x => x.Description == countryCheckbox.Text).Id).ToList();

            UpdateCompanies(countryIds);
            LettersBind();

            LoggingManager.Debug("Exiting DisplayData - CompaniesCountry");
        }

        private void LettersBind()
        {

            LoggingManager.Debug("Entering LettersBind - CompaniesCountry");

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
            LoggingManager.Debug("Exiting LettersBind - CompaniesCountry");

            
        }
        protected void BtnCompaniesSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompaniesSearchClick - CompaniesCountry");

            string url = string.Format("~/companiessearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
            new Utility().RedirectUrl(Response, url);

            LoggingManager.Debug("Exiting BtnCompaniesSearchClick - CompaniesCountry");

        }
     
        protected void LettersItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            LoggingManager.Debug("Entering LettersItemDataBound - CompaniesCountry");

            var data = (DataRowView)eventArgs.Item.DataItem;

            if ((string)data[0] == LetterFilter || (LetterFilter == null && (string)data[0] == "All"))
            {
                var lnkletter = (LinkButton)eventArgs.Item.FindControl("lnkletter");
                lnkletter.Enabled = false;
            }

            LoggingManager.Debug("Exiting LettersItemDataBound - CompaniesCountry");
        }

        protected void LettersItemCommand(object source, CommandEventArgs commandEventArgs)
        {

            LoggingManager.Debug("Entering LettersItemCommand - CompaniesCountry");

            if (commandEventArgs.CommandName == "Filter")
            {
                LetterFilter = (string)commandEventArgs.CommandArgument;
            }

            LoggingManager.Debug("Exiting LettersItemCommand - CompaniesCountry");

        }
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchClick - CompaniesCountry");

            IList<int> countryIds = (from RepeaterItem repeaterItem in rspCountry.Items select repeaterItem.FindControl("chbcountry") as CheckBox into countryCheckbox where countryCheckbox != null && countryCheckbox.Checked select MasterDataManager.AllCountries.First(x => x.Description == countryCheckbox.Text).Id).ToList();

            UpdateCompanies(countryIds);

            LoggingManager.Debug("Exiting BtnSearchClick - CompaniesCountry");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompaniesCountry");

            if (!Page.IsPostBack)
            {
                hfCountry.Value = "20";
                rspCountry.DataSource = MasterDataManager.AllCountries.ToList();
                rspCountry.DataBind();
            }
            LoggingManager.Debug("Exiting Page_Load - CompaniesCountry");
        }

        private void UpdateCompanies(IList<int> countryIds)
        {
            LoggingManager.Debug("Entering UpdateCompanies - CompaniesCountry");

            var cmpMgr = new CompanyManager();

            rspdata.DataSource = cmpMgr.GetCompaniesCountryList(LoginUserId,LetterFilter, countryIds);
            rspdata.DataBind();
            LoggingManager.Debug("Exiting UpdateCompanies - CompaniesCountry");

        }

        protected void Showmore(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Showmore - CompaniesCountry");
            //using (var context = huntableEntities.GetEntitiesWithNoLock())
            //{
            //    var countries = context.MasterCountries.ToList();
            //    rspCountry.DataSource = countries;
            //    rspCountry.DataBind();
            //    lnkshow.Visible = false;
            //}
            LoggingManager.Debug("Exiting Showmore - CompaniesCountry");
           
        }

        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompaniesCountry");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var photo = result.CompanyLogoId;

                LoggingManager.Debug("Exiting Picture - CompaniesCountry");

                return new FileStoreService().GetDownloadUrl(photo);
            }
            
        }

        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - CompaniesCountry");

            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }


            LoggingManager.Debug("Exiting FollowupClick - CompaniesCountry");
        }
        protected void UnfollowCompanyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnfollowClick-CompaniesCountry");
            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Successfully Unfollowed')", true);
            }
            LoggingManager.Debug("Exiting Unfollowupclick-CompaniesCountry");
        }
        public int Followers(object id)
        {
            LoggingManager.Debug("Entering Followers - CompaniesCountry");

            int s = Int32.Parse(id.ToString());
            var cmpMgr2 = new CompanyManager();

            LoggingManager.Debug("Exiting Followers - CompaniesCountry");

            return cmpMgr2.GetcompanyFollowers(s);
       
        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - CompaniesCountry");

            int p = Int32.Parse(id.ToString());
            var cmpMgr1 = new CompanyManager();

            LoggingManager.Debug("Exiting Jobs - CompaniesCountry");

            return cmpMgr1.GetJobsPostedByCompany(p);
        }
        protected void ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LoggingManager.Debug("Entering ItemDataBound - CompaniesCountry");

            Control div1 = e.Item.FindControl("Div1") as Control;
            Control div2 = e.Item.FindControl("div2") as Control;
            if (e.Item.DataItem != null)
            {
                int cmpid = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Id").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var companyid = context.Companies.FirstOrDefault(x => x.Userid == LoginUserId);
                    if (companyid != null && cmpid == companyid.Id)
                    {


                        div1.Visible = false;
                        div2.Visible = false;

                    }
                }
            }
            LoggingManager.Debug("Exiting ItemDataBound - CompaniesCountry");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - CompaniesCountry");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - CompaniesCountry");
                return null;
            }

        }
    }
}