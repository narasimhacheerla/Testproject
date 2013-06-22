using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Huntable.Data;
using Huntable.Entities.Enums;
using System.Data.Objects;
using Snovaspace.Util;
using Huntable.Business;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;
using System.Web.UI.HtmlControls;


namespace Huntable.UI
{
    public partial class ProfileStatusAtGlance : Page
    {
        private int _count=7;
        protected void Page_Load(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering Page_Load - ProfileStatusAtGlance.aspx");

            var objInvManager = new InvitationManager();
             int loogedinuserId = Convert.ToInt32(Common.GetLoggedInUserId());
             phGraph.Controls.Add(new LiteralControl("<iframe src='Graph.aspx' scrolling='no' frameborder='0' width='100%' height='100%'></iframe>"));
            if (!IsPostBack)
            {
               
                if (hfiled.Value == string.Empty) hfiled.Value = "3";
                _count = Convert.ToInt32(hfiled.Value);
                LoadProfileStatus();
                LoadProfileVisitedHistory(EProfileVisitedFrequencyType.LastWeek, _count);
                

                try
                {
                    bool userLoggedIn = Common.IsLoggedIn();
                    if (!IsPostBack)
                    {
                        if (userLoggedIn)
                        {
                            var user = Common.GetLoggedInUser();
                            var userid = Common.GetLoggedInUserId();
                            using (var context = huntableEntities.GetEntitiesWithNoLock())
                            {
                                var company = context.Users.FirstOrDefault(x => x.Id == userid);
                                if (company != null && company.IsCompany == true)
                                {
                                    divYes.Visible = false;
                                }
                            }
                            lblUserName.Text = user.Name;
                            var userList = objInvManager.GetUsercurrentrole(loogedinuserId);
                          
                            imgProfile.ImageUrl = user.UserProfilePictureDisplayUrl;
                            lblposition1.Text = userList.JobTitle;
                            lblcompany1.Text = userList.MasterCompany.Description;
                            if(user.CreatedDateTime!=null)
                            {
                                lblJoinedDate.Text = (user.CreatedDateTime).ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LoggingManager.Error(ex);
                }
            }
            LoggingManager.Debug("Exiting Page_Load - ProfileStatusAtGlance.aspx");
        }
        protected void BtnJobShowCLick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnJobShowCLick - ProfileStatusAtGlance.aspx");

            var loggedInUserId = Common.GetLoggedInUserId();
            if (loggedInUserId != null)
            {
              
                hfiled.Value = (Convert.ToInt32(hfiled.Value) + 10).ToString(CultureInfo.InvariantCulture);
                LoadProfileVisitedHistory(EProfileVisitedFrequencyType.All, Convert.ToInt32(hfiled.Value));
            }
            LoggingManager.Debug("Exiting BtnJobShowCLick - ProfileStatusAtGlance.aspx");
        }
        private void LoadProfileStatus()
        {
            LoggingManager.Debug("Entering LoadProfileStatus - ProfileStatusAtGlance.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? userId = Common.GetLoggedInUserId(Session);
                int recoCount;
                UserManager.SearchUsers(new Entities.SearchCriteria.UserSearchCriteria {Id = userId}, out recoCount).FirstOrDefault();
                
                scriptLiteral.Text = string.Format("<script type=\"text/javascript\">var histData ={0};</script>", GetProfileVisitedGraphData(context));
            }

            LoggingManager.Debug("Exiting LoadProfileStatus - ProfileStatusAtGlance.aspx");
        }

        private string GetProfileVisitedGraphData(huntableEntities contex)
        {
            LoggingManager.Debug("Entering GetProfileVisitedGraphData - ProfileStatusAtGlance.aspx");

            int? userId = Common.GetLoggedInUserId(Session);
            var res = contex.UserProfileVisitedHistories.Where(h => h.UserId == userId).GroupBy(h => EntityFunctions.TruncateTime(h.Date)).OrderBy(h => h.Key);

            string data = string.Empty;
            int recordsCount = 0;
            foreach (var hist in res)
            {
                recordsCount++;
                if (hist.Key != null)
                    data += string.Format("[\"{0}\",{1}],", string.Format("{0}/{1}/{2}", hist.Key.Value.Month, hist.Key.Value.Day, hist.Key.Value.Year), hist.Count());
            }

            if (recordsCount > 2 && data.Length > 0)
            {
                data = data.Substring(0, data.Length - 1);
                return "[" + data + "]";
            }
            LoggingManager.Debug("Exiting GetProfileVisitedGraphData - ProfileStatusAtGlance.aspx");
            
            return "null";
        }

        protected void BtnTodayClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnTodayClick - ProfileStatusAtGlance.aspx");

            LoadProfileVisitedHistory(EProfileVisitedFrequencyType.Today,_count);

            LoggingManager.Debug("Exiting BtnTodayClick - ProfileStatusAtGlance.aspx");
        }

        protected void BtnLastWeekClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnLastWeekClick - ProfileStatusAtGlance.aspx");

            LoadProfileVisitedHistory(EProfileVisitedFrequencyType.LastWeek,_count);

            LoggingManager.Debug("Exiting BtnLastWeekClick - ProfileStatusAtGlance.aspx");
        }

