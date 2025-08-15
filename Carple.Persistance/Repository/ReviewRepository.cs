using System.Data;
using Carple.Application.Interfaces.Repositories;
using Carple.Domain.Entities;
using Dapper;

namespace Carple.Persistance.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IDbConnection _db;

        public ReviewRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            if (_db.State != ConnectionState.Open)
                _db.Open();

            return await _db.QueryAsync<Review>(
                "ReviewMaster",
                new { FLAG = 1 },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Review?> GetByIdAsync(int reviewId)
        {
            if (_db.State != ConnectionState.Open)
                _db.Open();

            return await _db.QueryFirstOrDefaultAsync<Review>(
                "ReviewMaster",
                new { FLAG = 2, ReviewId = reviewId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> CreateAsync(Review review)
        {
            if (_db.State != ConnectionState.Open)
                _db.Open();

            return await _db.ExecuteAsync(
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
            if (_db.State != ConnectionState.Open)
                _db.Open();

            return await _db.ExecuteAsync(
                "ReviewMaster",
                new { FLAG = 4, ReviewId = reviewId, Rating = rating, Comment = comment },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> DeleteAsync(int reviewId)
        {
            if (_db.State != ConnectionState.Open)
                _db.Open();

            var result = await _db.QueryFirstOrDefaultAsync<int?>(
                "ReviewMaster",
                new { FLAG = 5, ReviewId = reviewId },
                commandType: CommandType.StoredProcedure
            );

            return result.HasValue && result.Value == 1;
        }
    }
}
