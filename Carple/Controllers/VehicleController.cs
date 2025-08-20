using Carple.Application.Common;
using Carple.Application.Interfaces.Services;
using Carple.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> CreateVehicle([FromBody] Vehicle vehicle)
        {
            var response = await _vehicleService.CreateVehicleAsync(vehicle);

            return Ok(response);
        }
        [HttpGet]

        public async Task<ActionResult<ApiResponse<IEnumerable<Vehicle>>>> GetAllVehicles()
        {
            var response = await _vehicleService.GetAllVehiclesAsync();
            if (response.Data == null || !response.Data.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<Vehicle>>(false, "No vehicles found", null));
            }
            return Ok(response);
        }
        [HttpGet("{vehicleId:int}")]
        public async Task<ActionResult<ApiResponse<Vehicle?>>> GetVehicleById(int vehicleId)
        {
            if (vehicleId <= 0)
            {
                return BadRequest(new ApiResponse<Vehicle?>(false, "Invalid VehicleId", null));
            }
            var response = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (response.Data == null)
            {
                return NotFound(new ApiResponse<Vehicle?>(false, "Vehicle not found", null));
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<string>>> UpdateVehicle([FromBody] Vehicle vehicle)
        {
            if (vehicle == null || vehicle.VehicleId <= 0)
            {
                return BadRequest(new ApiResponse<string>(false, "Invalid Vehicle data", null));
            }

            var response = await _vehicleService.UpdateVehicleAsync(vehicle);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("captain/{captainId:int}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Vehicle>>>> GetVehiclesByCaptain(int captainId)
        {
            if (captainId <= 0)
                return BadRequest(new ApiResponse<IEnumerable<Vehicle>>(false, "Invalid CaptainId", null));

            var response = await _vehicleService.GetVehiclesByCaptainIdAsync(captainId);
            if (response.Data == null || !response.Data.Any())
                return NotFound(new ApiResponse<IEnumerable<Vehicle>>(false, "No vehicles found for this captain", null));

            return Ok(response);
        }

        [HttpPut("verify/{vehicleId:int}")]
        public async Task<ActionResult<ApiResponse<string>>> MarkVehicleAsVerified(int vehicleId)
        {
            if (vehicleId <= 0)
                return BadRequest(new ApiResponse<string>(false, "Invalid VehicleId", null));

            var response = await _vehicleService.MarkVehicleAsVerifiedAsync(vehicleId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        [HttpDelete("{vehicleId:int}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteVehicle(int vehicleId)
        {
            if (vehicleId <= 0)
                return BadRequest(new ApiResponse<string>(false, "Invalid VehicleId", null));

            var response = await _vehicleService.DeleteVehicleAsync(vehicleId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
