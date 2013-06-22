using System;
using System.Collections.Generic;
using System.Web;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class AdminExportData : System.Web.UI.Page
    {
        protected DateTime FromDate()
        {
            LoggingManager.Debug("Entering FromDate - AdminExportData");

            DateTime checkFrom;
            DateTime fromDate = Utils.GetDefaultFromdate();
            if (DateTime.TryParse(_fromDateText.Text, out checkFrom))
            {
                fromDate = checkFrom;
            }
            LoggingManager.Debug("Exiting FromDate - AdminExportData");


            return fromDate;
        }

        protected DateTime ToDate()
        {
            LoggingManager.Debug("Entering ToDate - AdminExportData");

            DateTime checkTo;
            DateTime fromDate = Utils.GetDefaultTodate();
            if (DateTime.TryParse(_toDateText.Text, out checkTo))
            {
                fromDate = checkTo;
            }
            LoggingManager.Debug("Exiting ToDate - AdminExportData");

            return fromDate;
            
        }

        protected void BtnExportUsersClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnExportUsersClick - AdminExportData");

            var csv = new ExportManager();
            const string attachment = "attachment; filename=ExportUsers.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(csv.ExportUsers(FromDate(), ToDate()));
            HttpContext.Current.Response.End();

            LoggingManager.Debug("Exiting BtnExportUsersClick - AdminExportData");
        }
        protected void BtnExportJobsClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnExportJobsClick - AdminExportData");

           
            var csv = new ExportManager();
            const string attachment = "attachment; filename=ExportJobs.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(csv.ExportJobs(FromDate(), ToDate()));
            HttpContext.Current.Response.End();

            LoggingManager.Debug("Exiting BtnExportJobsClick - AdminExportData");

        }
    }
}