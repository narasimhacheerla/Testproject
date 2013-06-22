using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using System.Data;
using Huntable.Business;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompaniesHome : System.Web.UI.Page
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
            LoggingManager.Debug("Entering Page_Prerender - CompaniesHome");

            DisplayData();

            LoggingManager.Debug("Exiting Page_Prerender - CompaniesHome");
        }

        private void DisplayData()
        {
            LoggingManager.Debug("Entering DisplayData - CompaniesHome");

            IList<int> industryIds = (from RepeaterItem repeaterItem in rspIndustry.Items select repeaterItem.FindControl("chbInd") as CheckBox into industryCheckbox where industryCheckbox != null && industryCheckbox.Checked select MasterDataManager.AllIndustries.First(x => x.Description == industryCheckbox.Text).Id).ToList();

            UpdateIndustries(industryIds);
            LettersBind();

            LoggingManager.Debug("Exiting DisplayData - CompaniesHome");

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

        protected void Page_Load(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering Page_Load - CompaniesHome");
            if (!Page.IsPostBack)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var countries = MasterDataManager.AllIndustries.ToList();
                    rspIndustry.DataSource = countries;
                    rspIndustry.DataBind();
                  
                }
            }
            LoggingManager.Debug("Exiting Page_Load - CompaniesHome");
        }

        private void UpdateIndustries(IList<int> industryIds)
        {
            LoggingManager.Debug("Entering UpdateIndustries - CompaniesHome");
            var cmpMgr = new CompanyManager();

            rspdata.DataSource = cmpMgr.GetCompaniesIndustryList(LoginUserId,LetterFilter, industryIds);
            rspdata.DataBind();
            
            LoggingManager.Debug("Exiting UpdateIndustries - CompaniesHome");

        }

        protected void BtnCompaniesSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompaniesSearchClick - CompaniesHome");

            string url = string.Format("~/companiessearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
            new Utility().RedirectUrl(Response, url);

            LoggingManager.Debug("Exiting BtnCompaniesSearchClick - CompaniesHome");

        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchClick - CompaniesHome");

            IList<int> industryIds = (from RepeaterItem repeaterItem in rspIndustry.Items select repeaterItem.FindControl("chbInd") as CheckBox into industryCheckbox where industryCheckbox != null && industryCheckbox.Checked select MasterDataManager.AllIndustries.First(x => x.Description == industryCheckbox.Text).Id).ToList();
            UpdateIndustries(industryIds);

            LoggingManager.Debug("Exiting BtnSearchClick - CompaniesHome");

        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompaniesHome");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var photo = result.CompanyLogoId;

            LoggingManager.Debug("Exiting Picture - CompaniesHome");

                return new FileStoreService().GetDownloadUrl(photo);
            }

        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - CompaniesHome");
            var button = sender as Button;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }
            LoggingManager.Debug("Exiting FollowupClick - CompaniesHome");


        }
        protected void UnfollowCompanyClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnfollowClick-CompaniesHome");
            var button = sender as Button;
            if(button!=null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.UnfollowCompany(LoginUserId, Id);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                
            }
            LoggingManager.Debug("Exiting Unfollowupclick-CompaniesHome");
        }
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
        //    public static object Followers(int id)
        //    {
        //        int s = Int32.Parse(id.ToString());

        //        var cmpMgr2 = new CompanyManager();
        //        return cmpMgr2.GetCompanyFollowers(s);

        //}
        public int Followers(object id)
        {
            LoggingManager.Debug("Entering Followers - CompaniesHome");

            int s = Int32.Parse(id.ToString());
            var cmpMgr2 = new CompanyManager();

            LoggingManager.Debug("Exiting Followers - CompaniesHome");

            return cmpMgr2.GetcompanyFollowers(s);
            

        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - CompaniesHome");

            int p = Int32.Parse(id.ToString());
            var cmpMgr1 = new CompanyManager();

            LoggingManager.Debug("Exiting Jobs - CompaniesHome");

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
