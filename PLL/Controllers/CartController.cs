using System.Security.Claims;
using BLL.ModelVM.Cart;

using BLL.ModelVM.CartDetails;

namespace PLL.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
		private readonly ICartService _cartService;
        private readonly UserManager<User> userManager;

        public CartController(ICartService cartService,UserManager<User> userManager)
		{
			_cartService = cartService;
            this.userManager = userManager;
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartVM cartVM)
        {
            Console.WriteLine("Received ID: " + cartVM.ProductId);
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "Please log in first" });
            }

            await _cartService.AddProductToCartAsync(user.Id, cartVM.ProductId, cartVM.Price, cartVM.Quantity);
            return Json(new { success = true, message = "Added to Cart Successfully" });
        }
        [HttpGet]
        public async Task<IActionResult> GetCartItemCount()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Json(new { count = 0 });

            var count = await _cartService.GetCartItemCountAsync(user.Id);
            return Json(new { count });
        }

        public async Task<IActionResult> Index()
        {
            var details = await _cartService.GetAllCarts();
            var details = await _cartService.GetAllCarts() ?? new List<DisplayCartDetailsVM>();
            return View(details);
        }
        public IActionResult Checkout()
        {
            
            return View();

        }
    }
}
