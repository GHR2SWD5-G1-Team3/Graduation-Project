
namespace DAL.Repo.Abstract
{
    public interface IAppliedCouponRepo:IGenericRepo<AppliedCoupon>
    {

		Task<bool> RemoveAppliedCouponAsync(string userId, long productId, long couponId);
		
	}
}
