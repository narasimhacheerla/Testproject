using Snovaspace.Util.Logging;
namespace Huntable.Business
{
    public class CustomProfileVisitedHistory
    {
        private int? nullable;

        public string Name { get; set; }
        public string Jobtitle { get; set; }
        public string MasterCompany { get; set; }
        public int? ID { get; set; }

        public CustomProfileVisitedHistory(string name, string jobtitle, string masterCompany, int? id)
        {
            LoggingManager.Debug("Entering CustomProfileVisitedHistory  - CustomProfileVisitedHistory.cs");
            Name = name;
            Jobtitle = jobtitle;
            MasterCompany = masterCompany;
            ID = id;
            LoggingManager.Debug("Exiting CustomProfileVisitedHistory  - CustomProfileVisitedHistory.cs");
        }

        
    }
}
