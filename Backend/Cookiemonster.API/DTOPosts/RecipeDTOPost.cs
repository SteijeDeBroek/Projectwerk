using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOPosts
{
    public class RecipeDTOPost
    {
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int TotalUpvotes { get; set; }

        public int TotalDownvotes { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}