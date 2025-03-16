using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
