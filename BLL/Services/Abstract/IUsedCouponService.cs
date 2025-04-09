namespace BLL.Services.Abstract
{
    public interface IUsedCouponService
    {
        Task<bool> CreateUsedCouponAsync(UsedCoupons newUsedCoupon);
        //Task<bool> UpdateUsedCouponAsync(long couponId, UsedCoupons updatedUsedCoupon, string userName);
        Task<List<UsedCoupons>> GetAllUsedCouponsAsync(Expression<Func<UsedCoupons, bool>>? filter = null, params Expression<Func<UsedCoupons, object>>[] includeProperty);
        Task<UsedCoupons> GetUsedCouponAsync(Expression<Func<UsedCoupons, bool>>? filter = null);
        //Task<bool> DeleteUsedCouponAsync(long usedCouponId, string userName);
    }
}
