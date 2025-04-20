
using BLL.ModelVM.CartDetails;
using System.Security.Claims;

namespace PLL.Controllers
{
    [Authorize]
    public class CartDetailsController : Controller
	{
		private readonly ICartDetailsService _cartDetailsService;

		public CartDetailsController(ICartDetailsService cartDetailsService)
		{
			_cartDetailsService = cartDetailsService;
		}
		
		public async Task<IActionResult> Index()
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId != null)
			{
				var cartDetails = await _cartDetailsService.GetAllCartDetails(userId);
				var price = _cartDetailsService.GetCartPrice(cartDetails);
				var total = price + 3;
				ViewData["SupTotal"] = price;
				ViewData["Total"] = total;
				return View(cartDetails);
			}
			else return BadRequest();
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

		public IActionResult Delete(long id)
		{
		   _cartDetailsService.RemoveFromCart(id);
			return RedirectToAction("Index");
		}
	}
}
