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
    public partial class CompaniesSize : System.Web.UI.Page
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
            LoggingManager.Debug("Entering Page_Prerender - CompaniesSize");

            DisplayData();

            LoggingManager.Debug("Exiting Page_Prerender - CompaniesSize");
        }

        private void DisplayData()
        {

            LoggingManager.Debug("Entering DisplayData - CompaniesSize");

            IList<int>   sizeIds = (from RepeaterItem repeaterItem in rspSize.Items select repeaterItem.FindControl("chbsize") as CheckBox into sizeCheckbox where sizeCheckbox != null && sizeCheckbox.Checked select MasterDataManager.AllEmployees.First(x => x.NoofEmployess == sizeCheckbox.Text).Id).ToList();

            UpdateSize(sizeIds);
            LettersBind();

            LoggingManager.Debug("Exiting DisplayData - CompaniesSize");
        }
      
        private void LettersBind()
        {
            LoggingManager.Debug("Entering LettersBind - CompaniesSize");

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

            LoggingManager.Debug("Exiting LettersBind - CompaniesSize");
        }

        protected void LettersItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            LoggingManager.Debug("Entering LettersItemDataBound - CompaniesSize");

            var data = (DataRowView)eventArgs.Item.DataItem;

            if ((string)data[0] == LetterFilter || (LetterFilter == null && (string)data[0] == "All"))
            {
                var lnkletter = (LinkButton)eventArgs.Item.FindControl("lnkletter");
                lnkletter.Enabled = false;
            }
            LoggingManager.Debug("Exiting LettersItemDataBound - CompaniesSize");
        }

        protected void LettersItemCommand(object source, CommandEventArgs commandEventArgs)
        {
            LoggingManager.Debug("Entering LettersItemCommand - CompaniesSize");

            if (commandEventArgs.CommandName == "Filter")
            {
                LetterFilter = (string) commandEventArgs.CommandArgument;
            }
            LoggingManager.Debug("Exiting LettersItemCommand - CompaniesSize");
        }

        protected void BtnCompaniesSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompaniesSearchClick - CompaniesSize");

            string url = string.Format("~/companiessearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
            new Utility().RedirectUrl(Response, url);

            LoggingManager.Debug("Exiting BtnCompaniesSearchClick - CompaniesSize");

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompaniesSize");

            if (!Page.IsPostBack)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {                  
                   var size = MasterDataManager.AllEmployees.ToList();
                    rspSize.DataSource = size;
                    rspSize.DataBind();
                }
            }
            var cmpMgr = new CompanyManager();
            rspdata.DataSource = cmpMgr.GetCompaniessizeList(LoginUserId,null);
            rspdata.DataBind();

            LoggingManager.Debug("Exiting Page_Load - CompaniesSize");
        }
        private void UpdateSize(IList<int> sizeIds)
        {
            LoggingManager.Debug("Entering UpdateSize - CompaniesSize");

            var cmpMgr = new CompanyManager();

            rspdata.DataSource = cmpMgr.GetCompaniessizeList(LoginUserId,LetterFilter, sizeIds);
            rspdata.DataBind();

            LoggingManager.Debug("Exiting UpdateSize - CompaniesSize");
        }
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchClick - CompaniesSize");

            IList<int> sizeIds = (from RepeaterItem repeaterItem in rspSize.Items select repeaterItem.FindControl("chbsize") as CheckBox into sizeCheckbox where sizeCheckbox != null && sizeCheckbox.Checked select MasterDataManager.AllEmployees.First(x => x.NoofEmployess == sizeCheckbox.Text).Id).ToList();
            UpdateSize(sizeIds);

            LoggingManager.Debug("Exiting BtnSearchClick - CompaniesSize");
        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompaniesSize");
            
            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var photo = result.CompanyLogoId;

                LoggingManager.Debug("Exiting Picture - CompaniesSize");

                return new FileStoreService().GetDownloadUrl(photo);
            }

            
        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - CompaniesSize");

            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }


            LoggingManager.Debug("Exiting FollowupClick - CompaniesSize");
        }
        protected void UnfollowCompanyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnfollowClick-CompaniesSize");
            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
            }
            LoggingManager.Debug("Exiting Unfollowupclick-CompaniesSize");
        }
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompaniesSize");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompaniesSize");

                return 0;
            }
        }
        public int Followers(object id)
        {
            LoggingManager.Debug("Entering Followers - CompaniesSize");

            int s = Int32.Parse(id.ToString());
            var cmpMgr2 = new CompanyManager();

            LoggingManager.Debug("Exiting Followers - CompaniesSize");

            return cmpMgr2.GetcompanyFollowers(s);
        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - CompaniesSize");

            int p = Int32.Parse(id.ToString());
            var cmpMgr1 = new CompanyManager();

            LoggingManager.Debug("Exiting Jobs - CompaniesSize");

            return cmpMgr1.GetJobsPostedByCompany(p);
        }
        protected void ItemDataBound(object sender, ListViewItemEventArgs e)
        {
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
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                return null;
            }

        }
    }
}