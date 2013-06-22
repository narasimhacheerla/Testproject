using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snovaspace.Facebook.Entities
{
    public class WorkInfo
    {
        public Employer employer { get; set; }

        public Location location { get; set; }

        public Position position { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }

        public string description { get; set; }
    }    
}
