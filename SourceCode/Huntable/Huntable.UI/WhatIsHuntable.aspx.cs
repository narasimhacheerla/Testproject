using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class WhatIsHuntable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - WhatIsHuntable.aspx");

            LoggingManager.Debug("Entering Page_Load - WhatIsHuntable.aspx");

        }

        protected void btnsign_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnsign_Click- WhatIsHuntable.aspx");

            Response.Redirect("Default.aspx");
            LoggingManager.Debug("Exiting btnsign_Click- WhatIsHuntable.aspx");
        }

        protected void spbtn(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering spbtn - WhatIsHuntable.aspx");
            Response.Redirect("Super-power.aspx");
            LoggingManager.Debug("Exiting spbtn - WhatIsHuntable.aspx");
        }
    }
}