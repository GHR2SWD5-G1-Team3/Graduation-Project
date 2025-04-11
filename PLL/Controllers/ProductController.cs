namespace PLL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ISubCategoryServices subCategoryService;
        private readonly UserManager<User> userManager;

        public ProductController(IProductService productService, IMapper mapper, ISubCategoryServices subCategoryService, UserManager<User> userManager)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.subCategoryService = subCategoryService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync(p=>p.IsDeleted == false,p=> p.User, p=>p.SubCategory.Category );
            return View(products);
        }
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.SubCategories = subCategoryService.GetAllSubCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM model)
        {
            var user = await userManager.GetUserAsync(User);
            model.UserId = user.Id;
            if (ModelState.IsValid)
            {
                model.ImagePath = UploadFiles.UploadFile("img/Products", model.ImageFile);
                var product = mapper.Map<CreateProductVM, Product>(model);
                if (product.UserId == null)
                {
                    return RedirectToAction("SignIn", "Account");
                }
                var result = await productService.CreateProductAsync(product);
                if (!result)
                {
                    ViewBag.SubCategories = subCategoryService.GetAllSubCategories();
                    return View(model);
                }
                return RedirectToAction("Index");

            }
            ViewBag.SubCategories = subCategoryService.GetAllSubCategories();
            return View(model);
        }
        public async Task<IActionResult> Details(long id)
        {
            var product = await productService.GetProductAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
