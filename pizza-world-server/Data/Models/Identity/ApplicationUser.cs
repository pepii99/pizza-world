namespace pizza_hub.Data.Models.Identity;

using Microsoft.AspNetCore.Identity;
using pizza_hub.Data.Models.Pizza;

public class ApplicationUser : IdentityUser
{
    public override string? UserName { get; set; }

    public string? Password { get; set; }

    public IEnumerable<Pizza> Pizzas { get; } = new HashSet<Pizza>();
}
