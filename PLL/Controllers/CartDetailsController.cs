using BLL.ModelVM.CartDetails;

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

		[HttpPost]
		public IActionResult Create(DisplayCartDetailsVM model)
		{
			if (!ModelState.IsValid)
			{
                var result = _cartDetailsService.AddToCart(model);
				return Ok("Item added successfuly");
            }
			return BadRequest("Missing Data");
		}

		//public IActionResult Delete(long id)
		//{
		//	var result = _cartDetailsService.Delete(id);
		//	return RedirectToAction("Index");
		//}
	}
}
