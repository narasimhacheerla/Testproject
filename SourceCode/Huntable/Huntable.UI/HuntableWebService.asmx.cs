using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Huntable.Data;
using Huntable.Business;
using System.Text;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    /// <summary>
    /// Summary description for HuntableWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HuntableWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetEmploymentHistoryAchievements(string EmploymentHistoryId)
        {
            LoggingManager.Debug("Entering GetEmploymentHistoryAchievements - HuntableWebService.asmx");
            
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int id = Convert.ToInt16(EmploymentHistoryId);
                var strReturn = new List<string>();
                EmploymentHistory history = context.EmploymentHistories.First(h => h.Id == id);
                foreach (var p in history.EmploymentHistoryAchievements)
                {
                    strReturn.Add(p.Summary);
                }
            LoggingManager.Debug("Exiting GetEmploymentHistoryAchievements - HuntableWebService.asmx");
                return string.Join("~", strReturn);
            }

            
        }

        [WebMethod]
        public string GetEmploymentHistoryVedios(string EmploymentHistoryId)
        {
            LoggingManager.Debug("Entering GetEmploymentHistoryVedios - HuntableWebService.asmx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var strReturn = new List<string>();
                EmploymentHistory history = context.EmploymentHistories.First(h => h.Id == Convert.ToInt32(EmploymentHistoryId));
                foreach (var p in history.EmploymentHistoryVideos)
                {
                    strReturn.Add(p.VideoURL);
                }
                LoggingManager.Debug("Exiting GetEmploymentHistoryVedios - HuntableWebService.asmx");
                return string.Join("~", strReturn);
            }
        }

        public class SkillSearchResponse
        {

            public string desc;
            public int id;
        }

        [WebMethod]
        public List<string> SearchSkills(string word)
        {
            LoggingManager.Debug("Entering SearchSkills - HuntableWebService.asmx");

            //List<SkillSearchResponse> skills = MasterDataManager.AllSkills.Where(s => s.Description.ToLower().Contains(word.ToLower())).Select(s => new SkillSearchResponse(){id=s.Id, desc=s.Description}).ToList();
            List<string> skills = MasterDataManager.AllSkills.Where(s => s.Description.ToLower().Contains(word.ToLower())).Select(s => s.Description).ToList();
            LoggingManager.Debug("Exiting SearchSkills - HuntableWebService.asmx");

            return skills;
            
        }
        [WebMethod]
        public  List<SkillSearchResponse> Searchskillsexp(string word)
        {
             LoggingManager.Debug("Entering SearchSkills - HuntableWebService.asmx");

            List<SkillSearchResponse> skills = MasterDataManager.AllSkills.Where(s => s.Description.ToLower().Contains(word.ToLower())).Select(s => new SkillSearchResponse(){id=s.Id, desc=s.Description}).ToList();
            //List<SkillSearchResponse> skills = MasterDataManager.AllSkills.Where(s => s.Description.ToLower().Contains(word.ToLower())).Select(s => s.Description).ToList();
            LoggingManager.Debug("Exiting SearchSkills - HuntableWebService.asmx");

            return skills;
        }
        public List<string> SearchSkillsForExp(string word)
        {
            LoggingManager.Debug("Entering SearchSkillsForExp - HuntableWebService.asmx");

            var skills = MasterDataManager.AllSkills.Where(s => s.Description.ToLower().Contains(word.ToLower())).Select(s => s.Description).ToList();

            LoggingManager.Debug("Exiting SearchSkillsForExp - HuntableWebService.asmx");

            return skills;
        }

        public class LanguageSearchResponse
        {
            public string desc;
            public int id;
        }

        [WebMethod]
        public List<LanguageSearchResponse> SearchLanguages(string word)
        {
            LoggingManager.Debug("Entering SearchLanguages - HuntableWebService.asmx");

            var skills = MasterDataManager.AllMasterLanguages.Where(s => s.Description.ToLower().Contains(word.ToLower())).Select(s => new LanguageSearchResponse() { id = s.Id, desc = s.Description }).ToList();
            
            LoggingManager.Debug("Exiting SearchLanguages - HuntableWebService.asmx");

            return skills;
        }

        public class InterestSearchResponse
        {
            public string desc;
            public int id;
        }

        [WebMethod]
        public List<InterestSearchResponse> SearchInterests(string word)
        {
            LoggingManager.Debug("Entering SearchInterests - HuntableWebService.asmx");

            var interests = MasterDataManager.AllInterests.Where(s => s.Description.ToLower().Contains(word.ToLower())).Select(s => new InterestSearchResponse() { id = s.Id, desc = s.Description }).ToList();

            LoggingManager.Debug("Exiting SearchInterests - HuntableWebService.asmx");
            return interests;
        }

        [WebMethod(true)]
        public List<string> SearchComposeToUser(string word)
        {

            LoggingManager.Debug("Entering SearchComposeToUser - HuntableWebService.asmx");
            List<User> user = new List<User>();

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                var objMessageManager = new UserMessageManager();
              
                var usrbl = context.UserBlockLists.Where(y=>y.BlockedUserId == loggedInUserId).ToList();
                user = context.Users.Where(u => (u.FirstName.ToLower().Contains(word.ToLower()) || u.LastName.ToLower().Contains(word.ToLower())) && u.Id != loggedInUserId && u.IsVerified == true ).ToList();
                foreach (var usrb in usrbl)
                {
                    user = user.Where(x => x.Id != usrb.UserId).ToList();
                
                }
            LoggingManager.Debug("Exiting SearchComposeToUser - HuntableWebService.asmx");

                return user.Select(s => s.FirstName + (s.LastName ?? "")).ToList();
            }
            //MasterDataManager.AllSkills.Where(s => s.Description.ToLower().Contains(word)).Select(s => s.Description).ToList();
            
        }
    }
}
