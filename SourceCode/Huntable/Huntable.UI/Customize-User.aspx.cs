using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Entities.SearchCriteria;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CustomizeUser : Page
    {
        private bool IsSearch
        {
            get
            {
                

                return Convert.ToBoolean(ViewState["ActionName"]);

               
            }
            set
            {
                ViewState["ActionName"] = value;
            }
        }

        private EUserSearchType SearchType
        {
            get
            {
                if (ViewState["SearchType"] != null)
                    return (EUserSearchType)ViewState["SearchType"];
                return EUserSearchType.None;
            }
            set
            {
                ViewState["SearchType"] = value;
            }
        }

        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CustomizeUser");

                var loggedInUserId = Common.GetLoggedInUserId(Session);
                if (loggedInUserId != null) return loggedInUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CustomizeUser");

                return 0;
            }
        }

        public delegate void DelPopulateSearchUsers(int pageIndex);

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeUser");
            var userId = Common.GetLoggedInUserId(Session);
            if(userId==null){pplYoumayKnowDiv.Visible= false;}
            var user = Common.GetLoggedInUser();
            if (!IsPostBack)
            {
                IsSearch = false;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    LoadDdLs(context);
                }
                LoadFindUsers(1);
            }
            btnSearchTopKeyword.ServerClick+=BtnSearchTopKeywordServerClick;
            var delPopulate = new DelPopulateSearchUsers(LoadFindUsers);
            pagerUsers.UpdatePageIndex = delPopulate;
            if (user != null)
            {
                if (user.IsCompany == true)
                {
                    jobsal.Visible = false;
                }
            }

            LoggingManager.Debug("Exiting Page_Load - CustomizeUser");

        }

        private void LoadDdLs(huntableEntities context)
        {
            LoggingManager.Debug("Entering LoadDdLs - CustomizeUser");
            try
            {
                LoggingManager.Debug("Entering LoadDDls - UserSearch.aspx");
                {
                    var countries = MasterDataManager.AllCountries.Select(c => new { c.Description, c.Id }).ToList();
                    countries.Insert(0, new { Description = "-- Select --", Id = -1 });

                    new Snovaspace.Util.Utility().BindDropdownList(ddlSearchCountry, countries);

                    cbShowConnections.Checked = context.Users.First(u => u.Id == LoginUserId).ShowFeedsFromMyConnections;
                    cbShowConnections.AutoPostBack = true;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadDDLs - CustomizeUser");

        }

        private void LoadFindUsers(int pageIndex)
        {
            try
            {
                LoggingManager.Debug("Entering LoadFindUsers - Customize-User");
                int totalRecords;
                List<User> users;
                if (IsSearch)
                {
                    var searchCriteria = BuildSearchCriteria();
                    users = UserManager.SearchUsersSp(searchCriteria, out totalRecords, pagerUsers.RecordsPerPage, pageIndex-1, LoginUserId);
                }
                else
                {
                    users = UserManager.GetUsersAndFriends(LoginUserId, pageIndex - 1, pagerUsers.RecordsPerPage, out totalRecords);
                    if (users.Count == 0)
                    {
                        IsSearch = true;
                        this.ViewState["UserSkill"] = UserManager.GetUserSkill(LoginUserId);
                        this.ViewState["UserIndustry"] = UserManager.GetUserIndustry(LoginUserId);
                        SearchType = EUserSearchType.UserSkillMatched;
                        var searchCriteria = BuildSearchCriteria();
                        users = UserManager.SearchUsersSp(searchCriteria, out totalRecords, pagerUsers.RecordsPerPage, 1, LoginUserId);
                    }
                }
               
                LoggingManager.Info("Page Index:" + pageIndex);
                pagerUsers.TotalRecords = totalRecords;
                lstUsers.DataSource = users.OrderByDescending(u=>u.Id);
                lstUsers.DataBind();

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadFindUsers - Customize-User");
        }

        protected void BtnKeywordSearchClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnKeywordSearchClick - Customize-User");

            SearchType = EUserSearchType.KeyWord;
            IsSearch = true;
            LoadFindUsers(1);

            LoggingManager.Debug("Exiting BtnKeywordSearchClick - Customize-User");
        }

        protected void BtnSkillsSearchClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnSkillsSearchClick - Customize-User");

            SearchType = EUserSearchType.Skills;
            IsSearch = true;
            LoadFindUsers(1);

            LoggingManager.Debug("Exiting BtnSkillsSearchClick - Customize-User");


        }

        protected void BtnSearchTopKeywordServerClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchTopKeywordServerClick - Customize-User");

            SearchType = EUserSearchType.HeaderKeyword;
            IsSearch = true;
            LoadFindUsers(1);

            LoggingManager.Debug("Exiting BtnSearchTopKeywordServerClick - Customize-User");
        }

        protected void FollowupClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering FollowupClick - Customize-User");

            var button = sender as LinkButton;
            if (button != null)
            {
                int userId = Convert.ToInt32(button.CommandArgument);
                UserManager.FollowUser(LoginUserId, userId);

                var objMessageManager = new UserMessageManager();
                objMessageManager.FollowUser(userId, LoginUserId);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }
            LoadFindUsers(pagerUsers.PageIndex);

            LoggingManager.Debug("Exiting FollowupClick - Customize-User");
        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnSearchClick -Customize-User");
            SearchType = EUserSearchType.None;
            IsSearch = true;
            LoadFindUsers(1);
            LoggingManager.Debug("Exiting BtnSearchClick -  Customize-User");
        }

        private UserSearchCriteria BuildSearchCriteria()
        {
            LoggingManager.Debug("Entering BuildSearchCriteria - Customize-User");

            var criteria = new UserSearchCriteria();
            switch (SearchType)
            {
                case EUserSearchType.HeaderKeyword:
                    criteria.Keywords = txtTopSearchKeyword.Text;
                    break;

                case EUserSearchType.KeyWord:
                    criteria.Keywords = txtSearchKeywords.Text == "Keywords" ? string.Empty : txtSearchKeywords.Text;
                    break;

                case EUserSearchType.Skills:
                    criteria.Skill = txtSearchSkills.Text == "Skill" ? string.Empty : txtSearchSkills.Text;
                    break;

                case EUserSearchType.UserSkillMatched:
                    criteria.Skill = this.ViewState["UserSkill"] as string;
                    criteria.Industry = this.ViewState["UserIndustry"] as string;
                    break;

                default:
                    criteria.FirstName = txtSearchFirstName.Text == "First name" ? string.Empty : txtSearchFirstName.Text;
                    LoggingManager.Info("SearchFirstName:" + txtSearchFirstName.Text);
                    criteria.LastName = txtSearchLastName.Text == "Last name" ? string.Empty : txtSearchLastName.Text;
                    LoggingManager.Info("SearchLastName:" + txtSearchLastName.Text);
                    criteria.Keywords = txtSearchKeywords.Text == "Keywords" ? string.Empty : txtSearchKeywords.Text;
                    criteria.IsProfileAvailable = ddlAvailableNow.SelectedIndex == 0;
                    int totalExp;
                    if (int.TryParse(txtSearchExp.Text, out totalExp))
                    {
                        criteria.TotalExp = totalExp;
                    }

                    criteria.CompanyName = txtSearchCompany.Text == "Company" ? string.Empty : txtSearchCompany.Text;
                    LoggingManager.Info("SearchCompany:" + txtSearchCompany.Text);
                    //criteria.IsCurrentCompany = rbSearchCompanyType.SelectedIndex == 0;

                    if (ddlSearchCountry.SelectedIndex > 0)
                    {
                        var selectedCountryId = Convert.ToInt32(ddlSearchCountry.SelectedItem.Value);
                        criteria.CountryId = selectedCountryId;
                    }
                    criteria.Skill= txtSearchSkills.Text == "Skills" ? string.Empty : txtSearchSkills.Text;
                    criteria.SchoolName = txtSearchSchool.Text == "School" ? string.Empty : txtSearchSchool.Text;
                    break;
            }

            LoggingManager.Debug("Exiting BuildSearchCriteria -Customize-User");
            return criteria;
        }

        protected void CbShowConnectionsCheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering CbShowConnectionsCheckedChanged - Customize-User");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == LoginUserId);
                user.ShowFeedsFromMyConnections = cbShowConnections.Checked;
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting CbShowConnectionsCheckedChanged - Customize-User");
        }
        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - Customize-User");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var user_to = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == user_to && y.FollowingUserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                else
                    LoggingManager.Debug("Exiting IsThisUserFollowingCompany - Customize-User");
                    return false;

            }



        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - Customize-User");
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().UserUrlGenerator(userid);
            }
            else
            {
                LoggingManager.Debug("Entering UrlGenerator - Customize-User");
                return null;
            }


        }
    }

    public enum EUserSearchType
    {
        None,
        KeyWord,
        Skills,
        HeaderKeyword,
        UserSkillMatched
    }

}