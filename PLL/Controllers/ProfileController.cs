using System.Threading.Tasks;

namespace PLL.Controllers
{
    public class ProfileController(IUserServices userServices) : Controller
    {
        IUserServices _userServices = userServices;
        [HttpGet]
        public async Task<IActionResult> Index(string Id)
        {
            var user = await _userServices.GetAsync(a => a.Id == Id);
            return View(user);
        }
    }
}
