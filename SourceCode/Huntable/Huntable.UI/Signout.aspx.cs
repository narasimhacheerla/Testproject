using System;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class Signout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Signout.aspx");

            Session.Abandon();
            Session.Clear();

            int n = new Random().Next(1, 9);
            switch (n)
            {
                case 1: Response.Redirect("signout1.aspx");
                    break;
                case 2: Response.Redirect("signout2.aspx");
                    break;
                case 3: Response.Redirect("signout3.aspx");
                    break;
                case 4: Response.Redirect("signout4.aspx");
                    break;
                case 5: Response.Redirect("signout5.aspx");
                    break;
                case 6: Response.Redirect("signout6.aspx");
                    break;
                case 7: Response.Redirect("signout7.aspx");
                    break;
                case 8: Response.Redirect("signout8.aspx");
                    break;
                case 9: Response.Redirect("signout9.aspx");
                    break;
            }
            LoggingManager.Debug("Exiting Page_Load - Signout.aspx");
        }
    }
}