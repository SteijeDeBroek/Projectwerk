using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOPosts
{
    public class ImageDTOPost
    {
        public string Base64Image { get; set; } = string.Empty;
        [Required]
        public int RecipeId { get; set; }
    }
}