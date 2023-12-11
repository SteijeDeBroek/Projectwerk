using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOGets
{
    public class RecipeDTOGet
    {
        public int RecipeId { get; set; }

        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int TotalUpvotes { get; set; }

        public int TotalDownvotes { get; set; }

        public DateTime CreationDate { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }
    }
}