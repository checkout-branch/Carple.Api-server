using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Interfaces.Repositories;
using Carple.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Carple.Persistance.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Payment>(
                "PaymentMaster",
                new { FLAG = 1 },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Payment?> GetByIdAsync(int paymentId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Payment>(
                "PaymentMaster",
                new { FLAG = 2, PaymentId = paymentId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(Payment payment)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                "PaymentMaster",
                new
                {
                    FLAG = 3,
                    payment.RideId,
                    payment.PaymentMethod,
                    payment.Amount,
                    payment.PaymentStatus,
                    payment.TransactionId,
                    payment.Latitude,
                    payment.Longitude,
                    payment.Address,
                    payment.Pincode
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
