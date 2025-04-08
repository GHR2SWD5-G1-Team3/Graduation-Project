namespace DAL.Repo.Abstract
{
    public interface ICouponRepo:IGenericRepo<Coupon>
    {
        Task<(bool, string?)> Edit(string user, Coupon coupon , long Id);
        Task<bool> DeleteById(string user, long id);
    }
}
