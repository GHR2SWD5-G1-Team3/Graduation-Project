using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IFavoriteProductServices _favoriteService;
        private readonly UserManager<User> _userManager;
        public IActionResult Index()
        {
            return View();
        }
        
        public WishlistController(IFavoriteProductServices favoriteService, UserManager<User> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(long productId)
        {
            var userId = _userManager.GetUserId(User);
            var isFavorite = await _favoriteService.IsFavouriteAsync(userId, productId);

            if (isFavorite)
            {
                var (success, message) = await _favoriteService.Delete(userId, productId);
                return Json(new
                {
                    success = success,
                    message = message,
                    isFavorite = false
                });
            }
            else
            {
                var (success, message) = await _favoriteService.Create(userId, productId);
                return Json(new
                {
                    success = success,
                    message = message,
                    isFavorite = true
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> IsFavourite(long productId)
        {
            var userId = _userManager.GetUserId(User);
            var isFavorite = await _favoriteService.IsFavouriteAsync(userId, productId);
            return Json(new { isFavorite });
        }

    }
}
