namespace DAL.Repo.Abstract
{
    public interface ICouponRepo:IGenericRepo<Coupon>
    {
        (bool, string?) Edit(string user, Coupon coupon , long Id);
        bool DeleteById(string user, long id);
    }
}
