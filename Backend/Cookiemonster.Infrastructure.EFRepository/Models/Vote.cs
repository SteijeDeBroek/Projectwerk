using Cookiemonster.Infrastructure.EFRepository.Interfaces;

namespace Cookiemonster.Infrastructure.EFRepository.Models
{

    public partial class Vote : IDeletable
    {
        public bool Vote1 { get; set; }

        public DateTime Timestamp { get; set; }

        public int RecipeId { get; set; }

        public int UserId { get; set; }

        public virtual RecipeDTOPost Recipe { get; set; } = null!;

        public virtual User User { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public bool IsDeletable { get; } = false;
    }
}