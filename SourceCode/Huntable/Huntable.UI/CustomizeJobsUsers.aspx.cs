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
    public partial class CustomizeJobsUsers : System.Web.UI.Page
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

            private EUserSearchTypejob SearchType
            {
                get
                {
                    if (ViewState["SearchType"] != null)
                        return (EUserSearchTypejob)ViewState["SearchType"];
                    return EUserSearchTypejob.None;
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
                    LoggingManager.Debug("Entering LoginUserId - CustomizeJobsUsers");

                    var loggedInUserId = Common.GetLoggedInUserId(Session);
                    if (loggedInUserId != null) return loggedInUserId.Value;

                    LoggingManager.Debug("Exiting LoginUserId - CustomizeJobsUsers");

                    return 0;
                }
            }

            public delegate void DelPopulateSearchUsers(int pageIndex);

            protected void Page_Load(object sender, EventArgs e)
            {
                LoggingManager.Debug("Entering Page_Load - CustomizeJobsUsers");

                if (!IsPostBack)
                {
                    IsSearch = false;
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        LoadDdLs(context);
                    }
                    LoadFindUsers(1);
                }
                btnSearchTopKeyword.ServerClick += BtnSearchTopKeywordServerClick;
                var delPopulate = new DelPopulateSearchUsers(LoadFindUsers);
                pagerUsers.UpdatePageIndex = delPopulate;

                LoggingManager.Debug("Exiting Page_Load - CustomizeJobsUsers");
            }

            private void LoadDdLs(huntableEntities context)
            {
                LoggingManager.Debug("Entering LoadDdLs - CustomizeJobsUsers");

                try
                {
                    
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
                LoggingManager.Debug("Exiting LoadDDLs - CustomizeJobsUsers");

            }

            private void LoadFindUsers(int pageIndex)
            {
                LoggingManager.Debug("Entering LoadFindUsers - CustomizeJobsUsers");

                try
                {
                    
                    int totalRecords;
                    List<User> users;
                    if (IsSearch)
                    {
                        var searchCriteria = BuildSearchCriteria();
                        users = UserManager.SearchUsersSp(searchCriteria, out totalRecords, pagerUsers.RecordsPerPage, pageIndex - 1, LoginUserId);
                    }
                    else
                    {
                        users = UserManager.GetUsersAndFriends(LoginUserId, pageIndex - 1, pagerUsers.RecordsPerPage, out totalRecords);
                    }

                    LoggingManager.Info("Page Index:" + pageIndex);
                    pagerUsers.TotalRecords = totalRecords;
                    lstUsers.DataSource = users;
                    lstUsers.DataBind();

                }
                catch (Exception ex)
                {
                    LoggingManager.Error(ex);
                }
                LoggingManager.Debug("Exiting LoadFindUsers - CustomizeJobsUsers");
            }

            protected void BtnKeywordSearchClick(object sender, ImageClickEventArgs e)
            {
                LoggingManager.Debug("Entering BtnKeywordSearchClick - CustomizeJobsUsers");

                SearchType = EUserSearchTypejob.KeyWord;
                IsSearch = true;
                LoadFindUsers(1);

                LoggingManager.Debug("Exiting BtnKeywordSearchClick - CustomizeJobsUsers");
            }

            protected void BtnSkillsSearchClick(object sender, ImageClickEventArgs e)
            {
                LoggingManager.Debug("Entering BtnSkillsSearchClick - CustomizeJobsUsers");

                SearchType = EUserSearchTypejob.Skills;
                IsSearch = true;
                LoadFindUsers(1);

                LoggingManager.Debug("Exiting BtnSkillsSearchClick - CustomizeJobsUsers");


            }

            protected void BtnSearchTopKeywordServerClick(object sender, EventArgs e)
            {
                LoggingManager.Debug("Entering BtnSearchTopKeywordServerClick - CustomizeJobsUsers");

                SearchType = EUserSearchTypejob.HeaderKeyword;
                IsSearch = true;
                LoadFindUsers(1);

                LoggingManager.Debug("Exiting BtnSearchTopKeywordServerClick - CustomizeJobsUsers");
            }

            protected void FollowupClick(object sender, EventArgs e)
            {
                LoggingManager.Debug("Entering FollowupClick - CustomizeJobsUsers");

                var button = sender as LinkButton;
                if (button != null)
                {
                    int userId = Convert.ToInt32(button.CommandArgument);
                    UserManager.FollowUser(LoginUserId, userId);
                }
                LoadFindUsers(pagerUsers.PageIndex);

                LoggingManager.Debug("Exiting FollowupClick - CustomizeJobsUsers");
            }

            protected void BtnSearchClick(object sender, EventArgs e)
            {

                LoggingManager.Debug("Entering BtnSearchClick - CustomizeJobsUsers");
                SearchType = EUserSearchTypejob.None;
                IsSearch = true;
                LoadFindUsers(1);
                LoggingManager.Debug("Exiting BtnSearchClick -  CustomizeJobsUsers");
            }

            private UserSearchCriteria BuildSearchCriteria()
            {
                LoggingManager.Debug("Entering BuildSearchCriteria - CustomizeJobsUsers");
                var criteria = new UserSearchCriteria();
                switch (SearchType)
                {
                    case EUserSearchTypejob.HeaderKeyword:
                        criteria.Keywords = txtTopSearchKeyword.Text;
                        break;

                    case EUserSearchTypejob.KeyWord:
                        criteria.Keywords = txtSearchKeywords.Text == "Keywords" ? string.Empty : txtSearchKeywords.Text;
                        break;

                    case EUserSearchTypejob.Skills:
                        criteria.Skill = txtSearchSkills.Text == "Skills" ? string.Empty : txtSearchSkills.Text;
                        break;

                    default:
                        criteria.FirstName = txtSearchFirstName.Text == "Firstname" ? string.Empty : txtSearchFirstName.Text;
                        LoggingManager.Info("SearchFirstName:" + txtSearchFirstName.Text);
                        criteria.LastName = txtSearchLastName.Text == "Lastname" ? string.Empty : txtSearchLastName.Text;
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
                        criteria.Skill = txtSearchSkills.Text == "Skills" ? string.Empty : txtSearchSkills.Text;
                        criteria.SchoolName = txtSearchSchool.Text == "School" ? string.Empty : txtSearchSchool.Text;
                        break;
                }

                LoggingManager.Debug("Exiting BuildSearchCriteria - CustomizeJobsUsers");
                return criteria;
            }

            protected void CbShowConnectionsCheckedChanged(object sender, EventArgs e)
            {
                LoggingManager.Debug("Entering CbShowConnectionsCheckedChanged - CustomizeJobsUsers");

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var user = context.Users.First(u => u.Id == LoginUserId);
                    user.ShowFeedsFromMyConnections = cbShowConnections.Checked;
                    context.SaveChanges();
                }
                LoggingManager.Debug("Exiting CbShowConnectionsCheckedChanged - CustomizeJobsUsers");
            }
        }

        public enum EUserSearchTypejob
        {
            None,
            KeyWord,
            Skills,
            HeaderKeyword
        }
    }
