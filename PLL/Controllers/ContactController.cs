using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
