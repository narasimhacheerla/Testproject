using System.Collections.Generic;

namespace Huntable.Entities
{
    public class CustomFeaturedUserCompanies
    {
        public CustomFeaturedUserCompanies()
        {
        }

        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyLogoFilePath { get; set; }
    }
}