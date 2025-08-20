using Carple.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideJoinRequestController : ControllerBase
    {
        private readonly IRideJoinRequestService _service;
        public RideJoinRequestController(IRideJoinRequestService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] RideJoinRequest request)
        {
            var result = await _service.CreateRequestAsync(request);
            return Ok(result);
        }

        [HttpGet("{rideId:int}")]
        public async Task<IActionResult> GetRequestsByRide(int rideId)
        {
            var result = await _service.GetRequestsByRideIdAsync(rideId);
            return Ok(result);
        }

        [HttpPut("{requestId:int}")]
        public async Task<IActionResult> UpdateRequestStatus(int requestId, [FromQuery] string status)
        {
            var result = await _service.UpdateRequestStatusAsync(requestId, status);
            return Ok(result);
        }
    }
}
