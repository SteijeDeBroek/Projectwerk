using Cookiemonster.Interfaces;

namespace Cookiemonster.Models
{
    public class User : IDeletable
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public bool isDeleted { get; set; }
        public bool isDeletable { get; } = false;
    }
}
