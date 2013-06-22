using System.Collections.Generic;
using Snovaspace.Util.Logging;

namespace Huntable.Entities
{
    public class FeaturedSelections
    {
        
        public FeaturedSelections()
        {
            LoggingManager.Debug("Entering FeaturedSelections  -  FeaturedSelections.cs");
            Industries = new List<int>();
            Skills = new List<int>();
            Interests = new List<int>();
            Countries = new List<int>();
            
            LoggingManager.Debug("Exiting FeaturedSelections  -  FeaturedSelections.cs");
        }

        public int UserId { get; set; }
        public string Jobpackage { get; set; }
        public IList<int> Industries { get; set; }
        public IList<int> Skills { get; set; }
        public IList<int> Interests { get; set; }
        public IList<int> Countries { get; set; }
        public int PremiumPackage { get; set; }
        
    }
}