using DAL.Entities;

namespace PLL.Controllers
{
    public class UserController(IUserServices userServices, IStringLocalizer<SharedResource> stringLocalizer, IMapper mapper) : Controller
    {
        private readonly IUserServices _services = userServices;
        private readonly IStringLocalizer<SharedResource> SharedLocalizer = stringLocalizer;
        private readonly IMapper _mapper = mapper;
        // GET: UserController
        public async Task<ActionResult> Index()
        {
            ViewBag.Users= await _services.GetAllAsync(a=>a.RoleName=="User");
            return View();
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
        public async Task<ActionResult> Create(AddNewUserVM newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result=await _services.CreateAsync(newUser);
                    if(result.Item1)
                        return RedirectToAction(nameof(Index));
                    return View(newUser);
                }
                return View(newUser);
            }
            catch
            {
                return View(newUser);
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _services.GetAsync(a => a.Id == id);
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
                    var result = await _services.UpdateAsync(editUser);
                    if (result)
                    {
                        var userId = editUser.Id;
                        return RedirectToAction("Index", "Profile", new { userId });
                    }
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
        public ActionResult Delete()
        {
            return View();
        }

        // POST: UserController/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, string deletedBy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _services.DeleteAsync(id, deletedBy);
                    if (result)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    return View();
                }
                return View();

            }
            catch
            {
                return View();
            }
        }
    }
}
