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
    public class RideRepository : IRideRepository
    {

        private readonly string _connectionString;
        public RideRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CreateAsync(Ride ride)
        {
            using var connection = new SqlConnection(_connectionString);

            var rideId = await connection.ExecuteScalarAsync<int>(
                "Ridemaster",  
                new
                {
                    FLAG = 2,  
                    ride.RideType,
                    ride.UserId,
                    ride.CaptainId,
                    ride.VehicleId,
                    ride.PickupLocationId,
                    ride.DropLocationId,
                    ride.RideStatus,
                    ride.ScheduledTime,
                    ride.RideDate,
                    ride.MaxPassengers,
                    ride.CurrentPassengers,
                    ride.EstimatedFare,
                    ride.ActualFare,
                    ride.PickupLatitude,
                    ride.PickupLongitude,
                    ride.PickupAddress,
                    ride.PickupPincode,
                    ride.DropLatitude,
                    ride.DropLongitude,
                    ride.DropAddress,
                    ride.DropPincode,
                    ride.DistanceInKm
                },
                commandType: CommandType.StoredProcedure
            );

            return rideId; 
        }
        public async Task<int> UpdateAsync(int rideId, string rideStatus)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.ExecuteAsync(
                "Ridemaster",
                new { FLAG = 3, RideId = rideId, RideStatus = rideStatus },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<int> DeleteAsync(int rideId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.ExecuteAsync(
                "Ridemaster",
                new { FLAG = 4, RideId = rideId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Ride?> GetByIdAsync(int rideId)
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryFirstOrDefaultAsync<Ride>(
                "Ridemaster",
                new { FLAG = 5, RideId = rideId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Ride>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<Ride>(
                "Ridemaster",
                new { FLAG = 6 },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
