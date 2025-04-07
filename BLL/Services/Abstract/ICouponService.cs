namespace BLL.Services.Abstract
{
    public interface ICouponService
    {
        Task<bool> CreateCouponAsync(Coupon newCoupon);
        Task<bool> UpdateCouponAsync(long couponId, Coupon updatedCoupon, string userName);
        Task<List<Coupon>> GetAllCouponsAsync(Expression<Func<Coupon, bool>>? filter = null, params Expression<Func<Coupon, object>>[] includeProperty);
        Task<Coupon> GetCouponAsync(Expression<Func<Coupon, bool>>? filter = null);
        Task<bool> DeleteCouponAsync(long productId, string userName);
    }
}
