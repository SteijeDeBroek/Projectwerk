using Cookiemonster.Interfaces;

namespace Cookiemonster.Models
{
    public class Recipe : IDeletable
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalUpvotes { get; set; }
        public int TotalDownvotes { get; set; }
        public DateTime CreationDate { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public Category RecipeCategory { get; set; }
        public User User { get; set; }
        public List<Vote> Votes { get; set; }
        public List<Todo> Todos { get; set; }
        public List<Image> Images { get; set; }
        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = false;
    }
}
