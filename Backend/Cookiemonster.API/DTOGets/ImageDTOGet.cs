namespace Cookiemonster.API.DTOGets
{
    public class ImageDTOGet
    {
        public int ImageId { get; set; }

        public string Base64Image { get; set; } = string.Empty;

        public int RecipeId { get; set; }
    }
}