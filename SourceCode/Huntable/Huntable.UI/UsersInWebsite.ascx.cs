using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class UsersInWebsite : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UsersInWebsite - UsersInWebsite.aspx");


            LoggingManager.Debug("Exiting UsersInWebsite - UsersInWebsite.aspx");
        }
    }
}