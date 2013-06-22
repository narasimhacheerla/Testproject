using System;
using System.Data.Objects;
using System.Linq;

namespace Snovaspace.Util
{
    public static class SnovaExtensions
    {
        public static string ToTraceString<T>(this IQueryable<T> t)
        {
            string sql = "";
            var oqt = t as ObjectQuery<T>;
            if (oqt != null)
                sql = oqt.ToTraceString();
            return sql;
        }

        public static DateTime GetLastMonthStartDate(this DateTime date)
        {
            var first = new DateTime(date.Year, date.Month, 1).AddMonths(-1);
            return first;
        }

        public static DateTime GetLastMonthEndDate(this DateTime date)
        {
            var last = new DateTime(date.Year, date.Month, 1).AddDays(-1);
            return last;
        }
    }
}