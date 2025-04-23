using BLL.ModelVM.CartDetails;

namespace PLL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var details = await _cartService.GetAllCarts() ?? new List<DisplayCartDetailsVM>();
            return View(details);
        }
        public IActionResult Checkout()
        {
            
            return View();

        }
    }
}
