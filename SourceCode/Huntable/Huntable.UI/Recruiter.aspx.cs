using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;
namespace Huntable.UI
{
    public partial class Recruiter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering Page_Load - Recruiter.aspx");

            LoggingManager.Debug("Exiting Page_Load - Recruiter.aspx");
        }
        protected void DownloadTool(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DownloadTool- Recruiter.aspx");
            string fileName = "HuntableRecruiter.msi";
            string path = "C:\\filest\\";
            string fullPath = path + fileName;
            var file = new FileInfo(fullPath);

            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/x-zip-compressed";
            Response.WriteFile(fullPath);
            Response.End();
            LoggingManager.Debug("Exiting DownloadTool- Recruiter.aspx");

        }
    }
}