using Microsoft.AspNetCore.Mvc;
using pizza_hub.Services.Pizza;
using pizza_hub.Dtos.Pizza;
using pizza_hub.Data.Models.ServiceFactory;
using pizza_hub.Models.Pizzas;
using pizza_hub.Helpers;

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
        var result = await _pizzaService.GetAllAsync();

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [CustomAuthorize]
    [HttpGet("GetMessage")]
    public async Task<ActionResult<ServiceResponse<List<GetPizzaDto>>>> GetMessage()
    {
        var result = await _pizzaService.GetAllAsync();

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetPizzaDto>>> GetSingleById(int id)
    {
        var pizza = await _pizzaService.GetPizzaByIdAsync(id);

        if (pizza.Data == null)
            return NotFound();

        return Ok(pizza);
    }

    [CustomAuthorize]
    [HttpPost("Create")]
    public async Task<ActionResult<ServiceResponse<GetPizzaDto>>> Create(CreatePizzaRequestModel newPizza)
    {
        var result = await _pizzaService.CreateNewPizza(newPizza);
        return Created(nameof(Created), result);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetPizzaDto>>> UpdatePizzaAsync(
        UpdatePizzaDto model
    )
    {
        var result = await _pizzaService.UpdatePizzaAsync(model);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpDelete]
    public async Task<ActionResult<ServiceResponse<GetPizzaDto>>> DeletePizzaAsync(int id)
    {
        var result = await _pizzaService.DeletePizzaAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    // POST action

    //[HttpPost]
    //public IActionResult Create(Pizza pizza)
    //{
    //    PizzaService.Add(pizza);
    //    return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
    //}

    //// PUT action

    //[HttpPut("{id}")]
    //public IActionResult Update(int id, Pizza pizza)
    //{
    //    if (id != pizza.Id)
    //        return BadRequest();

    //    var existingPizza = PizzaService.Get(id);
    //    if (existingPizza is null)
    //        return NotFound();

    //    PizzaService.Update(pizza);

    //    return NoContent();
    //}

    //// DELETE action

    //[HttpDelete("{id}")]
    //public IActionResult Delete(int id)
    //{
    //    var pizza = PizzaService.Get(id);

    //    if (pizza is null)
    //        return NotFound();

    //    PizzaService.Delete(id);

    //    return NoContent();
    //}
}
