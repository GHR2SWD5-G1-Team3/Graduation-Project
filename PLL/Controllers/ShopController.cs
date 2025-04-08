using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ShopController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync(null, p=>p.SubCategory,p=>p.SubCategory.Category);
            var mappedProduct = mapper.Map<List<DisplayProductInShopVM>>(products);
            return View(mappedProduct);
        }

    }
}
