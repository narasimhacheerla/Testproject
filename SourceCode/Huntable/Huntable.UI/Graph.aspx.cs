using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using Huntable.Entities.Enums;
using System.Data.Objects;
using Snovaspace.Util;
using Huntable.Business;
using System.Web.UI.HtmlControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class Graph : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering Page_Load - Graph.aspx");

            if (!IsPostBack)
            {
                LoadProfileStatus();
                LoadProfileVisitedHistory(EProfileVisitedFrequencyType.All);
            }
            LoggingManager.Debug("Exiting Page_Load - Graph.aspx");
            
        }
        private void LoadProfileStatus()
        {
            LoggingManager.Debug("Entering LoadProfileStatus - Graph.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? userId = Common.GetLoggedInUserId(Session);
               
              
                int recoCount;
                UserManager.SearchUsers(new Entities.SearchCriteria.UserSearchCriteria { Id = userId }, out recoCount).FirstOrDefault();
               
                scriptLiteral.Text = string.Format("<script type=\"text/javascript\">var histData ={0};</script>", GetProfileVisitedGraphData(context));
            }

            LoggingManager.Debug("Exiting LoadProfileStatus - Graph.aspx");


        }

        private void LoadProfileVisitedHistory(EProfileVisitedFrequencyType profileVisitedFrequencyType)
        {
            LoggingManager.Debug("Entering LoadProfileVisitedHistory - Graph.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? userId = Common.GetLoggedInUserId(Session);
                IQueryable<UserProfileVisitedHistory> profileVisitedHistory = context.UserProfileVisitedHistories.Where(u => u.UserId == userId);
                switch (profileVisitedFrequencyType)
                {
                    case EProfileVisitedFrequencyType.Today:
                        profileVisitedHistory = profileVisitedHistory.Where(u => EntityFunctions.TruncateTime(u.Date) == EntityFunctions.TruncateTime(EntityFunctions.AddDays(DateTime.Now, 1)));
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
                List<User> users = profileVisitedHistory.Where(u => u.VisitorUserId.HasValue).Select(h => h.User1).Distinct().ToList();
               
            }

            LoggingManager.Debug("Exiting LoadProfileVisitedHistory - Graph.aspx");
        }

        private string GetCurrentRole(User user)
        {
            LoggingManager.Debug("Entering GetCurrentRole - Graph.aspx");
            LoggingManager.Debug("Exiting GetCurrentRole - Graph.aspx");
            return user.EmploymentHistories.Where(e => e.IsCurrent == true && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
            
        }

        private string GetProfileVisitedGraphData(huntableEntities contex)
        {
            LoggingManager.Debug("Entering GetProfileVisitedGraphData - Graph.aspx");

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
            LoggingManager.Debug("Exiting GetProfileVisitedGraphData - Graph.aspx");
            
            return "null";
        }
    }
}
