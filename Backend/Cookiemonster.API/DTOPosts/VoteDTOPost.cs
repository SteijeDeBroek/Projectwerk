﻿namespace Cookiemonster.API.DTOPosts
{
    public class VoteDTOPost
    {
        public bool Vote1 { get; set; }

        public DateTime Timestamp { get; set; }

        public int RecipeId { get; set; }

        public int UserId { get; set; }
    }
}