using BLL.ModelVM.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryServices subCategoryServices;
        private readonly ICategoryServices categoryServices;

        public SubCategoryController(ISubCategoryServices subCategoryServices, ICategoryServices categoryServices)
        {
            this.subCategoryServices = subCategoryServices;
            this.categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index()
        {
            var result = await subCategoryServices.GetAllSubCategories(
                filter: c => c.IsDeleted == false,
                includeProperty: c => c.Products
            );
            return View(result);
        }
        //create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await categoryServices.GetAllActivateCategories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubCategoryVM subCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await categoryServices.GetAllActivateCategories();
                return View(subCategoryVM);
            }
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
            var user = "Fatma";
            if (!ModelState.IsValid)
            {
                return View(subCategory);
            }
            var result = await subCategoryServices.Edit(id, user, subCategory);
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
            var user = "Admin";
            var (isSuccess, message) = await subCategoryServices.DeleteByID(id, user);

            if (!isSuccess)
            {
                Console.WriteLine($"Delete failed for ID {id}: {message}");
                return Json(new { success = false, message });
            }

            return Json(new { success = true, message = "deleted successfully!" });
        }


    }
}
