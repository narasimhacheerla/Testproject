using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class ajax_like : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load -  ajax-like .aspx");
            string FeedId = "0";
            if (Request["FeedId"] != null)
            {
                FeedId = Convert.ToString(Request["FeedId"]);
            }
            string FeedType = "";
            if (Request["FeedType"] != null)
            {
                FeedType = Convert.ToString(Request["FeedType"]);
            }
            string RefRecordId = "";
            if (Request["RefRecordId"] != null)
            {
                RefRecordId = Convert.ToString(Request["RefRecordId"]);
            }
            UserFeedLikedUser1.FeedId = FeedId;
            UserFeedLikedUser1.FeedType = FeedType;
            UserFeedLikedUser1.RefRecordId = RefRecordId;
            LoggingManager.Debug("Exiting Page_Load -  ajax-like .aspx");
        }
    }
}