namespace PLL.Areas.Admin.Models.Order
{
    public class AdminOrderVM
    {
        public long Id { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus Status { get; set; }
    }

}
