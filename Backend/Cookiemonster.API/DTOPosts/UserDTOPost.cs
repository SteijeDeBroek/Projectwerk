using System.ComponentModel.DataAnnotations;

namespace Cookiemonster.API.DTOPosts
{
    public class UserDTOPost
    {
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;

        public byte Role { get; set; }
    }
}