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
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public PizzaService(ApplicationDbContext context, IMapper mapper, IIdentityService identityService)
    {
        _mapper = mapper;
        _context = context;
        _identityService = identityService;
    }

    public async Task<ServiceResponse<List<GetPizzaDto>>> GetAllAsync()
    {

        var pizzas = await _context.Pizzas.ToListAsync();
        var pizzasDto = pizzas.Select(p => _mapper.Map<GetPizzaDto>(p)).ToList();

        return new ServiceResponse<List<GetPizzaDto>> { Data = pizzasDto };
    }

    public async Task<ServiceResponse<GetPizzaDto>> GetPizzaByIdAsync(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);

        var response = ServiceResponseFactory<GetPizzaDto>.CreateServiceResponse();

        response.Data = _mapper.Map<GetPizzaDto>(pizza);

        return response;
    }

    public async Task<ServiceResponse<CreatePizzaRequestModel>> CreateNewPizza(CreatePizzaRequestModel model)
    {
        var response = ServiceResponseFactory<CreatePizzaRequestModel>.CreateServiceResponse();

        var userId = _identityService.GetUserId();

        var pizza = new CreatePizzaRequestModel
        {
            Name = model.Name,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            IsGlutenFree = model.IsGlutenFree,
            ApplicationUserId = userId,
        };

        await _context.Pizzas.AddAsync(_mapper.Map<Pizza>(pizza));

        await _context.SaveChangesAsync();

        response.Data = pizza;

        return response;
    }

    public async Task<ServiceResponse<GetPizzaDto>> UpdatePizzaAsync(UpdatePizzaDto model)
    {
        var response = ServiceResponseFactory<GetPizzaDto>.CreateServiceResponse();

        try
        {
            var pizza = await _context.Pizzas.FindAsync(model.Id);
            if (pizza == null)
            {
                response.Success = false;
                response.Message = "Pizza not found";
                return response;
            }
            pizza.Name = model.Name;
            pizza.IsGlutenFree = model.IsGlutenFree;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<GetPizzaDto>> DeletePizzaAsync(int id)
    {
        var response = ServiceResponseFactory<GetPizzaDto>.CreateServiceResponse();

        try
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                response.Success = false;
                response.Message = "Pizza not found";
                return response;
            }
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<IEnumerable<PizzaListingResponseModel>> ByUser(string userId)
        => await _context
            .Pizzas
            .Where(x => x.ApplicationUserId == userId)
            .Select(p => new PizzaListingResponseModel
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                IsGlutenFree = p.IsGlutenFree,
                Name = p.Name,
            })
            .ToListAsync();
}

//public static List<Pizza> GetAll() => Pizzas;

//public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

//public static void Add(Pizza pizza)
//{
//    pizza.Id = nextId++;
//    Pizzas.Add(pizza);
//}

//public static void Delete(int id)
//{
//    var pizza = Get(id);
//    if (pizza is null)
//        return;

//    Pizzas.Remove(pizza);
//}

//public static void Update(Pizza pizza)
//{
//    var index = Pizzas.FindIndex(p => p.Id == pizza.Id);

//    if (index == -1)
//        return;

//    Pizzas[index] = pizza;
//}
