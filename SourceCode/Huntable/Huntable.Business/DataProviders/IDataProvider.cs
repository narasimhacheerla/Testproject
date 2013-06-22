using System.Collections.Generic;

namespace Huntable.Business.DataProviders
{
    public interface IDataProvider
    {
        IList<dynamic> GetItems(string search, string startsWith, int pageIndex, int pageSize);
        void UpdateItems(List<int> newlyCheckedList, List<int> newlyUncheckedList);
    }
}