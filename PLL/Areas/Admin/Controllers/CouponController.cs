namespace PLL.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Vendor")]
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly ICouponService couponService;
        private readonly IMapper mapper;

        public CouponController(ICouponService couponService, IMapper mapper)
        {
            this.couponService = couponService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var coupons = await couponService.GetAllCouponsAsync(c=>c.IsDeleted == false);
            return View(coupons);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCouponVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            model.CreatedBy = User.Identity.Name;
            var newCoupon = mapper.Map<Coupon>(model);
            await couponService.CreateCouponAsync(newCoupon);
            return RedirectToAction("Index");
        }
        public async Task <IActionResult> Edit(long id)
        {
            var coupon = await couponService.GetCouponAsync(c => c.Id == id && c.IsDeleted == false);
            if (coupon == null)
                return NotFound();
            var model = mapper.Map<EditCouponVM>(coupon);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCouponVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var updatedCoupon = mapper.Map<Coupon>(model);
            await couponService.UpdateCouponAsync(model.Id, updatedCoupon, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            await couponService.DeleteCouponAsync(id, User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
