using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(string comment, int rate, string userId, long productId);
        Task<bool> EditReviewAsync(long reviewId, string comment, int rate, string userId, long productId);
        Task<bool> DeleteReviewAsync(long reviewId, string userId);
        Task<List<Review>> GetReviewsByProductAsync(long productId);
        Task<Review> GetReviewByIdAsync(long id); // Return single review, instead of Task
    }
}
