namespace BLL.ModelVM.Coupon
{
    public class CreateCouponVM
    {
        [Required(ErrorMessage = "Code is Required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Discount is Required.")]
        [Range(1, 100)]
        public int Discount { get; set; }
        [Required(ErrorMessage = "Expired Date is Required.")]
        public DateTime ExpiredAt { get; set; }
        [Required(ErrorMessage = "Usage Limit is Required.")]
        public int UsageLimit { get; set; }
        public string? CreatedBy { get; set; }
    }
}
