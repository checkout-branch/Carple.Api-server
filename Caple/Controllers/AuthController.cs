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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Invalid credentials");

<<<<<<< Updated upstream
        [HttpGet("info")]
        public IActionResult Info()
        {
            return Ok(new { Version = "1.0.0", Environment = "Productin" });
        }
        [HttpGet("name")]
        public IActionResult Name()
        {
            return Ok(new { Name = "Sanoof" });
=======
            return Ok(result);
>>>>>>> Stashed changes
        }

    }
}