        protected void BtnLatMonthClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnLatMonthClick - ProfileStatusAtGlance.aspx");

            LoadProfileVisitedHistory(EProfileVisitedFrequencyType.LastMonth,_count);

            LoggingManager.Debug("Exiting BtnLatMonthClick - ProfileStatusAtGlance.aspx");
        }

        protected void BtnAllClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnAllClick - ProfileStatusAtGlance.aspx");

            LoadProfileVisitedHistory(EProfileVisitedFrequencyType.All,_count);

            LoggingManager.Debug("Exiting BtnAllClick - ProfileStatusAtGlance.aspx");
        }
        

        private void LoadProfileVisitedHistory(EProfileVisitedFrequencyType profileVisitedFrequencyType,int count)
        {
            LoggingManager.Debug("Entering LoadProfileVisitedHistory - ProfileStatusAtGlance.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? userId = Common.GetLoggedInUserId(Session);
                IQueryable<UserProfileVisitedHistory> profileVisitedHistory = context.UserProfileVisitedHistories.Where(u => u.UserId == userId);
                switch (profileVisitedFrequencyType)
                {
                    case EProfileVisitedFrequencyType.Today:
                        profileVisitedHistory = profileVisitedHistory.Where(u => EntityFunctions.TruncateTime(u.Date) == EntityFunctions.TruncateTime(DateTime.Today)).OrderByDescending(u=>u.Date);
                        break;

                    case EProfileVisitedFrequencyType.LastWeek:
                        profileVisitedHistory = profileVisitedHistory.Where(u => EntityFunctions.TruncateTime(DateTime.Now) >= EntityFunctions.TruncateTime(u.Date)
                            && EntityFunctions.TruncateTime(EntityFunctions.AddDays(EntityFunctions.TruncateTime(DateTime.Now), -7)) <= EntityFunctions.TruncateTime(u.Date));
                        break;

                    case EProfileVisitedFrequencyType.LastMonth:
                        DateTime lastMonthStartDate = DateTime.Now.Date.GetLastMonthStartDate();
                        DateTime lastMonthEnddate = DateTime.Now.Date.GetLastMonthEndDate();
                        profileVisitedHistory = profileVisitedHistory.Where(u => EntityFunctions.TruncateTime(lastMonthStartDate) >= EntityFunctions.TruncateTime(u.Date)
                            && EntityFunctions.TruncateTime(lastMonthEnddate) <= EntityFunctions.TruncateTime(u.Date));
                        break;
                }
                List<User> users = profileVisitedHistory.Where(u => u.VisitorUserId.HasValue).Select(h => h.User1).Distinct().Take(count).ToList();

                rpProfileVisitedHistory.DataSource = users.Select(u => new { ImagePath = u.UserProfilePictureDisplayUrl, u.Name, Role = GetCurrentRole(u), u.Title,Date=GetDateTime(u), u.Id }).OrderByDescending(f => f.Date);
                rpProfileVisitedHistory.DataBind();
            }

            LoggingManager.Debug("Exiting LoadProfileVisitedHistory - ProfileStatusAtGlance.aspx");
        }

        private string GetCurrentRole(User user)
        {
            LoggingManager.Debug("Entering GetCurrentRole - ProfileStatusAtGlance.aspx");
            LoggingManager.Debug("Exiting GetCurrentRole - ProfileStatusAtGlance.aspx");
            return user.EmploymentHistories.Where(e => e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();

        }
        private string GetDateTime(User user)
        {
            LoggingManager.Debug("Entering GetDateTime - ProfileStatusAtGlance.aspx");
            LoggingManager.Debug("Exiting GetDateTime - ProfileStatusAtGlance.aspx");
            int? userId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
             var xyz = context.UserProfileVisitedHistories.Where(e => e.VisitorUserId == user.Id && e.UserId == userId).Select(e => e.Date).ToList();
             xyz.Reverse();
             var xy =  xyz.FirstOrDefault();
             string y = xy.ToString("yyyy/MM/dd HH:mm:ss");
              return y;
            }
        }
        protected void BtnYesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnYesClick - ProfileStatusAtGlance.aspx");

            divYes.Visible = false;

            LoggingManager.Debug("Exiting BtnYesClick - ProfileStatusAtGlance.aspx");
        }
        protected void Itembound(object sender , RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering itembound - ProfileStatusAtGlance.aspx");
            var a1 = (HtmlAnchor)e.Item.FindControl("A1");
            var a2 = (HtmlAnchor)e.Item.FindControl("A2");
            if (e.Item.DataItem != null)
            {
                int strUsername = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Id").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == strUsername);
                    if (usr != null && usr.IsCompany == null)
                    {
                        a1.HRef = new UrlGenerator().UserUrlGenerator(usr.Id);
                        a2.HRef = new UrlGenerator().UserUrlGenerator(usr.Id);
                    }
                    else
                    {
                        var cmpny = context.Companies.FirstOrDefault(x => x.Userid == usr.Id);
                        if (cmpny != null)
                        {
                            a1.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                            a2.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                        }
                    }

                }
            }

            LoggingManager.Debug("Exiting itembound - ProfileStatusAtGlance.aspx");
        }
    }
}