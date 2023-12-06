using Cookiemonster.Infrastructure.EFRepository.Interfaces;
using Cookiemonster.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookiemonster.Infrastructure.EFRepository.Models
{
    //public interface IDeletable { }

    public partial class Category: IDeletable
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string BannerUri { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public bool IsDeleted { get; set; }
        public bool IsDeletable { get; } = false;
    }
}
