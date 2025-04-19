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
            var products = await productService.GetAllProductsAsync(filter: p => p.IsDeleted == false, p=>p.SubCategory,p=>p.SubCategory.Category);
            var mappedProduct = mapper.Map<List<DisplayProductInShopVM>>(products);
            return View(mappedProduct);
        }
        public async Task<IActionResult> ProductDetails(long id)
        {
            var product = await productService.GetProductAsync(p => p.Id == id, p => p.SubCategory, product => product.Reviews);
            if (product == null)
            {
                return NotFound();
            }
            var productVM = mapper.Map<ProductDetailsVM>(product);
            return View(productVM);
        }
    }
}
