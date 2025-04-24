using BLL.ModelVM.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryServices subCategoryServices;
        private readonly ICategoryServices categoryServices;
        private readonly UserManager<User> userManager;

        public SubCategoryController(ISubCategoryServices subCategoryServices, ICategoryServices categoryServices, UserManager<User> userManager)
        {
            this.subCategoryServices = subCategoryServices;
            this.categoryServices = categoryServices;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                var result = await subCategoryServices.GetAllSubCategories(
                filter: c => c.IsDeleted == false,
                includeProperty: c => c.Products
                );
                return View(result);
            }
            return View("Home", "Error");
        }
        //create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await categoryServices.GetAllActivateCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSubCategoryVM subCategoryVM)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("SignIn", "Account");
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await categoryServices.GetAllActivateCategories();
                return View(subCategoryVM);
            }
            subCategoryVM.UserId = user.Id;
            var (success, error) = await subCategoryServices.CreateAsync(subCategoryVM);
            if (!success)
            {
                ViewBag.Categories = await categoryServices.GetAllActivateCategories();
                ViewBag.Message = error;
                return View(subCategoryVM);
            }

            return RedirectToAction("Index", "SubCategory");
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
            var subcategory = await subCategoryServices.GetById(id);
            if (subcategory.Item2 == false)
            {
                ViewBag.Message = subcategory.Item3;
            }
            ViewBag.Categories = await categoryServices.GetAllActivateCategories();
            return View(subcategory.Item1);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SubCategoryVM subCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(subCategory);
            }
            var result = await subCategoryServices.Edit(User.Identity.Name,id, subCategory);
            if (!result.Item1)
            {
                ViewBag.Categories = await categoryServices.GetAllActivateCategories();
                ViewBag.Message = result.Item2;
                return View(subCategory);
            }

            return RedirectToAction("Index", "SubCategory");
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
            var (isSuccess, message) = await subCategoryServices.DeleteByID(User.Identity.Name, id);

            if (!isSuccess)
            {
                return Json(new { success = false, message });
            }

            return Json(new { success = true, message = "deleted successfully!" });
        }


    }
}
