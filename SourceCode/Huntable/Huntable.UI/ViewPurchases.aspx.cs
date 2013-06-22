using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class ViewPurchases : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ViewPurchases.aspx");

             var userId = Business.Common.GetLoggedInUserId(Session);
            var objInvManager = new InvitationManager();
            if (userId != null)
            {
                var result = objInvManager.GetPurchases(userId.Value);
         

                if (result.Count != 0)

                {
                    gvSummary.DataSource = result;
                    gvSummary.DataBind();
                }
                else
                {
                    lblTotalEarnings.Text = "You Have No Purchases To Display";
                    lblTotalEarnings.Visible = true;

                }
            }

            LoggingManager.Debug("Exiting Page_Load - ViewPurchases.aspx");
        }
        protected void GvRowDataBound(object sender, GridViewRowEventArgs e)
        {
            LoggingManager.Debug("Entering GvRowDataBound - ViewPurchases.aspx");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Text =  e.Row.Cells[2].Text;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[3].Text = "$" +e.Row.Cells[3].Text;
            }

            LoggingManager.Debug("Exiting GvRowDataBound - ViewPurchases.aspx");
        }
    }
}