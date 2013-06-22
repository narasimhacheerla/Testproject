using System;
using Huntable.Business.DataProviders;
using Snovaspace.Util.Logging;
using Huntable.Business;

namespace Huntable.UI
{
    public partial class CustomizeFeedsSkill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeFeedsSkill");
            var userId = Common.GetLoggedInUserId(Session);
            if(userId==null){ucPplYouMayKnow.Visible= false;}

            var user = Common.GetLoggedInUser();

            ucAtoZFeedsSkill.DataProvider = new FeedsSkillDataProvider();

            if (user.IsCompany == true)
            {
                jobuser.Visible = false;
            }
            LoggingManager.Debug("Exiting Page_Load - CustomizeFeedsSkill");
        }
    }
}