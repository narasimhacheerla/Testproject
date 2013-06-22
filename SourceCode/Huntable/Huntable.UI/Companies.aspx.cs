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
    public partial class Companies : System.Web.UI.Page
    {
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - Companies");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - Companies");
                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Companies");

            var result = CompanyManager.GetUserFollowingCompanies(LoginUserId);

            dtCompanies.DataSource = result;
            dtCompanies.DataBind();
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                if (loggedInUserId != null)
                {


                    var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
                    if (usri != null && usri.IsPremiumAccount == null)
                    {
                        bimage.Visible = true;
                        pimage.Visible = false;
                    }
                    else
                    {
                        bimage.Visible = false;
                        pimage.Visible = true;
                    }
                }
                else
                {
                    bimage.Visible = true;
                    pimage.Visible = false;
                }
            }

            LoggingManager.Debug("Exiting Page_Load - Companies");
        }

        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - Companies");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var photo = result.CompanyLogoId;

            LoggingManager.Debug("Exiting Picture - Companies");

                return new FileStoreService().GetDownloadUrl(photo);
            }
            

        }
        public int Jobs(object id)
        {
            LoggingManager.Debug("Entering Jobs - Companies");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Companies.FirstOrDefault(x => x.Id == p);
                var name = result.CompanyName;

            LoggingManager.Debug("Exiting Jobs - Companies");

                return  context.Jobs.Count(x => x.CompanyName == name);
            }
        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - Companies");

            var button = sender as LinkButton;
            if (button != null)
            {
                int companyid = Convert.ToInt32(button.CommandArgument);
                CompanyManager.FollowCompany(LoginUserId,companyid);
            }
            LoggingManager.Debug("Exiting FollowupClick - Companies");
        }
    }
}