using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.Domain.Entities
{
    public class JobCategory : BaseEntity
    {
        [ForeignKey(nameof(Job))]
        public int JobId { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Job Job { get; set; }
        public Category Category { get; set; }
    }
}