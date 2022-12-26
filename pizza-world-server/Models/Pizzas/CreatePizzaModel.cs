namespace pizza_hub.Models.Pizzas;

using System.ComponentModel.DataAnnotations;
using static pizza_hub.Data.Validation.Pizza;

public class CreatePizzaModel
{
    [Required]
    public string? Name { get; set; }

    [Required]
    [MaxLength(MaxDescriptionLenght)]
    public string? Description { get; set; }

    [Required]
    public string? ImageUrl { get; set; }

    public bool IsGlutenFree { get; set; }

    public string? ApplicationUserId { get; set; }

    //public IEnumerable<Ingredients> Ingredients { get; set; } = new HashSet<Ingredients>();
}
