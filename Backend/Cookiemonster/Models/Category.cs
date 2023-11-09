using Cookiemonster.Interfaces;

namespace Cookiemonster.Models
{
    public class Category : IDeletable
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BannerURI { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = false;
    }
}
