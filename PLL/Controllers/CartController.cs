using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
