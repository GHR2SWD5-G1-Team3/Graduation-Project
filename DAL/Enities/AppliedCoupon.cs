namespace DAL.Enities
{
    public class AppliedCoupon(long userId, long productId, long couponId)
    {
        [ForeignKey(nameof(User))]
        public long UserId { get; private set; } = userId;
        public User? User { get; set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; private set; } = productId;
        public Product? Product { get; set; }
        [ForeignKey(nameof(Coupon))]
        public long CouponId { get; private set; } = couponId;
        public Coupon? Coupon { get; set; }

    }
}
