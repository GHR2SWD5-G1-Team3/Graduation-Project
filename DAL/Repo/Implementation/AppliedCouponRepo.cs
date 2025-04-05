

using Microsoft.EntityFrameworkCore;

namespace DAL.Repo.Implementation
{
    public class AppliedCouponRepo(ApplicationDBContext context) : GenericRepo<AppliedCoupon>(context), IAppliedCouponRepo
    {
        public bool Exists(string userId, long productId, long couponId)
		{
			return Get(ac => ac.UserId == userId && ac.ProductId == productId && ac.CouponId == couponId) != null;
		}

		public AppliedCoupon? GetByIds(string userId, long productId, long couponId)
		{
			return Get(ac => ac.UserId == userId && ac.ProductId == productId && ac.CouponId == couponId);
		}

		public List<AppliedCoupon> GetByProduct(long productId)
		{
			return GetAll(ac => ac.ProductId == productId);
		}

		public List<AppliedCoupon> GetByUser(string userId)
		{
			return GetAll(ac => ac.UserId == userId);
		}
		public void RemoveAppliedCoupon(string userId, long productId, long couponId)
		{
			var appliedCoupon = Get(ac => ac.UserId == userId && ac.ProductId == productId && ac.CouponId == couponId);
			if (appliedCoupon != null)
			{
				Db.AppliedCoupons.Remove(appliedCoupon);
				Db.SaveChanges();
			}
		}
	}
}
