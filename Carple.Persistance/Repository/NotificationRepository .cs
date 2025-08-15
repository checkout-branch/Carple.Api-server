using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Interfaces.Repositories;
using Carple.Domain.Enities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Carple.Persistance.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;

        public NotificationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Notification>(
                "NotificationMaster",
                new { FLAG = 1 },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Notification?> GetByIdAsync(int notificationId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Notification>(
                "NotificationMaster",
                new { FLAG = 2, NotificationId = notificationId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(int userId, int? captainId, string message, string notificationType)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                "NotificationMaster",
                new { FLAG = 3, UserId = userId, CaptainId = captainId, Message = message, NotificationType = notificationType },
                commandType: CommandType.StoredProcedure
            );
        }

        //public async Task<bool> MarkAsReadAsync(int notificationId)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    var rowsAffected = await connection.ExecuteAsync(
        //        "NotificationMaster",
        //        new { FLAG = 4, NotificationId = notificationId },
        //        commandType: CommandType.StoredProcedure
        //    );
        //    return rowsAffected > 0;
        //}
        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            using var connection = new SqlConnection(_connectionString);

            try
            {
                await connection.ExecuteAsync(
                    "NotificationMaster",
                    new { FLAG = 4, NotificationId = notificationId },
                    commandType: CommandType.StoredProcedure
                );

                // If no exception from SP, it's a success
                return true;
            }
            catch (SqlException ex)
            {
                // Check for your SP's "not found" error code
                if (ex.Message.Contains("Invalid NotificationId"))
                {
                    return false; // ID doesn't exist
                }

                throw; // Other DB error
            }
        }

    }
}

