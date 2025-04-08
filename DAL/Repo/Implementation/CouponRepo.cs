namespace DAL.Repo.Implementation
{
    public class CouponRepo : GenericRepo<Coupon>, ICouponRepo
    {
        public CouponRepo(ApplicationDBContext context) : base(context)
        {
        }
        public async Task<(bool, string?)> Edit(string user, Coupon coupon , long Id)
        {
            try
            {
                var Coupon = await Db.Coupons.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (Coupon == null)
                    return (false, "coupon Not Found Database");
                var result = Coupon.Edit(user, coupon.Code, coupon.ExpiredAt,coupon.UsageLimit,coupon.Discount);
                if (result)
                {
                    await Db.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating entity: {ex.Message}");
                return (false, ex.Message);
            }
        }
        public async Task<bool> DeleteById(string user, long Id)
        {
            try
            {
                var coupon = await Db.Coupons.FirstOrDefaultAsync(cat => cat.Id == Id);
                if (coupon == null)
                    return false;
                var result = coupon.Delete(user);
                if (result)
                {
                    await Db.SaveChangesAsync();
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
