using JobFinder.Domain.Enums;

namespace JobFinder.Application.Dtos.Companies
{
    public class CompanyDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City Location { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public int JobsCount { get; set; }

    }
}