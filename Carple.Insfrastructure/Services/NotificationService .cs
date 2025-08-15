using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Application.Interfaces.Repositories;
using Carple.Application.Interfaces.Services;
using Carple.Domain.Enities;

namespace Carple.Insfrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<ApiResponse<IEnumerable<Notification>>> GetAllNotificationsAsync()
        {
            var data = await _notificationRepository.GetAllAsync();
            return new ApiResponse<IEnumerable<Notification>>(true, "Fetched successfully", data);
        }

        public async Task<ApiResponse<Notification>> GetNotificationByIdAsync(int notificationId)
        {
            var data = await _notificationRepository.GetByIdAsync(notificationId);
            if (data == null)
                return new ApiResponse<Notification>(false, "Notification not found", null);
            return new ApiResponse<Notification>(true, "Fetched successfully", data);
        }

        public async Task<ApiResponse<string>> CreateNotificationAsync(int userId, int? captainId, string message, string notificationType)
        {
            await _notificationRepository.CreateAsync(userId, captainId, message, notificationType);
            return new ApiResponse<string>(true, "Notification created successfully", null);
        }

        //public async Task<ApiResponse<string>> MarkNotificationAsReadAsync(int notificationId)
        //{
        //    var success = await _notificationRepository.MarkAsReadAsync(notificationId);
        //    if (!success)
        //        return new ApiResponse<string>(false, "Notification not found", null);
        //    return new ApiResponse<string>(true, "Notification marked as read", null);
        //}
        public async Task<ApiResponse<string>> MarkNotificationAsReadAsync(int notificationId)
        {
            // Step 1: Get the notification first
            var notification = await _notificationRepository.GetByIdAsync(notificationId);

            if (notification == null)
                return new ApiResponse<string>(false, "Notification not found", null);

            if (notification.IsRead)
                return new ApiResponse<string>(true, "Notification already marked as read", null);

            // Step 2: Mark as read
            var success = await _notificationRepository.MarkAsReadAsync(notificationId);

            if (!success)
                return new ApiResponse<string>(false, "Notification not found", null);

            return new ApiResponse<string>(true, "Notification marked as read", null);
        }

    }
}
