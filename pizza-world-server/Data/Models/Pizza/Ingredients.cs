namespace pizza_hub.Data.Models.Pizza;

public class Ingredients
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public IEnumerable<Pizza> Pizzas { get; set; } = new HashSet<Pizza>();
}
