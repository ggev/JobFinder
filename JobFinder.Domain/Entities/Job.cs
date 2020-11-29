using JobFinder.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.Domain.Entities
{
    public class Job : BaseEntity
    {
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public EmploymentType EmploymentType { get; set; }

        public Company Company { get; set; }
        public ICollection<UserJob> UserJobs { get; set; }
        public ICollection<JobCategory> JobCategories { get; set; }
    }
}