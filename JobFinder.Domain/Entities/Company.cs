using System.Collections.Generic;
using JobFinder.Domain.Enums;

namespace JobFinder.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public City Location { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}