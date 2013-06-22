using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class UploadPortfolio : System.Web.UI.Page
    {
        private List<EmploymentHistory> EmploymentHistories
        {
            get
            {
                return Session["EmpHistories"] as List<EmploymentHistory>;
            }
        }

        private int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId  - UploadPortfolio.aspx");
                LoggingManager.Debug("Exiting LoginUserId  - UploadPortfolio.aspx");
                return Common.GetLoggedInUserId(Session).Value;
            }
        }

        private string EmploymentHistoryId
        {
            get
            {
                LoggingManager.Debug("Entering EmploymentHistoryId  - UploadPortfolio.aspx");
                LoggingManager.Debug("Exiting EmploymentHistoryId  - UploadPortfolio.aspx");
                return (String.IsNullOrEmpty(Request.QueryString["id"]) ? null : Request.QueryString["id"].ToString());
            }
        }

        protected void AddPortfolioClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering AddPortfolioClick  - UploadPortfolio.aspx");

            int p_convertedId;
            EmploymentHistory p_employmentHistory = int.TryParse(EmploymentHistoryId, out p_convertedId) ? EmploymentHistories.First(h => h.Id == p_convertedId) : EmploymentHistories.First(h => h.TempId == EmploymentHistoryId);
            if (filePhotoUpload.HasFile)
            {
                UserProfileManager.UploadPortfolioPicture(filePhotoUpload, p_convertedId, LoginUserId);
            }

            LoggingManager.Debug("Exiting AddPortfolioClick  - UploadPortfolio.aspx");
        }
    }
}