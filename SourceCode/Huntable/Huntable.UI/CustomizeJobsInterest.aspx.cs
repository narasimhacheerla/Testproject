using System;

namespace Huntable.UI
{
    using Huntable.Business.DataProviders;
    using Snovaspace.Util.Logging;

    public partial class CustomizeJobsInterest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeJobsInterest");

            ucAtoZFeedsInterest.DataProvider = new JobsUserInterestDataProvider();

            LoggingManager.Debug("Exiting Page_Load - CustomizeJobsInterest");
        }
    }
}