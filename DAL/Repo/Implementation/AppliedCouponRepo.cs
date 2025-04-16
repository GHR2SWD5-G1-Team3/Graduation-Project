namespace DAL.Repo.Implementation
{
    public class AppliedCouponRepo(ApplicationDBContext context) : GenericRepo<AppliedCoupon>(context), IAppliedCouponRepo
    {
		public async Task<bool> RemoveAppliedCouponAsync(string userId, long productId, long couponId)
		{
            try
            {
                var appliedCoupon = await GetAsync(ac => ac.UserId == userId && ac.ProductId == productId && ac.CouponId == couponId);
                if (appliedCoupon != null)
                {
                    Db.Remove(appliedCoupon);
                    var result = await Db.SaveChangesAsync();
                    if (result > 0)
                        return true;
                    return false;
                }
                return false;
            } catch
            {
                return false;
            }
			
		}
	}
}
