namespace BLL.Services.Implementation
{
    public class UsedCouponService : IUsedCouponService
    {
        private readonly IUsedCouponRepo usedCouponRepo;

        public UsedCouponService(IUsedCouponRepo usedCouponRepo)
        {
            this.usedCouponRepo = usedCouponRepo;
        }

        public async Task<bool> CreateUsedCouponAsync(UsedCoupons newUsedCoupon)
        {
            try
            {
                if (newUsedCoupon != null)
                {
                    var result = await usedCouponRepo.CreateAsync(newUsedCoupon);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<UsedCoupons>> GetAllUsedCouponsAsync(Expression<Func<UsedCoupons, bool>>? filter = null, params Expression<Func<UsedCoupons, object>>[] includeProperty)
        {
            try
            {
                return await usedCouponRepo.GetAllAsync(filter, includeProperty);
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        public async Task<UsedCoupons> GetUsedCouponAsync(Expression<Func<UsedCoupons, bool>>? filter = null)
        {
            try
            {
                return await usedCouponRepo.GetAsync(filter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
