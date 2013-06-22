using System;

namespace Huntable.Business
{
    public static class Utils
    {
        public static DateTime GetDefaultFromdate()
        {
            return DateTime.Now.AddDays(-30);
        }

        public static DateTime GetDefaultTodate()
        {
            return DateTime.Now;
        }

        public static string CheckGetSringValue(string s)
        {
            return string.IsNullOrEmpty(s) ? "" : s;
        }
    }
}
