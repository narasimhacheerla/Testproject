using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class SeeMoreRecruiter : System.Web.UI.Page
    {
        readonly InvitationManager _objInvManager = new InvitationManager();
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - SeeMoreRecruiter.aspx");
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                LoggingManager.Debug("Exiting LoginUserId - SeeMoreRecruiter.aspx");
                return 0;
            }
        }
        protected string LetterFilter
        {
            get
            {
                LoggingManager.Debug("Entering LetterFilter - SeeMoreRecruiter.aspx");
                LoggingManager.Debug("Exiting LetterFilter - SeeMoreRecruiter.aspx");
                return Session["Companies_letterFilter"] as string;
            }
            set
            {
                Session["Companies_letterFilter"] = value == "All" ? null : value;
            }
        }

        protected void Page_Prerender(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Prerender - SeeMoreRecruiter.aspx");

            DisplayData();

            LoggingManager.Debug("Exiting Page_Prerender - SeeMoreRecruiter.aspx");

        }

        private void DisplayData()
        {
            LoggingManager.Debug("Entering DisplayData - SeeMoreRecruiter.aspx");

            LettersBind();
            UpdateCompanies();

            LoggingManager.Debug("Exiting DisplayData - SeeMoreRecruiter.aspx");
        }

        private void LettersBind()
        {
            LoggingManager.Debug("Entering LettersBind - SeeMoreRecruiter.aspx");

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

            LoggingManager.Debug("Exiting LettersBind - SeeMoreRecruiter.aspx");
        }
        private void UpdateCompanies()
        {
            LoggingManager.Debug("Entering UpdateCompanies - SeeMoreRecruiter.aspx");

            var cmpMgr = new CompanyManager();
            int? userId = Common.GetLoggedInUserId();

            if (userId != null)
            {
               
                      var company = new CompanyManager();
                var count = company.GetFeaturedrecuirtersCount(LoginUserId, LetterFilter);
                if (count != 0)
                {
                    var result2 = company.GetFeaturedUserComapnies(LoginUserId, LetterFilter);

                    rpsmrlist.DataSource = result2;
                    rpsmrlist.DataBind();

                }
                //    }
                else
                {
                   var dt = CompanyManager.GetFeaturedUserPCompanies(8,LetterFilter);
                   rpsmrlist.DataSource = dt;
                   rpsmrlist.DataBind();
                }
              
               
            }
            else
            {
                var dt = CompanyManager.GetFeaturedUserPCompanies(500,LetterFilter);
                rpsmrlist.DataSource = dt;
                rpsmrlist.DataBind();
            }
            LoggingManager.Debug("Exiting UpdateCompanies - SeeMoreRecruiter.aspx");

        }
        protected void CommandCompanyEmployeeUnFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering CommandCompanyEmployeeUnFollowClick - SeeMoreRecruiter.aspx");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);

                var btnCompanyEmployeeFollow = sender as LinkButton;
                if (btnCompanyEmployeeFollow != null)
                {
                    int companyEmployeeUserId = Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument);
                }

                //if (btnCompanyEmployeeFollow != null && btnCompanyEmployeeFollow.Text == "Following")
                //{
                    var usrmgr = new CompanyManager();

                    if (loginUserId != null)
                    {
                        CompanyManager.UnfollowCompany(loginUserId.Value, Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument));
                        btnCompanyEmployeeFollow.Text = "Follow";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully unfollowed')", true);                    
                    } 
                //}
            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting CommandCompanyEmployeeUnFollowClick - SeeMoreRecruiter.aspx");
        }
        protected void CommandCompanyEmployeeFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering CommandCompanyEmployeeFollow_Click - SeeMoreRecruiter.aspx");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);

                var btnCompanyEmployeeFollow = sender as LinkButton;
                if (btnCompanyEmployeeFollow != null)
                {
                    int companyEmployeeUserId = Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument);
                }

                //if (btnCompanyEmployeeFollow != null && btnCompanyEmployeeFollow.Text == "Follow")
                //{

                    if (loginUserId != null)
                    {
                        CompanyManager.FollowCompany(loginUserId.Value, Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument));
                        btnCompanyEmployeeFollow.Text = "Following";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
                    }
                  
                //}
            }
            catch (Exception)
            {
                { }
                throw;
            }

            LoggingManager.Debug("Exiting CommandCompanyEmployeeFollow_Click - SeeMoreRecruiter");
        }
        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - SeeMoreRecruiter");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var userTo = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserCompaniesFollwers.Where(y => y.CompanyID == userTo && y.FollowingUserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                else
                    LoggingManager.Debug("Exiting IsThisUserFollowingCompany - SeeMoreRecruiter");

                    return false;

            }



        }
        protected void LettersItemDataBound(object sender, ListViewItemEventArgs eventArgs)
        {
            LoggingManager.Debug("Entering LettersItemDataBound - SeeMoreRecruiter.aspx");

            var data = (DataRowView)eventArgs.Item.DataItem;

            if ((string)data[0] == LetterFilter || (LetterFilter == null && (string)data[0] == "All"))
            {
                var lnkletter = (LinkButton)eventArgs.Item.FindControl("lnkletter");
                lnkletter.Enabled = false;
            }

            LoggingManager.Debug("Exiting LettersItemDataBound - SeeMoreRecruiter.aspx");
        }

        protected void LettersItemCommand(object source, CommandEventArgs commandEventArgs)
        {
            LoggingManager.Debug("Entering LettersItemCommand - SeeMoreRecruiter.aspx");

            if (commandEventArgs.CommandName == "Filter")
            {
                LetterFilter = (string)commandEventArgs.CommandArgument;
            }
            LoggingManager.Debug("Exiting LettersItemCommand - SeeMoreRecruiter.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - SeeMoreRecruiter.aspx");
            alpa.Visible = false;
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                if (loggedInUserId != null)
                {


                    var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
                    if (usri != null && usri.IsPremiumAccount == null)
                    {
                        bimage.Visible = true;
                        pimage.Visible = false;
                    }
                    else
                    {
                        bimage.Visible = false;
                        pimage.Visible = true;
                    }
                }
                else
                {
                    bimage.Visible = true;
                    pimage.Visible = false;
                }
            }
            LoggingManager.Debug("Exiting Page_Load - SeeMoreRecruiter.aspx");
        }

        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - SeeMoreRecruiter.aspx");

            if(id!=null)
            {
                int p = Int32.Parse(id.ToString());
                return new FileStoreService().GetDownloadUrl(p);
            }
            else
            {
                LoggingManager.Debug("Exiting Picture - SeeMoreRecruiter.aspx");

                return null;
            }
        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - SeeMoreRecruiter.aspx");

            int p = Int32.Parse(id.ToString());
            var cmpMgr = new CompanyManager();

            LoggingManager.Debug("Exiting Jobs - SeeMoreRecruiter.aspx");

            return cmpMgr.GetJobsPostedByCompany(p);
        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - SeeMoreRecruiter.aspx");
            var button = sender as LinkButton;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId, Id);
            }

            LoggingManager.Debug("Exiting FollowupClick - SeeMoreRecruiter.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering FollowupClick - SeeMoreRecruiter.aspx");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting FollowupClick - SeeMoreRecruiter.aspx");
                return null;
            }

        }
    }
}