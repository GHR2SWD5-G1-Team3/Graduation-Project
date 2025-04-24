using DAL.Repo.Implementation;

namespace BLL.Services.Implementation
{
    public class CouponService(ICouponRepo couponRepo) : ICouponService
    {
        private readonly ICouponRepo couponRepo = couponRepo;

        public async Task<bool> CreateCouponAsync(Coupon newCoupon)
        {
            try
            {
                if (newCoupon != null)
                {
                    var result =  await couponRepo.CreateAsync(newCoupon);
                    return true;
                }
                return false;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> DeleteCouponAsync(long couponId, string userName)
        {
            try
            {
                var result = await couponRepo.DeleteById(userName, couponId);
                if (result)
                    return true;
                return false;
            }
            catch
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
            catch 
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
            catch 
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
            catch
            {
                return null;
            }
        }
        public async Task<bool> PermentDelete(Coupon target)
        {
            try
            {
                var product = await couponRepo.GetAsync(p => p.Id == target.Id);
                if (product == null)
                    return false;
                var result = await couponRepo.PermentDelete(product);
                if (!result) return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
