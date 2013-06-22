using System;
using System.Configuration;

namespace Snovaspace.Util
{
    public static class ConfigurationManagerHelper
    {
        public static R GetAppsettingByKey<R>(string key)
        {
            return (R)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(R));
        }
    }
}

