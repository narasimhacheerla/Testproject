using System;
using Huntable.Business.DataProviders;
using Snovaspace.Util.Logging;
using Huntable.Business;


namespace Huntable.UI
{
    public partial class CustomizeFeedsInterest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeFeedsInterest");
            var userId = Common.GetLoggedInUserId(Session);
            if (userId == null) { ucPplUMayKnow1.Visible = false; }

            var user = Common.GetLoggedInUser();

            ucAtoZFeedsInterest.DataProvider = new FeedsInterestDataProvider();

            if (user.IsCompany == true)
            {
                jobuser.Visible = false;
            }
            LoggingManager.Debug("Exiting Page_Load - CustomizeFeedsInterest");
        }
    }
}