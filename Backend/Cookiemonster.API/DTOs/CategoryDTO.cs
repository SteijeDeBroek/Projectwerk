using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOs
{
    public class CategoryDTO
    {
        [MaxLength(500)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string BannerUri { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
