using DAL.Repo.Implementation;

namespace BLL.Services.Implementation
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepo couponRepo;
        public CouponService(ICouponRepo couponRepo)
        {
            this.couponRepo = couponRepo;
        }
        public async Task<bool> CreateCouponAsync(Coupon newCoupon)
        {
            try
            {
                if (newCoupon != null)
                {
                    var result = await couponRepo.CreateAsync(newCoupon);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCouponAsync(long productId, string userName)
        {
            try
            {
                var result = await couponRepo.DeleteById(userName, productId);
                if (result)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCouponAsync(long couponId, Coupon updatedCoupon, string userName)
        {
            try
            {
                var result = await couponRepo.Edit(userName, updatedCoupon, couponId);
                if (result.Item1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Coupon>> GetAllCouponsAsync(Expression<Func<Coupon, bool>>? filter = null, params Expression<Func<Coupon, object>>[] includeProperty)
        {
            try
            {
                return await couponRepo.GetAllAsync(filter, includeProperty);
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        public async Task<Coupon> GetCouponAsync(Expression<Func<Coupon, bool>>? filter = null)
        {
            try
            {
                return await couponRepo.GetAsync(filter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
