namespace pizza_hub.Models.Pizzas;

using System.ComponentModel.DataAnnotations;
using static Data.Validation.Pizza;

public class UpdatePizzaRequestModel
{
    public int Id { get; set; }

    [Required] 
    [MaxLength(MaxDescriptionLenght)]
    public string Description { get; set; }
}
