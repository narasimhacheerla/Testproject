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
    public partial class CompanyBlogsRecent : System.Web.UI.Page
    {
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompanyBlogsRecent");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                LoggingManager.Debug("Exiting LoginUserId - CompanyBlogsRecent");

                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompanyBlogsRecent");

            var cmpgr = new CompanyManager();
            var link=cmpgr.CompanyCarrerlink(LoginUserId);


            a_overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(OtherComId));
            a_Activity.HRef = "businessactivity.aspx?Id=" + OtherComId;
            a_article.HRef =  "article.aspx?Id=" + OtherComId;
            a_business.HRef = "companyblogsrecent.aspx?Id=" + OtherComId;
            a_careers.HRef = "companyjobs.aspx?Id=" + OtherComId;
            a_Product.HRef = "companyproducts.aspx?Id=" + OtherComId;


            LoggingManager.Debug("Exiting Page_Load - CompanyBlogsRecent");
        }
        private int? OtherComId
        {

            get
            {
                LoggingManager.Debug("Entering OtherComId - CompanyBlogsRecent");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting OtherComId - CompanyBlogsRecent");
                return null;
            }
        }
    }
}