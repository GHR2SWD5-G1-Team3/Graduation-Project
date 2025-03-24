namespace DAL.Enities
{
    public class Order(decimal totalPrice, bool isPaied, bool isDelivered, string phoneNumber, string city, string street, string paymentMethod)
    {
        public long Id { get; private set; }
        public decimal TotalPrice { get; private set; } = totalPrice;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public bool IsPaied { get; private set; } = isPaied;
        public bool IsDelivered { get; private set; } = isDelivered;
        public string PhoneNumber { get; private set; } = phoneNumber;
        public string City { get; private set; } = city;
        public string Street { get; private set; } = street;
        public string PaymentMethod { get; private set; } = paymentMethod;
        public Payment? Payment { get; set; }
        public required List<OrderDetails> Details { get;  set; }
    }
}
