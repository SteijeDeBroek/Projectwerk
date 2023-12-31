﻿using Cookiemonster.Infrastructure.EFRepository.Interfaces;

namespace Cookiemonster.Infrastructure.EFRepository.Models
{
    public class Todo : IDeletable
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public User User { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public bool IsDeletable { get; } = true;
    }
}
