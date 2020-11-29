using System.ComponentModel.DataAnnotations;
using JobFinder.Domain.Enums;

namespace JobFinder.Application.Dtos.Jobs
{
    public class AddJobModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CompanyId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(60)]
        public string Title { get; set; }
        [Required]
        [MinLength(2)]
        public string Description { get; set; }
        [Required]
        public EmploymentType EmploymentType { get; set; }
    }
}