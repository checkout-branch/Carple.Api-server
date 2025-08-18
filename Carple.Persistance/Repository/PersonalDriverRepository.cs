using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Interfaces.Repositories;
using Carple.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Carple.Persistance.Repository
{
    public class PersonalDriverRepository : IPersonalDriverRepository
    {
        private readonly string _connectionString;

        public PersonalDriverRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // View all personal driver rides
        public async Task<IEnumerable<Ride>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Ride>(
                "PersonalDriver",
                new { FLAG = 1 },
                commandType: CommandType.StoredProcedure
            );
        }

        // Insert new personal driver ride
        public async Task<int> CreateAsync(Ride ride)
        {
            using var conn = new SqlConnection(_connectionString);

            var rideId = await conn.ExecuteScalarAsync<int>(
                "PersonalDriver",
                new
                {
                    FLAG = 2,
                    ride.UserId,
                    ride.CaptainId,
                    ride.PickupLatitude,
                    ride.PickupLongitude,
                    ride.PickupAddress,
                    ride.PickupPincode,
                    ride.DropLatitude,
                    ride.DropLongitude,
                    ride.DropAddress,
                    ride.DropPincode,
                    ride.PickupLocationId,
                    ride.DropLocationId,
                    ride.DistanceInKm,
                    ride.EstimatedFare,
                    ride.ActualFare
                },
                commandType: CommandType.StoredProcedure
            );

            return rideId;
        }

        // Update ride status
        public async Task UpdateStatusAsync(int rideId, string status)
        {
            using var conn = new SqlConnection(_connectionString);

            await conn.ExecuteAsync(
                "PersonalDriver",
                new
                {
                    FLAG = 3,
                    RideId = rideId,
                    RideStatus = status
                },
                commandType: CommandType.StoredProcedure
            );
        }

        // Get ride by ID
        public async Task<Ride?> GetByIdAsync(int rideId)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Ride>(
                "PersonalDriver",
                new { FLAG = 4, RideId = rideId },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}