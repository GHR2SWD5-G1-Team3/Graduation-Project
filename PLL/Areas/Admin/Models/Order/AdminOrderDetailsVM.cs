namespace PLL.Areas.Admin.Models.Order
{
    public class AdminOrderDetailsVM
    {
        public long OrderId { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedOn { get; set; }

        public List<OrderDetailItemVM> OrderItems { get; set; }
    }

    public class OrderDetailItemVM
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal => UnitPrice * Quantity;
    }

}
