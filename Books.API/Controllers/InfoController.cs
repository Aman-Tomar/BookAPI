using Books.API.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IOptions<MySettings> settings;

        public InfoController(IOptions<MySettings> settings)
        {
            this.settings = settings;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(new
            {
                AppName = settings.Value.ApplicationName,
                Version = settings.Value.Version,
            });
        }
    }
}
