using Microsoft.AspNetCore.Mvc;
using pizza_hub.Helpers;

namespace pizza_hub.Controllers
{
    public class TestController : ApiController
    {

        [HttpGet("SecretMessage")]
        [CustomAuthorize]
        public IActionResult Get()
        {
            return Ok("Here is your message");
        }
    }
}
