using Cookiemonster.Infrastructure.EFRepository.Interfaces;

namespace Cookiemonster.Infrastructure.EFRepository.Models
{

    public partial class Image : IDeletable
    {
        public int ImageId { get; set; }

        public string Uri { get; set; } = null!;

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public bool IsDeletable { get; } = false;
    }
}