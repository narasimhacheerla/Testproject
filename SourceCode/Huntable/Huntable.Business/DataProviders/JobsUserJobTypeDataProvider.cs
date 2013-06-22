using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Huntable.Data;

namespace Huntable.Business.DataProviders
{
    public class JobsUserJobTypeDataProvider : DataProviderBase
    {
        public override IList<dynamic> GetItems(huntableEntities context, string searchContains, string startsWith, int pageIndex, int pageSize)
        {
            IList<dynamic> jobtypes = new BindingList<dynamic>();

            foreach (var item in MasterDataManager.AllJobTypes)
            {
                if (item.Description != null && (searchContains == null || item.Description.ToLower().Contains(searchContains)) && (startsWith == null || item.Description.ToLower().StartsWith(startsWith)))
                {
                    jobtypes.Add(item);
                }
            }

            IList<dynamic> allItems = new List<dynamic>();
            List<PreferredJobUserJobType> selectedJobTypes = context.PreferredJobUserJobTypes.ToList();

            foreach (var jobtype in jobtypes)
            {
                MasterJobType jobtype1 = jobtype;

                allItems.Add(new KeyValuePair<object, bool>(jobtype, selectedJobTypes.Any(x => x.UserId == UserId && x.MasterJobTypeId == jobtype1.Id)));
            }

            return allItems;
        }

        public override void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                foreach (int newlyCheckedItem in newlyCheckedList)
                {
                    context.AddToPreferredJobUserJobTypes(new PreferredJobUserJobType { UserId = UserId, MasterJobTypeId = newlyCheckedItem });
                }

                List<PreferredJobUserJobType> allUserJobTypes = context.PreferredJobUserJobTypes.ToList();

                foreach (int newlyCheckedItem in newlyUncheckedList)
                {
                    int item = newlyCheckedItem;
                    context.DeleteObject(allUserJobTypes.First(x => x.UserId == UserId && x.MasterJobTypeId == item));
                }

                context.SaveChanges();
            }
        }
    }
}