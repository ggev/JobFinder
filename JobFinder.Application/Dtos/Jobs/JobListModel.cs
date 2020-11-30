using JobFinder.Domain.Enums;

namespace JobFinder.Application.Dtos.Jobs
{
    public class JobListModel
    {
        public int Id { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyName { get; set; }
        public City Location { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public bool Bookmarked { get; set; }
        public bool AppliedForJob { get; set; }
    }
}