using BLL.ModelVM.CartDetails;
using BLL.ModelVM.Product;

namespace PLL.Controllers
{
    public class ShopController(IProductService productService, IMapper mapper, ICartDetailsService _cartDetailsService) : Controller
    {
        private readonly IProductService productService = productService;
        private readonly IMapper mapper = mapper;
        private readonly ICartDetailsService cartDetailsService = _cartDetailsService;
        
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync(filter: p => p.IsDeleted == false, p => p.SubCategory, p => p.SubCategory.Category);
            var mappedProduct = mapper.Map<List<DisplayProductInShopVM>>(products);
            return View(mappedProduct);
        }
        public async Task<IActionResult> ProductDetails(long id)
        {
            var product = await productService.GetProductAsync(p => p.Id == id, p => p.SubCategory, product => product.Reviews);
            var relatedProducts = await productService.GetAllProductsAsync(p => p.SubCategory.Name == product.SubCategory.Name && p.Id != id, p => p.SubCategory);
            var relatedVMs = relatedProducts.Take(10).Select(p => new RealtedProductsVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.UnitPrice,
                ImageUrl = p.ImagePath,
                SubCategoryName = p.SubCategory.Name,
                ReviewRate = (int)Math.Round((p.Reviews != null && p.Reviews.Any()) ? p.Reviews.Average(r => r.Rate) : 0)
            });
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.RelatedProducts = relatedVMs.ToList();
            var productVM = mapper.Map<ProductDetailsVM>(product);
            return View(productVM);
        }
    }
}
