using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Huntable.Data;

namespace Huntable.Business.DataProviders
{
    public class JobsUserSkillDataProvider : DataProviderBase
    {
        public override IList<dynamic> GetItems(huntableEntities context, string searchContains, string startsWith, int pageIndex, int pageSize)
        {
            IList<dynamic> skills = new BindingList<dynamic>();

            foreach (var item in MasterDataManager.AllSkills)
            {
                if (item.Description != null && (searchContains == null || item.Description.ToLower().Contains(searchContains)) && (startsWith == null || item.Description.ToLower().StartsWith(startsWith)))
                {
                    skills.Add(item);
                }
            }

            IList<dynamic> allItems = new List<dynamic>();
            List<PreferredJobUserSkill> selectedSkills = context.PreferredJobUserSkills.ToList();

            foreach (var skill in skills)
            {
                MasterSkill skill1 = skill;

                allItems.Add(new KeyValuePair<object, bool>(skill, selectedSkills.Any(x => x.UserId == UserId && x.MasterSkillId == skill1.Id)));
            }

            return allItems;
        }

        public override void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                foreach (int newlyCheckedItem in newlyCheckedList)
                {
                    context.AddToPreferredJobUserSkills(new PreferredJobUserSkill { UserId = UserId, MasterSkillId = newlyCheckedItem, CreatedDate = System.DateTime.Now});
                }

                List<PreferredJobUserSkill> allUserSkills = context.PreferredJobUserSkills.ToList();

                foreach (int newlyCheckedItem in newlyUncheckedList)
                {
                    int item = newlyCheckedItem;
                    context.DeleteObject(allUserSkills.First(x => x.UserId == UserId && x.MasterSkillId == item));
                }

                context.SaveChanges();
            }
        }
    }
}