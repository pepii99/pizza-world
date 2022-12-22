namespace pizza_hub.Services.Pizza;

using pizza_hub.Data.Models.ServiceFactory;
using pizza_hub.Dtos.Pizza;
using pizza_hub.Models.Pizzas;

public interface IPizzaService
{
    Task<ServiceResponse<List<GetPizzaDto>>> GetAllAsync();

    Task<ServiceResponse<GetPizzaDto>> GetPizzaByIdAsync(int id);

    Task<ServiceResponse<CreatePizzaRequestModel>> CreateNewPizza(CreatePizzaRequestModel model);

    Task<ServiceResponse<GetPizzaDto>> UpdatePizzaAsync(UpdatePizzaDto model);

    Task<ServiceResponse<GetPizzaDto>> DeletePizzaAsync(int id);

    Task<IEnumerable<PizzaListingResponseModel>> ByUser(string userId); 
}
