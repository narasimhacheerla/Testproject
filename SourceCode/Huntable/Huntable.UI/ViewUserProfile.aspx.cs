using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using OAuthUtility;
using Snovaspace.Util.Logging;
using System.Web;
using System.Web.UI;
using System.Net;
using Snovaspace.Util;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI
{
    public partial class ViewUserProfile : Page
    {

        private bool? IsOtherUserBlocked
        {
            get
            {
                LoggingManager.Debug("Entering IsOtherUserBlocked - ViewUserProfile.aspx");
                if (OtherUserId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var loggedInUserId = Common.GetLoggedInUserId(Session);
                        var blockedlist = context.UserBlockLists.FirstOrDefault(e => e.UserId == loggedInUserId && e.BlockedUserId == OtherUserId.Value);
                        var endorseBlock =
                            context.UserEndorseLists.FirstOrDefault(
                                e => e.EndorsedUserId == loggedInUserId && e.UserId == OtherUserId);
                        if (endorseBlock != null || OtherUserId.Value == LoginUserId)
                        {
                            divEndorseBLock.Visible = false;

                        }
                        if (OtherUserId.Value == LoginUserId)
                        {
                            nonc.Visible = false;

                        }

                        if (blockedlist != null)
                        {
                            divunblock.Visible = true;
                            partialunblock.Visible = false;
                        }
                        var blockedUser = context.UserBlockLists.FirstOrDefault(e => e.UserId == OtherUserId.Value && e.BlockedUserId == loggedInUserId);
                        if (blockedUser != null)
                        {
                            divMsg.Visible = false;
                            RequestEndorsements.Visible = false;
                            divEndorseBLock.Visible = false;

                        }
                        return false;
                    }
                }
                LoggingManager.Debug("Exiting IsOtherUserBlocked - ViewUserProfile.aspx");
                return null;
            }
        }
        protected void itembound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering itembound - ViewUserProfile.aspx");
            if (e.Item.DataItem != null)
            {
                var loginuserid = Common.GetLoggedInUserId(Session);
                if (loginuserid.HasValue)
                {
                    if (LoginUserId == OtherUserId)
                    {
                        Control a1 = (Control)e.Item.FindControl("aediv");
                        HtmlAnchor a2 = (HtmlAnchor)e.Item.FindControl("te");
                        a2.Visible = false;
                        a1.Visible = false;
                    }
                    if (!OtherUserId.HasValue)
                    {
                        Control a1 = (Control)e.Item.FindControl("aediv");
                        HtmlAnchor a2 = (HtmlAnchor)e.Item.FindControl("te");
                        a2.Visible = false;
                        a1.Visible = false;
                    }
                }
            }
            LoggingManager.Debug("Exiting itembound - ViewUserProfile.aspx");
        }
        private int? OtherUserId
        {

            get
            {
                LoggingManager.Debug("Entering OtherUserId - ViewUserProfile.aspx");
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                if (Page.RouteData.Values["ID"] != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
                }
                LoggingManager.Debug("Exiting OtherUserId - ViewUserProfile.aspx");
                return null;
            }
        }


        public string CalculateChatAuthHash()
        {
            var loginUser = Common.GetLoggedInUser();
            if (OtherUserId != null && loginUser != null)
            {
                var result = Common.GetChatUrl(loginUser.Id.ToString(), OtherUserId.Value.ToString(), loginUser.Name, loginUser.UserProfilePictureDisplayUrl);
                return result;
            }
            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                LoggingManager.Debug("Entering Page_load - ViewUserProfile.aspx");
                if (!IsPostBack)
                {
                    LoadProfile();

                    hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
                }
                if (btnBlockUser.Visible)
                {

                    SetBlockUserButttonText();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load -ViewUserProfile.aspx");
            if (Common.IsLoggedIn())
            {


            }
            if (hdnUserId.Value == "")
            {
                divBLock.Visible = false;
                ssd.Visible = false;
            }

        }

        private void LoadProfile()
        {
            try
            {
                LoggingManager.Debug("Entering LoadProfile - ViewUserProfile.aspx");
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var loginUserId = Common.GetLoggedInUserId(Session);
                    User user = null;
                    if (OtherUserId.HasValue)
                    {
                        hdnOtherUserId.Value = OtherUserId.Value.ToString();
                        var userp = context.Users.FirstOrDefault(x => x.Id == OtherUserId.Value);

                        showProfileImage12.HRef = userp.UserProfilePictureDisplayUrl;
                        vc.HRef = new UrlGenerator().UserUrlGenerator(OtherUserId.Value);
                        vt.HRef = new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                        visualcvactivity.HRef = new UrlGenerator().UserActivityUrlGenerator(OtherUserId.Value);
                        //visualcvactivity.HRef = "VisualCvactivity.aspx?UserId=" + OtherUserId.Value;
                        if (OtherUserId == loginUserId)
                        {
                            divMsg.Visible = false;
                            btnFollow.Visible = false;
                        }
                        nonc.Visible = true;
                        user = context.Users.First(u => u.Id == OtherUserId.Value);

                        var cmpnm = from a in context.EmploymentHistories
                                    join b in context.MasterCompanies on a.CompanyId equals b.Id
                                    where a.UserId == OtherUserId.Value && a.IsDeleted == null
                                    select new
                                    {
                                        a.Id,
                                        b.Description
                                    };
                        var cmpnmu = from a in context.EmploymentHistories
                                     join b in context.MasterCompanies on a.CompanyId equals b.Id
                                     where a.UserId == loginUserId && a.IsDeleted == null
                                     select new
                                     {
                                         a.Id,
                                         b.Description
                                     };



                        ddljobtitle.DataSource = cmpnm.Distinct();
                        ddljobtitle.DataTextField = "Description";
                        ddljobtitle.DataValueField = "Id";
                        ddljobtitle.DataBind();
                        ddljob.DataSource = cmpnmu.Distinct();
                        ddljob.DataTextField = "Description";
                        ddljob.DataValueField = "Id";
                        ddljob.DataBind();
                        // Logging the profile visited activity.
                        DisplayUserDetails(user);
                        if (OtherUserId.Value != loginUserId)
                            LoagUserProfileVisited(context, OtherUserId.Value, loginUserId);
                        if (loginUserId.HasValue && loginUserId.Value == OtherUserId.Value)
                        {
                            btnMessage.DataBind();
                            txtShareMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                            txtMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                            fe_text.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                            TextBox1.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);


                        }
                        txtShareMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                        txtMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                        fe_text.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                        TextBox1.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                        if (loginUserId != null)
                        {
                            var user1 = UserManager.FollowingUser(OtherUserId.Value, loginUserId.Value);
                            if (user1.Count != 0)
                            {
                                btnFollow.Visible = false;
                                LinkButton1.Visible = true;
                            }
                            else
                            {
                                btnFollow.Visible = true;
                                LinkButton1.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        txtShareMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                        txtMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                        fe_text.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                        TextBox1.Text = "https://huntable.co.uk/" + new UrlGenerator().UserTextUrlGenerator(loginUserId.Value);
                        
                        vc.HRef = "visualcv.aspx";
                        vt.HRef = "viewuserprofile.aspx";
                        visualcvactivity.HRef = "visualcvactivity.aspx";
                        nonc.Visible = false;
                        if (loginUserId.HasValue)
                        {
                            user = context.Users.First(u => u.Id == loginUserId.Value);
                            divBLock.Visible = false;
                            btnFollow.Visible = false;
                            showProfileImage12.HRef = user.UserProfilePictureDisplayUrl;
                        }
                        DisplayUserDetails(user);

                        divMsg.Visible = false;
                    }

                    //  if (user != null)
                    //  {
                    //       DisplayUserDetails(user);
                    //  }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadProfile - ViewUserProfile.aspx");

        }
        protected void UserCompaniesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UserCompaniesClick - ViewUserProfile.aspx");
            var userId = Common.GetLoggedInUserId(Session);
            int id = 0;
            if (OtherUserId.HasValue)
            {

                id = OtherUserId.Value;
            }
            else if (userId != null)
            {
                id = userId.Value;

            }
            if (id != 0)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
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
                LoggingManager.Debug("Exiting UserCompaniesClick - ViewUserProfile.aspx");
            }

            //ScriptManager.RegisterStartupScript(this, GetType(), "Click", "rowaction22();", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "rowAction22()", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "Companies", "rowAction22();", true);


        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - ViewUserProfile.aspx");
            if (id != null)
            {

                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var result = context.Users.FirstOrDefault(x => x.Id == p);

                    if (result != null)
                    {
                        var photo = result.PersonalLogoFileStoreId;

                        LoggingManager.Debug("Exiting Picture - ViewUserProfile.aspx");
                        return new FileStoreService().GetDownloadUrl(photo);
                    }
                }

            }
            return "~/HuntableImages/nomore.jpg";
        }
        public string Picturec(object id)
        {
            LoggingManager.Debug("Entering Picture - VisualCV.aspx");
            if (id != null)
            {

                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {


                    LoggingManager.Debug("Exiting Picture - VisualCV.aspx");
                    return new FileStoreService().GetDownloadUrl(p);

                }

            }
            return "~/HuntableImages/nomore.jpg";
        }
        private void LoagUserProfileVisited(huntableEntities context, int userId, int? loginUserId)
        {
            LoggingManager.Debug("Entering LoagUserProfileVisited - ViewUserProfile.aspx");

            var profileVisitedHistory = new UserProfileVisitedHistory
            {
                Date = DateTime.Now,
                UserId = userId,
                VisitorUserId = loginUserId,
                IPAddress = GetIpAddress()
            };
            context.UserProfileVisitedHistories.AddObject(profileVisitedHistory);
            context.SaveChanges();

            if (loginUserId != null)
                FeedManager.addFeedNotification(FeedManager.FeedType.Profile_Viewed, loginUserId.Value, userId, null);

            LoggingManager.Debug("Exiting LoagUserProfileVisited - ViewUserProfile.aspx");

        }

        private string GetIpAddress()
        {
            LoggingManager.Debug("Entering GetIpAddress - ViewUserProfile.aspx");

            string strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                                  Request.ServerVariables["REMOTE_ADDR"];

            LoggingManager.Debug("Exiting GetIpAddress - ViewUserProfile.aspx");

            return strIpAddress;
        }

        private void DisplayUserDetails(User user)
        {
            var loginUserId = Common.GetLoggedInUserId(Session);

            var objInvManager = new InvitationManager();
            try
            {
                LoggingManager.Debug("Entering DisplayUserDetails - ViewUserProfile.aspx");
                imgHasCompany.Visible = user.UserHasCompany;
                imgProfile.Src = user.UserProfilePictureDisplayUrl;
                lblSName.Text = user.Name;
                txtToAddress.Text = user.Name;
                var level1Count = user.LevelOneInvitedCount.HasValue ? user.LevelOneInvitedCount : 0;
                var level2Count = user.LevelTwoInvitedCount.HasValue ? user.LevelTwoInvitedCount : 0;
                var level3Count = user.LevelThreeInvitedCount.HasValue ? user.LevelThreeInvitedCount : 0;
                int count = Convert.ToInt32(level1Count + level2Count + level3Count);
                lblinvited.Text = count.ToString();
                var userEnorsements = objInvManager.GetUserEnorsements(user.Id);

                rspEndorse.DataSource = userEnorsements;
                rspEndorse.DataBind();
                lblEndorsement.Text = (userEnorsements != null) ? Convert.ToString(userEnorsements.Count) : "0";

                var level1Premiumcount = user.LevelOnePremiumCount.HasValue ? user.LevelOnePremiumCount.Value : 0;
                var level2Premiumcount = user.LevelTwoPremiumCount.HasValue ? user.LevelTwoPremiumCount.Value : 0;
                var level3Premiumcount = user.LevelThreePremiumCount.HasValue ? user.LevelThreePremiumCount.Value : 0;

                int joined = (Convert.ToInt32(level1Premiumcount + level2Premiumcount + level3Premiumcount));
                lblJoinedFriends.Text = joined.ToString();
                decimal affiliate = Convert.ToDecimal((level1Premiumcount * 4) + (level2Premiumcount * 1) + (level3Premiumcount * .5));
                lblAffiliate.Text = affiliate.ToString() + "$";
                lblName.Text = user.Name;
                //txtUserSearchKeyword.Text = "";
                lblUName.Text = user.Name;
                Label4.Text = user.Name;
                lblEndorsedUser.Text = user.Name;//for endorse dialog box
                LoggingManager.Info("User Name" + user.Name);

                lblTown.Text = string.Format("{0}", user.City);
                var currentEmployment = user.EmploymentHistories.FirstOrDefault(emp => emp.IsCurrent && (emp.IsDeleted == null || emp.IsDeleted == false));
                if (currentEmployment != null)
                {
                    LoggingManager.Info("Inside Current Employment");
                    lblCurrentRole.Text = string.Format("{0} at {1}", currentEmployment.JobTitle, currentEmployment.MasterCompany != null ? currentEmployment.MasterCompany.Description : string.Empty);
                    lblCurrentPosition.Text = lblCurrentRole.Text;

                }
                var eh = user.EmploymentHistories.Where(h => h.IsCurrent == false && (h.IsDeleted == null || h.IsDeleted == false) && h.MasterCompany != null && h.MasterYear1 != null).OrderBy(h => h.MasterYear1.Description).ThenBy(h => h.ToMonthID).Select(h => new { h.JobTitle, h.MasterCompany.Description }).ToList();
                var strEmpHist = "";
                var strEmpHistFirstTwo = "";
                var historyCount = 0;
                foreach (var e in eh)
                {
                    historyCount++;
                    if (historyCount <= 2)
                        strEmpHistFirstTwo += string.Format("{0} at {1} <br/> ", e.JobTitle, e.Description != null ? e.Description.Replace("~", "") : string.Empty);

                    strEmpHist += string.Format("{0} at {1} <br/> ~", e.JobTitle, e.Description ?? string.Empty);
                }
                strEmpHist = strEmpHist.Replace("'", "`").Replace("\"", "");

                lblPastPosition.Text = ((strEmpHistFirstTwo.Length > 0) ? strEmpHistFirstTwo.Substring(0, strEmpHistFirstTwo.Length ) : "") + ((historyCount > 2) ? "&nbsp;<a href='javascript:employmentHistoryMore(\"" + strEmpHist + "\");'>More</a>" : "");
                var eduh = user.EducationHistories.Where(e => e.Degree != null && e.MasterYear1 != null).OrderBy(e => e.MasterYear1.Description).ThenBy(e => e.ToMonthID).Select(e => new { e.Degree, e.Institution }).ToList();
                var strEduHist = "";
                var strEduHistFirstTwo = "";
                historyCount = 0;
                foreach (var e in eduh)
                {
                    historyCount++;
                    if (historyCount <= 2)
                        strEduHistFirstTwo += string.Format("{0} at {1} <br/>  ", e.Degree, e.Institution != null ? e.Institution.Replace("~", "") : string.Empty);

                    strEduHist += string.Format("{0} at {1} <br/> ~", e.Degree, e.Institution ?? string.Empty);
                }
                strEduHist = strEduHist.Replace("'", "`").Replace("\"", "");
                lblEducation.Text = ((strEduHistFirstTwo.Length > 0) ? strEduHistFirstTwo.Substring(0, strEduHistFirstTwo.Length - 2) : "") + ((historyCount > 2) ? "&nbsp;<a href='javascript:educationHistoryMore(\"" + strEduHist + "\");'>More</a>" : "");
                //lblSummary.Text = user.Summary.Replace("\n", "<br/>");
                lblSummary.Text = user.Summary.Replace("\n", "<br/>");

                var currentExperiences = user.EmploymentHistories.Where(x => x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { emp.JobTitle, emp.UserId, Company = emp.MasterCompany != null ? emp.MasterCompany.Description : string.Empty, Location = emp.MasterCountry != null ? emp.MasterCountry.Description : string.Empty, town = emp.Town != null ? emp.Town : string.Empty, Period = GetEmploymentPeriod(emp), Skill = GetEmployeSkill((int)(emp.Id)), Level = GetEmployeLevel((int)(emp.LevelId == null ? 9 : emp.LevelId)), Industry = GetEmployeIndustry((int)(emp.IndustryId == null ? 155 : emp.IndustryId)), emp.Description, startDate = GetEmpMasterYear(emp) * 12 + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();
                var pastExperiences = user.EmploymentHistories.Where(x => !x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { emp.JobTitle, emp.UserId, Company = emp.MasterCompany != null ? emp.MasterCompany.Description : string.Empty, Location = emp.MasterCountry != null ? emp.MasterCountry.Description : string.Empty, town = emp.Town != null ? emp.Town : string.Empty, Period = GetEmploymentPeriod(emp), Skill = GetEmployeSkill((int)(emp.Id)), Level = GetEmployeLevel((int)(emp.LevelId == null ? 9 : emp.LevelId)), Industry = GetEmployeIndustry((int)(emp.IndustryId == null ? 155 : emp.IndustryId)), emp.Description, startDate = GetEmpMasterYear(emp) * 12 + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();

                rptrExperience.DataSource = currentExperiences.Concat(pastExperiences).ToList();
                rptrExperience.DataBind();

                rpEducations.DataSource = user.EducationHistories.Select(edu => new { edu.Institution, Course = edu.Degree, Period = GetEducationPeriod(edu) });
                rpEducations.DataBind();
                lblLocation.Text = string.Format("{0}, {1}", user.County, user.CountryName);

                if (loginUserId == OtherUserId || OtherUserId == null)
                {

                    if (user.PhoneNumber != null)
                    {
                        lblPhoneNumber.Text = user.PhoneNumber;
                    }
                    if (user.HomeAddress != null)
                    {

                        lblAddress.Text = user.HomeAddress;
                    }
                    if (user.City != null)
                    {
                        lblCity.Text = user.City;
                    }
                    if (user.CountryName != null)
                    {
                        lblCountry.Text = user.CountryName;
                    }
                    if (user.DOB.HasValue)
                        lblBirthDay.Text = user.DOB.Value.ToShortDateString();
                    lblMaritalStatus.Text = user.IsMarried ? "Married" : string.Empty;
                }
                else
                {
                    headPersonal.Visible = false;
                    tblPersonal.Visible = false;
                }
                if (user.LastActivityDate.HasValue && user.LastActivityDate.Value > DateTime.Now.AddMinutes(-ConfigurationManagerHelper.GetAppsettingByKey<int>(Constants.LastActivityMinutesKey)))
                {
                    userOnline.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOnlineImagePathKey);
                    ImageButton1.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOnlineImagePathKey);
                    onlineinfo.Text = string.Format("Online Now");
                    Label1.Text = string.Format("Online Now");
                }
                else
                {
                    userOnline.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOfflineImagePathKey);
                    ImageButton1.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOfflineImagePathKey);
                    onlineinfo.Text = string.Format("Offline");
                    Label1.Text = string.Format("Offline");
                }
                var currentExperiences1 = user.EmploymentHistories.Where(x => x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { Skill = GetEmployeSkill((int)(emp.Id)), startDate = GetEmpMasterYear(emp) * 12 + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();
                var pastExperiences1 = user.EmploymentHistories.Where(x => !x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { Skill = GetEmployeSkill((int)(emp.Id)), startDate = GetEmpMasterYear(emp) * 12 + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();
                var pastcurrentskill = currentExperiences1.Concat(pastExperiences1).ToList();
                if (pastcurrentskill != null)
                {
                    //var skill =
                    //    currentEmployment.UserEmploymentSkills.Select(
                    //        s => s.MasterSkill != null ? s.MasterSkill.Description : null).ToList();
                    //if (skill != null)
                    //{
                    rpSkill.DataSource = pastcurrentskill.Where(x => x.Skill != string.Empty);
                        rpSkill.DataBind();
                    //}

                }
                var currentinstry = user.EmploymentHistories.Where(x => x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { Industry = GetEmployeIndustry((int)(emp.IndustryId == null ? 155 : emp.IndustryId)), startDate = GetEmpMasterYear(emp) * 12 + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();
                var pstinstry = user.EmploymentHistories.Where(x => !x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { Industry = GetEmployeIndustry((int)(emp.IndustryId == null ? 155 : emp.IndustryId)), startDate = GetEmpMasterYear(emp) * 12 + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();
                var pastcurrentinstry = currentinstry.Concat(pstinstry).ToList();
                if (pastcurrentinstry != null)
                {
                    rpindstry.DataSource = pastcurrentinstry.Where(x=>x.Industry != string.Empty);
                    rpindstry.DataBind();
                    //lbliustrndy.Text = string.Join(",", currentEmployment.MasterIndustry.Description);
                }
                GetUserLanguages(user.Id);
                GetUserInterests(user.Id);
                GetUserSkills(user.Id);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DisplayUserDetails - ViewUserProfile.aspx");
        }

        private static int GetEmpMasterYear(EmploymentHistory emp)
        {
            LoggingManager.Debug("Entering GetEmpMasterYear - ViewUserProfile.aspx");
            LoggingManager.Debug("Exiting GetEmpMasterYear - ViewUserProfile.aspx");
            return MasterDataManager.AllYears.Where(x => x.ID == emp.FromYearID).Select(year => Int32.Parse(year.Description)).FirstOrDefault();

        }

        private string GetEducationPeriod(EducationHistory history)
        {

            LoggingManager.Debug("Entering GetEducationPeriod - ViewUserProfile.aspx");

            string period = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                period = string.Format("{0} {1} - {2} {3}", history.MasterMonth.Description, history.MasterYear.Description, history.MasterMonth1.Description, history.MasterYear1.Description);
            }

            LoggingManager.Debug("Exiting GetEducationPeriod - ViewUserProfile.aspx");

            return period;
        }

        private string GetEmploymentPeriod(EmploymentHistory history)
        {

            LoggingManager.Debug("Entering GetEmploymentPeriod - ViewUserProfile.aspx");

            string period = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                period = string.Format("{0} {1} - {2} {3}", history.MasterMonth.Description, history.MasterYear1.Description, history.MasterMonth1.Description, history.MasterYear.Description);
            }
            LoggingManager.Debug("Exiting GetEmploymentPeriod - ViewUserProfile.aspx");

            return period;
        }

        protected void BtnProfileEditUpdateClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnProfileEditUpdateClick - ViewUserProfile.aspx");

            Response.Redirect("~/EditProfilePage.aspx");

            Response.Redirect("~/EditProfilePage.aspx", false);

            LoggingManager.Debug("Exiting BtnProfileEditUpdateClick - ViewUserProfile.aspx");
        }

        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - ViewUserProfile.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            if (loggedInUserId != null)
            {
                var result = jobManager.GetUserDetails(loggedInUserId.Value);

                if (result.IsPremiumAccount == false || result.IsPremiumAccount == null)
                {
                    Server.Transfer("WhatIsHuntableUpgrade.aspx");
                }
                else if (result.CreditsLeft == null || result.CreditsLeft == 0)
                {
                    Server.Transfer("BuyCredit.aspx");
                }
                else
                {
                    Server.Transfer("PostJob.aspx");
                }
            }




            LoggingManager.Debug("Exiting BtnPostOpportunityClick - ViewUserProfile.aspx");
        }

        protected void BtnEndorseUserClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnEndorseUserClick - ViewUserProfile.aspx");

            try
            {
                var sessionid = Session[SessionNames.LoggedInUserId];
                if (sessionid != null)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var loggedInUserId = Common.GetLoggedInUserId(Session);
                        var user = context.Users.FirstOrDefault(x => x.Id == OtherUserId);
                        if (OtherUserId != null)
                        {
                            if (loggedInUserId != null)
                            {
                                var tilte = ddljobtitle.SelectedItem;
                                if (tilte != null)
                                {
                                    int id = Convert.ToInt32(ddljobtitle.SelectedItem.Value);
                                    var endorse = new UserEndorseList
                                                      {
                                                          EmpHisId = id,
                                                          JobTitle = tilte.ToString(),
                                                          Comments = txtEndorseComment.InnerText,
                                                          UserId = OtherUserId.Value,
                                                          EndorsedUserId = loggedInUserId.Value,
                                                          EndorsedDateTime = DateTime.Now
                                                      };
                                    context.UserEndorseLists.AddObject(endorse);
                                    context.SaveChanges();
                                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Succesfully endorsed')", true);
                                    DisplayUserDetails(user);
                                    FeedManager.addFeedNotification(FeedManager.FeedType.Endorsed, loggedInUserId.Value,
                                                                    id, null);
                                    var username = context.Users.FirstOrDefault(u => u.Id == loggedInUserId);
                                    var socialManager = new SocialShareManager();
                                    var msg = username.FirstName + " " + username.LastName + " " + "has endorsed" + " " + "[UserName]" + " " + "for" + " " + endorse.JobTitle;
                                    socialManager.ShareOnFacebook(OtherUserId.Value, msg, "");


                                }
                                else
                                {

                                    var endorse = new UserEndorseList
                                                      {
                                                          Comments = txtEndorseComment.InnerText,
                                                          UserId = OtherUserId.Value,
                                                          EndorsedUserId = loggedInUserId.Value,
                                                          EndorsedDateTime = DateTime.Now
                                                      };
                                    context.UserEndorseLists.AddObject(endorse);
                                    context.SaveChanges();
                                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Succesfully endorsed')", true);
                                    DisplayUserDetails(user);
                                    var socialManager = new SocialShareManager();
                                    var username = context.Users.FirstOrDefault(u => u.Id == loggedInUserId);
                                    var msg = username.FirstName + " " + username.LastName + " " + "has endorsed" + " " + "[UserName]" + " " + "for" + " " + endorse.JobTitle;
                                    socialManager.ShareOnFacebook(OtherUserId.Value, msg, "");


                                }

                            }
                        }

                    }
                    if (OtherUserId != null) Response.Redirect("~/" + new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value));
                }
                else
                {
                    Response.Write(
                                "<script language='javascript'>alert('You are Not logged in. Please Login First');</script>");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnEndorseUserClick - ViewUserProfile.aspx");
        }

        protected void BtnRequestEndorseClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnRequestEndorseClick - ViewUserProfile.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    if (OtherUserId != null)
                    {
                        var userMessage = new UserMessage
                        {
                            SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                            SentTo = OtherUserId.Value,
                            Subject = "Endorsement Request",
                            Body = txtarea.InnerText,
                            IsActive = true,
                            SentIsActive = true,
                            IsRead = false,
                            SentDate = DateTime.Now
                        };
                        var objMessageManager = new UserMessageManager();
                        objMessageManager.SaveMessage(context, userMessage, true);
                        Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Endorsement request sent succesfully')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnRequestEndorseClick - ViewUserProfile.aspx");
        }

        protected void BtnBlockUserClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnBlockUserClick - ViewUserProfile.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var objMessageManager = new UserMessageManager();
                    if (OtherUserId != null)
                        objMessageManager.BlockUser(context, Convert.ToInt32(Common.GetLoggedInUserId(Session)), OtherUserId.Value);
                    SetBlockUserButttonText();
                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Blocked successfully')", true);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnBlockUserClick - ViewUserProfile.aspx");
        }
        protected void BtnFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnFollowClick - ViewUserprofile.aspx");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);
                if (OtherUserId != null)
                {
                    if (loginUserId != null)
                    {


                        UserManager.FollowUser(loginUserId.Value, OtherUserId.Value);
                        var messagemanager = new UserMessageManager();
                        messagemanager.FollowUser(OtherUserId.Value, loginUserId.Value);
                        btnFollow.Visible = false;
                        LinkButton1.Visible = true;
                        Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('You are now following')", true);


                    }
                }

            }
            catch (Exception)
            {
                { }
                throw;
            }
            LoggingManager.Debug("Exiting BtnFollowClick - ViewUserprofile.aspx");

        }
        protected void BtnUnFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnUnFollowClick - ViewUserprofile.aspx");
            int? loggedinuserid = Common.GetLoggedInUserId(Session);
            var usrmgr = new UserManager();
            if (OtherUserId != null)
                if (loggedinuserid != null)
                {
                    usrmgr.Unfollow(OtherUserId.Value, loggedinuserid.Value);
                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                } LinkButton1.Visible = false;
            btnFollow.Visible = true;
            LoggingManager.Debug("Exiting BtnUnFollowClick - ViewUserprofile.aspx");
        }
        private void SetBlockUserButttonText()
        {
            LoggingManager.Debug("Entering SetBlockUserButttonText - ViewUserprofile.aspx");

            if (IsOtherUserBlocked.HasValue && IsOtherUserBlocked.Value)
            {
                btnBlockUser.Text = "Unblock Messages from this user";
            }
            else
            {
                btnBlockUser.Text = "Block Messages from this user";
            }
            LoggingManager.Debug("Exiting SetBlockUserButttonText - ViewUserprofile.aspx");


        }
        //        protected void BtnUserSearchClick(object sender, EventArgs e)
        //{
        //     LoggingManager.Debug("Entering btnUserSearch_Click - HomePageAfterLoggingIn.aspx");
        //   try
        // {
        //   string url = string.Format("~/UserSearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
        // Response.Redirect(url, false);
        //}
        //catch (Exception ex)
        //{
        //   LoggingManager.Error(ex);
        //}
        //LoggingManager.Debug("Exiting btnUserSearch_Click - HomePageAfterLoggingIn.aspx");
        //}

        protected void ImgbtnPdfClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering ImgbtnPdfClick - ViewUserprofile.aspx");

            var loginUserId = Common.GetLoggedInUserId(Session);
            if (OtherUserId.HasValue)
            {
                HttpRequest request = HttpContext.Current.Request;

                string websiteurl = request.IsSecureConnection ? "https://" : "http://";

                websiteurl += request["HTTP_HOST"] + "/";
                string url = websiteurl + "UserPDF.aspx?UserId=" + OtherUserId + "";
                string filePath = UserProfileManager.CreatePDFForUrl(url, OtherUserId + ".pdf");
                DownLoadPdf(filePath, OtherUserId + ".pdf");
            }
            else
            {
                HttpRequest request = HttpContext.Current.Request;

                string websiteurl = request.IsSecureConnection ? "https://" : "http://";

                websiteurl += request["HTTP_HOST"] + "/";
                string url = websiteurl + "UserPDF.aspx?UserId=" + loginUserId + "";
                string filePath = UserProfileManager.CreatePDFForUrl(url, loginUserId + ".pdf");
                DownLoadPdf(filePath, loginUserId + ".pdf");
            }



            LoggingManager.Debug("Exiting ImgbtnPdfClick - ViewUserprofile.aspx");
        }

        private void DownLoadPdf(string pdfFilePathAndName, string fileName)
        {
            LoggingManager.Debug("Entering DownLoadPdf - ViewUserprofile.aspx");
            try
            {
                var client = new WebClient();
                Byte[] buffer = client.DownloadData(pdfFilePathAndName);

                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());

                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            LoggingManager.Debug("Exiting DownLoadPdf - ViewUserprofile.aspx");

        }

        protected void BtnMessageClick(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering BtnMessageClick - ViewUserProfile.aspx");
                if (Common.GetLoggedInUserId(Session) != null)
                {
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
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {

                        if (OtherUserId != null)
                        {
                            var userMessage = new UserMessage
                            {
                                SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                                SentTo = OtherUserId.Value,
                                Subject = hfSubject.Value,
                                Body = txtMessage.Text,
                                IsActive = true,
                                SentIsActive = true,
                                IsRead = false,
                                SentDate = DateTime.Now
                            };
                            var objMessageManager = new UserMessageManager();
                            objMessageManager.SaveMessage(context, userMessage);
                            Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Message sent succesfully ')", true);
                            //var control =
                            //       (HeaderAfterLoggingIn)LoadControl("HeaderAfterLoggingIn.ascx");

                            //objMessageManager.SaveMessage(context, userMessage);
                            //   this.Controls.Add(control);
                            //control.Flashmessage("message saved successfully");
                            //    lblMessage.Visible = true;


                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Please Login to Send Message.";
                    lblMessage.Visible = true;
                    Response.Write("<script language='javascript'>alert('You are not loggedin.Please login first');</script>");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnMessageClick - ViewUserProfile.aspx");
        }


        protected void BtnUnblockClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnUnblockClick - ViewUserprofile.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var resul = context.UserBlockLists.FirstOrDefault(x => x.UserId == LoginUserId && x.BlockedUserId == OtherUserId.Value);
                //var result = context.UserBlockLists.FirstOrDefault(u => u.UserId == loggedInUserId && u.BlockedUserId == OtherUserId.Value);
                context.UserBlockLists.DeleteObject(resul);
                context.SaveChanges();
            }
            divunblock.Visible = false;
            partialunblock.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Unblocked successfully')", true);
            LoggingManager.Debug("Exiting BtnUnblockClick - ViewUserprofile.aspx");

        }

        public string Format(object id)
        {
            LoggingManager.Debug("Entering Format - ViewUserprofile.aspx");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);
                if (result != null)
                {
                    var name = result.Name;
                    return name;
                }
            }
            LoggingManager.Debug("Exiting Format - ViewUserprofile.aspx");

            return null;
        }
        public string Pict(object id)
        {
            LoggingManager.Debug("Entering Pict - ViewUserprofile.aspx");

            var objInvManager = new InvitationManager();
            int p = Int32.Parse(id.ToString());
            var result = objInvManager.GetUsercurrentrole(p);

            string name = result != null ? result.JobTitle : "";

            LoggingManager.Debug("Exiting Pict - ViewUserprofile.aspx");

            return name;
        }
        //protected void btnChat_Click(object sender, EventArgs e)
        //{
        //    var loginUserId = Common.GetLoggedInUserId(Session);
        //    if (!loginUserId.HasValue)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "alert('You are not loggedin.Please login first');", true);


        //    }
        //    Response.Write("<script language='javascript'>window.open('AjaxChat/MessengerWindow.aspx?init=1&target=" + OtherUserId + "', '', 'width=650,height=400,resizable=1,menubar=0,status=0,toolbar=0')</script>");
        //}
        protected void RepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering RepeaterItemDataBound - ViewUserprofile.aspx");
            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("vup");

            if (e.Item.ItemType == ListItemType.Item)
            {

            }
            if (e.Item.DataItem != null)
            {

                int endorseuserid = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "EndorsedUserId").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == endorseuserid);

                    if (usr != null && usr.IsCompany != null)
                    {
                        var firstOrDefault = context.Companies.FirstOrDefault(x => x.Userid == endorseuserid);
                        if (firstOrDefault != null)
                        {
                            int cmpny = firstOrDefault.Id;
                            a1.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny);
                        }
                    }
                    else
                    {

                        a1.HRef = new UrlGenerator().UserUrlGenerator(endorseuserid);
                    }
                }
                var btn = (LinkButton)e.Item.FindControl("btnDelete");
                btn.Visible = !OtherUserId.HasValue;
            }

            LoggingManager.Debug("Exiting RepeaterItemDataBound - ViewUserprofile.aspx");
        }
        protected void DeleteEndorsements(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DeleteEndorsements - ViewUserprofile.aspx");

            var button = sender as LinkButton;
            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var user = context.UserEndorseLists.FirstOrDefault(x => x.Id == Id);
                    if (user != null) user.IsDeleted = true;
                    context.SaveChanges();
                    FeedManager.deleteFeedNotitifation(Huntable.Business.FeedManager.FeedType.Endorsed, Common.GetLoggedInUserId(Session).GetValueOrDefault(), user.Id);
                    LoadProfile();


                }
            }
            LoggingManager.Debug("Exiting DeleteEndorsements - ViewUserprofile.aspx");

        }
        public string Name(object id)
        {
            if (id != null)
            {
                int n = Int32.Parse(id.ToString());
                using (var cont = huntableEntities.GetEntitiesWithNoLock())
                {
                    var urn = cont.Users.FirstOrDefault(x => x.Id == n);
                    return urn.FirstName;
                }
            }
            else
            {
                return null;
            }
        }
        private void GetUserLanguages(int id)
        {
            LoggingManager.Debug("Entering GetUserLanguages - ViewUserProfile.aspx");

            pnlLanguages.Controls.Clear();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                pnlLanguages.Controls.Add(new LiteralControl("<table width='100%'>"));
                List<UserLanguage> userlanguages = context.UserLanguages.Where(l => l.UserId == id).ToList();
                foreach (var ul in userlanguages)
                {
                    pnlLanguages.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterLanguage.Description + "</td></tr>"));
                }
                pnlLanguages.Controls.Add(new LiteralControl("</table>"));
            }

            LoggingManager.Debug("Exiting GetUserLanguages - ViewUserProfile.aspx");
        }
        private void GetUserInterests(int id)
        {

            LoggingManager.Debug("Entering GetUserInterests - ViewUserProfile.aspx");

            pnlInterests.Controls.Clear();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<UserInterest> userInterest = context.UserInterests.Where(l => l.UserId == id).ToList();
                if (userInterest.Count > 0)
                {
                    pnlInterests.Controls.Add(new LiteralControl("<table width='100%'>"));
                    foreach (var ul in userInterest)
                    {
                        pnlInterests.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterInterest.Description + "</td></tr>"));
                    }
                    pnlInterests.Controls.Add(new LiteralControl("</tr></table>"));
                }
            }
            LoggingManager.Debug("Exiting GetUserInterests - ViewUserProfile.aspx");

        }
        private void GetUserSkills(int id)
        {
            LoggingManager.Debug("Entering GetUserSkills - ViewUserProfile.aspx");

            pnlExpertSkill.Controls.Clear();

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<UserSkill> userexpertskills = context.UserSkills.Where(l => l.UserId == id && l.SkillCategory == (int)Common.SkillCategory.Expert).ToList();
                List<UserSkill> usergoodskills = context.UserSkills.Where(l => l.UserId == id && l.SkillCategory == (int)Common.SkillCategory.Good).ToList();
                List<UserSkill> userstrongskills = context.UserSkills.Where(l => l.UserId == id && l.SkillCategory == (int)Common.SkillCategory.Strong).ToList();
                pnlExpertSkill.Controls.Add(new LiteralControl("<table width='100%'>"));
                PanelStrongSkill.Controls.Add(new LiteralControl("<table width='100%'>"));
                PanelGoodSkill.Controls.Add(new LiteralControl("<table width='100%'>"));
                foreach (var ul in userexpertskills)
                {
                    pnlExpertSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td></tr>"));

                }
                foreach (var ul in usergoodskills)
                {
                    PanelGoodSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td></tr>"));

                }
                foreach (var ul in userstrongskills)
                {
                    PanelStrongSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td></tr>"));

                }
                pnlExpertSkill.Controls.Add(new LiteralControl("</table>"));
                PanelGoodSkill.Controls.Add(new LiteralControl("</table>"));
                PanelStrongSkill.Controls.Add(new LiteralControl("</table>"));


            }
            LoggingManager.Debug("Exiting GetUserSkills - ViewUserProfile.aspx");

        }
        private int LoginUserId
        {
            get
            {
                return Common.GetLoggedInUserId(Session).Value;
            }
        }
        private string GetEmployeSkill(Int32 id)
        {
            LoggingManager.Debug("Entering GetEmployeSkill - ViewUserProfile.aspx");

            string skl = null;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var userskill = context.UserEmploymentSkills.FirstOrDefault(x => x.EmploymentHistoryId == id);
                if (userskill != null)
                {
                    var skill = context.MasterSkills.FirstOrDefault(x => x.Id == userskill.MasterSkillId);
                    if (skill != null)
                    {
                        if (skill.Description.Length > 18)
                        {
                            skl = skill.Description.Substring(0, 14) + "...";
                        }
                        else
                        {
                            skl = skill.Description;
                        }
                    }
                }
                else
                {
                    skl = "";
                }
            }
            LoggingManager.Debug("Exiting GetEmployeSkill - ViewUserProfile.aspx");
            return skl;


        }
        private string GetEmployeLevel(Int32 levelid)
        {
            LoggingManager.Debug("Entering GetEmployeLevel - ViewUserProfile.aspx");

            string level;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                level = context.MasterLevels.FirstOrDefault(x => x.ID == levelid).Description;
                if (level != null)
                {
                    if (level.Length > 18)
                    {
                        level = level.Substring(0, 14) + "...";
                    }

                }
            }
            LoggingManager.Debug("Exiting GetEmployeLevel - VisualCV.aspx");

            return level;
        }
        private string GetEmployeIndustry(Int32 industryid)
        {
            string industry;


            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                industry = context.MasterIndustries.FirstOrDefault(x => x.Id == industryid).Description;
                if (industry != null)
                {
                    if (industry.Length > 18)
                    {
                        industry = industry.Substring(0, 14) + "...";
                    }

                }
            }
            LoggingManager.Debug("Exiting GetEmployeLevel - VisualCV.aspx");



            return industry;
        }

        public static void SendEmail(string subject, StringBuilder body, params string[] toEmails)
        {
            LoggingManager.Debug("Entering SendEmail - ViewUserProfile.aspx");

            var msg = new MailMessage();
            string userName = ConfigurationManager.AppSettings["FromEmail"];
            string password = ConfigurationManager.AppSettings["FromEmailPassword"];
            msg.From = new MailAddress(userName, ConfigurationManager.AppSettings["FromUserName"]);
            foreach (string toEmail in toEmails) msg.To.Add(toEmail);
            msg.Subject = subject;
            msg.Body = body.ToString();
            msg.IsBodyHtml = true;
            string smtpAddress = ConfigurationManager.AppSettings["SMTPAddress"];
            int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
            var smtp = new SmtpClient(smtpAddress, smtpPort) { EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]), Credentials = new NetworkCredential(userName, password) };
            smtp.Send(msg);

            LoggingManager.Debug("Exiting SendEmail - ViewUserProfile.aspx");
        }

        protected void txtSharebyEmail_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering txtSharebyEmail_Click - ViewUserProfile.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = Common.GetLoggedInUser(context);
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                string message = string.Empty;

                const string url1 = "?UserId";
                const string url2 = "VisualCV.aspx";
                const string url3 = "viewuserprofile.aspx";
                const string url4 = "visualcvactivity.aspx";
                if (txtMessage.Text.ToLower().Contains(url1.ToLower()))
                {
                    if (loggedInUserId != null)
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(user.Name)
                            .Append(",has shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text)
                            .AppendLine()
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail(user.Name + " " + "shared a link", body, txtTo.Text);
                    }
                    else
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text)
                            .AppendLine()
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail("Shared a link", body, txtTo.Text);
                    }
                }
                else if (txtMessage.Text.ToLower().Contains(url2.ToLower()) || txtMessage.Text.ToLower().Contains(url3.ToLower()) ||
                    txtMessage.Text.ToLower().Contains(url4.ToLower()))
                {
                    var message1 = txtMessage.Text + "?UserId=" + user.Id;
                    if (loggedInUserId != null)
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(user.Name)
                            .Append(",has shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(message1)
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail(user.Name + " " + "shared a link", body, txtTo.Text);
                    }
                    else
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Shared a link with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(message1)
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail("Shared a link", body, txtTo.Text);
                    }
                }
                else
                {
                    if (loggedInUserId != null)
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(user.Name)
                            .Append(",has shared a message with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text)
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail(user.Name + " " + "shared a link", body, txtTo.Text);
                    }
                    else
                    {
                        StringBuilder body = new StringBuilder()
                            .Append("Hi, ")
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Shared a message with you in huntable")
                            .AppendLine()
                            .Append("<br/>")
                            .Append(txtMessage.Text)
                            .AppendLine()
                            .Append("<br/>")
                            .Append("Hope you like it")
                            .AppendLine();
                        SendEmail("Shared a link", body, txtTo.Text);
                    }
                }

            }
            LoggingManager.Debug("Exiting txtSharebyEmail_Click - ViewUserProfile.aspx");
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnShare_Click - ViewUserProfile.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            if (loggedInUserId != null)
            {
                var socialManager = new SocialShareManager();
                txtShareMessage.Text = txtShareMessage.Text;
                if (chkTwitter.Checked)
                {
                    socialManager.ShareOnTwitter(LoginUserId, txtShareMessage.Text);
                }
                if (chkFacebook.Checked)
                {
                    var text = txtShareMessage.Text.Trim();
                    var result = text.Split(' ');
                    var link = result[0];
                    var context = new huntableEntities();
                    var user = context.Users.FirstOrDefault(u => u.Id == LoginUserId);
                    if (user != null && user.PersonalLogoFileStoreId != null)
                    {
                        socialManager.ShareLinkOnFacebook(LoginUserId, "", "[UserName] has shared a link in Huntable", "", "", link,
                                                          "http://huntable.co.uk/loadfile.ashx?id=" + user.PersonalLogoFileStoreId);
                    }
                    else
                        socialManager.ShareLinkOnFacebook(LoginUserId, "", "[UserName] has shared a link in Huntable", "", "", link, "");
                }
                if (chkLinkedIn.Checked)
                {
                    socialManager.ShareOnLinkedIn(LoginUserId, txtShareMessage.Text, "");
                }
            }
            else
            {
                int count = 1;
                string strl = "http://www.linkedin.com/shareArticle?mini=true&url=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
                string str = "https://twitter.com/share?url=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
                string strf = "http://www.facebook.com/sharer.php?u=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
                if (chkTwitter.Checked)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + str + "','blank' + new Date().getTime(),'menubar=no') </script>");
                count = count + 1;

                if (chkFacebook.Checked)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strf + "','blank' + new Date().getTime(),'menubar=no') </script>");
                count = count + 1;

                if (chkLinkedIn.Checked)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strl + "','blank' + new Date().getTime(),'menubar=no') </script>");

            }
            LoggingManager.Debug("Exiting btnShare_Click - ViewUserProfile.aspx");
        }
        private void CheckSocialShare(string provider)
        {
            LoggingManager.Debug("Entering CheckSocialShare - ViewUserProfile.aspx");
            var user = Common.GetLoggedInUser();
            if (user == null) return;
            var check = user.OAuthTokens.Any(o => o.Provider == provider);
            if (check) return;
            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var callbackuri = baseUrl + "oauth.aspx";
            Session["oauthmode"] = "socialshare";
            OAuthWebSecurity.RequestAuthentication(provider, callbackuri);
            LoggingManager.Debug("Exiting CheckSocialShare - ViewUserProfile.aspx");
        }
        protected void chkFacebook_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkFacebook_CheckedChanged - ViewUserProfile.aspx");
            if (chkFacebook.Checked)
                CheckSocialShare("facebook");
            LoggingManager.Debug("Exiting chkFacebook_CheckedChanged - ViewUserProfile.aspx");
        }

        protected void chkLinkedIn_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkLinkedIn_CheckedChanged - ViewUserProfile.aspx");
            if (chkLinkedIn.Checked)
                CheckSocialShare("linkedin");
            LoggingManager.Debug("Exiting chkLinkedIn_CheckedChanged - ViewUserProfile.aspx");
        }

        protected void chkTwitter_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkTwitter_CheckedChanged - ViewUserProfile.aspx");
            if (chkTwitter.Checked)
                CheckSocialShare("twitter");
            LoggingManager.Debug("Exiting chkTwitter_CheckedChanged - ViewUserProfile.aspx");
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