using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompanyOverview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompanyOverview");

            overview.HRef = "companyoverview.aspx?Id=" + compId;
            activity.HRef = "businessactivity.aspx?Id=" + compId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + compId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + compId;
            careers.HRef = "companyjobs.aspx?Id=" + compId;
            article.HRef = "article.aspx?Id=" + compId;
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
            LoggingManager.Debug("Exiting Page_Load - CompanyOverview");
           
        }

        private int? compId
        {
            get
            {
                LoggingManager.Debug("Entering compId - CompanyOverview");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting compId - CompanyOverview");
                return null;
            }
        }
    }
}