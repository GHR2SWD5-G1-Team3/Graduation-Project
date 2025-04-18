using BLL.ModelVM.Category;

namespace PLL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryServices categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index()
        {
            var result = await categoryServices.GetAllActivateCategories(
                filter: c => c.IsDeleted == false,
                includeProperty: c => c.SubCategories
            );
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }
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
            var result = await categoryServices.Edit(id, category);
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
            var (isSuccess, message) = await categoryServices.DeleteByID(id);

            if (!isSuccess)
            {
                Console.WriteLine($"Delete failed for ID {id}: {message}");
                return Json(new { success = false, message });
            }

            return Json(new { success = true, message = "deleted successfully!" });
        }

    }
}
