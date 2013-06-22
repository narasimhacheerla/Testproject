using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class TopReferrers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - TopReferrers.aspx");
            try
            {
                if (!IsPostBack)
                {
                    LoadTopReferrers(5);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - TopReferrers.aspx");
        }


        private void LoadTopReferrers(int limit)
        {
            LoggingManager.Debug("Entering LoadTopReferrers - TopReferrers.aspx");
            try
            {
                

                var objInvManager = new InvitationManager();
                int userId = (Business.Common.GetLoggedInUserId(Session)).Value;
                if (Request.QueryString["uid"] != null)
                {
                    int.TryParse(Request.QueryString["uid"], out userId);
                }
                var invitefrnds = objInvManager.GetTopRecommendators(userId, limit);
                if (invitefrnds.Count > 0)
                {
                    gvTopReferrers.DataSource = invitefrnds;
                    gvTopReferrers.DataBind();
                }
                else
                {
                    njbs.Visible = true;
                }
                hfLimit.Value = limit.ToString();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadTopReferrers - TopReferrers.aspx");
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            LoggingManager.Debug("Entering gv_RowDataBound - TopReferrers.aspx");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
            }

            LoggingManager.Debug("Exiting gv_RowDataBound - TopReferrers.aspx");
        }
        public string ShowMessage
        {
           set
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", "alert('" + value + "');", true);
            }
        }

        protected void lbtnShowMore_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnShowMore_Click - TopReferrers.aspx");

            int limit = int.Parse(hfLimit.Value);
            LoadTopReferrers(limit+5);

            LoggingManager.Debug("Exiting lbtnShowMore_Click - TopReferrers.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - TopReferrers.aspx");
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().UserUrlGenerator(userid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - TopReferrers.aspx");
                return null;
            }

        }
    }
}