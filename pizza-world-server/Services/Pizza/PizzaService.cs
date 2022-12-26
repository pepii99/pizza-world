namespace pizza_hub.Services.Pizza;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pizza_hub.Data;
using pizza_hub.Data.Models.Pizza;
using pizza_hub.Data.Models.ServiceFactory;
using pizza_hub.Dtos.Pizza;
using pizza_hub.Models.Pizzas;
using pizza_hub.Services.Identity;

public class PizzaService : IPizzaService
{
    private readonly ApplicationDbContext _data;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public PizzaService(ApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _mapper = mapper;
        _data = context;
        _identityService = identityService;
    }

    public async Task<ServiceResponse<List<GetPizzaDto>>> GetAllAsync()
    {

        var pizzas = await _data.Pizzas.ToListAsync();
        var pizzasDto = pizzas.Select(p => _mapper.Map<GetPizzaDto>(p)).ToList();

        return new ServiceResponse<List<GetPizzaDto>> { Data = pizzasDto };
    }

    public async Task<ServiceResponse<PizzaDetailsServiceModel>> Details(int id)
    {
        var response = ServiceResponseFactory<PizzaDetailsServiceModel>.CreateServiceResponse();
        var pizza = await _data.Pizzas.FindAsync(id);

        if (pizza == null)
        {
            response.Success = false;
            response.Message = "Pizza not found";
            return response;
        }

        response.Data = _mapper.Map<PizzaDetailsServiceModel>(pizza);

        return response;
    }

    public async Task<ServiceResponse<CreatePizzaModel>> CreateNewPizza(CreatePizzaModel model)
    {
        var response = ServiceResponseFactory<CreatePizzaModel>.CreateServiceResponse();

        var userId = _identityService.GetUserId();

        var pizza = new CreatePizzaModel
        {
            Name = model.Name,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            IsGlutenFree = model.IsGlutenFree,
            ApplicationUserId = userId,
        };

        await _data.Pizzas.AddAsync(_mapper.Map<Pizza>(pizza));

        await _data.SaveChangesAsync();

        response.Data = pizza;

        return response;
    }

    public async Task<ServiceResponse<UpdatePizzaRequestModel>> Update(UpdatePizzaRequestModel model, string userId = "empty")
    {
        var response = ServiceResponseFactory<UpdatePizzaRequestModel>.CreateServiceResponse();

        var pizza = await _data
            .Pizzas
            .Where(p => p.Id == model.Id && p.ApplicationUserId == userId)
            .FirstOrDefaultAsync();

        if (pizza == null)
        {
            response.Success = false;
            response.Message = "User or pizza not correct";
            return response;
        }

        response.Success = true;
        pizza.Description = model.Description;
        await _data.SaveChangesAsync();

        return response;
    }

    public async Task<ServiceResponse<GetPizzaDto>> DeletePizzaAsync(int id)
    {
        var response = ServiceResponseFactory<GetPizzaDto>.CreateServiceResponse();

        try
        {
            var pizza = await _data.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                response.Success = false;
                response.Message = "Pizza not found";
                return response;
            }
            _data.Pizzas.Remove(pizza);
            await _data.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<IEnumerable<PizzaListingServiceModel>> ByUser(string userId)
        => await _data
            .Pizzas
            .Where(x => x.ApplicationUserId == userId)
            .Select(p => new PizzaListingServiceModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                IsGlutenFree = p.IsGlutenFree,
                Name = p.Name,
            })
            .ToListAsync();
}