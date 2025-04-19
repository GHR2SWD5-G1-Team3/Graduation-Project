using System.IO;

namespace BLL.ModelVM.Order
{
    public class CreateOrderVM(decimal totalPrice, bool isPaied, bool isDelivered, string phoneNumber, string city, string street, string paymentMethod)
    {
        public decimal TotalPrice { get;  set; } = totalPrice;
        public bool IsPaied { get;  set; } = isPaied;
        public bool IsDelivered { get;  set; } = isDelivered;
        public string PhoneNumber { get;  set; } = phoneNumber;
        public string City { get;  set; } = city;
        public string Street { get;  set; } = street;
        public string PaymentMethod { get;  set; } = paymentMethod;
        public bool IsPaid { get; internal set; }
    }
}
