using NuGet.Packaging.Signing;

namespace PLL.Areas.Admin.Controllers
{
    [Authorize]   
    [Area("Admin")]
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
            var products = await productService.GetAllProductsAsync(p => p.IsDeleted == false, p => p.User, p => p.SubCategory.Category);
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.SubCategories = await subCategoryService.GetAllSubCategories();
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
            var productVM = mapper.Map<ProductDetailsVM>(product);
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var userName = await userManager.GetUserAsync(User);
            await productService.DeleteAsync(id, userName.Email);
            return RedirectToAction("Index", "Product");
        }
        public async Task<IActionResult> Edit(long id)
        {
            var user = await userManager.GetUserAsync(User);
            var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
            var product = await productService.GetProductAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            if (product.UserId == user.Id || isAdmin)
            {
                var editproduct = mapper.Map<EditProductVM>(product);
                ViewBag.SubCategories = await subCategoryService.GetAllSubCategories();
                return View(editproduct);
            }

            return RedirectToAction("AccessDenied");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductVM model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.SubCategories = subCategoryService.GetAllSubCategories();
                return View(model);
            }
            var product = await productService.GetProductAsync(p => p.Id == model.Id);
            if(product == null)
            {
                return NotFound();
            }
            if(model.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Products", product.ImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                model.ImagePath = UploadFiles.UploadFile("img/Products", model.ImageFile);
            }
            else
            {
                model.ImagePath = product.ImagePath;
            }
            var updatedProduct = mapper.Map(model, product);
            var userName = User.Identity.Name;
            await productService.EditAsync(updatedProduct.Id,updatedProduct, userName);
            return RedirectToAction("Index","Product");
        }
    }
}
