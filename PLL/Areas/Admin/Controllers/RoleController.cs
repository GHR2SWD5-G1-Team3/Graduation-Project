namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController(IUserServices userServices, IRoleServices roleServices) : Controller
    {
        private readonly IUserServices _userServices = userServices;
        private readonly IRoleServices _roleServices = roleServices;
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var displayCustomers = await _roleServices.GetAllRoles();
            return View(displayCustomers.Item1);
        }
    }
}
