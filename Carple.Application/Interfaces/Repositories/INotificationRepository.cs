using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Enities;

namespace Carple.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification?> GetByIdAsync(int notificationId);
        Task<int> CreateAsync(int userId, int? captainId, string message, string notificationType);
        //Task<string> MarkAsReadAsync(int notificationId);
        Task<bool> MarkAsReadAsync(int notificationId);

    }
}
