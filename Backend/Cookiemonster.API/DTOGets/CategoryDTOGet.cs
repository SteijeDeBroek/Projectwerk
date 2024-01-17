using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOGets
{
    public class CategoryDTOGet
    {
        public int CategoryId { get; set; }

        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Base64Banner { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
