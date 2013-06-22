using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Huntable.Data;

namespace Huntable.Business.DataProviders
{
    public class JobsUserInterestDataProvider : DataProviderBase
    {
        public override IList<dynamic> GetItems(huntableEntities context, string searchContains, string startsWith, int pageIndex, int pageSize)
        {
            IList<dynamic> interests = new BindingList<dynamic>();

            foreach (var item in MasterDataManager.AllInterests)
            {
                if (item.Description != null && (searchContains == null || item.Description.ToLower().Contains(searchContains)) && (startsWith == null || item.Description.ToLower().StartsWith(startsWith)))
                {
                    interests.Add(item);
                }
            }

            IList<dynamic> allItems = new List<dynamic>();
            List<PreferredJobUserInterest> selectedInterests = context.PreferredJobUserInterests.ToList();

            foreach (var interest in interests)
            {
                MasterInterest interest1 = interest;

                allItems.Add(new KeyValuePair<object, bool>(interest, selectedInterests.Any(x => x.UserId == UserId && x.MasterInterestId == interest1.Id)));
            }

            return allItems;
        }

        public override void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                foreach (int newlyCheckedItem in newlyCheckedList)
                {
                    context.AddToPreferredJobUserInterests(new PreferredJobUserInterest { UserId = UserId, MasterInterestId = newlyCheckedItem });
                }

                List<PreferredJobUserInterest> allUserInterests = context.PreferredJobUserInterests.ToList();

                foreach (int newlyCheckedItem in newlyUncheckedList)
                {
                    int item = newlyCheckedItem;
                    context.DeleteObject(allUserInterests.First(x => x.UserId == UserId && x.MasterInterestId == item));
                }

                context.SaveChanges();
            }
        }
    }
}