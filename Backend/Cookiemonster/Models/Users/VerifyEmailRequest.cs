namespace Cookiemonster.Models.Users;

using System.ComponentModel.DataAnnotations;

public class VerifyEmailRequest
{
    [Required]
    public string Token { get; set; }
}