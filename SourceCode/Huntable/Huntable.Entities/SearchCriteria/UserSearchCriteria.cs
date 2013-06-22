using System.Collections.Generic;
using System.Text;

namespace Huntable.Entities.SearchCriteria
{
    public class UserSearchCriteria
    {
        public int? Id { get; set; }
        public string Skill { get; set; }
        public string Keywords { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public bool? IsCurrentPosition { get; set; }
        public string CompanyName { get; set; }
        public bool? IsCurrentCompany { get; set; }
        public string SchoolName { get; set; }
        public double? ExperienceFrom { get; set; }
        public double? ExperienceTo { get; set; }
        public string Year { get; set; }
        public bool? IsProfileAvailable { get; set; }
        public int? CountryId { get; set; }
        public double? TotalExp { get; set; }
        public string Industry { get; set; }
        public int? Interest { get; set; }
        public string LanguageKnown { get; set; }
        public string JobTitle { get; set; }
        public int? Industryid { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id = " + Id);
            builder.AppendLine("Keywords = " + Keywords);
            builder.AppendLine("FirstName = " + FirstName);
            builder.AppendLine("LastName = " + LastName);
            builder.AppendLine("Title = " + Title);
            builder.AppendLine("IsCurrentPosition = " + IsCurrentPosition);
            builder.AppendLine("CompanyName = " + CompanyName);
            builder.AppendLine("IsCurrentCompany = " + IsCurrentCompany);
            builder.AppendLine("SchoolName = " + SchoolName);
            builder.AppendLine("SkillsText = " + Skill);
            builder.AppendLine("ExperienceFrom = " + ExperienceFrom);
            builder.AppendLine("ExperienceTo = " + ExperienceTo);
            builder.AppendLine("Year = " + Year);
            builder.AppendLine("IsProfileAvailable = " + IsProfileAvailable);
            builder.AppendLine("CountryId = " + CountryId);
            return builder.ToString();
        }
    }
}
