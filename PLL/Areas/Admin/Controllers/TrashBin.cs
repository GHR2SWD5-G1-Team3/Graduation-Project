
using BLL.ModelVM.Coupon;

namespace PLL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrashBinController : Controller
    {
        private readonly IProductService productService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly ICouponService couponService;

        public TrashBinController(IProductService productService, UserManager<User> userManager, IMapper mapper, ICouponService couponService)
        {
            this.productService = productService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.couponService = couponService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DeletedProducts()
        {
            var user = await userManager.GetUserAsync(User);
            List<Product> del;
            List<DeletedProductsVM> viewModel;
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                del = await productService.GetAllProductsAsync(p => p.IsDeleted == true, p => p.User, p => p.SubCategory.Category);
                viewModel = mapper.Map<List<DeletedProductsVM>>(del);
                return View(viewModel);
            }
            del = await productService.GetAllProductsAsync(p => p.IsDeleted == true && p.UserId == user.Id, p => p.User, p => p.SubCategory.Category);
            viewModel = del.Select(p => new DeletedProductsVM
            {
                Id = p.Id,
                Name = p.Name,
                UserName = p.User.UserName,
                DeletedOn = p.DeletedOn ?? DateTime.MinValue,
                DeletedBy = p.DeletedBy
            }).ToList();
            return View(viewModel);
        }
        public async Task<IActionResult> DeletedCoupons()
        {
            var user = await userManager.GetUserAsync(User);
            List<Coupon> del;
            List<DeletedCouponVM> viewModel;
            if (await userManager.IsInRoleAsync(user, "Admin"))
            {
                del = await couponService.GetAllCouponsAsync(p => p.IsDeleted == true);
                viewModel = mapper.Map<List<DeletedCouponVM>>(del);
                return View(viewModel);
            }
            del = await couponService.GetAllCouponsAsync(p => p.IsDeleted == true);
            viewModel = del.Select(p => new DeletedCouponVM
            {
                Id = p.Id,
                Code = p.Code,
                CreatedBy = p.CreatedBy,
                DeletedOn = p.DeletedOn ?? DateTime.MinValue,
                DeletedBy = p.DeletedBy
            }).ToList();
            return View(viewModel);
        }
    }
}
