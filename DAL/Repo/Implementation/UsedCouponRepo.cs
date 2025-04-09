namespace DAL.Repo.Implementation
{
    public class UsedCouponRepo(ApplicationDBContext context) : GenericRepo<UsedCoupons>(context), IUsedCouponRepo
    {
    }
}
