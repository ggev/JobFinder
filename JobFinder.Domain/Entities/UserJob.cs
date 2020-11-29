using System.ComponentModel.DataAnnotations.Schema;

namespace JobFinder.Domain.Entities
{
    public class UserJob : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [ForeignKey(nameof(Job))]
        public int JobId { get; set; }

        public bool Bookmarked { get; set; }
        public bool AppliedForJob { get; set; }

        public User User { get; set; }
        public Job Job { get; set; }
    }
}