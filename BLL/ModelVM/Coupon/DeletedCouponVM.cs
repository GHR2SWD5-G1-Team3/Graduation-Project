namespace BLL.ModelVM.Coupon
{
    public class DeletedCouponVM
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DeletedOn { get; set; }
        public string DeletedBy { get; set; }
    }
}
