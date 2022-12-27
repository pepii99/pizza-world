using Microsoft.AspNetCore.Mvc;
using pizza_hub.Services.Pizza;
using pizza_hub.Dtos.Pizza;
using pizza_hub.Data.Models.ServiceFactory;
using pizza_hub.Models.Pizzas;
using pizza_hub.Helpers;
using pizza_hub.Data.Models.Identity;

namespace pizza_hub.Controllers;

public class PizzaController : ApiController
{
    private readonly IPizzaService _pizzaService;

    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetPizzaDto>>>> GetAll()
    {
        return await _pizzaService.GetAllAsync();
    }

    [HttpGet("{id}")]
    [CustomAuthorize]
    public async Task<ActionResult<ServiceResponse<PizzaDetailsServiceModel>>> Details(int id)
    {
        return await _pizzaService.Details(id);
    }

    [CustomAuthorize]
    [HttpGet("GetById")]
    public async Task<IEnumerable<PizzaListingServiceModel>> ByUserId()
    {
        var user = GetUser();

        return await _pizzaService.ByUser(user.Id);
    }

    [CustomAuthorize]
    [HttpPost("Create")]
    public async Task<ActionResult<ServiceResponse<GetPizzaDto>>> Create(CreatePizzaModel newPizza)
    {
        var result = await _pizzaService.CreateNewPizza(newPizza);
        return Created(nameof(Created), result);
    }

    [CustomAuthorize]
    [HttpPut("Update")]
    public async Task<ActionResult<ServiceResponse<GetPizzaDto>>> Update(UpdatePizzaRequestModel model)
    {
        var user = GetUser();

        var result = await _pizzaService.Update(model, user.Id);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [CustomAuthorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetPizzaDto>>> Delete(int id)
    {
        var user = GetUser();

        var result = await _pizzaService.Delete(id, user.Id);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    private ApplicationUser GetUser()
    {
        var user = (ApplicationUser)HttpContext.Items["User"];
        return user;
    }
}
