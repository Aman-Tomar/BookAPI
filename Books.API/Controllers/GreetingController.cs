using Books.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly IGreetingService greetingService;

        public GreetingController(IGreetingService greetingService)
        {
            this.greetingService = greetingService;
        }

        [HttpGet("{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Greet(string name)
        {
            var greeting = greetingService.Greet(name);
            return Ok(greeting);
        }
    }
}
