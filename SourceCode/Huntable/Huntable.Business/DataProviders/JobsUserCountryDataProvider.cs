using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Huntable.Data;

namespace Huntable.Business.DataProviders
{
    public class JobsUserCountryDataProvider : DataProviderBase
    {
        public override IList<dynamic> GetItems(huntableEntities context, string searchContains, string startsWith, int pageIndex, int pageSize)
        {
            IList<dynamic> countries = new BindingList<dynamic>();

            foreach (var item in MasterDataManager.AllCountries)
            {
                if (item.Description != null && (searchContains == null || item.Description.ToLower().Contains(searchContains)) && (startsWith == null || item.Description.ToLower().StartsWith(startsWith)))
                {
                    countries.Add(item);
                }
            }

            IList<dynamic> allItems = new List<dynamic>();
            List<PreferredJobUserCountry> selectedCountries = context.PreferredJobUserCountries.ToList();

            foreach (var country in countries)
            {
                MasterCountry country1 = country;

                allItems.Add(new KeyValuePair<object, bool>(country, selectedCountries.Any(x => x.UserId == UserId && x.MasterCountryId == country1.Id)));
            }

            return allItems;
        }

        public override void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                foreach (int newlyCheckedItem in newlyCheckedList)
                {
                    context.AddToPreferredJobUserCountries(new PreferredJobUserCountry { UserId = UserId, MasterCountryId = newlyCheckedItem });
                }

                List<PreferredJobUserCountry> allUserCountries = context.PreferredJobUserCountries.ToList();

                foreach (int newlyCheckedItem in newlyUncheckedList)
                {
                    int item = newlyCheckedItem;
                    context.DeleteObject(allUserCountries.First(x => x.UserId == UserId && x.MasterCountryId == item));
                }

                context.SaveChanges();
            }
        }
    }
}