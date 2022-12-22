using System.ComponentModel.DataAnnotations;

namespace pizza_hub.Data.Models.Identity;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
