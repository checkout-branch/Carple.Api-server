using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Application.Common;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ApiResponse<IEnumerable<Review>>> GetAllReviewsAsync();
        Task<ApiResponse<Review>> GetReviewByIdAsync(int reviewId);
        Task<ApiResponse<string>> CreateReviewAsync(Review review);
        Task<ApiResponse<string>> UpdateReviewAsync(int reviewId, int rating, string? comment);
        Task<ApiResponse<string>> DeleteReviewAsync(int reviewId);
    }
}
