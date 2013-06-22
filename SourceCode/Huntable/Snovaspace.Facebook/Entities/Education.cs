using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snovaspace.Facebook.Entities
{
    public class Education
    {
        public School school { get; set; }

        public Year year { get; set; }

        public string type { get; set; }
    }
}
