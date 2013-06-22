using System;
using Huntable.Business.DataProviders;
using Snovaspace.Util.Logging;
using Huntable.Business;

namespace Huntable.UI
{
    public partial class CustomizeFeedsCountry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeFeedsCountry");
            var userId = Common.GetLoggedInUserId(Session);
            if (userId == null) { ucPplUMayKnow1.Visible = false; }

            ucAtoZFeedsCountries.DataProvider = new FeedsCountryDataProvider();

            LoggingManager.Debug("Exiting Page_Load - CustomizeFeedsCountry");
        }
    }
}