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
    public partial class CompanyFollowConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompanyFollowConfirmation");
            int cmpnyid = Convert.ToInt32(Request.QueryString["Cmpid"]);
            int userid = Convert.ToInt32(Request.QueryString["UserId"]);
           
            var result = new CompanyManager().IsUserFollowingCompany(userid, cmpnyid);
            if (result > 0)
            {
                pnlAlreadyConfirmed.Visible = true;
            }
            else
            {
                CompanyManager.FollowCompany(userid, cmpnyid);
                pnlSuccess.Visible = true;
            }
            LoggingManager.Debug("Exiting Page_Load - CompanyFollowConfirmation");
        }
    }
}