using Carple.Application.Dto;
using Carple.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var apiKey = await _authService.RegisterAsync(dto);
            return Ok(new { MasterApiKey = apiKey });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }
        [HttpGet("info")]
        public IActionResult Info()
        {
            return Ok(new { Version = "1.0.0", Environment = "Productin" });
        }
        [HttpGet("name")]
        public IActionResult Name()
        {
            return Ok("");

        }
        [HttpGet("throw-generic")]
        public IActionResult ThrowGeneric()
        {
            throw new Exception("Generic unhandled exception occurred.");
        }

    }
}
