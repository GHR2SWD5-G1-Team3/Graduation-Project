using static System.Net.Mime.MediaTypeNames;

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
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        public Payment? Payment { get; set; }
        public required List<OrderDetails> Details { get;  set; }

        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user, decimal totalPrice, bool isPaied, bool isDelivered, string phoneNumber, string city, string street, string paymentMethod)
        {
            if (user == null) return false;
            TotalPrice = totalPrice;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            PaymentMethod = paymentMethod;
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }

    }
}
