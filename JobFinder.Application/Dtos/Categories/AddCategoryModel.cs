using System.ComponentModel.DataAnnotations;

namespace JobFinder.Application.Dtos.Categories
{
    public class AddCategoryModel
    {
        private string _name;

        [MinLength(2)]
        [MaxLength(30)]
        [Required]
        public string Name
        {
            get => _name;
            set => _name = value.TrimStart().TrimEnd();
        }
    }
}