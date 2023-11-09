namespace Cookiemonster.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string URI { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public bool isDeleted { get; set; }
    }
}
