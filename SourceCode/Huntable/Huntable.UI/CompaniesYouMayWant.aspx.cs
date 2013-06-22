using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompaniesYouMayWant : System.Web.UI.Page
    {


        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompaniesYouMayWant");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompaniesYouMayWant");

                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompaniesYouMayWant");

            var objInvManager = new UserManager();
            var userList = objInvManager.GetFeaturedCompanie(LoginUserId);
            if (userList.Count != 0)
            {
                rspcom.DataSource = userList;
                rspcom.DataBind();
            }
            else
            {
                var result = objInvManager.GetFeaturedCompaniesDefault();
                rspcom.DataSource = result;
                rspcom.DataBind();
            }
            LoggingManager.Debug("Exiting Page_Load - CompaniesYouMayWant");
        }

        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - CompaniesYouMayWant");

            var button = sender as LinkButton;
            if (button != null)
            {
                int userId = Convert.ToInt32(button.CommandArgument);
                UserManager.FollowCompany(LoginUserId, userId);
                //UserManager.Deletecompany(_loogedinuserId, userId);
            }

            LoggingManager.Debug("Exiting FollowupClick - CompaniesYouMayWant");

        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - CompaniesYouMayWant");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.MasterCompanies.FirstOrDefault(x => x.Id == p);

                byte[] photo = result.Logo;

                LoggingManager.Debug("Exiting Picture - CompaniesYouMayWant");

                return new FileStoreService().GetDownloadUrl(Convert.ToInt32(photo));
            }
            
        }
        public int Jobs(object descritpion)
        {
            LoggingManager.Debug("Entering Jobs - CompaniesYouMayWant");

            string p = descritpion.ToString();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Jobs.Where(x => x.CompanyName == descritpion).ToList();
                var count = result.Count();

                LoggingManager.Debug("Exiting Jobs - CompaniesYouMayWant");
                return count;
            }

        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - CompaniesYouMayWant");
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - CompaniesYouMayWant");
                return null;
            }

        }
    }
}