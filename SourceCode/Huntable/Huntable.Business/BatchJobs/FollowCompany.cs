using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huntable.Data;

namespace Huntable.Business.BatchJobs
{
    public class FollowCompany
    {

        public void Run(int userid,int? invitedcmpid)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var company = context.Companies.FirstOrDefault(x => x.Userid == invitedcmpid);
                CompanyManager.FollowCompany(userid, company.Id);
            }
        }
    }
}
