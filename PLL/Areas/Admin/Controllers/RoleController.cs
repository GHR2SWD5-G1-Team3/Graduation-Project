namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController(IUserServices userServices, IRoleServices roleServices,IStringLocalizer<SharedResource> stringLocalizer) : Controller
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IRoleServices _roleServices = roleServices;
        private readonly IStringLocalizer<SharedResource> SharedLocalizer = stringLocalizer;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var displayRoles = await _roleServices.GetAllRoles(a=>a.IsDeleted!=true);
            return View(displayRoles.Item1);
        }
        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string roleName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _roleServices.AddRole(roleName);
                    if (result.Item1)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Massege = result.Item2;
                    return View(roleName);
                }
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(roleName);
            }
            catch
            {
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(roleName);
            }
        }
        // GET: RoleController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string roleid)
        {
                var result = await _roleServices.SoftDeleted(roleid, "Admin");
                return RedirectToAction(nameof(Index));
 
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles()
        {
            var displayCustomers = await _userServices.GetAllAsync ();
            return View(displayCustomers);
        }
        [HttpGet]
        public IActionResult EditAsync(string Id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string Id,string roleName)
        {
            try
            {
                    var result = await _roleServices.UpdatUserRole(Id,roleName);
                    if (result.Item1)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Massege = result.Item2;
                    return View();
            }
            catch
            {
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletedRoles()
        {
            var displayRoles = await _roleServices.GetAllRoles(a => a.IsDeleted == true);
            return View(displayRoles.Item1);
        }


    }
}
