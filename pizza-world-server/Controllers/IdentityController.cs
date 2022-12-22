using Microsoft.AspNetCore.Mvc;
using pizza_hub.Data.Models.Identity;
using pizza_hub.Services.Identity;


namespace pizza_hub.Controllers;

public class IdentityController : ApiController
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost(nameof(Login))]
    public IActionResult Login(AuthenticateRequest model)
    {
        var response = _identityService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        var result = await _identityService.Register(model);

        if(result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _identityService.GetAll();
        return Ok(users);
    }
}
