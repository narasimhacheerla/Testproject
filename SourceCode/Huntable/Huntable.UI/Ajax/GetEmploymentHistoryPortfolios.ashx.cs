using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.Ajax
{
    /// <summary>
    /// Summary description for GetEmploymentHistoryPortfolios
    /// </summary>
    public class GetEmploymentHistoryPortfolios : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var EmploymentHistoryId = context.Request["id"];
            context.Response.ContentType = "text/plain";
            var strReturn = new List<string>();
            using (var dbcontext = huntableEntities.GetEntitiesWithNoLock())
            {
                int id = Convert.ToInt16(EmploymentHistoryId);
                EmploymentHistory history = dbcontext.EmploymentHistories.First(h => h.Id == id);
                foreach (var p in history.EmploymentHistoryPortfolios)
                {
                    strReturn.Add(new FileStoreService().GetDownloadUrlDirect(p.FileId));
                }
            }
            context.Response.Write(string.Join("~#~", strReturn));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}