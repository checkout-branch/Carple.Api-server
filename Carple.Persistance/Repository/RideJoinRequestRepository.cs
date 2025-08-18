using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Carple.Persistance.Repository
{
    public class RideJoinRequestRepository : IRideJoinRequestRepository
    {
        private readonly string _connectionString;
        public RideJoinRequestRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Create a new join request
        public async Task<int> CreateAsync(RideJoinRequest request)
        {
            using var connection = new SqlConnection(_connectionString);
            var requestId = await connection.ExecuteScalarAsync<int>(
                "RideJoinRequestsMaster",
                new
                {
                    FLAG = 1,
                    RideId = request.RideId,
                    UserId = request.UserId,
                    RequestedSeats = request.RequestedSeats
                },
                commandType: CommandType.StoredProcedure
            );
            return requestId;
        }

        // Get all requests for a specific ride
        public async Task<IEnumerable<RideJoinRequest>> GetByRideIdAsync(int rideId)
        {
            using var connection = new SqlConnection(_connectionString);
            var requests = await connection.QueryAsync<RideJoinRequest>(
                "RideJoinRequestsMaster",
                new { FLAG = 2, RideId = rideId },
                commandType: CommandType.StoredProcedure
            );
            return requests;
        }

        // Update request status (Accept / Reject)
        public async Task<string> UpdateStatusAsync(int requestId, string status)
        {
            using var connection = new SqlConnection(_connectionString);
            var result = await connection.QueryFirstAsync<string>(
                "RideJoinRequestsMaster",
                new
                {
                    FLAG = 3,
                    RequestId = requestId,
                    RequestStatus = status
                },
                commandType: CommandType.StoredProcedure
            );
            return result;
        }
    }
}