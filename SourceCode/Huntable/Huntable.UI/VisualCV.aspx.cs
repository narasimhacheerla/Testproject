using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using OAuthUtility;
using Snovaspace.Util.Logging;
using System.Net;
using Snovaspace.Util;
using System.Text;
using Snovaspace.Util.FileDataStore;
using System.Web.UI.DataVisualization.Charting;


namespace Huntable.UI
{

    public partial class VisualCV : System.Web.UI.Page
    {
        readonly StringBuilder strInterests = new StringBuilder();
        List<int> w = new List<int>();
        List<int> z = new List<int>();
        List<int> y = new List<int>();
        List<string> s = new List<string>();
        private int? OtherUserId
        {
            get
            {
                LoggingManager.Debug("Entering OtherUserId - VisualCV.aspx");
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                if (Page.RouteData.Values["ID"] != null && (Page.RouteData.Values["ID"]).ToString() != "ShareMail.aspx" && (Page.RouteData.Values["ID"]).ToString() != "ChartImg.axd")
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
                }

                LoggingManager.Debug("Exiting OtherUserId - VisualCV.aspx"); 
                
                return null;
            }
        }

        public string CalculateChatAuthHash()
        {
            var loginUser = Common.GetLoggedInUser();
            if (OtherUserId != null && loginUser != null)
            {
                var result = Common.GetChatUrl(loginUser.Id.ToString(), OtherUserId.Value.ToString(), loginUser.Name,loginUser.UserProfilePictureDisplayUrl);
                return result;
            }
            return null;
        }

