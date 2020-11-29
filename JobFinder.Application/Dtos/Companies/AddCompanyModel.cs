using System.ComponentModel.DataAnnotations;
using JobFinder.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Application.Dtos.Companies
{
    public sealed class AddCompanyModel
    {
        [MinLength(2)]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [Required]
        public City Location { get; set; }
        [Required]
        [MinLength(5)]
        public string Address { get; set; }
        [Required]
        public IFormFile Logo { get; set; }
    }
}