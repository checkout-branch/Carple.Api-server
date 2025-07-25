using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Caple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { Status = "Healthy" });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new { Status = "Running" });
        }

        [HttpGet("info")]
        public IActionResult Info()
        {
            return Ok(new { Version = "1.0.0", Environment = "Production" });
        }
    }
}
