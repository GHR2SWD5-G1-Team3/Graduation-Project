using BLL.ModelVM.User;
namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CustomerController(IUserServices userServices,IStringLocalizer<SharedResource> stringLocalizer) : Controller
    {
      private readonly IUserServices _userServices =userServices;
        private readonly IStringLocalizer<SharedResource> SharedLocalizer = stringLocalizer;
        public async Task<ActionResult> Index()
        {
            var displayCustomers= await _userServices.GetAllAsync(a=>a.RoleName== "Customer" && a.IsDeleted == false);
            return View(displayCustomers);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AddNewUserVM newUserVM)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userServices.CreateAsync(newUserVM);
                    if (result.Item1)
                        return RedirectToAction(nameof(Index));
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
