using System.ComponentModel.DataAnnotations;

namespace JobFinder.Application.Dtos.Categories
{
    public class EditCategoryModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [MinLength(2)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}