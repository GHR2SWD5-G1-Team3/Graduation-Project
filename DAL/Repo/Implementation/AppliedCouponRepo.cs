

using Microsoft.EntityFrameworkCore;

namespace DAL.Repo.Implementation
{
    public class AppliedCouponRepo : GenericRepo<AppliedCoupon>, IAppliedCouponRepo
    {
		
        public AppliedCouponRepo(ApplicationDBContext context) : base(context)
        {
        }

		public bool Exists(long userId, long productId, long couponId)
		{
			return Get(ac => ac.UserId == userId && ac.ProductId == productId && ac.CouponId == couponId) != null;
		}

		public AppliedCoupon? GetByIds(long userId, long productId, long couponId)
		{
			return Get(ac => ac.UserId == userId && ac.ProductId == productId && ac.CouponId == couponId);
		}

		public List<AppliedCoupon> GetByProduct(long productId)
		{
			return GetAll(ac => ac.ProductId == productId);
		}

		public List<AppliedCoupon> GetByUser(long userId)
		{
			return GetAll(ac => ac.UserId == userId);
		}
		public void RemoveAppliedCoupon(long userId, long productId, long couponId)
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
