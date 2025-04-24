namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryServices categoryServices;
        private readonly UserManager<User> userManager;
        public CategoryController(ICategoryServices categoryServices,UserManager<User> userManager)
        {
            this.categoryServices = categoryServices;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if(await userManager.IsInRoleAsync(user, "Admin"))
            {
                var result = await categoryServices.GetAllActivateCategories(
                    filter: c => c.IsDeleted == false,
                    includeProperty: c => c.SubCategories
                );
                return View(result);
            }
            return View("Home","Error");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM categoryVM)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("SignIn", "Account");
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }
            categoryVM.UserId = user.Id;
            var (success, error) = await categoryServices.CreateAsync(categoryVM);
            if (!success)
            {
                ViewBag.Message = error;
                return View(categoryVM);
            }
            return RedirectToAction("Index", "Category");
        }

        //edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null || !await userManager.IsInRoleAsync(user, "Admin"))
            {
                return View("Home", "Error"); 
            }
            var category = await categoryServices.GetById(id);
            if (category.Item2 == false)
            {
                ViewBag.Message = category.Item3;
            }
            return View(category.Item1);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryVM category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var result = await categoryServices.Edit(User.Identity.Name,id, category);
            if (!result.Item1)
            {
                ViewBag.Departments = await categoryServices.GetAllActivateCategories();
                ViewBag.Message = result.Item2;
                return View(category);
            }

            return RedirectToAction("Index", "Category");
        }
        //delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null || !await userManager.IsInRoleAsync(user, "Admin"))
            {
                return Json(new { success = false, message = "Access Denied" });
            }
            var (isSuccess, message) = await categoryServices.DeleteByID(User.Identity.Name,id);

            if (!isSuccess)
            {
                return Json(new { success = false, message });
            }

            return Json(new { success = true, message = "deleted successfully!" });
        }

    }
}
