
namespace DAL.Repo.Abstract
{
    public interface IAppliedCouponRepo:IGenericRepo<AppliedCoupon>
    {
		AppliedCoupon? GetByIds(long userId, long productId, long couponId);
		List<AppliedCoupon> GetByUser(long userId);
		List<AppliedCoupon> GetByProduct(long productId);
		bool Exists(long userId, long productId, long couponId);
		public void RemoveAppliedCoupon(long userId, long productId, long couponId);
		
	}
}
