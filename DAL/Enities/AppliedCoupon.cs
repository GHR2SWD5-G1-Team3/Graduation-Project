namespace DAL.Enities
{
    public class AppliedCoupon
    {
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey(nameof(Coupon))]
        public long CouponId { get; set; }
        public Coupon? Coupon { get; set; }

    }
}
