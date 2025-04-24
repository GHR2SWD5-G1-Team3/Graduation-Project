namespace BLL.ModelVM.Order
{
    public class OrderDetailsVM
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDelivered { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public List<OrderProductVM> Products { get; set; }
        public decimal TotalPrice => Products?.Sum(p => p.Total) ?? 0;
        }

}
