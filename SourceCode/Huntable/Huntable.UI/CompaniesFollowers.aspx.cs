using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.FileDataStore;
using System.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompaniesFollowers : System.Web.UI.Page
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
            LoggingManager.Debug("Entering PagePrerender - CompaniesFollowers");

            DisplayData();

            LoggingManager.Debug("Exiting PagePrerender - CompaniesFollowers");
        }

        private void DisplayData()
        {
            LoggingManager.Debug("Entering DisplayData - CompaniesFollowers");

            IList<int> followersIds = (from RepeaterItem repeaterItem in rspFollowers.Items select repeaterItem.FindControl("chbfollowers") as CheckBox into followerCheckbox where followerCheckbox != null && followerCheckbox.Checked select MasterDataManager.AllEmployees.First(x => x.NoofEmployess == followerCheckbox.Text).Id).ToList();
            List<MasterEmployee> masterEmployees = new List<MasterEmployee>();
            foreach (var followerId in followersIds)
            {
                masterEmployees.Add(MasterDataManager.AllEmployees.Single(x => x.Id == followerId));
            }
            UpdateCompanies(masterEmployees);
            LettersBind();
          //  UpdateCompanies(null);
          //LettersBind();
            LoggingManager.Debug("Exiting DisplayData - CompaniesFollowers");
        }

        private void LettersBind()
        {
            LoggingManager.Debug("Entering LettersBind - CompaniesFollowers");

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
            LoggingManager.Debug("Exiting LettersBind - CompaniesFollowers");
        }

        protected void LettersItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            LoggingManager.Debug("Entering LettersItemDataBound - CompaniesFollowers");

            var data = (DataRowView)eventArgs.Item.DataItem;

            if ((string)data[0] == LetterFilter || (LetterFilter == null && (string)data[0] == "All"))
            {
                var lnkletter = (LinkButton)eventArgs.Item.FindControl("lnkletter");
                lnkletter.Enabled = false;
            }
            LoggingManager.Debug("Exiting LettersItemDataBound - CompaniesFollowers");
        }
        
        protected void BtnCompaniesSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompaniesSearchClick - CompaniesFollowers");

            string url = string.Format("~/companiessearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
            new Utility().RedirectUrl(Response, url);

            LoggingManager.Debug("Exiting BtnCompaniesSearchClick - CompaniesFollowers");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompaniesFollowers");

            if (!Page.IsPostBack)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    var followers = MasterDataManager.AllEmployees.ToList();
                    rspFollowers.DataSource = followers;
                    rspFollowers.DataBind();
                }
                var cmpMgr = new CompanyManager();
                rspdata.DataSource = cmpMgr.GetCompaniessizeList(LoginUserId,null);
                rspdata.DataBind();

                LoggingManager.Debug("Exiting Page_Load - CompaniesFollowers");
            }
        }
        protected void LettersItemCommand(object source, CommandEventArgs commandEventArgs)
        {
            LoggingManager.Debug("Entering LettersItemCommand - CompaniesFollowers");

            if (commandEventArgs.CommandName == "Filter")
            {
                LetterFilter = (string)commandEventArgs.CommandArgument;
            }

            LoggingManager.Debug("Exiting LettersItemCommand - CompaniesFollowers");
        }
        private void UpdateCompanies(List<MasterEmployee> followersIds)
        {
            LoggingManager.Debug("Entering UpdateCompanies - CompaniesFollowers");

            var cmpMgr = new CompanyManager();

            rspdata.DataSource = cmpMgr.GetCompanyFollowersIList(LoginUserId, followersIds, LetterFilter);
            rspdata.DataBind();

            LoggingManager.Debug("Exiting UpdateCompanies - CompaniesFollowers");
        }
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchClick - CompaniesFollowers");

            IList<int> followerIds = (from RepeaterItem repeaterItem in rspFollowers.Items select repeaterItem.FindControl("chbfollowers") as CheckBox into countryCheckbox where countryCheckbox != null && countryCheckbox.Checked select MasterDataManager.AllEmployees.First(x => x.NoofEmployess == countryCheckbox.Text).Id).ToList();

            List<MasterEmployee> masterEmployees = new List<MasterEmployee>();
            foreach (var followerId in followerIds)
            {
                masterEmployees.Add(MasterDataManager.AllEmployees.Single(x => x.Id == followerId));
            }

            UpdateCompanies(masterEmployees);

            LoggingManager.Debug("Exiting BtnSearchClick - CompaniesFollowers");
        }
        

        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompaniesFollowers");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var photo = result.CompanyLogoId;

            LoggingManager.Debug("Exiting Picture - CompaniesFollowers");

                return new FileStoreService().GetDownloadUrl(photo);
            }

            

        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - CompaniesFollowers");

            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }


            LoggingManager.Debug("Exiting FollowupClick - CompaniesFollowers");
        }
        protected void UnfollowCompanyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnfollowClick-CompaniesFollowers");
            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
            }
            LoggingManager.Debug("Exiting Unfollowupclick-CompaniesFollowers");
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
        public int Followers(object id)
        {
            LoggingManager.Debug("Entering Followers - CompaniesFollowers");

            int s = Int32.Parse(id.ToString());
            var cmpMgr2 = new CompanyManager();

            LoggingManager.Debug("Exiting Followers - CompaniesFollowers");

            return cmpMgr2.GetcompanyFollowers(s);
            
        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - CompaniesFollowers");

            int p = Int32.Parse(id.ToString());
            var cmpMgr1 = new CompanyManager();

            LoggingManager.Debug("Exiting Jobs - CompaniesFollowers");

            return cmpMgr1.GetJobsPostedByCompany(p);

           

        }
        protected void ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LoggingManager.Debug("Entering ItemDataBound - CompaniesFollowers");
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

            LoggingManager.Debug("Exiting ItemDataBound - CompaniesFollowers");
        }

        public string UrlGenerator(object id)
        {

            LoggingManager.Debug("Entering UrlGenerator - CompaniesFollowers");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {

                LoggingManager.Debug("Entering UrlGenerator - CompaniesFollowers");
                return null;
            }

        }
     

    }
}