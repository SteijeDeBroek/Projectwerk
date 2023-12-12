using Cookiemonster.Infrastructure.EFRepository.Interfaces;


namespace Cookiemonster.Infrastructure.EFRepository.Models
{

    public partial class RecipeDTOPost : IDeletable
    {
        public int RecipeId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int TotalUpvotes { get; set; }

        public int TotalDownvotes { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();

        public User User { get; set; } = null!;

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();

        public bool IsDeleted { get; set; }
        public bool IsDeletable { get; } = false;
    }
}