using System;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class GridUserControl : System.Web.UI.UserControl
    {
        public GridView GridDetails
        {
            get { return gvDetails; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - GridUserControl.aspx");

            LoggingManager.Debug("Exiting Page_Load - GridUserControl.aspx");

        }
    }
}