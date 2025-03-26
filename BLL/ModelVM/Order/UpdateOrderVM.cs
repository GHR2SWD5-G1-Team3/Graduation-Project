using System.IO;

namespace BLL.ModelVM.Order
{
    public class UpdateOrderVM(decimal totalPrice, string phoneNumber, string city, string street, string paymentMethod)
    {
        public decimal TotalPrice { get;  set; } = totalPrice;
        public string PhoneNumber { get;  set; } = phoneNumber;
        public string City { get;  set; } = city;
        public string Street { get;  set; } = street;
        public string PaymentMethod { get;  set; } = paymentMethod;
        public string? ModifiedBy { get;  set; }
        public DateTime ModifiedOn { get;  set; }
    }
}
