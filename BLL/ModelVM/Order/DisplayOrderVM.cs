namespace BLL.ModelVM.Order
{
    public class DisplayOrderVM
    {
        public decimal TotalPrice { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public bool IsPaied { get;  set; }
        public bool IsDelivered { get;  set; }
        public string PhoneNumber { get;  set; }
        public string City { get;  set; }
        public string Street { get;  set; }
        public string PaymentMethod { get;  set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
    }
}
