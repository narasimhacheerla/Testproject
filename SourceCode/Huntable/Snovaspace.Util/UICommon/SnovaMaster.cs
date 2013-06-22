using System;
using System.Web.UI;
using Snovaspace.Util.FileDataStore;

namespace Snovaspace.Util.UICommon
{
    public class SnovaMaster : MasterPage
    {
      
        private void LogRequestDetails()
        {
            if(Request.CurrentExecutionFilePath.EndsWith("aspx"))
            {
                using (var fileStoreEntities = new FileStoreEntities())
                {
                    var usageAudit = new UsageAudit
                                         {
                                             CurrentExecutionFilePath = Request.CurrentExecutionFilePath,
                                             PhysicalApplicationPath = Request.PhysicalApplicationPath,
                                             Url = Request.Url.ToString(),
                                             UserAgent = Request.UserAgent
                                         };

                    if (Request.UrlReferrer != null) usageAudit.UrlReferrer = Request.UrlReferrer.ToString();
                    usageAudit.RemoteAddress = Request.ServerVariables["REMOTE_ADDR"];
                    usageAudit.LogonUser = Request.ServerVariables["LOGON_USER"];
                    usageAudit.HttpHost = Request.ServerVariables["HTTP_HOST"];
                    usageAudit.CreatedDateTime = DateTime.Now;
                    usageAudit.MachineName = Environment.MachineName;

                    fileStoreEntities.AddToUsageAudits(usageAudit);
                    fileStoreEntities.SaveChanges();
                }
            }
        }
    }
}