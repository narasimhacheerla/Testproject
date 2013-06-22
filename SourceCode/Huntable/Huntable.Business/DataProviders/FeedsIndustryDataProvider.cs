using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Huntable.Data;

namespace Huntable.Business.DataProviders
{
    public class FeedsIndustryDataProvider : DataProviderBase
    {
        public override IList<dynamic> GetItems(huntableEntities context, string searchContains, string startsWith, int pageIndex, int pageSize)
        {
            IList<dynamic> industries = new BindingList<dynamic>();

            foreach (var item in MasterDataManager.AllIndustries)
            {
                if (item.Description != null && (searchContains == null || item.Description.ToLower().Contains(searchContains)) && (startsWith == null || item.Description.ToLower().StartsWith(startsWith)))
                {
                    industries.Add(item);
                }
            }

            IList<dynamic> allItems = new List<dynamic>();
            List<PreferredFeedUserIndustry> selectedIndustries = context.PreferredFeedUserIndustries.ToList();

            foreach (var industry in industries)
            {
                MasterIndustry industry1 = industry;

                allItems.Add(new KeyValuePair<object, bool>(industry, selectedIndustries.Any(x => x.UserId == UserId && x.MasterIndustryId == industry1.Id)));
            }

            return allItems;
        }

        public override void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                foreach (int newlyCheckedItem in newlyCheckedList)
                {
                    context.AddToPreferredFeedUserIndustries(new PreferredFeedUserIndustry { UserId = UserId, MasterIndustryId = newlyCheckedItem });
                }

                List<PreferredFeedUserIndustry> allUserIndustries = context.PreferredFeedUserIndustries.ToList();

                foreach (int newlyCheckedItem in newlyUncheckedList)
                {
                    int item = newlyCheckedItem;
                    context.DeleteObject(allUserIndustries.First(x => x.UserId == UserId && x.MasterIndustryId == item));
                }

                context.SaveChanges();
            }
        }
    }
}