namespace PLL.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICartDetailsService cartDetailsService;
        private readonly IFavoriteProductServices favoriteService;
        private readonly UserManager<User> userManager;
        public ShopController(IProductService productService, IMapper mapper, ICartDetailsService cartDetailsService, IFavoriteProductServices favoriteService, UserManager<User> userManager)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.cartDetailsService = cartDetailsService;
            this.favoriteService = favoriteService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(new List<DisplayProductInShopVM>());
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsPage(string category, string subCategory, int page = 1, int pageSize = 8)
        {
            var userId = userManager.GetUserId(User);
            var products = await productService.GetProducts(category, subCategory, page, pageSize);
            var favoriteProductIds = await favoriteService.GetFavoriteProductIds(userId);
            var productVMs = mapper.Map<List<DisplayProductInShopVM>>(products);
            foreach (var vm in productVMs)
            {
                vm.IsFavorite = favoriteProductIds.Contains(vm.Id);
            }
            return PartialView("_ProductCards", productVMs);
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
            var relatedProducts = await productService.GetAllProductsAsync(
            p => p.SubCategory.Name == product.SubCategory.Name && p.Id != id &&!p.IsDeleted, p => p.SubCategory);
            var relatedVMs = relatedProducts.Take(6).Select(p => new RealtedProductsVM
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
