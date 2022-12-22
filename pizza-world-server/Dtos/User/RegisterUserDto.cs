using System.ComponentModel.DataAnnotations;

namespace pizza_hub.Dtos.User;

public class RegisterUserDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
