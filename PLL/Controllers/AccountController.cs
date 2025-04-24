namespace PLL.Controllers
{
    public class AccountController(IAccountServices accountServices ,IStringLocalizer<SharedResource> stringLocalizer) : Controller
    {
        private readonly IAccountServices _accountServices=accountServices;
        private readonly IStringLocalizer<SharedResource> SharedLocalizer = stringLocalizer;

        // GET: AccountController/Register
        [HttpGet]
        public ActionResult Registeration()
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
                    var result = await _accountServices.SignUp(signUpvM);
                    if (result.Item1)
                        return RedirectToAction(nameof(Index),"Home");
                    ViewBag.Massege = result.Item2;
                    return View(signUpvM);
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
        public ActionResult LogIn()
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

        // POST: AccountController/SignOut
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _accountServices.SignOut();
                return RedirectToAction(actionName:"Index",controllerName:"Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}
