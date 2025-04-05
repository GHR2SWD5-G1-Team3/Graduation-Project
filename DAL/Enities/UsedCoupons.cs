namespace DAL.Enities
{
    public class UsedCoupons (string userId, long couponId)
    {
        public long Id { get; private set; }
        public bool IsUsed { get; private set; }=true;
        [ForeignKey(nameof(User))]
        public string UserId { get; private set; } = userId;
        public User? User { get; set; }
        [ForeignKey(nameof(Coupon))]
        public long CouponId { get; private set; } = couponId;
        public Coupon? Coupon { get; set; }
    }
}
