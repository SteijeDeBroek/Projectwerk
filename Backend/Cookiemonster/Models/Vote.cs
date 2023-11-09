using Cookiemonster.Interfaces;

namespace Cookiemonster.Models
{
    public class Vote : IDeletable
    {
        public bool VoteValue { get; set; }
        public DateTime Timestamp { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public Recipe Recipe { get; set; }
        public User User { get; set; }
        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = false;
    }
}
