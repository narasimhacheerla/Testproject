using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Snovaspace.Util.Logging;
using System.Data.Objects;
using System.Data.Entity;

namespace Huntable.UI
{
    public partial class JobsStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - JobsStatus .aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var jobsStatus = (from j in context.JobsStatusDailies
                                  group j by new { dttm = EntityFunctions.TruncateTime(j.DateTime), j.Type } into jgroup
                                 select new
                                 {
                                    
                                    dttime = jgroup.Key.dttm,
                                    type = jgroup.Key.Type,
                                    NoOfJobs = jgroup.Max(x=>x.NoOfJobsUploaded)
                                    
                                 }).OrderBy(x=>x.dttime).ToList();
                dlJobsStatus.DataSource = jobsStatus;
                dlJobsStatus.DataBind();
                var shinejobs = context.CountofShine();
               
            }
            LoggingManager.Debug("Exiting Page_Load - JobsStatus .aspx");
        }
    }
}