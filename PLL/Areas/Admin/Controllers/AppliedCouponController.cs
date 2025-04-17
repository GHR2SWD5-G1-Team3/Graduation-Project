namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin, Vendor")]
    [Area("Admin")]
    public class AppliedCouponController : Controller
    {
        private readonly IAppliedCouponService appliedCouponService;
        private readonly IProductService productService;
        private readonly UserManager<User> userManager;
        private readonly ICouponService couponService;

        public AppliedCouponController(IAppliedCouponService appliedCouponService, IProductService productService, UserManager<User> userManager, ICouponService couponService)
        {
            this.appliedCouponService = appliedCouponService;
            this.productService = productService;
            this.userManager = userManager;
            this.couponService = couponService;
        }
        public async Task<IActionResult> Index()
        {
            var appliedCoupons = await appliedCouponService.GetAllAppliedCouponsAsync(null, ac=>ac.Coupon, ac=>ac.User, ac => ac.Product);
            return View(appliedCoupons);
        }
        public async Task<IActionResult> Create()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var userId = currentUser?.Id;
            var isAdmin = await userManager.IsInRoleAsync(currentUser, "Admin");

            IEnumerable<Product> products;

            if (isAdmin)
            {
                products = await productService.GetAllProductsAsync(p => p.IsDeleted == false); // All products for admin
            }
            else
            {
                products = await productService.GetAllProductsAsync(p => p.UserId == userId && p.IsDeleted == false); // Vendor's products only
            }

            var model = new AssignCouponVM
            {
                Products = products,
                Coupons = await couponService.GetAllCouponsAsync(c => c.IsDeleted == false)
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(AssignCouponVM model)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var userId = currentUser?.Id;
            var isAdmin = await userManager.IsInRoleAsync(currentUser, "Admin");

            if (!ModelState.IsValid)
            {
                model.Products = isAdmin
                    ? await productService.GetAllProductsAsync() // all products
                    : await productService.GetAllProductsAsync(p => p.UserId == userId); // vendor's products

                model.Coupons = await couponService.GetAllCouponsAsync(c => !c.IsDeleted);
                return View(model);
            }

            var result = isAdmin
                ? await appliedCouponService.CreateAppliedCouponAsync(new AppliedCoupon(userId, model.ProductId, model.CouponId))
                : await appliedCouponService.AssignCouponToProductAsync(userId, model.ProductId, model.CouponId);

            if (!result.success)
            {
                model.Products = isAdmin
                    ? await productService.GetAllProductsAsync()
                    : await productService.GetAllProductsAsync(p => p.UserId == userId);

                model.Coupons = await couponService.GetAllCouponsAsync(c => !c.IsDeleted);
                TempData["Error"] = "Error Occared";
                ModelState.AddModelError("", "this coupon is already assigned to this product");
                return View(model);
            }

            TempData["success"] = "Coupon Assigned Successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAppliedCoupon(long productId, long couponId)
        {
            await appliedCouponService.RemoveAppliedCouponAsync(userManager.GetUserId(User), productId, couponId);
            return RedirectToAction("Index");
        }
    }
}
