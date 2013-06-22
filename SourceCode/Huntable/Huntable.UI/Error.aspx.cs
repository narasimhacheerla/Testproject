using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Error.aspx");

            LoggingManager.Debug("Exiting Page_Load - Error.aspx");
        }
    }
}