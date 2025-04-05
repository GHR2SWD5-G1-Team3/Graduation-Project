namespace BLL.Services.Abstract
{
    public interface IAppliedCouponService
    {
		(bool success, string message) ApplyCoupon(string userId, long productId, long couponId);
		List<AppliedCoupon> GetAppliedCouponsByUser(string userId);
		List<AppliedCoupon> GetAppliedCouponsByProduct(long productId);
		(bool success, string message) RemoveAppliedCoupon(string userId, long productId, long couponId);
	}
}
