using System;

namespace Huntable.UI
{
    using Huntable.Business.DataProviders;
    using Snovaspace.Util.Logging;
    using Huntable.Business;

    public partial class CustomizeJobsSkill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeJobsSkill");
             var userId = Common.GetLoggedInUserId(Session);
            if(userId==null){ucPplUMayKnow1.Visible= false;}
            ucAtoZFeedsSkill.DataProvider = new JobsUserSkillDataProvider();

            LoggingManager.Debug("Exiting Page_Load - CustomizeJobsSkill");
        }
    }
}