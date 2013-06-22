using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snovaspace.Facebook.Entities
{
    public class FacebookProfile
    {
        public string id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string link { get; set; }
        public string birthday { get; set; }
        // public string work { get; set; }
        public string timezone { get; set; }
        public string local { get; set; }
        public string verified { get; set; }
        public string updated_time { get; set; }

        public string bio { get; set; }

        public Language[] languages { get; set; }

        public WorkInfo[] work { get; set; }

        public Education[] education { get; set; }

        public Location location { get; set; }

        public string gender { get; set; }

        public string email { get; set; }

        public string website { get; set; }
    }
}
