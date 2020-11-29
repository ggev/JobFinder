using JobFinder.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Application.Dtos.Companies
{
    public class EditCompanyModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }
        public City? Location { get; set; }
        [MinLength(5)]
        public string Address { get; set; }
        public IFormFile Logo { get; set; }
    }
}
