using Cookiemonster.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookiemonster.Models
{

    public partial class Image : IDeletable
    {
        public int ImageId { get; set; }

        public string Uri { get; set; } = null!;

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;

        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = false;
    }
}