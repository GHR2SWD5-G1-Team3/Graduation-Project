namespace BLL.ModelVM.Coupon
{
    public class AssignCouponVM
    {
        [Required]
        public long ProductId { get; set; }

        [Required]
        public long CouponId { get; set; }
        public IEnumerable<DAL.Enities.Product>? Products { get; set; } = new List<DAL.Enities.Product>();
        public List<DAL.Enities.Coupon>? Coupons { get; set; } = new();
    }
}
