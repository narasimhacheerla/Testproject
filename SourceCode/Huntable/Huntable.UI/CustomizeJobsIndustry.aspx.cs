using System;

namespace Huntable.UI
{
    using Business.DataProviders;
    using Snovaspace.Util.Logging;
    using Huntable.Business;

    public partial class CustomizeJobsIndustry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeJobsIndustry");
            var userId = Common.GetLoggedInUserId(Session);
            if(userId==null){ucPplUMayKnow1.Visible= false;}

            ucAtoZFeedsIndustries.DataProvider = new JobsUserIndustryDataProvider();

            LoggingManager.Debug("Entering Page_Load - CustomizeJobsIndustry");
        }
    }
}