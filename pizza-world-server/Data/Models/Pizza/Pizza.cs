namespace pizza_hub.Data.Models.Pizza;

using System.ComponentModel.DataAnnotations;
using pizza_hub.Data.Models.Identity;
using static Validation.Pizza;

public class Pizza
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    [MaxLength(MaxDescriptionLenght)]
    public string? Description { get; set; }

    [Required]
    public string? ImageUrl { get; set; }

    public bool IsGlutenFree { get; set; }

    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public IEnumerable<Ingredients> Ingredients { get; set; } = new HashSet<Ingredients>();
}
