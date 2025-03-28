namespace BLL.Services.Abstract
{
    public interface ICouponService
    {
        bool Create(string code, DateTime? expiredAt, int? usageLimit, int discount);
        bool Update(string code, DateTime? expiredAt, int? usageLimit, int discount);
        List<Coupon> GetAll(Expression<Func<Coupon, bool>>? filter = null);
        Coupon GetById(int id);
        bool Delete (int id);
    }
}
