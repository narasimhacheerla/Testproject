using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using System.Data;
using Snovaspace.Util;

namespace Huntable.UI
{
    public partial class CompaniesSearch : System.Web.UI.Page
    {
        protected void Page_Prerender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Prerender - CompaniesHome");
           
            LettersBind();
            DispalyData();

            LoggingManager.Debug("Exiting Page_Prerender - CompaniesHome");
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
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompaniesSearch");

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["keyword"]))
                {
                     hfkeyword.Value= Request.QueryString["keyword"];
                    UpdateResult(hfkeyword.Value);
                }
            }
            LoggingManager.Debug("Exiting Page_Load - CompaniesSearch");

        }
        public void DispalyData()
        {
            LoggingManager.Debug("Entering DispalyData - CompaniesSearch");

            UpdateResult(hfkeyword.Value);
            Session[ToString() + "_LettersData"] = null;
            LoggingManager.Debug("Exiting DispalyData - CompaniesSearch");
        }

        public void UpdateResult(string keyword)
        {
            LoggingManager.Debug("Entering UpdateResult - CompaniesSearch");
            var company = new CompanyManager();
            if(LetterFilter!=null)
            {
              
                rspdata.DataSource = company.GetCompanySearch(string.Empty, LetterFilter, LoginUserId);
                LetterFilter = null;
                rspdata.DataBind();
            }
            else
            {
                rspdata.DataSource = company.GetCompanySearch(keyword, LetterFilter, LoginUserId);
                rspdata.DataBind();
            }
            LoggingManager.Debug("Exiting UpdateResult - CompaniesSearch");
           
        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompaniesSearch");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var photo = result.CompanyLogoId;

                LoggingManager.Debug("Exiting Picture - CompaniesSearch");

                return new FileStoreService().GetDownloadUrl(photo);
            }
           
        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - CompaniesSearch");
            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, Id);
            }
            LoggingManager.Debug("Exiting FollowupClick - CompaniesSearch");


        }
        protected void UnfollowCompanyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnfollowClick-CompaniesSearch");
            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, Id);

            }
            LoggingManager.Debug("Exiting Unfollowupclick-CompaniesSearch");
        }
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompaniesSearch");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompaniesSearch");

                return 0;
            }
        }
        public int Followers(object id)
        {
            LoggingManager.Debug("Entering Followers - CompaniesSearch");

            int s = Int32.Parse(id.ToString());
            var cmpMgr2 = new CompanyManager();

            LoggingManager.Debug("Exiting Followers - CompaniesSearch");

            return cmpMgr2.GetcompanyFollowers(s);
            
        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - CompaniesSearch");

            int p = Int32.Parse(id.ToString());
            var cmpMgr1 = new CompanyManager();

            LoggingManager.Debug("Exiting Jobs - CompaniesSearch");

            return cmpMgr1.GetJobsPostedByCompany(p);

            
        }
        private void LettersBind()
        {
            LoggingManager.Debug("Entering LettersBind - CompaniesHome");

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

            LoggingManager.Debug("Exiting LettersBind - CompaniesHome");
        }

        protected void LettersItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            LoggingManager.Debug("Entering LettersItemDataBound - CompaniesHome");

            var data = (DataRowView)eventArgs.Item.DataItem;

            if ((string)data[0] == LetterFilter || (LetterFilter == null && (string)data[0] == "All"))
            {
                var lnkletter = (LinkButton)eventArgs.Item.FindControl("lnkletter");
                lnkletter.Enabled = false;
            }
            LoggingManager.Debug("Exiting LettersItemDataBound - CompaniesHome");

        }

        protected void LettersItemCommand(object source, CommandEventArgs commandEventArgs)
        {
            LoggingManager.Debug("Entering LettersItemCommand - CompaniesHome");

            if (commandEventArgs.CommandName == "Filter")
            {
                LetterFilter = (string)commandEventArgs.CommandArgument;
            }
            LoggingManager.Debug("Exiting LettersItemCommand - CompaniesHome");

        }
        protected void BtnCompaniesSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompaniesSearchClick - CompaniesHome");
            hfkeyword.Value = txtUserSearchKeyword.Text;
            UpdateResult(hfkeyword.Value);
            LoggingManager.Debug("Exiting BtnCompaniesSearchClick - CompaniesHome");

        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - CompaniesSearch");

            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - CompaniesSearch");

                return null;
            }


        }
    }

}