namespace PLL.Controllers
{
    public class ShopController(IProductService productService, IMapper mapper, ICartDetailsService _cartDetailsService) : Controller
    {
        private readonly IProductService productService = productService;
        private readonly IMapper mapper = mapper;
        private readonly ICartDetailsService cartDetailsService = _cartDetailsService;
        
        public async Task<IActionResult> Index()
        {
            return View(new List<DisplayProductInShopVM>());
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsPage(string category, string subCategory, int page = 1, int pageSize = 8)
        {
            var products = await productService.GetProducts(category, subCategory, page, pageSize);
            return PartialView("_ProductCards", products);
        }
        [HttpGet]
        public async Task<IActionResult> SearchProducts(string query)
        {
            var products = await productService.GetProducts("", "", 1, int.MaxValue); 
            var filtered = products.Where(p =>
                p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return Ok(mapper.Map<List<DisplayProductInShopVM>>(filtered));
        }

        [HttpPost]
        public async Task<IActionResult> RenderProductCards([FromBody] List<DisplayProductInShopVM> products)
        {
            return PartialView("_ProductCards", products);
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
