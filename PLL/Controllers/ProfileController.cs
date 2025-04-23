namespace PLL.Controllers
{
    public class ProfileController(IProfileServices profileServices, UserManager<User> userManager) : Controller
    {
        private readonly IProfileServices _profileServices = profileServices;
        private readonly UserManager<User> _userManager = userManager;
        [HttpGet]
        public async Task<IActionResult> Index(string userId)
        {
            var currentUser= await _userManager.GetUserAsync(User);
            var profileVm = await _profileServices.GetUserProfileAsync(userId,currentUser.Id);
            return View(profileVm);
        }
    }
}
