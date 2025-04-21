namespace PLL.Areas.Admin.Models.Order
{
    public class AdminOrderFilterVM
    {
        public string? UserName { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

}
