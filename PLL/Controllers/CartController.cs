using BLL.Services.Abstract;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class CartController : Controller
    {
		private readonly ICartDetailsService _cartDetailsService;

		public CartController(ICartDetailsService cartDetailsService)
		{
			_cartDetailsService = cartDetailsService;
		}

		public IActionResult Index()
        {
            var details = _cartDetailsService.GetAllCartDetails();
            return View(details);
        }

    }
}
