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
            var details =await _cartService.GetAllCarts();
            return View(details);
        }

    }
}
