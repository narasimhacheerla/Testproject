using System;

namespace Huntable.UI
{
    using Huntable.Business.DataProviders;
    using Snovaspace.Util.Logging;

    public partial class CustomizeJobsCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeJobsCompany");

            ucAtoZFeedsCompany.DataProvider = new JobsUserCompanyDataProvider();

            LoggingManager.Debug("Exiting Page_Load - CustomizeJobsCompany");
        }
    }
}