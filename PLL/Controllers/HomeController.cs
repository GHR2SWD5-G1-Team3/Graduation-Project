using System.Diagnostics;
using BLL.ModelVM.Home;
using Microsoft.AspNetCore.Mvc;
using PLL.Models;

namespace PLL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService productService;
        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeVM
            {
                TopProducts = await productService.TopProducts(),
                BestProducts = await productService.BestProducts()
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
