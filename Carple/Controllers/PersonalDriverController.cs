using Carple.Application.Interfaces.Services;
using Carple.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalDriverController : ControllerBase
    {
        private readonly IPersonalDriverService _service;

        public PersonalDriverController(IPersonalDriverService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRides()
        {
            var result = await _service.GetAllRidesAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRide([FromBody] Ride ride)
        {
            var result = await _service.CreateRideAsync(ride);
            return Ok(result);
        }

        [HttpPut("{rideId}/status")]
        public async Task<IActionResult> UpdateRideStatus(int rideId, [FromQuery] string status)
        {
            var result = await _service.UpdateRideStatusAsync(rideId, status);
            return Ok(result);
        }

        [HttpGet("{rideId}")]
        public async Task<IActionResult> GetRideById(int rideId)
        {
            var result = await _service.GetRideByIdAsync(rideId);
            return Ok(result);
        }
    }
}
