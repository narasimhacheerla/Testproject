using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    using Huntable.Business.DataProviders;

    public partial class CustomizeJobsJobType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeJobsJobType");
            var userId = Common.GetLoggedInUserId(Session);
            if(userId==null){ucPplUMayKnow1.Visible= false;}

            ucAtoZFeedsJobType.DataProvider = new JobsUserJobTypeDataProvider();

            LoggingManager.Debug("Exiting Page_Load - CustomizeJobsJobType");
        }
    }
}