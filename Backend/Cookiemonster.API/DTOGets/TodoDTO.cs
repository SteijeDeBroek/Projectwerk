using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOGets
{
    public class TodoDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}