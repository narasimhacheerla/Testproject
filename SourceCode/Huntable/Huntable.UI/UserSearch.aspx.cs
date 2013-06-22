using System;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Entities.SearchCriteria;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using System.Web.UI;

namespace Huntable.UI
{
    public partial class UserSearch : System.Web.UI.Page
    {
        public delegate void DelPopulateSearchUsers(int pageIndex);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering Page_Load - UserSearch.aspx");

                if (!IsPostBack)
                {
                    bool userLoggedIn = Common.IsLoggedIn();
                    rtContent.Visible = userLoggedIn;
                    LoadDdLs();

                    if (!string.IsNullOrEmpty(Request.QueryString["keyword"]))
                    {
                        txtSearchKeywords.Text = Request.QueryString["keyword"];
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["Country"]))
                    {
                        ddlSearchCountry.SelectedItem.Text = Request.QueryString["Country"];
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["Industry"]))
                    {
                        ddlindustry.SelectedItem.Text = Request.QueryString["Industry"];
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["Skill"]))
                    {
                        txtSearchSkill.Text = Request.QueryString["Skill"];
                    }
                    
                    SearchUsers(1);
                    hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
                }
                else
                {
                    var delPopulate = new DelPopulateSearchUsers(SearchUsers);
                    pagerUsers.UpdatePageIndex = delPopulate;
                }

                //using (var context= huntableEntities.GetEntitiesWithNoLock())
                //{
                //    var listOfCompanies = context.UserCompanies.ToList();
                //    dlListOfCompanies.DataSource = listOfCompanies;
                //    dlListOfCompanies.DataBind();
                //}

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - UserSearch.aspx");
        }

        private void LoadDdLs()
        {
            try
            {
                LoggingManager.Debug("Entering LoadDdLs - UserSearch.aspx");
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var countries = context.MasterCountries.Select(c => new { c.Description, c.Id }).ToList();
                    var industry = context.MasterIndustries.Select(c => new { c.Description, c.Id }).ToList();
                    var interest = context.MasterInterests.Select(c => new { c.Description, c.Id }).ToList();
                    industry.Insert(0, new { Description = "-- Select --", Id = -1 });
                    
                    ddlindustry.DataSource = industry;
                    ddlindustry.DataTextField = "Description";
                    ddlindustry.DataValueField = "Id";
                    ddlindustry.DataBind();

                    ddlinterest.DataSource = interest;
                    ddlinterest.DataTextField = "Description";
                    ddlinterest.DataValueField = "Id";
                    ddlinterest.DataBind();

                    countries.Insert(0, new { Description = "-- Select --", Id = -1 });
                    ddlSearchCountry.DataSource = countries;
                    ddlSearchCountry.DataTextField = "Description";
                    ddlSearchCountry.DataValueField = "Id";
                    ddlSearchCountry.DataBind();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadDdLs - UserSearch.aspx");

        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchClick - UserSearch.aspx");
            SearchUsers(1);
            LoggingManager.Debug("Exiting BtnSearchClick -  UserSearch.aspx");
        }

        protected void BtnBeforeLoginClick(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'>alert('You are not loggedin.Please login first');</script>");
        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - UserSearch.aspx");
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());


                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    return new FileStoreService().GetDownloadUrl(p);
                }

            }
            else
            {
                LoggingManager.Debug("Exiting Picture - UserSearch.aspx");
                return null;
            }

        }
       

        protected void UserCompaniesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UserCompaniesClick - UserSearch.aspx");
            var button = sender as ImageButton;
            if (button!= null)
            {
                var userId = button.CommandArgument;
                int id = Convert.ToInt32(userId);
                using ( var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usercompaniesList = from uc in context.UserCompanies
                                            join c in context.Companies on uc.CompanyId equals c.Id
                                            where uc.UserId == id && c.IsVerified == true
                                            select new
                                                {
                                                    c.Id,
                                                    c.CompanyName,
                                                    c.CompanyWebsite,
                                                    c.CompanyEmail,
                                                    c.CompanyLogoId
                                                };



                    dlListOfCompanies.DataSource = usercompaniesList;
                    dlListOfCompanies.DataBind();
                }
                LoggingManager.Debug("Exiting UserCompaniesClick - UserSearch.aspx");
                 
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "Click", "rowaction22();", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "rowAction22()", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "Companies", "rowAction22();", true);

            
        }
        protected void BtnMessageClick(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering BtnMessage_Click - UserSearch.aspx");
                if (Common.GetLoggedInUserId(Session) != null)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {

                        var button = sender as Button;
                        if (rbMessageList.SelectedValue == "0")
                        {
                            hfSubject.Value = "Job Enquiry";
                        }
                        else if (rbMessageList.SelectedValue == "1")
                        {
                            hfSubject.Value = "Request endorsement";
                        }
                        else if (rbMessageList.SelectedValue == "2")
                        {
                            hfSubject.Value = "Introduce Yourself";
                        }
                        else if (rbMessageList.SelectedValue == "3")
                        {
                            hfSubject.Value = "New Business opportunity";
                        }
                        else if (rbMessageList.SelectedValue == "4")
                        {
                            hfSubject.Value = "Your Recruitment requirement";
                        }
                        else
                        {
                            hfSubject.Value = "Introduce Yourself";
                        }

                        if (button != null)
                        {
                            var userMessage = new UserMessage
                                                  {
                                                      SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                                                      SentTo = Convert.ToInt32(button.CommandArgument),
                                                      Subject = hfSubject.Value,
                                                      Body = txtMessage.Text,
                                                      IsActive = true,
                                                      SentIsActive = true,
                                                      IsRead = false,
                                                      SentDate = DateTime.Now
                                                  };

                            var objMessageManager = new UserMessageManager();
                            //lblMessage.Text = objMessageManager.SaveMessage(context, userMessage) == 1 ? "Message Sent Successfully." : "Message Sending Failed. Please try again.";
                            if (objMessageManager.SaveMessage(context, userMessage) == 1)
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Message sent succesfully ')", true);
                            }
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Please Login to Send Message.";
                    Response.Write("<script type='text/javascript' language='javascript'>alert('You are not loggedin.Please login first');</script>");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnMessage_Click - UserSearch.aspx");
        }

        private void SearchUsers(int pageIndex)
        {
            try
            {
                LoggingManager.Debug("Entering SearchUsers - UserSearch.aspx");
                var searchCriteria = BuildSearchCriteria();
                int totalRecords;
                var users = UserManager.SearchUsers(searchCriteria, out totalRecords, pagerUsers.RecordsPerPage, pageIndex, true);
                LoggingManager.Info("Page Index:" + pageIndex);
                pagerUsers.TotalRecords = totalRecords;
                lblNoOfSearchResults.Text = Convert.ToString(totalRecords);
                rpUserSearchResults.DataSource = users;
                rpUserSearchResults.DataBind();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting SearchUsers - UserSearch.aspx");
        }
        protected void RpUserSearchResultsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering RpUserSearchResultsItemDataBound - UserSearch.aspx");

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    BlockUsers(e);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting RpUserSearchResultsItemDataBound - UserSearch.aspx");
        }
        private void BlockUsers(RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering BlockUsers - UserSearch.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int userId = Convert.ToInt32(Common.GetLoggedInUserId());
                var user = context.UserBlockLists.FirstOrDefault(i => i.BlockedUserId == userId);
                if (user != null)
                {
                    int blockedUserId = user.UserId;

                    var msgBtn = e.Item.FindControl("btnMessage") as Button;
                    var userList = e.Item.DataItem as User;
                    if (userList != null && userList.Id == blockedUserId)
                    {
                        if (msgBtn != null)
                        {
                            msgBtn.Enabled = false;
                            msgBtn.Visible = false;
                        }
                    }
                }
            }

            LoggingManager.Debug("Exiting BlockUsers - UserSearch.aspx");
        }

        protected string GetUrl(object userId)
        {
            LoggingManager.Debug("Entering GetUrl - UserSearch.aspx");

            int? loggedInUserId = Common.GetLoggedInUserId();

            if (loggedInUserId.HasValue && (Int32)userId != loggedInUserId.Value)
            {
                var loginUser = Common.GetLoggedInUser();
                var result = Common.GetChatUrl(loginUser.Id.ToString(), userId.ToString(), loginUser.Name, loginUser.UserProfilePictureDisplayUrl);
               // return "window.open('AjaxChat/MessengerWindow.aspx?init=1&target=" + userId + "', '" + userId + "', 'width=650,height=400,resizable=1,menubar=0,status=0,toolbar=0'); return false";
                return result;
            }

            LoggingManager.Debug("Exiting GetUrl - UserSearch.aspx");
            return null;
        }


        private string GetActualTextBoxValue(TextBox tb, string defaultvalue)
        {
            LoggingManager.Debug("Entering GetActualTextBoxValue - UserSearch.aspx");

            string actualvalue = (tb.Text.Trim() != defaultvalue) ? tb.Text : string.Empty;

            LoggingManager.Debug("Exiting GetActualTextBoxValue - UserSearch.aspx");
            return actualvalue;
        }

        private UserSearchCriteria BuildSearchCriteria()
        {
            LoggingManager.Debug("Entering BuildSearchCriteria - UserSearchCriteria .aspx");

            var criteria = new UserSearchCriteria
            {
                FirstName = GetActualTextBoxValue(txtSearchFirstName, "FirstName"),
                LastName = GetActualTextBoxValue(txtSearchLastName, "LastName"),
                Keywords = GetActualTextBoxValue(txtSearchKeywords, "Keywords"),
                CompanyName = GetActualTextBoxValue(txtSearchCompany, "Company"),
                Skill = GetActualTextBoxValue(txtSearchSkill, "Skill"),
                JobTitle = GetActualTextBoxValue(txtSearchTitle, "Title"),
                Industryid= Convert.ToInt32(ddlindustry.SelectedItem.Value),
                Interest = Convert.ToInt32(ddlinterest.SelectedItem.Value),
                //LanguageKnown=GetActualTextBoxValue(txtLanguagesKnown,"Language Known")
            };

            criteria.Keywords = GetActualTextBoxValue(txtSearchKeywords, "e.g: Name, Company, Skill, Job title");
            criteria.Keywords = GetActualTextBoxValue(txtSearchKeywords, "Search here...");

            LoggingManager.Info("SearchFirstName: " + criteria.FirstName + " , SearchLastName: " + criteria.LastName + " , SearchKeywords: " + criteria.Keywords + " , SearchCompany: " + criteria.CompanyName);

            if (rbListAvailable.SelectedIndex > 0)
            {
                criteria.IsProfileAvailable = rbListAvailable.SelectedIndex == 1;
            }

            if (rbSearchCompanyType.SelectedIndex > 0)
            {
                criteria.IsCurrentCompany = rbSearchCompanyType.SelectedIndex == 1;
            }

            if (ddlSearchCountry.SelectedIndex > 0)
            {
                var selectedCountryId = Convert.ToInt32(ddlSearchCountry.SelectedItem.Value);
                criteria.CountryId = selectedCountryId;
            }

            if (!string.IsNullOrEmpty(txtSearchExpTo.Text) && !string.IsNullOrEmpty(txtSearchExpFrom.Text))
            {
                criteria.ExperienceFrom = Convert.ToDouble(txtSearchExpFrom.Text);
                criteria.ExperienceTo = Convert.ToDouble(txtSearchExpTo.Text);
            }

            LoggingManager.Debug("Exiting BuildSearchCriteria - UserSearchCriteria .aspx");
            return criteria;
        }
        public string UrlGenerator(object id)
        {

            LoggingManager.Debug("Entering UrlGenerator - UserSearchCriteria .aspx");
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().UserUrlGenerator(userid);
            }
            else
            {

                LoggingManager.Debug("Exiting UrlGenerator - UserSearchCriteria .aspx");
                return null;
            }

        }
        public string CompanyUrlGenerator(object id)
        {
            LoggingManager.Debug("Entering CompanyUrlGenerator - UserSearchCriteria .aspx");
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(userid);
            }
            else
            {
                LoggingManager.Debug("Exiting CompanyUrlGenerator - UserSearchCriteria .aspx");
                return null;
            }

        }
    }
}