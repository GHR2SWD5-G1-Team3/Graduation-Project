using BLL.ModelVM.CartDetails;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PLL.Controllers
{
    [Authorize]
    public class CartDetailsController : Controller
	{
		private readonly ICartDetailsService _cartDetailsService;
        private readonly ICouponService _couponService;
        private readonly UserManager<User> _userManager;
        private readonly IAppliedCouponService _appliedCouponService;
        private readonly ICartService _cartService;

        public CartDetailsController(ICartDetailsService cartDetailsService, ICouponService couponService, UserManager<User> userManager, IAppliedCouponService appliedCouponService, ICartService cartService)
		{
			_cartDetailsService = cartDetailsService;
            _couponService = couponService;
            _userManager = userManager;
            _appliedCouponService = appliedCouponService;
            _cartService = cartService;
        }
		
		public async Task<IActionResult> Index()
		{
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userId != null)
			{
				var cartDetails = await _cartDetailsService.GetAllCartDetails(userId);
				var price = _cartDetailsService.GetCartPrice(cartDetails);
				var total = price + 3;
				ViewData["SupTotal"] = price;
				ViewData["Total"] = total;
				return View(cartDetails);
			}
			else return BadRequest();
		}

		[HttpPost]
		public IActionResult Create(DisplayCartDetailsVM model)
		{
			if (!ModelState.IsValid)
			{
                var result = _cartDetailsService.AddToCart(model);
				return Ok("Item added successfuly");
            }
			return BadRequest("Missing Data");
		}
		[HttpPost]
		public IActionResult Delete(long Id)
		{
		   _cartDetailsService.RemoveFromCart(Id);
			return RedirectToAction("Index");
		}
        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(string couponCode)
        {
            // Check if the coupon code exists
            var coupon = await _couponService.GetCouponAsync(c => c.Code == couponCode);
            if (coupon == null)
            {
                TempData["Message"] = "The coupon code is incorrect.";
                return Json(new { message = TempData["Message"], success = false });
                //return RedirectToAction("Index", "CartDetails");
            }

            // Check if the coupon is expired
            if (coupon.ExpiredAt <= DateTime.Now)
            {
                TempData["Message"] = "The coupon code is expired.";
                return Json(new { message = TempData["Message"], success = false });
                //return RedirectToAction("Index", "CartDetails");
            }
            // Check if the user has already applied this coupon
            var user = await _userManager.GetUserAsync(User); // Or use User.Identity.GetUserId() depending on your authentication system
            var userAppliedCoupon = await _appliedCouponService.GetAppliedCouponAsync(uc => uc.UserId == user.Id && uc.CouponId == coupon.Id);

            if (userAppliedCoupon != null)
            {
                // If the coupon has been applied before, show a message
                TempData["Message"] = "You have already applied this coupon.";
                return Json(new { message = TempData["Message"], success = false });
                //return RedirectToAction("Index", "CartDetails");
            }
            // Check if the coupon is assigned to any product
            var appliedProduct = await _appliedCouponService.GetAppliedCouponAsync(ac => ac.CouponId == coupon.Id);
            if (appliedProduct == null)
            {
                TempData["Message"] = "The coupon code is not assigned to any product yet.";
                return Json(new { message = TempData["Message"], success = false });
                //return RedirectToAction("Index", "CartDetails");
            }
            // Check if the product exists in the cart
            var productInCartDetails = await _cartDetailsService.GetCartDetails(user.Id, appliedProduct.ProductId); // Consider using a dynamic cart ID
            if (productInCartDetails == null)
            {
                TempData["Message"] = "The coupon code is not assigned to any of your products.";
                return Json(new { message = TempData["Message"], success = false });
                //return RedirectToAction("Index", "CartDetails");
            }
            var createResult = await _appliedCouponService.CreateAppliedCouponAsync(new(user.Id, productInCartDetails.ProductId, coupon.Id));
            if(!createResult.success)
            {
                TempData["Message"] = "sorry we cannot assign this coupon";
                return Json(new { message = TempData["Message"], success = false });
                //return RedirectToAction("Index", "CartDetails");
            }
            if (coupon.UsedNumber >= coupon.UsageLimit)
            {
                TempData["Message"] = "The coupon code is exceeded its Used Numbers.";
                return Json(new { message = TempData["Message"], success = false });
                //return RedirectToAction("Index", "CartDetails");
            }
            // Apply the discount (calculate the discounted price)
            var discountAmount = (coupon.Discount / 100.0m) * productInCartDetails.Price;
            productInCartDetails.Price = (productInCartDetails.Price - discountAmount);

            coupon.IncreaseUsageNumber(coupon.UsedNumber + 1);
            var result = await _couponService.UpdateCouponAsync(coupon.Id, coupon, User.Identity.Name);


            // Optionally save changes here to the cart or DB
            await _cartDetailsService.UpdateAsync(productInCartDetails);

            // Optionally, you could update the cart total here if needed

            TempData["Message"] = "Coupon applied successfully!";
            return Json(new { message = TempData["Message"], success = true });
            //return RedirectToAction("Index", "CartDetails");
        }
    }
}
