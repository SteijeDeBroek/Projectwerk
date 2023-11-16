using Cookiemonster.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookiemonster.Models
{

    public partial class Vote : IDeletable
    {
        public bool Vote1 { get; set; }

        public DateTime Timestamp { get; set; }

        public int RecipeId { get; set; }

        public int UserId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;

        public virtual User User { get; set; } = null!;

        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = false;
    }
}