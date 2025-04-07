namespace BLL.Services.Abstract
{
    public interface IAppliedCouponService
    {
        Task<(bool success, string message)> AssignCouponToProduct(string userId, long productId, long couponId);
        Task<(bool success, string message)> ApplyCoupon(string userId, long productId, long couponId);
        Task<AppliedCoupon> GetAppliedCoupon(Expression<Func<AppliedCoupon, bool>>? filte = null);
        Task<(bool success, string message)> RemoveAppliedCoupon(string userId, long productId, long couponId);
        Task<List<AppliedCoupon>> GetAllAppliedCoupons(Expression<Func<AppliedCoupon, bool>>? filte = null, params Expression<Func<AppliedCoupon, object>>[] includeProperties);
    }
}
