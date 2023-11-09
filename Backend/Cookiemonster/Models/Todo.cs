using Cookiemonster.Interfaces;

namespace Cookiemonster.Models
{
    public class Todo : IDeletable
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public User User { get; set; }
        public Recipe Recipe { get; set; }
        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = true;
    }
}
