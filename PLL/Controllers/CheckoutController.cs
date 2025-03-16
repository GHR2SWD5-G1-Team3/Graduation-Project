using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
