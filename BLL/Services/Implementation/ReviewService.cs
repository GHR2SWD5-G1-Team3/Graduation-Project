
namespace BLL.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo _reviewRepo;

        public ReviewService(IReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public async Task<bool> AddReviewAsync(string comment, int rate, string userId, long productId)
        {
            // Perform validation here if necessary (e.g., rate should be between 1 and 5)
            var review = new Review(comment, rate, userId, productId);
            var result = _reviewRepo.Create(review);
            return result.Item1;
        }

        public async Task<bool> EditReviewAsync(long reviewId, string comment, int rate, string userId, long productId)
        {
            var review = _reviewRepo.Get(r => r.Id == reviewId);
            if (review == null || review.IsDeleted)
                return false;

            // Perform additional validation if necessary
            return review.Edit(userId, comment, rate, userId, productId) && _reviewRepo.Update(review);
        }

        public async Task<bool> DeleteReviewAsync(long reviewId, string userId)
        {
            var review = _reviewRepo.Get(r => r.Id == reviewId);
            if (review == null || review.IsDeleted)
                return false;

            review.Delete(userId);
            return _reviewRepo.Update(review);
        }

        public async Task<List<Review>> GetReviewsByProductAsync(long productId)
        {
            return _reviewRepo.GetAll(r => r.ProductId == productId && !r.IsDeleted);
        }

        public Task<Review> GetReviewByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
