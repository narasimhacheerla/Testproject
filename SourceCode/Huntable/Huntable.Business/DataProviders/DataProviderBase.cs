using System.Collections.Generic;
using Huntable.Data;

namespace Huntable.Business.DataProviders
{
    public abstract class DataProviderBase : IDataProvider
    {
        private int _userId;
        protected int UserId
        {
            get
            {
                if (_userId <= 0)
                {
                    var loggedInUserId = Common.GetLoggedInUserId();
                    if (loggedInUserId != null) return _userId = loggedInUserId.Value;
                }
                return _userId;
            }
        }

        public abstract IList<dynamic> GetItems(huntableEntities context, string searchContains, string startsWith, int pageIndex, int pageSize);

        public IList<dynamic> GetItems(string searchContains, string startsWith, int pageIndex, int pageSize)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                searchContains = string.IsNullOrWhiteSpace(searchContains) ? null : searchContains.ToLower();
                startsWith = string.IsNullOrWhiteSpace(startsWith) ? null : startsWith.ToLower();

                IList<dynamic> countries = GetItems(context, searchContains, startsWith, pageIndex, pageSize);

                return countries;
            }
        }

        public abstract void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList);
    }
}