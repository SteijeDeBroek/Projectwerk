using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOGets
{
    public class VoteDTO
    {
        public bool Vote1 { get; set; }

        public DateTime Timestamp { get; set; }
        [Required]
        public int RecipeId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}