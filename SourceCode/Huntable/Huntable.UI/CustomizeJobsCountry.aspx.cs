using System;

namespace Huntable.UI
{
    using Huntable.Business.DataProviders;
    using Snovaspace.Util.Logging;
    using Huntable.Business;

    public partial class CustomizeJobsCountry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeJobsCountry");
            var userId = Common.GetLoggedInUserId(Session);
            if(userId==null){ucPplUMayKnow1.Visible= false;}
            ucAtoZFeedsCountries.DataProvider = new JobsUserCountryDataProvider();

            LoggingManager.Debug("Exiting Page_Load - CustomizeJobsCountry");
        }
    }
}