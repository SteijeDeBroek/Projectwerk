﻿using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOs
{
    public class UserDTO
    {
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        public byte Role { get; set; }
    }
}