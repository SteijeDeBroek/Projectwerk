using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cookiemonster.Infrastructure.EFRepository.Models
{

    public partial class Image : IDeletable
    {
        public int ImageId { get; set; }

        [NotMapped]
        public string Base64Image
        {
            get
            {
                return Convert.ToBase64String(ImageBlob);

            }
            set
            {
                ImageBlob = Convert.FromBase64String(value);
            }
        }

        public byte[] ImageBlob { get; set; } = null!;

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public bool IsDeletable { get; } = false;
    }
}