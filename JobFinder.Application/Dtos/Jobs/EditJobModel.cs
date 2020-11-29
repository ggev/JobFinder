using JobFinder.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Application.Dtos.Jobs
{
    public class EditJobModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(60)]
        public string Title { get; set; }
        [MinLength(2)]
        public string Description { get; set; }
        public EmploymentType? EmploymentType { get; set; }
    }
}