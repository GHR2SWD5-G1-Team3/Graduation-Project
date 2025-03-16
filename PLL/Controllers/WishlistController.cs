using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
