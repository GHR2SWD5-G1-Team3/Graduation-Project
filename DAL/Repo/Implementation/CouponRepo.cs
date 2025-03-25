namespace DAL.Repo.Implementation
{
    public class CouponRepo : GenericRepo<Coupon>, ICouponRepo
    {
        public CouponRepo(ApplicationDBContext context) : base(context)
        {
        }
        public (bool, string?) Edit(string user, Coupon coupon , long Id)
        {
            try
            {
                var Coupon = Db.Coupons.Where(c => c.Id == Id).FirstOrDefault();
                if (Coupon == null)
                    return (false, "coupon Not Found Database");
                var result = Coupon.Edit(user, coupon.Code, coupon.ExpiredAt,coupon.UsageLimit,coupon.Discount);
                if (result)
                {
                    Db.SaveChanges();
                    return (true, null);
                }
                return (false, "Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public bool DeleteById(string user, long Id)
        {
            try
            {
                var coupon = Db.Coupons.FirstOrDefault(cat => cat.Id == Id);
                if (coupon == null)
                    return false;
                var result = coupon.Delete(user);
                if (result)
                {
                    Db.SaveChanges();
                    return (true);
                }
                return (false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entity: {ex.Message}");
                return false;
            }
        }

    }
}
