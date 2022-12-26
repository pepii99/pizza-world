
namespace pizza_hub.Models.Pizzas
{
    public class PizzaListingServiceModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsGlutenFree { get; set; }
    }
}
