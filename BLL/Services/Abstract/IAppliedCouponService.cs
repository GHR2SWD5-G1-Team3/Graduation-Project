namespace BLL.Services.Abstract
{
    public interface IAppliedCouponService
    {
        Task<(bool success, string message)> AssignCouponToProductAsync(string userId, long productId, long couponId);
        Task<(bool success, string message)> ApplyCouponAsync(string userId, long productId, long couponId);
        Task<(bool success, string message)> CreateAppliedCouponAsync(AppliedCoupon appliedCoupon);
        Task<AppliedCoupon> GetAppliedCouponAsync(Expression<Func<AppliedCoupon, bool>>? filte = null);
        Task<(bool success, string message)> RemoveAppliedCouponAsync(string userId, long productId, long couponId);
        Task<List<AppliedCoupon>> GetAllAppliedCouponsAsync(Expression<Func<AppliedCoupon, bool>>? filte = null, params Expression<Func<AppliedCoupon, object>>[] includeProperties);
    }
}
