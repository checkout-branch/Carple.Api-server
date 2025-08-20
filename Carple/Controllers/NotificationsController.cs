using Carple.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carple.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController:ControllerBase
    {

        private readonly INotificationService _notificationService;
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationService.GetAllNotificationsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _notificationService.GetNotificationByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int userId, int? captainId, string message, string notificationType)
        {
            var result = await _notificationService.CreateNotificationAsync(userId, captainId, message, notificationType);
            return Ok(result);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _notificationService.MarkNotificationAsReadAsync(id);
            return Ok(result);
        }
    }
}