        private bool? IsOtherUserBlocked
        {
            get
            {
                LoggingManager.Debug("Entering IsOtherUserBlocked - VisualCV.aspx");
                if (OtherUserId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var loggedInUserId = Common.GetLoggedInUserId(Session);
                        var blockedlist =
                            context.UserBlockLists.FirstOrDefault(
                                e => e.UserId == loggedInUserId && e.BlockedUserId == OtherUserId.Value);
                        var endorseBlock =
                            context.UserEndorseLists.FirstOrDefault(
                                e => e.EndorsedUserId == loggedInUserId && e.UserId == OtherUserId);
                        if (endorseBlock != null || OtherUserId.Value == loggedInUserId)
                        {
                            divEndorseBLock.Visible = false;

                        }
                        if (blockedlist != null)
                        {

                            divunblock.Visible = true;
                            partialunblock.Visible = false;
                        }
                        var blockedUser =
                            context.UserBlockLists.FirstOrDefault(
                                e => e.UserId == OtherUserId.Value && e.BlockedUserId == loggedInUserId);
                        if (blockedUser != null)
                        {
                            divOnlineNoewChatWithMe.Visible = false;
                            RequestEndorsements.Visible = false;
                            //ucMessage.Visible = false;
                            p_msg_like.Attributes.Add("style", "display:none;");
                            //endorsecomplete.Visible = false;

                        }
                    }

                }
                LoggingManager.Debug("Exiting IsOtherUserBlocked - VisualCV.aspx");
                return null;
            }

        }

        private int? UserId
        {
            get
            {
                LoggingManager.Debug("Entering UserId - VisualCV.aspx");
                int userId;
                if (int.TryParse(Request.QueryString["UserId"], out userId))
                {
                    return userId;
                }
                else if (Page.RouteData.Values["ID"] != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
                }
              
                else
                {
                    var sessionid = Session[SessionNames.LoggedInUserId];
                    if (sessionid != null)
                        if (int.TryParse(Session[SessionNames.LoggedInUserId].ToString(), out userId))
                        {
                            return userId;
                        }
                }
                LoggingManager.Debug("Exiting UserId - VisualCV.aspx");
                return null;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering Page_load - VisualCV.aspx");
                if (!IsPostBack)
                {
                    LoadProfile();
                    hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
                    var loginUserId = Common.GetLoggedInUserId(Session);
                    if (FeedManager.GetIfProfileLiked(loginUserId ?? 0, OtherUserId ?? loginUserId ?? 0, FeedManager.FeedType.Like_User_Profile.ToString()))
                    {
                        hypLikeProfile.HRef = "javascript:" + "MarkDirectUnlike(0, '" + FeedManager.FeedType.Like_User_Profile.ToString() + "', " + (OtherUserId ?? loginUserId ?? 0).ToString() + ")";
                        hypLikeProfile.InnerHtml = "liked my profile";
                    }
                    else
                    {
                        hypLikeProfile.HRef = "javascript:" + "MarkDirectLike(0, '" + FeedManager.FeedType.Like_User_Profile.ToString() + "', " + (OtherUserId ?? loginUserId ?? 0).ToString() + ")";
                        hypLikeProfile.InnerHtml = "like my profile";
                    }

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            if (hdnUserId.Value == "")
            {
                divBLock.Visible = false;
                sssd.Visible = false;
                a_chat.Visible = false;
                onlineinfo.Visible = false;
                userOnline.Visible = false;
                flwr.Visible = false;
                flwngr.Visible = false;
                //p_msg_like.Visible = false;
                //ucMessage.Visible = false;
                p_msg_like.Attributes.Add("style", "display:none;");
            }
            if (!OtherUserId.HasValue)
            {
                divBLock.Visible = false;

            }
            LoggingManager.Debug("Exiting Page_Load -VisualCV.aspx");
        }
        private void LoagUserProfileVisited(huntableEntities context, int userId, int? loginUserId)
        {
            LoggingManager.Debug("Entering LoagUserProfileVisited - VisualCV.aspx");
            DateTime dt = Convert.ToDateTime(Session["Datetm"].ToString());
            var profileVisitedHistory = new UserProfileVisitedHistory
            {
                
                UserId = userId,
                VisitorUserId = loginUserId,
                IPAddress = GetIpAddress(),
                Date = dt
            };
            context.UserProfileVisitedHistories.AddObject(profileVisitedHistory);
            context.SaveChanges();
            LoggingManager.Debug("Exiting LoagUserProfileVisited - VisualCV.aspx");
        }
        private string GetIpAddress()
        {
            LoggingManager.Debug("Entering GetIpAddress - VisualCV.aspx");

            string strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                                  Request.ServerVariables["REMOTE_ADDR"];

            LoggingManager.Debug("Exiting GetIpAddress - VisualCV.aspx");
            return strIpAddress;
        }

        protected void BtnRequestEndorseClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnRequestEndorseClick - VisualCV.aspx");
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
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Endorsement request sent succesfully')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnRequestEndorseClick - VisualCV.aspx");
        }

        private void LoadProfile()
        {
            try
            {
                LoggingManager.Debug("Entering LoadProfile - VisualCV.aspx");
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var loginUserId = Common.GetLoggedInUserId(Session);
                    User user = null;
                    if (OtherUserId.HasValue)
                    {
                        hdnOtherUserId.Value = OtherUserId.Value.ToString();
                        
                        var userp = context.Users.FirstOrDefault(x => x.Id == OtherUserId.Value);

                        showProfileImage12.HRef = userp.UserProfilePictureDisplayUrl;
                        vc.HRef = new UrlGenerator().UserUrlGenerator( OtherUserId.Value);
                        vt.HRef = new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value);
                        vca.HRef = new UrlGenerator().UserActivityUrlGenerator(OtherUserId.Value);
                        user = context.Users.First(u => u.Id == OtherUserId.Value);
                        // Logging the profile visited activity.
                        if (loginUserId.HasValue)
                        {
                            LoagUserProfileVisited(context, OtherUserId.Value, loginUserId);
                            var following =
                                context.PreferredFeedUserUsers.FirstOrDefault(
                                    x => x.UserId == OtherUserId.Value && x.FollowingUserId == loginUserId.Value);
                            if (following != null)
                            {
                                flwngr.Visible = true;
                                flwr.Visible = false;
                            }
                            else
                            {
                                flwngr.Visible = false;
                                flwr.Visible = true;
                            }
                        }

                        if (user.LastActivityDate.HasValue && user.LastActivityDate.Value > DateTime.Now.AddMinutes(-ConfigurationManagerHelper.GetAppsettingByKey<int>(Constants.LastActivityMinutesKey)))
                        {
                            userOnline.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOnlineImagePathKey);
                            onlineinfo.Text = string.Format("Online Now");

                        }
                        else
                        {
                            userOnline.ImageUrl = ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOfflineImagePathKey);
                            onlineinfo.Text = string.Format("Offline");

                        }

                        if (loginUserId.HasValue && loginUserId.Value == OtherUserId.Value)
                        {
                            divOnlineNoewChatWithMe.Visible = false;
                            //divLikeMyProfile.Visible = false;
                            divLikeMyProfile.Attributes.Add("style", "display:none;");
                            flwr.Visible = false;
                            flwngr.Visible = false;
                            userOnline.Visible = false;
                            onlineinfo.Visible = false;
                            skilladj.Attributes.Add("style", "margin-top:-97px;");
                            txtShareMessage.Text = "https://huntable.co.uk/" +new UrlGenerator().UserUrlGenerator(loginUserId.Value);
                            txtMessage.Text = "https://huntable.co.uk/"+new UrlGenerator().UserUrlGenerator(loginUserId.Value);
                            fe_text.Text = "https://huntable.co.uk/"+new UrlGenerator().UserUrlGenerator(loginUserId.Value);
                        }
                        txtShareMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserUrlGenerator(OtherUserId.Value);
                        txtMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserUrlGenerator(OtherUserId.Value);
                        fe_text.Text = "https://huntable.co.uk/" + new UrlGenerator().UserUrlGenerator(OtherUserId.Value);
                        var cmpnm = from a in context.EmploymentHistories
                                    join b in context.MasterCompanies on a.CompanyId equals b.Id
                                    where a.UserId == OtherUserId.Value && a.IsDeleted == null
                                    select new
                                    {
                                        a.Id,
                                        b.Description
                                    };
                        var cmpnyusr = context.Companies.FirstOrDefault(x => x.Userid == loginUserId);
                        if (cmpnyusr == null)
                        {
                            var cmpnmu = from a in context.EmploymentHistories
                                         join b in context.MasterCompanies on a.CompanyId equals b.Id
                                         where a.UserId == loginUserId && a.IsDeleted == null
                                         select new
                                             {
                                                 a.Id,
                                                 b.Description
                                             };
                            ddljobtitle.DataSource = cmpnm;
                            ddljobtitle.DataTextField = "Description";
                            ddljobtitle.DataValueField = "Id";
                            ddljobtitle.DataBind();
                            ddljob.DataSource = cmpnmu;
                            ddljob.DataTextField = "Description";
                            ddljob.DataValueField = "Id";
                            ddljob.DataBind();
                        }
                    }
                    else
                    {
                        skilladj.Attributes.Add("style", "margin-top:-97px;");
                        txtShareMessage.Text = "https://huntable.co.uk/" + new UrlGenerator().UserUrlGenerator(loginUserId.Value);
                        txtMessage.Text = "https://huntable.co.uk/"+ new UrlGenerator().UserUrlGenerator(loginUserId.Value);
                        fe_text.Text ="https://huntable.co.uk/"+ new UrlGenerator().UserUrlGenerator(loginUserId.Value);
                        
                        vc.HRef = "visualcv.aspx";
                        vt.HRef = "viewuserprofile.aspx";
                        vca.HRef = "visualcvactivity.aspx";
                       // divLikeMyProfile.Visible = false;
                        divLikeMyProfile.Attributes.Add("style", "display:none;");
                        skilladj.Attributes.Add("style", "margin-top:-97px;");
                        if (loginUserId.HasValue)
                        {
                            user = context.Users.First(u => u.Id == loginUserId.Value);
                            showProfileImage12.HRef = user.UserProfilePictureDisplayUrl;
                        }
                        flwr.Visible = false;
                        flwngr.Visible = false;
                     
                    }

                    if (user != null)
                    {
                        CancelButton.HRef = "visualcv.aspx?UserId=" + user.Id;
                        DisplayUserDetails(user);
                        DisplaySkills(user);
                        DisplayPositionLookingFor(user);
                        DisplayLanguages(user);
                        DisplayInterests(user);
                        PlotGrowthChart(user);

                    }                
                   
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadProfile - VisualCV.aspx");

        }        
        private class Growth
        {
            public int Id { set; get; }
            public int Rank { set; get; }
            public string Description { set; get; }
            public DateTime FromDate { set; get; }
            public DateTime ToDate { set; get; }
        }
        protected void BtnUnblockClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnUnblockClick - VisualCV.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var resul = context.UserBlockLists.FirstOrDefault(x => x.UserId == loggedInUserId && x.BlockedUserId == OtherUserId.Value);
                //var result = context.UserBlockLists.FirstOrDefault(u => u.UserId == loggedInUserId && u.BlockedUserId == OtherUserId.Value);
                context.UserBlockLists.DeleteObject(resul);
                context.SaveChanges();
            }
            //divunblock.Visible = false;
            partialunblock.Visible = true;
            divunblock.Visible = false;
            divBLock.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay(' Unblocked successfully')", true);
            LoggingManager.Debug("Exiting BtnUnblockClick - Viewprofile.aspx");

        }
        protected void BtnBlockUserClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnBlockUserClick - VisualCV.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var objMessageManager = new UserMessageManager();
                    if (OtherUserId != null)
                        objMessageManager.BlockUser(context, Convert.ToInt32(Common.GetLoggedInUserId(Session)), OtherUserId.Value);
                    divBLock.Visible = false;
                    divunblock.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Blocked successfully')", true);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnBlockUserClick - VisualCV.aspx");
        }
        private void PlotGrowthChart(User user)
        {
            try
            {

                LoggingManager.Debug("Entering PlotGrowthChart - VisualCV.aspx");
                var employeeGrowth = user.EmploymentHistories.Where(emp => emp.LevelId != null && emp.MasterYear != null && emp.MasterMonth != null && (emp.IsDeleted==false||emp.IsDeleted==null)).OrderBy(emp => Convert.ToInt16(emp.MasterYear.Description)).ThenBy(emp => emp.MasterLevel.Rank).Select(emp => new Growth { Id = emp.Id, Rank = emp.MasterLevel.Rank, Description = emp.MasterLevel.Description, FromDate = new DateTime(Convert.ToInt16(emp.MasterYear1.Description), Convert.ToInt16(emp.FromMonthID), 1), ToDate = new DateTime(Convert.ToInt16(emp.MasterYear.Description), Convert.ToInt16(emp.ToMonthID), 1) }).ToList();
                var empMinYear = user.EmploymentHistories.Where(emp => emp.LevelId != null && emp.MasterYear1 != null && emp.MasterMonth1 != null &&(emp.IsDeleted==false||emp.IsDeleted==null)).Min(emp => Convert.ToInt16(emp.MasterYear1.Description));
                var empMaxYear = user.EmploymentHistories.Where(emp => emp.LevelId != null && emp.MasterYear != null && emp.MasterMonth != null &&(emp.IsDeleted==false||emp.IsDeleted==null)).Max(emp => Convert.ToInt16(emp.MasterYear.Description));

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    foreach (var l in context.MasterLevels.Where(x=>x.ID<=8))
                    {
                        if (!employeeGrowth.Select(e => e.Description).ToList().Contains(l.Description))
                        {
                            employeeGrowth.Add(new Growth { Id = 0, Rank = l.Rank, Description = l.Description, ToDate = new DateTime(empMinYear, 1, 1), FromDate = new DateTime(empMinYear, 1, 1) });
                        }
                    }
                }
                int intRowCtr = 0;
                Series srChart = new Series();
                srChart.ChartType = SeriesChartType.RangeBar;
                srChart.YValuesPerPoint = 2;
                srChart["DrawingSideBySide"] = "false";

                IList<Color> colors = new List<Color>();

                colors.Add(Color.Black);
                colors.Add(Color.Blue);
                colors.Add(Color.BlueViolet);
                colors.Add(Color.Chartreuse);
                colors.Add(Color.Chocolate);
                colors.Add(Color.DarkGoldenrod);
                colors.Add(Color.DarkOliveGreen);
                colors.Add(Color.DarkOrchid);
                colors.Add(Color.DarkRed);
                colors.Add(Color.DarkSlateBlue);
                colors.Add(Color.DeepSkyBlue);
                colors.Add(Color.Firebrick);
                colors.Add(Color.ForestGreen);
                colors.Add(Color.Gold);
                colors.Add(Color.Gray);

                foreach (var cr in employeeGrowth.OrderBy(e => e.Rank))
                {
                    intRowCtr++;
                    srChart.Points.AddXY(cr.Rank, cr.FromDate, cr.ToDate);
                    srChart.Points[intRowCtr - 1].AxisLabel = cr.Description;
                    srChart.Points[intRowCtr - 1].Color = colors[intRowCtr - 1];
                }

                chartGrowth.Series.Add(srChart);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting PlotGrowthChart - VisualCV.aspx");
        }

        private void DisplayInterests(User user)
        {
            try
            {
                LoggingManager.Debug("Entering DisplayInterests - VisualCV.aspx");

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    IEnumerable<UserInterest> userInterest = context.UserInterests.Where(l => l.UserId == user.Id);
                    if (userInterest.Any())
                    {
                        var i = 0;
                        foreach (var ul in userInterest)
                        {
                            i++;
                            strInterests.Append("<span><img height='38' width='40' alt='Interest' src='https://huntable.co.uk/images/interests/" + ul.MasterInterest.Description + ".png' /><br />" + ul.MasterInterest.Description + "</span>");
                            if (i % 5 == 0)
                            {
                                strInterests.Append("<br />");
                            }
                            //strInterests.Append("<li class='jcarousel-item jcarousel-item-horizontal jcarousel-item-" + i.ToString() + " jcarousel-item-" + i.ToString() + "-horizontal' style='float: left; list-style: none;' jcarouselindex='" + i.ToString() + "'><img src='images/interests/" + ul.MasterInterest.Description + ".png' width='40' height='38' alt='Interest' /><br/>" + ul.MasterInterest.Description + "</li>");
                        }

                    }
                    else
                    {
                        strInterests.Append("<span>No data to display.</span>");
                    }
                    pInterest.InnerHtml = strInterests.ToString();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DisplayInterests - VisualCV.aspx");
        }

        private void DisplayLanguages(User user)
        {
            try
            {
                LoggingManager.Debug("Entering DisplayLanguages - VisualCV.aspx");
                var strLanguages = new StringBuilder();
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    strLanguages.Append("<table class='language-known-cv'>");
                    IEnumerable<UserLanguage> userlanguages = context.UserLanguages.Where(l => l.UserId == user.Id);
                    if (userlanguages.Any())
                    {
                        foreach (var ul in userlanguages)
                        {
                            strLanguages.Append("<tr><td width='43%'>" + ul.MasterLanguage.Description + "</td><td width='5%'>:</td><td width='52%'><img src='https://huntable.co.uk/images/rating-star" + ul.Level + ".png' /></td></tr>");
                        }
                    }
                    else
                    {
                        strLanguages.Append("<tr><td>No data to display.</td></tr>");
                    }
                    strLanguages.Append("</table>");
                    divLanguages.InnerHtml = strLanguages.ToString();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DisplayLanguages - VisualCV.aspx");
        }

        private void DisplayPositionLookingFor(User user)
        {
            try
            {
                LoggingManager.Debug("Entering DisplayPositionLookingFor - VisualCV.aspx");

                if (user.ShowPositionLookingFor == true)
                {
                    divPositionLookingFor.InnerText = user.PositionLookingFor;
                    lnkHidePosittionLookingFor1.Text = "Hide";
                }
                else
                {
                    divPositionLookingFor.Visible = false;
                    lnkHidePosittionLookingFor1.Text = "Show";
                }
                SetBlockUserButttonText();
                if (user.ShowExpectedSalary == true)
                {

                }

                else
                {
                    divExpectedSalary.Visible = false;
                    lnkHideExpectedSalary1.Text = "Show";
                }
                using (var con = huntableEntities.GetEntitiesWithNoLock())
                {
                    int usrid = user.Id;
                    var empsal = con.PreferredJobUserSalaries.Where(x => x.UserId == usrid).ToList().LastOrDefault();
                    if (empsal != null)
                    {
                        int? minsal = empsal.MinSalary;
                        int? maxsal = empsal.MaxSalary;
                        int? curencyid = empsal.CurrencyTypeId;
                        var masterMaximumSalary = con.MasterMaximumSalaries.FirstOrDefault(x => x.Id == maxsal);
                        if (masterMaximumSalary != null)
                        {
                            int? mxsal = masterMaximumSalary.MaximumSalary;
                            var masterMinimumSalary = con.MasterMinimumSalaries.FirstOrDefault(x => x.Id == minsal);
                            if (masterMinimumSalary != null)
                            {
                                int? mnsal = masterMinimumSalary.MinimumSalary;
                                var masterCurrencyType = con.MasterCurrencyTypes.FirstOrDefault(x => x.ID == curencyid);
                                if (masterCurrencyType != null)
                                {
                                    string currencytype = masterCurrencyType.Description;
                                    divExpectedSalary.InnerText = mnsal.ToString() + "-" + mxsal.ToString() + " " + currencytype;
                                }
                            }
                        }
                    }
                    lnkHideExpectedSalary1.Text = "Hide";
                }
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                if (OtherUserId != null && loggedInUserId != (int)OtherUserId)
                {
                    lnkHideExpectedSalary1.Visible = false;
                    lnkHidePosittionLookingFor1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DisplayPositionLookingFor - VisualCV.aspx");
        }

        private void DisplaySkills(User user)
        {
            try
            {
                LoggingManager.Debug("Entering DisplaySkills - VisualCV.aspx");
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    IEnumerable<UserSkill> userSkills = context.UserSkills.Where(l => l.UserId == user.Id);
                    var strExpertSkills = new StringBuilder();
                    var strStrongSkills = new StringBuilder();
                    var strGoodSkills = new StringBuilder();

                    foreach (var ul in userSkills)
                    {
                        if (ul.SkillCategory == (int)Common.SkillCategory.Expert)
                        {
                            if (ul.HowLong == "1")
                            {
                                strExpertSkills.Append("<tr><td width='200px'>" + ul.MasterSkill.Description + "</td><td width='70px' align='right'>" + ul.HowLong + " Year</td></tr>");
                            }
                            else
                            {
                                strExpertSkills.Append("<tr><td width='200px'>" + ul.MasterSkill.Description + "</td><td width='70px' align='right'>" + ul.HowLong + " Years</td></tr>");
                            }
                        }
                        if (ul.SkillCategory == (int)Common.SkillCategory.Strong)
                        {
                            if (ul.HowLong == "1")
                            {
                                strStrongSkills.Append("<tr><td width='200px'>" + ul.MasterSkill.Description + "</td><td width='70px' align='right'>" + ul.HowLong + " Year</td></tr>");
                            }
                            else
                            {
                                strStrongSkills.Append("<tr><td width='200px'>" + ul.MasterSkill.Description + "</td><td width='70px' align='right'>" + ul.HowLong + " Years</td></tr>");
                            }
                        }
                        if (ul.SkillCategory == (int)Common.SkillCategory.Good)
                        {
                            if (ul.HowLong == "1")
                            {
                                strGoodSkills.Append("<tr><td width='200px'>" + ul.MasterSkill.Description + "</td><td width='70px' align='right'>" + ul.HowLong + " Year</td></tr>");
                            }
                            else
                            {
                                strGoodSkills.Append("<tr><td width='200px'>" + ul.MasterSkill.Description + "</td><td width='70px' align='right'>" + ul.HowLong + " Years</td></tr>");
                            }
                        }
                    }
                    if (strExpertSkills.Length > 0)
                    {
                        divExpertSkills.InnerHtml = "<table>" + strExpertSkills + "</table>";
                    }
                    else
                    {
                        divExpertSkills.InnerHtml = "<table><tr><td width=270px'>User yet to add skill details.</td></tr></table>";
                    }
                    if (strStrongSkills.Length > 0)
                    {
                        divStrongSkills.InnerHtml = "<table>" + strStrongSkills + "</table>";
                    }
                    else
                    {
                        divStrongSkills.InnerHtml = "<table><tr><td width=270px'>User yet to add skill details.</td></tr></table>";
                    }
                    if (strGoodSkills.Length > 0)
                    {
                        divGoodSkills.InnerHtml = "<table>" + strGoodSkills + "</table>";
                    }
                    else
                    {
                        divGoodSkills.InnerHtml = "<table><tr><td width=270px'>User yet to add skill details.</td></tr></table>";
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Existing DisplaySkills - VisualCV.aspx");
        }

        protected void HidePosittionLookingFor(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering HidePosittionLookingFor - VisualCV.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                User user = null;
                if (OtherUserId.HasValue)
                {
                    user = context.Users.First(u => u.Id == OtherUserId.Value);
                }
                else
                {
                    if (loginUserId.HasValue)
                    {
                        user = context.Users.First(u => u.Id == loginUserId.Value);
                    }
                }
                if (user != null)
                {
                    if (lnkHidePosittionLookingFor1.Text == "Hide")
                    {
                        user.ShowPositionLookingFor = false;
                        divPositionLookingFor.Visible = false;
                        context.SaveChanges();
                        lnkHidePosittionLookingFor1.Text = "Show";
                        return;
                    }
                    if (lnkHidePosittionLookingFor1.Text == "Show")
                    {
                        user.ShowPositionLookingFor = true;
                        divPositionLookingFor.Visible = true;
                        context.SaveChanges();
                        lnkHidePosittionLookingFor1.Text = "Hide";
                        return;
                    }

                }
            }
            LoggingManager.Debug("Exiting HidePosittionLookingFor - VisualCV.aspx");
        }

        protected void HideExpectedSalary(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering HideExpectedSalary - VisualCV.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                User user = null;
                if (OtherUserId.HasValue)
                {
                    user = context.Users.First(u => u.Id == OtherUserId.Value);
                }
                else
                {
                    if (loginUserId.HasValue)
                    {
                        user = context.Users.First(u => u.Id == loginUserId.Value);
                    }
                }
                if (user != null)
                {
                    if (lnkHideExpectedSalary1.Text == "Hide")
                    {
                        user.ShowExpectedSalary = false;
                        divExpectedSalary.Visible = false;
                        context.SaveChanges();
                        lnkHideExpectedSalary1.Text = "Show";
                        return;
                    }
                    if (lnkHideExpectedSalary1.Text == "Show")
                    {
                        user.ShowExpectedSalary = true;
                        divExpectedSalary.Visible = true;
                        context.SaveChanges();
                        lnkHideExpectedSalary1.Text = "Hide";
                        return;
                    }
                }
            }
            LoggingManager.Debug("Exiting HideExpectedSalary - VisualCV.aspx");
        }
        private void SetBlockUserButttonText()
        {
            LoggingManager.Debug("Entering SetBlockUserButttonText - VisualCV.aspx");

            if (IsOtherUserBlocked.HasValue && IsOtherUserBlocked.Value)
            {
                btnBlockUser.Text = "Unblock Messages from this user";
            }
            else
            {
                btnBlockUser.Text = "Block Messages from this user";
            }
            LoggingManager.Debug("Exiting SetBlockUserButttonText - VisualCV.aspx");


        }
        private void DisplayUserDetails(User user)
        {
            try
            {
                LoggingManager.Debug("Entering DisplayUserDetails - VisualCV.aspx");

                imgProfile.Src = user.UserProfilePictureDisplayUrl;
                
                lblName.Text = user.Name;
               imgHasCompany.Visible=  user.UserHasCompany;
                if (user.Summary != null)
                {

                    if (user.Summary.Length > 400)
                    {
                        lblAboutMyself.Text = user.Summary.Replace("\n", "<br/>").Substring(0, 400);
                        lnkReadMoreAboutMyself.Visible = true;
                        lblabt.Text = user.Summary.Replace("\n", "<br/>");

                    }
                    else
                    {
                        lblAboutMyself.Text = user.Summary.Replace("\n", "<br/>");
                        ;
                        lnkReadMoreAboutMyself.Visible = false;
                    }
                }
                var currentEmployment = user.EmploymentHistories.FirstOrDefault(emp => emp.IsCurrent && (emp.IsDeleted==null || emp.IsDeleted==false));
                if (currentEmployment != null)
                {
                    LoggingManager.Info("Inside Current Employment");
                    lblCurrentRole.Text = lblCurrentRoleProfileSection.Text = string.Format("{0}", currentEmployment.JobTitle);
                    lblWorkingAt.Text = currentEmployment.MasterCompany != null ? currentEmployment.MasterCompany.Description : string.Empty;
                   
                  

                }
                lblLivein.Text = user.City + (string.IsNullOrEmpty(user.CountryName) ? "" : ", " + user.CountryName);//LocationToDisplay
                lblExperience.Text = user.TotalExperienceInYears.ToString() + " years";

                rptrExperience.DataSource = user.EmploymentHistories.Where(emp => emp.MasterYear1 != null && ((emp.IsDeleted == null || emp.IsDeleted == false))).Select(emp => emp.MasterYear1 != null ? new { emp.Id, emp.JobTitle, Company = emp.MasterCompany != null ? emp.MasterCompany.Description : string.Empty, Period = GetEmploymentPeriod(emp), emp.Description, Skill = GetEmployeSkill((int)(emp.Id)), Level = GetEmployeLevel((int)(emp.LevelId == null ? 9 : emp.LevelId)), Industry = GetEmployeIndustry((int)(emp.IndustryId == null ? 155 : emp.IndustryId)), Duration = GetEmploymentDuration(emp), fromYear = emp.MasterYear1 == null ? string.Empty : emp.MasterYear1.Description, fromMonth = emp.MasterMonth1 == null ? string.Empty : emp.MasterMonth1.ID.ToString() } : null).OrderByDescending(emp => emp == null ? string.Empty : emp.fromYear).ThenByDescending(emp => emp == null ? string.Empty : emp.fromMonth);
                rptrExperience.DataBind();
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    var employementid = context.EmploymentHistories.Where(x => x.UserId == user.Id && (x.IsDeleted == null || x.IsDeleted == false)).ToList();

                    foreach (var empid in employementid)
                    {
                        w.Add(empid.Id);

                    }
                    for (int i = 0; i < w.Count; i++)
                    {
                        int k = w[i];
                        var vides = context.EmploymentHistoryVideos.Where(x => x.EmplementHistoryId == k).ToList();
                        foreach (var vid in vides)
                        {
                            if (vid.VideoURL != "")
                            {
                                s.Add(vid.VideoURL);
                            }
                        }
                    }
                }
                if (s != null)
                {
                    rptr.DataSource = s;
                    rptr.DataBind();
                    vlbl.Visible = false;
                }
                else
                {
                    vlbl.Visible = true;
                }



            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            lblEndorsedUser.Text = user.Name;//for endorse dialog box
            LoggingManager.Debug("Exiting DisplayUserDetails - VisualCV.aspx");
        }
        protected void UserCompaniesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UserCompaniesClick - VisualCV.aspx");
            int id=0;
            if (OtherUserId.HasValue)
            {

                 id = OtherUserId.Value;
            }
            else if (UserId.HasValue)
            {
                 id = UserId.Value;

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
                    LoggingManager.Debug("Exiting UserCompaniesClick - VisualCV.aspx");
                }
            }
            
            //ScriptManager.RegisterStartupScript(this, GetType(), "Click", "rowaction22();", true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "rowAction22()", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "Companies", "rowAction22();", true);


        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - VisualCV.aspx");
            if (id != null)
            {

                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var result = context.Users.FirstOrDefault(x => x.Id == p);

                    if (result != null)
                    {
                        var photo = result.PersonalLogoFileStoreId;

                        LoggingManager.Debug("Exiting Picture - VisualCV.aspx");
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
        protected string GetMyPictures()
        {

            LoggingManager.Debug("Entering GetMyPictures - VisualCV.aspx");

            var myPictureList = "";
            const string myPicture = "<li class='jcarousel-item-h jcarousel-item-horizontal jcarousel-item-{1} jcarousel-item-{1}-horizontal' style='float: left; list-style: none;' jcarouselindex='{1}'><img src='{0}' width='100' runat='server' id='imgPicture' height='100' alt='portfolio' /></li>";
            var objInvManager = new UserManager();
            //var pictureList = objInvManager.GetPictures(UserId.Value);

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                var employementid = context.EmploymentHistories.Where(x => x.UserId == UserId.Value && (x.IsDeleted == null || x.IsDeleted == false)).ToList();
                int index = 1;
                foreach (var empid in employementid)
                {
                    z.Add(empid.Id);

                }
                foreach (int k in z)
                {
                    int k1 = k;
                    var picures = context.EmploymentHistoryPortfolios.Where(x => x.EmplementHistoryId == k1).ToList();
                    foreach (var pictur in picures)
                    {
                        y.Add(pictur.FileId);
                    }
                }
                foreach (int t in y)
                {
                    myPictureList += string.Format(myPicture, new FileStoreService().GetDownloadUrlDirect(t), index);
                    index++;
                }
            }
            LoggingManager.Debug("Exiting GetMyPictures - VisualCV.aspx");
            return myPictureList;
        }

        private string GetEmploymentPeriod(EmploymentHistory history)
        {
            LoggingManager.Debug("Entering GetEmploymentPeriod - VisualCV.aspx");

            string period = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                period = string.Format("{0} - {1}", history.MasterYear1.Description, history.MasterYear.Description);
            }
            LoggingManager.Debug("Exiting GetEmploymentPeriod - VisualCV.aspx");

            return period;
        }
        private string GetEmploymentDuration(EmploymentHistory history)
        {
            LoggingManager.Debug("Entering GetEmploymentDuration - VisualCV.aspx");

            string duaration = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                var startDate = new DateTime(Convert.ToInt16(history.MasterYear1.Description), Convert.ToInt16(history.MasterMonth1.ID), 1);
                var endDate = new DateTime(Convert.ToInt16(history.MasterYear.Description), Convert.ToInt16(history.MasterMonth.ID), 1);
                var diff = endDate.Subtract(startDate);
                var years = Convert.ToInt16(Math.Floor(diff.Days / 365.25));
                var months = Convert.ToInt16(((diff.Days % 365.25) / 30));
                duaration = string.Format("{0} Years {1} Months", years.ToString(), months.ToString());
            }

            LoggingManager.Debug("Exiting GetEmploymentDuration - VisualCV.aspx");

            return duaration;
        }

        protected void BtnEndorseUserClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnEndorseUserClick - VisualCV.aspx");

            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var loggedInUserId = Common.GetLoggedInUserId(Session);
                    if (OtherUserId != null)
                    {
                        if (loggedInUserId != null)
                        {
                            var tilte = ddljobtitle.SelectedItem;
                            if (tilte != null)
                            {
                                int id = Convert.ToInt32(ddljobtitle.SelectedItem.Value);
                                var endorse = new UserEndorseList { EmpHisId = id, JobTitle = tilte.ToString(), Comments = txtEndorseComment.InnerText, UserId = OtherUserId.Value, EndorsedUserId = loggedInUserId.Value, EndorsedDateTime = DateTime.Now };
                                context.UserEndorseLists.AddObject(endorse);
                                context.SaveChanges();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully endorsed')", true);
                                var socialManager = new SocialShareManager();
                                var user = context.Users.FirstOrDefault(u => u.Id == loggedInUserId);
                                var msg = user.FirstName + " " + user.LastName + " " + "has endorsed" + " " + "[UserName]" +" " + "for" + " " + endorse.JobTitle;
                                socialManager.ShareOnFacebook(OtherUserId.Value, msg, "");
                                FeedManager.addFeedNotification(FeedManager.FeedType.Endorsed, loggedInUserId.Value, id, null);

                            }
                            else
                            {

                                var endorse = new UserEndorseList { Comments = txtEndorseComment.InnerText, UserId = OtherUserId.Value, EndorsedUserId = loggedInUserId.Value, EndorsedDateTime = DateTime.Now };
                                context.UserEndorseLists.AddObject(endorse);
                                context.SaveChanges();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully endorsed')", true);
                                var socialManager = new SocialShareManager();
                                var user = context.Users.FirstOrDefault(u => u.Id == loggedInUserId);
                                var msg = user.FirstName + " " + user.LastName +  " " + "has endorsed" + " " +"[UserName]"+ " " + "for" + " " + endorse.JobTitle;
                                socialManager.ShareOnFacebook(OtherUserId.Value, msg, "");

                            }
                        }
                    }

                }
                Response.Redirect("~/"+new UrlGenerator().UserTextUrlGenerator(OtherUserId.Value));
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnEndorseUserClick - VisualCV.aspx");
        }

        protected void FollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowClick - VisualCV.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (OtherUserId.HasValue)
                {
                    var loginUserId = Common.GetLoggedInUserId(Session);

                    UserManager.FollowUser(loginUserId.Value, OtherUserId.Value);
                    flwr.Visible = false;
                    flwngr.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
                }
            }
            LoggingManager.Debug("Exiting FollowClick - VisualCV.aspx");
        }
        protected void UnfollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnfollowClick - VisualCV.aspx");
            var loginUserId = Common.GetLoggedInUserId(Session);
            var usrmngr = new UserManager();
            if (OtherUserId != null) if (loginUserId != null)
                {
                    usrmngr.Unfollow(OtherUserId.Value, loginUserId.Value);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                } flwr.Visible = true;
            flwngr.Visible = false;
            LoggingManager.Debug("Exiting UnfollowClick - VisualCV.aspx");
        }

        private string GetEmployeSkill(Int32 id)
        {
            LoggingManager.Debug("Entering GetEmployeSkill - VisualCV.aspx");


            string skl = null;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var userskill = context.UserEmploymentSkills.FirstOrDefault(x => x.EmploymentHistoryId == id);
                if (userskill != null)
                {
                    var skill = context.MasterSkills.FirstOrDefault(x => x.Id == userskill.MasterSkillId);
                    if (skill != null)
                    {
                        if (skill.Description.Length > 16)
                        {
                            skl = skill.Description.Substring(0, 12) + "...";
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
            LoggingManager.Debug("Exiting GetEmployeSkill - VisualCV.aspx");
            return skl;

           
        }
        private string GetEmployeLevel(Int32 levelid)
        {
            LoggingManager.Debug("Entering GetEmployeLevel - VisualCV.aspx");

            string level;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                level = context.MasterLevels.FirstOrDefault(x => x.ID == levelid).Description;
                if (level != null)
                {
                    if (level.Length > 16)
                    {
                        level = level.Substring(0, 12) + "...";
                    }
                    else
                    {
                        level = level;
                    }
                }
            }
            LoggingManager.Debug("Exiting GetEmployeLevel - VisualCV.aspx");

            return level;
        }
        private string GetEmployeIndustry(Int32 industryid)
        {
            LoggingManager.Debug("Entering GetEmployeLevel - VisualCV.aspx");
            if (industryid != null)
            {
                string industry;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    industry = context.MasterIndustries.FirstOrDefault(x => x.Id == industryid).Description;
                    if (industry != null)
                    {
                        if (industry.Length > 16)
                        {
                            industry = industry.Substring(0, 12) + "...";
                        }
                        else
                        {
                            industry = industry;
                        }
                    }
                }
                LoggingManager.Debug("Exiting GetEmployeLevel - VisualCV.aspx");

                return industry;
            }
            else
            {
                return "noskilladded";
            }
        }
        public static void SendEmail(string subject, StringBuilder body, params string[] toEmails)
        {
            LoggingManager.Debug("Entering SendEmail -  VisualCV.aspx");

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

            LoggingManager.Debug("Exiting SendEmail -  VisualCV.aspx");
        }

        protected void txtSharebyEmail_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering txtSharebyEmail_Click -  VisualCV.aspx");
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
                            .Append(txtMessage.Text)
                            .AppendLine()
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
                            .Append(user.Name)
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
            LoggingManager.Debug("Exiting txtSharebyEmail_Click -  VisualCV.aspx");
        }
        protected void btnShare_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnShare_Click - VisualCV.aspx");
            var socialManager = new SocialShareManager();
            txtShareMessage.Text = txtShareMessage.Text;
            var loginUserId = Common.GetLoggedInUserId(Session);
            //var text = txtShareMessage.Text.Trim();
            //var result = text.Split(' ');
            //var link = result[0];
            //string strl = "http://www.linkedin.com/shareArticle?mini=true&url=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
            //string str = "https://twitter.com/share?url=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
            //string strf = "http://www.facebook.com/sharer.php?u=" + txtShareMessage.Text + "&summary=" + txtShareMessage.Text;
            //if (chkTwitter.Checked)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + str + "','blank' + new Date().getTime(),'menubar=no') </script>");
            //count = count + 1;
            //if (chkLinkedIn.Checked)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strl + "','blank' + new Date().getTime(),'menubar=no') </script>");
            //count = count + 1;
            //if (chkFacebook.Checked)
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), " popup" + count.ToString(), "<script language=javascript>window.open('" + strf + "','blank' + new Date().getTime(),'menubar=no') </script>");
            if (chkFacebook.Checked)
            {
                var text = txtShareMessage.Text.Trim();
                var result = text.Split(' ');
                var link = result[0];
                var context = new huntableEntities();
                var user = context.Users.FirstOrDefault(u => u.Id == loginUserId);
                if (user != null && user.PersonalLogoFileStoreId != null)
                {                    
                        socialManager.ShareLinkOnFacebook(loginUserId.Value, "", "[UserName] has shared a link in Huntable",
                                                          "", "", link, "http://huntable.co.uk/loadfile.ashx?id=" + user.PersonalLogoFileStoreId);
                }
                else
                {
                    socialManager.ShareLinkOnFacebook(loginUserId.Value, "", "[UserName] has shared a link in Huntable",
                                                          "", "", link, "");
                }
            }
           
            if (chkTwitter.Checked)
            {                
                socialManager.ShareOnTwitter(loginUserId.Value, txtShareMessage.Text);
            }
           
            if (chkLinkedIn.Checked)
            {
                socialManager.ShareOnLinkedIn(loginUserId.Value, txtShareMessage.Text, "");
            }
                       
            LoggingManager.Debug("Exiting btnShare_Click -  VisualCV.aspx");
        }
        protected void chkTwitter_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkTwitter_CheckedChanged - VisualCV.aspx");

            if (chkTwitter.Checked)
                CheckSocialShare("twitter");
            LoggingManager.Debug("Entering chkTwitter_CheckedChanged - VisualCV.aspx");
        }

        protected void chkLinkedIn_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkLinkedIn_CheckedChanged - VisualCV.aspx");
            if (chkLinkedIn.Checked)
                CheckSocialShare("linkedin");
            LoggingManager.Debug("Exiting chkLinkedIn_CheckedChanged - VisualCV.aspx");
        }

        protected void chkFacebook_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkFacebook_CheckedChanged - VisualCV.aspx");
            if (chkFacebook.Checked)
                CheckSocialShare("facebook");
            LoggingManager.Debug("Exiting chkFacebook_CheckedChanged - VisualCV.aspx");
        }

        private void CheckSocialShare(string provider)
        {
            LoggingManager.Debug("Entering CheckSocialShare - VisualCV.aspx");
            var user = Common.GetLoggedInUser();
            if (user == null) return;
            var check = user.OAuthTokens.Any(o => o.Provider == provider);
            if (check) return;
            var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
            var callbackuri = baseUrl + "oauth.aspx";
            Session["oauthmode"] = "socialshare";
            OAuthWebSecurity.RequestAuthentication(provider, callbackuri);
            LoggingManager.Debug("Exiting CheckSocialShare - VisualCV.aspx");
        }
        public string CompanyUrlGenerator(object id)
        {
            LoggingManager.Debug("Entering CompanyUrlGenerator - UserSearchCriteria .aspx");
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return "~/"+new UrlGenerator().CompanyUrlGenerator(userid);
            }
            else
            {
                LoggingManager.Debug("Exiting CompanyUrlGenerator - UserSearchCriteria .aspx");
                return null;
            }

        }
    }
}