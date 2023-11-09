namespace Cookiemonster.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
        public bool isDeleted { get; set; }
    }
}
