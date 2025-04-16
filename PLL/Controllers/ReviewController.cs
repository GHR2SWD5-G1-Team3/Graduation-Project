using PLL.Models;

namespace PLL.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<IdentityUser> _userManager;

        public ReviewController(IReviewService reviewService, UserManager<IdentityUser> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        public async Task<IActionResult> ProductReviews(long productId)
        {
            var reviews = await _reviewService.GetReviewsByProductAsync(productId);
            ViewBag.ProductId = productId;
            return View(reviews);
        }

        public IActionResult AddReview(long productId)
        {
            return View(new ReviewViewModel { ProductId = productId });
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);
            var result = await _reviewService.AddReviewAsync(model.Comment, model.Rate, userId, model.ProductId);

            if (result)
                return RedirectToAction("ProductReviews", new { productId = model.ProductId });

            ModelState.AddModelError("", "Failed to add review.");
            return View(model);
        }

        public async Task<IActionResult> EditReview(long id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null) return NotFound();

            var model = new ReviewViewModel
            {
                Id = review.Id,
                Comment = review.Comment,
                Rate = review.Rate,
                ProductId = review.ProductId,
                UserId = review.UserId,
                CreatedAt = review.CreatedAt
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditReview(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);
            var result = await _reviewService.EditReviewAsync(model.Id, model.Comment, model.Rate, userId, model.ProductId);

            if (result)
                return RedirectToAction("ProductReviews", new { productId = model.ProductId });

            ModelState.AddModelError("", "Failed to update review.");
            return View(model);
        }

        public async Task<IActionResult> DeleteReview(long id, long productId)
        {
            var userId = _userManager.GetUserId(User);
            await _reviewService.DeleteReviewAsync(id, userId);

            return RedirectToAction("ProductReviews", new { productId });
        }
    }
}
