using System.Collections.Generic;

namespace JobFinder.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<JobCategory> JobCategories { get; set; }
    }
}