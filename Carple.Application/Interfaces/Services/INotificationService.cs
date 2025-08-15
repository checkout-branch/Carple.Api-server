using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Domain.Enities;

namespace Carple.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<ApiResponse<IEnumerable<Notification>>> GetAllNotificationsAsync();
        Task<ApiResponse<Notification>> GetNotificationByIdAsync(int notificationId);
        Task<ApiResponse<string>> CreateNotificationAsync(int userId, int? captainId, string message, string notificationType);
        Task<ApiResponse<string>> MarkNotificationAsReadAsync(int notificationId);
    }
}
