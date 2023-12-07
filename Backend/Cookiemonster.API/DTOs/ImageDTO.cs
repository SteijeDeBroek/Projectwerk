namespace Cookiemonster.API.DTOs
{
    public class ImageDTO
    {
        public string Base64Image { get; set; } = string.Empty;

        public int RecipeId { get; set; }
    }
}