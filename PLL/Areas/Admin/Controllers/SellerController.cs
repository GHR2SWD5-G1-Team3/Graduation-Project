using BLL.ModelVM.User;
using BLL.Services.Implementation;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SellerController(IUserServices userServices, IStringLocalizer<SharedResource> stringLocalizer) : Controller
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IStringLocalizer<SharedResource> SharedLocalizer = stringLocalizer;

        // GET: SellerController
        public async Task<ActionResult> IndexAsync()
        {
            var displayCustomers = await _userServices.GetAllAsync(a => a.RoleName == "Vendor" && a.IsDeleted == false);
            return View(displayCustomers);
        }

        // GET: SellerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddNewUserVM newUserVM)
        {
            try
            {
                if (ModelState.IsValid) 
                { 
                    var result= await _userServices.CreateAsync(newUserVM);
                    if(result.Item1)
                        return RedirectToAction(nameof(IndexAsync));
                    ViewBag.Massege = result.Item2;
                    return View(newUserVM);
                }
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(newUserVM);
            }
            catch
            {
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(newUserVM);
            }
        }

        // GET: SellerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
