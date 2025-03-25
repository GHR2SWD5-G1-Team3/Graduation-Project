
namespace DAL.Repo.Implementation
{
    public class AppliedCouponRepo : GenericRepo<AppliedCoupon>, IAppliedCouponRepo
    {
        public AppliedCouponRepo(ApplicationDBContext context) : base(context)
        {
        }
    }
}
