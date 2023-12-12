using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cookiemonster.Infrastructure.EFRepository.Models
{
    //public interface IDeletable { }

    public partial class Category : IDeletable
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [NotMapped]
        public string Base64Banner
        {
            get
            {
                return Convert.ToBase64String(BannerBlob);

            }
            set
            {
                BannerBlob = Convert.FromBase64String(value);
            }
        }

        public byte[] BannerBlob { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<RecipeDTOPost> Recipes { get; set; } = new List<RecipeDTOPost>();

        public bool IsDeleted { get; set; }
        public bool IsDeletable { get; } = false;
    }
}
