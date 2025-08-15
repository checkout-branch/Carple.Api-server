using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Application.Interfaces;
using Carple.Domain.Entities;

namespace Carple.Insfrastructure.Services
{
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ApiResponse<IEnumerable<Review>>> GetAllReviewsAsync()
        {
            var data = await _reviewRepository.GetAllAsync();
            return new ApiResponse<IEnumerable<Review>>(true, "Fetched successfully", data);
        }

        public async Task<ApiResponse<Review>> GetReviewByIdAsync(int reviewId)
        {
            var data = await _reviewRepository.GetByIdAsync(reviewId);
            if (data == null)
                return new ApiResponse<Review>(false, "Review not found", null);

            return new ApiResponse<Review>(true, "Fetched successfully", data);
        }

        public async Task<ApiResponse<string>> CreateReviewAsync(Review review)
        {
            await _reviewRepository.CreateAsync(review);
            return new ApiResponse<string>(true, "Review created successfully", null);
        }

        public async Task<ApiResponse<string>> UpdateReviewAsync(int reviewId, int rating, string? comment)
        {
            await _reviewRepository.UpdateAsync(reviewId, rating, comment);
            return new ApiResponse<string>(true, "Review updated successfully", null);
        }

        public async Task<ApiResponse<string>> DeleteReviewAsync(int reviewId)
        {
            var success = await _reviewRepository.DeleteAsync(reviewId);
            if (!success)
                return new ApiResponse<string>(false, "Review not found", null);

            return new ApiResponse<string>(true, "Review deleted successfully", null);
        }
    }
}
