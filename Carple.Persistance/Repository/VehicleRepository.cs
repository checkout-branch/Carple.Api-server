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
    public class VehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString;

        public VehicleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> CreateAsync(Vehicle vehicle)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                "VehicleMaster",
                new
                {
                    FLAG = 2,
                    vehicle.VehicleId,
                    vehicle.CaptainId,
                    vehicle.OwnerId,
                    vehicle.VehicleNumber,
                    vehicle.Model,
                    vehicle.Brand,
                    vehicle.Type,
                    vehicle.RCBook,
                    vehicle.InsuranceCopy,
                    vehicle.Color,
                    vehicle.SeatingCapacity,
                    vehicle.Features,
                    vehicle.VehicleImage
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Vehicle>(
                "VehicleMaster",
                new { FLAG = 1 },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<Vehicle?> GetByIdAsync(int vehicleId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Vehicle>(
                "VehicleMaster",
                new { FLAG = 5, VehicleId = vehicleId },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<int> UpdateAsync(Vehicle vehicle)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                "VehicleMaster",
                new
                {
                    FLAG = 3,
                    vehicle.VehicleId,
                    vehicle.VehicleNumber,
                    vehicle.Model,
                    vehicle.Brand,
                    vehicle.Type,
                    vehicle.RCBook,
                    vehicle.InsuranceCopy,
                    vehicle.Color,
                    vehicle.SeatingCapacity,
                    vehicle.Features,
                    vehicle.VehicleImage

                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<int> DeleteAsync(int vehicleId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                "VehicleMaster",
                new { FLAG = 4, VehicleId = vehicleId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Vehicle>> GetByCaptainIdAsync(int captainId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Vehicle>(
                "VehicleMaster",
                new { FLAG = 6, CaptainId = captainId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> MarkAsVerifiedAsync(int vehicleId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                "VehicleMaster",
                new { FLAG = 7, VehicleId = vehicleId },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}