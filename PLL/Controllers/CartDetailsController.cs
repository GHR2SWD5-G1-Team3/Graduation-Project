
namespace PLL.Controllers
{
	public class CartDetailsController : Controller
	{
		private readonly ICartDetailsService _cartDetailsService;

		public CartDetailsController(ICartDetailsService cartDetailsService)
		{
			_cartDetailsService = cartDetailsService;
		}

		public IActionResult Index()
		{
			var cartDetails = _cartDetailsService.GetAllCartDetails();
			return View(cartDetails);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CartDetails model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = _cartDetailsService.AddToCart(model);
			if (result.Item1)
			{
				return RedirectToAction("Index");
			}
			ModelState.AddModelError("", result.Item2);
			return View(model);
		}

		//public IActionResult Delete(long id)
		//{
		//	var result = _cartDetailsService.Delete(id);
		//	return RedirectToAction("Index");
		//}
	}
}
