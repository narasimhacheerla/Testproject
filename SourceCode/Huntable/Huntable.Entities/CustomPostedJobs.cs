namespace Huntable.Entities
{
    public class CustomPostedJobs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string JobDescription { get; set; }
        public int? CompanyLogoPath { get; set; }
        public int Salary { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string PostedDate { get; set; }
        public int? TotalViews { get; set; }
        public int? TotalApplications { get; set; }
    }
}