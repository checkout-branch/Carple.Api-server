using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Interfaces;
using Carple.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Carple.Persistance.Repository
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly IDbConnection _dbConnection;

        public ReviewRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<Review>(
                "ReviewMaster",
                new { FLAG = 1 },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Review?> GetByIdAsync(int reviewId)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<Review>(
                "ReviewMaster",
                new { FLAG = 2, ReviewId = reviewId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(Review review)
        {
            return await _dbConnection.ExecuteAsync(
                "ReviewMaster",
                new
                {
                    FLAG = 3,
                    review.RideId,
                    review.UserId,
                    review.CaptainId,
                    review.Rating,
                    review.Comment
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> UpdateAsync(int reviewId, int rating, string? comment)
        {
            return await _dbConnection.ExecuteAsync(
                "ReviewMaster",
                new { FLAG = 4, ReviewId = reviewId, Rating = rating, Comment = comment },
                commandType: CommandType.StoredProcedure
            );
        }

        //public async Task<bool> DeleteAsync(int reviewId)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    var rowsAffected = await connection.ExecuteAsync(
        //        "ReviewMaster",
        //        new { FLAG = 5, ReviewId = reviewId },
        //        commandType: CommandType.StoredProcedure
        //    );
        //    return rowsAffected > 0;
        //}
        public async Task<bool> DeleteAsync(int reviewId)
        {
            var result = await _dbConnection.QueryFirstOrDefaultAsync<int?>(
                "ReviewMaster",
                new { FLAG = 5, ReviewId = reviewId },
                commandType: CommandType.StoredProcedure
            );
            return result.HasValue && result.Value == 1;
        }

    }
}
