using BLL.ModelVM.User;
using System.Threading.Tasks;

namespace PLL.Controllers
{
    public class UserController(IUserServices userServices) : Controller
    {
        private readonly IUserServices _services = userServices;
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
        public async Task<ActionResult> Create(AddNewUser newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result=await _services.CreateAsync(newUser);
                    if(result)
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
        public ActionResult Edit()
        {
            return View();
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
                    if(result)
                       return RedirectToAction(nameof(Index));
                    return View(editUser);
                }   
                    return View(editUser);
            }
            catch
            {
                return View(editUser);
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: UserController/Delete/5
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
