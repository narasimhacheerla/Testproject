using System;
using Huntable.Business.DataProviders;
using Snovaspace.Util.Logging;
using Huntable.Business;

namespace Huntable.UI
{
    public partial class CustomizeFeedsIndustry : System.Web.UI.Page
    {  
        protected void Page_Load(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering Page_Load - CustomizeFeedsIndustry");
            var userId = Common.GetLoggedInUserId(Session);
            if(userId==null){ucpplYouMayKnow.Visible= false;}

            var user = Common.GetLoggedInUser();
            ucAtoZFeedsIndustries.DataProvider = new FeedsIndustryDataProvider();
            if (user.IsCompany == true)
            {
                jobsal.Visible = false;
            }


            LoggingManager.Debug("Exiting Page_Load - CustomizeFeedsIndustry");
        }
    }
}