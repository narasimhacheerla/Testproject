using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Entities.SearchCriteria;
using Huntable.Business;
using Huntable.Data;
using OAuthUtility;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompaniesFindFollowers : Page
    {
        private string _callbackuri;
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
        public delegate void DelPopulateSearchUsers(int pageIndex);
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Common.GetLoggedInUserId(Session);
            if (userId == null) { pplYouMayKnowDiv.Visible = false; }

            _callbackuri = FullyQualifiedApplicationPath + "oauth.aspx";
           
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
        }
        protected void IbtnFacebookClick(object sender, ImageClickEventArgs e)
        {
            OAuthWebSecurity.RequestAuthentication("Facebook", _callbackuri);

            //  Response.Redirect("oauth.aspx?currpage=facebook", false);
        }
        protected void IbtnLinkedInClick(object sender, ImageClickEventArgs e)
        {
            OAuthWebSecurity.RequestAuthentication("LinkedIn", _callbackuri);
            //  Response.Redirect("oauth.aspx?currpage=linkedin", false);
        }
        protected void IbtnTwitterClick(object sender, ImageClickEventArgs e)
        {
            OAuthWebSecurity.RequestAuthentication("Twitter", _callbackuri);
            // Response.Redirect("oauth.aspx?currpage=twitter", false);
        }
        protected void IbtnGoogleClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnGoogleClick - InviteFriends.aspx");
            try
            {
                OAuthWebSecurity.RequestAuthentication("Google", _callbackuri);
                // Response.Redirect("oauth.aspx?currpage=gmail",false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnGoogleClick - InviteFriends.aspx");
        }
        protected void IbtnYahooClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnYahooClick - InviteFriends.aspx");
            try
            {
                OAuthWebSecurity.RequestAuthentication("Yahoo", _callbackuri);
                // Response.Redirect("oauth.aspx?currpage=yahoo");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnYahooClick - InviteFriends.aspx");
        }
        protected void UploadInvites(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UploadInvites - InviteFriends.aspx");

            new CompanyMessageManager().UploadContactsFromFileUploadControl(Page, fuInvitationFriends, LoginUserId);

            LoggingManager.Debug("Exiting UploadInvites - InviteFriends.aspx");
        }
        protected void IbtnLiveClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("oauth.aspx?currpage=live", false);
        }
        protected void BtnInviteByEmailAddressesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnInviteByEmailAddressesClick - InviteFriends.aspx");
            try
            {
                var contacts = txtMailIDs.Text.Split(',');
                if (contacts.Length > 0)
                {
                    var emailcontact =
                    contacts.Select(email => new Contact { Name = string.Empty, Email = email.Trim() }).ToList();

                    var user = Common.GetLoginUser(Session);
                   new CompanyMessageManager().SendInvitationByEmail(Page, LoginUserId, emailcontact,user.IsCompany);
                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function",
                                                            "overlay('Invitaion sent succesfully')", true);
                } //var message = Constants.CustomInvitationMessage;
                //message = message.Replace("[first name]", "friend");
                //txtMessage.Text = message;
                //mpeCustomize.Show();
                //hfMessage.Value = message;
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting BtnInviteByEmailAddressesClick - InviteFriends.aspx");
        }
        protected void lbNoCustom_Click(object sender, EventArgs e)
        {
            mpeCustomize.Hide();
            SendInvites();
        }
        private void SendInvites()
        {
            var contacts = txtMailIDs.Text.Split(',');
            var user = Common.GetLoginUser(Session);
            if (contacts.Length > 0)
            {
                var emailcontacts =
                    contacts.Select(email => new Contact { Name = string.Empty, Email = email.Trim() }).ToList();

                new CompanyMessageManager().SendInvitationByEmail(Page, LoginUserId, emailcontacts,user.IsCompany);

                txtMailIDs.Text = "";
            }
        }
        protected void btnCustomInvite_Click(object sender, EventArgs e)
        {
            int customId;
            int imageId;

            if (string.IsNullOrWhiteSpace(txtMailIDs.Text.Trim()))
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "Please enter atleast one email id.");
                return;
            }

            int.TryParse(hfImageId.Value, out imageId);

            if (imageId > 0)
            {
                mpeCustomize.Hide();

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var customInv = new CustomInvitationDetail();
                    context.CustomInvitationDetails.AddObject(customInv);
                    customInv.Message = txtMessage.Text;
                    customInv.PhotoFileStoreId = imageId;

                    context.SaveChanges();
                    customId = customInv.Id;
                }
                SendInvites();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Invitaion sent succesfully')", true);
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "please select an image");
                mpeCustomize.Show();
            }
        }
        protected void lbtnChangeImage_Click(object sender, EventArgs e)
        {
            if (fuPhoto.HasFile)
            {
                var id = new FileStoreService().LoadFileFromFileUpload(Constants.CustomInvitationImages, fuPhoto);
                Img1.Src = new FileStoreService().GetDownloadUrl(id);
                hfImageId.Value = id.ToString();

            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "please select an image");

            }
            mpeCustomize.Show();
        }
        protected void BtnSkillsSearchClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnSkillsSearchClick - Customize-User");

            SearchType = EUserSearchType.Skills;
            IsSearch = true;
            LoadFindUsers(1);

            LoggingManager.Debug("Exiting BtnSkillsSearchClick - Customize-User");


        }
        protected void BtnKeywordSearchClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnKeywordSearchClick - Customize-User");

            SearchType = EUserSearchType.KeyWord;
            IsSearch = true;
            LoadFindUsers(1);

            LoggingManager.Debug("Exiting BtnKeywordSearchClick - Customize-User");
        }
        public string FullyQualifiedApplicationPath
        {
            get
            {
                //Return variable declaration
                var appPath = string.Empty;

                //Getting the current context of HTTP request
                var context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format("{0}://{1}{2}{3}",
                                            context.Request.Url.Scheme,
                                            context.Request.Url.Host,
                                            context.Request.Url.Port == 80
                                                ? string.Empty
                                                : ":" + context.Request.Url.Port,
                                            context.Request.ApplicationPath);
                }

                if (!appPath.EndsWith("/"))
                    appPath += "/";

                return appPath;
            }
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
                    users = UserManager.SearchUsersSp(searchCriteria, out totalRecords, pagerUsers.RecordsPerPage, pageIndex - 1, LoginUserId);
                }
                else
                {
                    users = CompanyManager.GetUsersAndFriends(LoginUserId, pageIndex - 1, pagerUsers.RecordsPerPage, out totalRecords);
                    if (users.Count == 0)
                    {
                        IsSearch = true;
                        this.ViewState["UserSkill"] = UserManager.GetUserSkill(LoginUserId);
                        this.ViewState["UserIndustry"] = UserManager.GetUserIndustry(LoginUserId);
                        SearchType = EUserSearchType.UserSkillMatched;
                        var searchCriteria = BuildSearchCriteria();
                        users = UserManager.SearchUsersSp(searchCriteria, out totalRecords, pagerUsers.RecordsPerPage, pageIndex - 1, LoginUserId);
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
                    criteria.Skill = txtSearchSkills.Text == "Skills" ? string.Empty : txtSearchSkills.Text;
                    criteria.SchoolName = txtSearchSchool.Text == "School" ? string.Empty : txtSearchSchool.Text;
                    break;
            }

            LoggingManager.Debug("Exiting BuildSearchCriteria -Customize-User");
            return criteria;
        }
        protected void BtnSearchClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnSearchClick -Customize-User");
            SearchType = EUserSearchType.None;
            IsSearch = true;
            LoadFindUsers(1);
            LoggingManager.Debug("Exiting BtnSearchClick -  Customize-User");
        }
        protected void FollowupClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering FollowupClick - Customize-User");

            var button = sender as LinkButton;
            if (button != null)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {


                    int userId = Convert.ToInt32(button.CommandArgument);
                    CompanyMessageManager.SendInvitation(userId , LoginUserId);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Invitaion sent succesfully')", true);
                
                }
                LoadFindUsers(pagerUsers.PageIndex);

                LoggingManager.Debug("Exiting FollowupClick - Customize-User");
            }
        }

        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - CompanyView");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var user_to = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.Invitations.Where(y => y.InvitedId == user_to && y.UserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                else
                    return false;

            }


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

                    //cbShowConnections.Checked = context.Users.First(u => u.Id == LoginUserId).ShowFeedsFromMyConnections;
                    //cbShowConnections.AutoPostBack = true;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadDDLs - CustomizeUser");

        }
        protected void BtnSearchTopKeywordServerClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchTopKeywordServerClick - Customize-User");

            SearchType = EUserSearchType.HeaderKeyword;
            IsSearch = true;
            LoadFindUsers(1);

            LoggingManager.Debug("Exiting BtnSearchTopKeywordServerClick - Customize-User");
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
        public string UserUrlGenerator(object id)
        {
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().UserUrlGenerator(companyid);
            }
            else
            {
                return null;
            }

        }
    }
}