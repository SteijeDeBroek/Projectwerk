using Cookiemonster.Interfaces;

namespace Cookiemonster.Models
{
    public class Todo : IDeletable
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = true;
    }
}
