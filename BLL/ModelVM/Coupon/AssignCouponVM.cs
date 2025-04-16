namespace BLL.ModelVM.Coupon
{
    public class AssignCouponVM
    {
        [Required]
        public long ProductId { get; set; }

        [Required]
        public long CouponId { get; set; }
        public IEnumerable<DAL.Entities.Product>? Products { get; set; } = new List<DAL.Entities.Product>();
        public List<DAL.Entities.Coupon>? Coupons { get; set; } = new();
    }
}
