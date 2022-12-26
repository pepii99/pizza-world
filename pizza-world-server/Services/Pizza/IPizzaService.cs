namespace pizza_hub.Services.Pizza;

using pizza_hub.Data.Models.ServiceFactory;
using pizza_hub.Dtos.Pizza;
using pizza_hub.Models.Pizzas;

public interface IPizzaService
{
    Task<ServiceResponse<List<GetPizzaDto>>> GetAllAsync();

    Task<ServiceResponse<PizzaDetailsServiceModel>> Details(int id);

    Task<ServiceResponse<CreatePizzaModel>> CreateNewPizza(CreatePizzaModel model);

    Task<ServiceResponse<UpdatePizzaRequestModel>> Update(UpdatePizzaRequestModel model, string userId);

    Task<ServiceResponse<GetPizzaDto>> Delete(int id);

    Task<IEnumerable<PizzaListingServiceModel>> ByUser(string userId); 
}
