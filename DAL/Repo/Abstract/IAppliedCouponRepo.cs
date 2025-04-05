
namespace DAL.Repo.Abstract
{
    public interface IAppliedCouponRepo:IGenericRepo<AppliedCoupon>
    {
		AppliedCoupon? GetByIds(string userId, long productId, long couponId);
		List<AppliedCoupon> GetByUser(string userId);
		List<AppliedCoupon> GetByProduct(long productId);
		bool Exists(string userId, long productId, long couponId);
		public void RemoveAppliedCoupon(string userId, long productId, long couponId);
		
	}
}
