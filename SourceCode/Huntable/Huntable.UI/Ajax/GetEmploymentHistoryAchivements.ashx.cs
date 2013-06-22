﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Huntable.Data;

namespace Huntable.UI.Ajax
{
    /// <summary>
    /// Summary description for GetEmploymentHistoryAchivements
    /// </summary>
    public class GetEmploymentHistoryAchivements : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var employmentHistoryId = context.Request["id"];
            context.Response.ContentType = "text/plain";
            var strReturn = new List<string>();
            using (var dbcontext = huntableEntities.GetEntitiesWithNoLock())
            {
                int id = Convert.ToInt16(employmentHistoryId);
                EmploymentHistory history = dbcontext.EmploymentHistories.First(h => h.Id == id);
                foreach (var p in history.EmploymentHistoryAchievements)
                {
                    strReturn.Add(p.Summary);
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