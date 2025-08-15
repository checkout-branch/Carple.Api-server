using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carple.Domain.Entities;

namespace Carple.Application.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int reviewId);
        Task<int> CreateAsync(Review review);
        Task<int> UpdateAsync(int reviewId, int rating, string? comment);
        Task<bool> DeleteAsync(int reviewId);
    }
}
