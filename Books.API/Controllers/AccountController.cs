using Books.API.Services;
using Books.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.logger = logger;
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser()
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Result = "User Registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            logger.LogInformation("User {Username} attempted to log in.", model.Username);
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                logger.LogWarning("User {Username} failed to log in.", model.Username);
                return Unauthorized("Invalid credentials.");
            }

            logger.LogInformation("User {Username} logged in successfully.", model.Username);
            var token = await tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    } 
}
