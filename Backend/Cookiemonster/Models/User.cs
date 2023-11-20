using Cookiemonster.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.Models
{

    public partial class User : IDeletable
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;
        
        public string Password { get; set; } = null!;

        public byte Role { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
        public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = false;
    }
}