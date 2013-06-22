using System.Text;

namespace Huntable.Entities.SearchCriteria
{
   public  class JobSearchCriteria
    {
       public string JobTitle { get; set; }
       public string Keywords { get; set; }
       public int JobType { get; set; }
       public int Country { get; set; }
       public string Location { get; set; }
       public int Salary { get; set; }
       public int Experience { get; set; }
       public string ReferenceNo { get; set; }
       public int Industry { get; set; }
       public int Skill { get; set; }
       public string  Company { get; set; }
       public string SkillText { get; set; }
      

       public override string ToString()
       {
           var builder = new StringBuilder();
           builder.AppendLine("JobTitle = " + JobTitle);
           builder.AppendLine("Keywords = " + Keywords);
           builder.AppendLine("JobType = " + JobType);
           builder.AppendLine("Country = " + Country);
           builder.AppendLine("Location = " + Location);
           builder.AppendLine("Salary = " + Salary);
           builder.AppendLine("Experience = " + Experience);
           builder.AppendLine("ReferenceNo = " + ReferenceNo);
           builder.AppendLine("Industry = " + Industry);
           builder.AppendLine("Skill = " + SkillText);
           builder.AppendLine("Company = " + Company);
           return builder.ToString();
       }
    }
}
