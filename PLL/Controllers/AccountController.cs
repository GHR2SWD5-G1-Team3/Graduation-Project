namespace PLL.Controllers
{
    public class AccountController(IAccountServices accountServices ,IStringLocalizer<SharedResource> stringLocalizer) : Controller
    {
        private readonly IAccountServices _accountServices=accountServices;
        private readonly IStringLocalizer<SharedResource> SharedLocalizer = stringLocalizer;
        // GET: AccountController/Register
        [HttpGet]
        public async Task<ActionResult> Registeration()
        {
            return View();
        }

        // POST: AccountController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registeration(SignUpvM signUpvM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    signUpvM.Image = UploadFiles.UploadFile("UserPersonnalImages",signUpvM.UploadImage);
                    var result = await _accountServices.SignUp(signUpvM);
                    if (result)
                        return RedirectToAction(nameof(Index),"Home");
                }
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(signUpvM);
            }
            catch
            {
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(signUpvM);
            }
        }
        // GET: AccountController/LogIn
        [HttpGet]
        public async Task<ActionResult> LogIn()
        {
            return View();
        }
        // Post: AccountController/LogIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(SignInVM signInVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result= await _accountServices.SignIn(signInVM.UserName, signInVM.Password);
                    if (result)
                       return RedirectToAction("Index","Home");
                }
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(signInVM);
            }
            catch
            {
                ViewBag.Massege = SharedLocalizer["SomeThingWrong"];
                return View(signInVM);
            }
        }
      

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
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
