using Carple.Application.Interfaces.Services;
using Carple.Domain.Entities;
using Carple.Insfrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideservice _rideservices;
        public RideController(IRideservice rideservices)
        {
            _rideservices = rideservices;
        }

        [HttpPost]

        public async Task<IActionResult> CreateRide([FromBody] Ride ride)
        {

            var result = await _rideservices.CreateRideAsync(ride);


            return Ok(result);

        }

        [HttpPut("{rideId:int}/status")]
        public async Task<IActionResult> UpdateRideStatus(int rideId, [FromBody] string rideStatus)
        {
            var result = await _rideservices.UpdateRideStatusAsync(rideId, rideStatus);
            return Ok(result);
        }

        [HttpDelete("{rideId:int}")]
        public async Task<IActionResult> DeleteRide(int rideId)
        {
            var result = await _rideservices.DeleteRideAsync(rideId);
            return Ok(result);
        }

        [HttpGet("{rideId:int}")]
        public async Task<IActionResult> GetRideById(int rideId)
        {
            var result = await _rideservices.GetRideByIdAsync(rideId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRides()
        {
            var result = await _rideservices.GetAllRidesAsync();
            return Ok(result);
        }

    }
}
