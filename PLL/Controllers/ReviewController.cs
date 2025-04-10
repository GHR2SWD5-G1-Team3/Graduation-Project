using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReview(string comment, int rate, string userId, long productId)
        {
            var result = await _reviewService.AddReviewAsync(comment, rate, userId, productId);
            if (result)
                return Ok("Review added successfully.");
            return BadRequest("Failed to add review.");
        }

        [HttpPut("edit/{reviewId}")]
        public async Task<IActionResult> EditReview(long reviewId, string comment, int rate, string userId, long productId)
        {
            var result = await _reviewService.EditReviewAsync(reviewId, comment, rate, userId, productId);
            if (result)
                return Ok("Review updated successfully.");
            return BadRequest("Failed to update review.");
        }

        [HttpDelete("delete/{reviewId}")]
        public async Task<IActionResult> DeleteReview(long reviewId, string userId)
        {
            var result = await _reviewService.DeleteReviewAsync(reviewId, userId);
            if (result)
                return Ok("Review deleted successfully.");
            return BadRequest("Failed to delete review.");
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProduct(long productId)
        {
            var reviews = await _reviewService.GetReviewsByProductAsync(productId);
            return Ok(reviews);
        }
    }
}
