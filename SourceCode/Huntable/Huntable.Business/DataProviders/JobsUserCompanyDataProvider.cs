using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Huntable.Data;

namespace Huntable.Business.DataProviders
{
    public class JobsUserCompanyDataProvider : DataProviderBase
    {
        public override IList<dynamic> GetItems(huntableEntities context, string searchContains, string startsWith, int pageIndex, int pageSize)
        {
            IList<dynamic> companies = new BindingList<dynamic>();

            foreach (var item in MasterDataManager.AllMasterCompanies)
            {
                if (item.Description != null && (searchContains == null || item.Description.ToLower().Contains(searchContains)) && (startsWith == null || item.Description.ToLower().StartsWith(startsWith)))
                {
                    companies.Add(item);
                }
            }

            IList<dynamic> allItems = new List<dynamic>();
            List<PreferredJobUserCompany> selectedCompanies = context.PreferredJobUserCompanies.ToList();

            foreach (var company in companies)
            {
                MasterCompany company1 = company;

                allItems.Add(new KeyValuePair<object, bool>(company, selectedCompanies.Any(x => x.UserId == UserId && x.MasterCompanyId == company1.Id)));
            }

            return allItems;
        }

        public override void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                foreach (int newlyCheckedItem in newlyCheckedList)
                {
                    context.AddToPreferredJobUserCompanies(new PreferredJobUserCompany { UserId = UserId, MasterCompanyId = newlyCheckedItem });
                }

                List<PreferredJobUserCompany> allUserCompanies = context.PreferredJobUserCompanies.ToList();

                foreach (int newlyCheckedItem in newlyUncheckedList)
                {
                    int item = newlyCheckedItem;
                    context.DeleteObject(allUserCompanies.First(x => x.UserId == UserId && x.MasterCompanyId == item));
                }

                context.SaveChanges();
            }
        }
    }
}