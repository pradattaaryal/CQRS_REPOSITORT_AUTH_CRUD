using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using practices.CQRS.Commands;
using practices.Model;
using System.Threading.Tasks;

namespace practices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly CommandHandler _commandHandler;
        private readonly IConfiguration _configuration;

        public AuthController(CommandHandler commandHandler, IConfiguration configuration)
        {
            _commandHandler = commandHandler;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            try
            {
                var token = await _commandHandler.HandleLoginAsync(loginDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupUserDto signupDto)
        {
            try
            {
                var result = await _commandHandler.HandleSignupAsync(signupDto);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
