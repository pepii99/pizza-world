using pizza_hub.Data.Models.Identity;

namespace pizza_hub.Models.Pizzas
{
    public class PizzaDetailsServiceModel : PizzaListingServiceModel
    {
        public string Description { get; set; }

        public string ApplicationUserId { get; set; }
        public string UserName { get; set; }
    }
}
