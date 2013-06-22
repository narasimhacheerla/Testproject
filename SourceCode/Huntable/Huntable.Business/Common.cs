using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.Business
{
    public class Common
    {
        public enum SkillCategory
        {
            Expert = 1,
            Strong = 2,
            Good = 3
        }
        public static User GetLoggedInUser()
        {
            return GetLoggedInUser(huntableEntities.GetEntitiesWithNoLock());
        }       

        public static User GetLoggedInUser(huntableEntities huntableEntities)
        {

            LoggingManager.Debug("Entering GetLoggedInUser  - Common.cs");

            int? loggedInId = GetLoggedInUserId();

            LoggingManager.Debug("Exiting GetLoggedInUser  - Common.cs");

            return loggedInId.HasValue ? huntableEntities.Users.FirstOrDefault(x => x.Id == loggedInId) : null;
        }

        public static string GetApplicationBaseUrl()
        {
            LoggingManager.Debug("Entering GetApplicationBaseUrl  - Common.cs");

            //Return variable declaration 
            string appPath = null;

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

            if (appPath != null && !appPath.EndsWith("/"))
                appPath += "/";

            LoggingManager.Debug("Exiting GetApplicationBaseUrl  - Common.cs");

            return appPath;
        }

        public static string GetChatUrl(string userId, string targetId, string name,string thumbUrl)
        {
            var timstamp = DateTime.Now.ToString("yyMMddhhmmss");
            var hash = CalculateChatAuthHash(userId, targetId,timstamp);
            var result = GetApplicationBaseUrl() + "Chat/MessengerWindow.aspx?init=1&id=" + userId + "&target=" +
                     targetId + "&timestamp=" + timstamp + "&hash=" + hash + "&name=" + name+"&thumbUrl="+thumbUrl.Replace("~","");
            return result;
        }

        public static string CalculateChatAuthHash(string userId, string targetId, string timestamp)
        {
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["AuthSecretKey"]))
                throw new Exception("AuthSecretKey must be specified in your web.config file");

            var sha1 = new SHA1Managed();
            var paramBytes = Encoding.UTF8.GetBytes(userId + targetId + timestamp + ConfigurationManager.AppSettings["AuthSecretKey"]);
            var hashBytes = sha1.ComputeHash(paramBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hash;
        }

        public static bool IsLoggedIn()
        {
            return HttpContext.Current.Session[SessionNames.LoggedInUserId] != null;
        }


        public static int? GetLoggedInUserId()
        {
            LoggingManager.Debug("Entering GetLoggedInUserId  - Common.cs");
            try
            {
                if (HttpContext.Current.Session[SessionNames.LoggedInUserId] != null) return (int)HttpContext.Current.Session[SessionNames.LoggedInUserId];
            }
            catch (Exception exception)
            {
                LoggingManager.Error(exception);
            }

            LoggingManager.Debug("Exiting GetLoggedInUserId  - Common.cs");

            return null;
        }

        public static void LogLastActivityDate(HttpSessionState session)
        {
            LoggingManager.Debug("Entering LogLastActivityDate  - Common.cs");

            UpdateUserActivityDate(session);

            LoggingManager.Debug("Exiting LogLastActivityDate  - Common.cs");
        }

        public static void FillDropDownBox(DropDownList dropDownList, List<KeyValuePair<string, int>> listOfValues)
        {
            LoggingManager.Debug("Entering FillDropDownBox  - Common.cs");
            dropDownList.DataSource = listOfValues;
            dropDownList.DataTextField = "Key";
            dropDownList.DataValueField = "Value";
            dropDownList.DataBind();
            LoggingManager.Debug("Exiting FillDropDownBox  - Common.cs");
        }

        public static int? GetMonthId(huntableEntities context, string monthName)
        {
            LoggingManager.Debug("Entering GetMonthId  - Common.cs");
            if (monthName.Length > 3) monthName = monthName.Substring(0, 3);
            int? monthId = null;

            var month = context.MasterMonths.FirstOrDefault(j => j.Description.ToUpper() == monthName.ToUpper());
            if (month != null)
            {
                monthId = month.ID;
            }
            LoggingManager.Debug("Exiting GetMonthId  - Common.cs");

            return monthId;
        }

        public static int? GetMonthId(huntableEntities context, int monthNumber)
        {
            LoggingManager.Debug("Entering GetMonthId  - Common.cs");
            int? monthId = null;

            var month = context.MasterMonths.FirstOrDefault(j => j.ID == monthNumber);
            if (month != null)
            {
                monthId = month.ID;
            }
            LoggingManager.Debug("Exiting GetMonthId  - Common.cs");
            return monthId;
        }

        public static int? GetYearId(huntableEntities context, string year)
        {
            LoggingManager.Debug("Entering GetYearId  - Common.cs");
            if (year.Length > 3) year = year.Substring(0, 3);
            int? yearId = null;

            var yearObj = context.MasterYears.FirstOrDefault(j => j.Description == year);
            if (yearObj != null)
            {
                yearId = yearObj.ID;
            }
            LoggingManager.Debug("Exiting GetYearId  - Common.cs");

            return yearId;
        }

        public static int? GetYearId(huntableEntities context, int year)
        {
            LoggingManager.Debug("Entering GetYearId  - Common.cs");
            int? yearId = null;
            string yearAsString = year.ToString();
            var yearObj = context.MasterYears.FirstOrDefault(j => j.Description.Trim() == yearAsString);
            if (yearObj != null)
            {
                yearId = yearObj.ID;
            }
            LoggingManager.Debug("Exiting GetYearId  - Common.cs");

            return yearId;
        }

        public static int? GetLoggedInUserId(HttpSessionState session)
        {
            LoggingManager.Debug("Entering GetLoggedInUserId  - Common.cs");
            int? userId = null;

            if (session[SessionNames.LoggedInUserId] != null)
            {
                userId = Convert.ToInt32(session[SessionNames.LoggedInUserId]);
            }
            LoggingManager.Debug("Exiting GetLoggedInUserId  - Common.cs");
            return userId;
        }

        public static int? GetUserFeedPagesize(HttpSessionState session)
        {
            LoggingManager.Debug("Entering GetUserFeedPagesize  - Common.cs");
            int? pagesize = null;

            if (session[SessionNames.UserFeedPagesize] != null)
            {
                pagesize = Convert.ToInt32(session[SessionNames.UserFeedPagesize]);
            }
            LoggingManager.Debug("Exiting GetUserFeedPagesize  - Common.cs");
            return pagesize;
        }

        public static User GetLoginUser(HttpSessionState session)
        {
            LoggingManager.Debug("Entering GetLoginUser  - Common.cs");
            var userId = GetLoggedInUserId(session);
            if (userId.HasValue)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    context.Users.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                    return context.Users.First(u => u.Id == userId.Value);
                }
            }
            LoggingManager.Debug("Exiting GetLoginUser  - Common.cs");
            return null;
        }

        public static UserEmailPreference GetuserEmailPref(int userId)
        {
            LoggingManager.Debug("Entering GetuserEmailPref  - Common.cs");
            if (userId != 0)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    return context.UserEmailPreferences.FirstOrDefault(u => u.UserId == userId);
                }
            }
            LoggingManager.Debug("Exiting GetuserEmailPref  - Common.cs");
            return null;
        }

        public static void UpdateUserActivityDate(HttpSessionState session)
        {
            LoggingManager.Debug("Entering UpdateUserActivityDate  - Common.cs");
            var loginUserId = GetLoggedInUserId(session);
            if (loginUserId.HasValue)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    context.Users.First(u => u.Id == loginUserId).LastActivityDate = DateTime.Now;
                    context.SaveChanges();
                }
            }
            LoggingManager.Debug("Exiting UpdateUserActivityDate  - Common.cs");
        }

        public static void UpdateUserActivityDate(int userId)
        {
            LoggingManager.Debug("Entering UpdateUserActivityDate  - Common.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                context.Users.First(u => u.Id == userId).LastActivityDate = DateTime.Now;
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting UpdateUserActivityDate  - Common.cs");
        }

        public static User GetUserById(string userId)
        {
            LoggingManager.Debug("Entering GetUserById  - Common.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? userIDint = new Utility().ConvertToInt(userId);

             LoggingManager.Debug("Exiting GetUserById  - Common.cs");

                return userIDint == null ? null : context.Users.FirstOrDefault(u => u.Id == userIDint);
            }
            
        }

        public static int GetUnreadMessagesCount(huntableEntities entities, User user)
        {

            return entities.UserMessages.Count(x => x.SentTo == user.Id && !x.IsRead && x.IsActive);
        }
        public static int GetUnreadMessagesCountCompany(huntableEntities entities, Company user)
        {
            return entities.UserMessages.Count(x => x.SentTo == user.CompanyId && !x.IsRead && x.IsActive);
        }
        public static User GetUserByEmailId(string email)
        {
            LoggingManager.Debug("Entering GetUserByEmailId  - Common.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                LoggingManager.Debug("Exiting GetUserByEmailId  - Common.cs");

                return context.Users.FirstOrDefault(x => x.EmailAddress == email);
            }
        }
        public static Company GetCompanyByEmailId(string email)
        {
            LoggingManager.Debug("Entering GetUserByEmailId  - Common.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                LoggingManager.Debug("Exiting GetUserByEmailId  - Common.cs");

                return context.Companies.FirstOrDefault(x => x.CompanyEmail == email);
            }
        }
        public static string GetApplicationBasePath()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }
    }
}
