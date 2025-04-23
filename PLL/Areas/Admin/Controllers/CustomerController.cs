namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CustomerController(IUserServices userServices,IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper) : Controller
    {
      private readonly IUserServices _userServices =userServices;
        private readonly IStringLocalizer<SharedResource> SharedLocalizer = stringLocalizer;
        private readonly IMapper _mapper = mapper;

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
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userServices.GetAsync(a => a.Id == id);
            if (user == null) return NotFound();
            var edit = _mapper.Map<EditUser>(user);
            return View(edit);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUser editUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userServices.UpdateAsync(editUser);
                    if (result)
                        return RedirectToAction(nameof(Index));
                    ViewBag.Massege = ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                    return View(editUser);
                }
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(editUser);
            }
            catch
            {
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(editUser);
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
        public async Task<ActionResult> DeleteAsync(string userId)
        {

            var result = await _userServices.DeleteAsync(userId, "Admin");
            return RedirectToAction(nameof(Index));
        }
    }
}

