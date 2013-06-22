using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class PeopleYouMayKnowDelete
    {

        public void Run(int loggedinuserid,int useridtofollow)
        {
            LoggingManager.Debug("Entering into JobAlert");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var list =
                    context.MasterPeoples.Where(x => x.UserId == loggedinuserid && x.MutualFollowerId == useridtofollow).ToList();
                foreach (var masterPeople in list)
                {
                     context.MasterPeoples.DeleteObject(masterPeople);
                     context.SaveChanges();
                }
                
            }

                //List<PreferredFeedUserUser> dupliacteUsersList =
                //    context.PreferredFeedUserUsers.Where(s => s.FollowingUserId == loggedinuserid).ToList();
                //foreach (PreferredFeedUserUser eachDupliacteUsersList in dupliacteUsersList)
                //{
                //    var duplicateFollower =
                //        context.MasterPeoples.Where(
                //            s =>
                //            s.UserId == eachDupliacteUsersList.FollowingUserId &&
                //            s.MutualFollowerId == eachDupliacteUsersList.UserId).ToList();
                //    foreach (var eachDuplicateFollower in duplicateFollower)
                //    {
                //        context.MasterPeoples.DeleteObject(eachDuplicateFollower);
                //        context.SaveChanges();
                //    }
                //}
            }
        }
    }
  

