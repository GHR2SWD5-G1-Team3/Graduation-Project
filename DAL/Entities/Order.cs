namespace DAL.Entities
{
    public class Order
    {
        public long Id { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsPaied { get; private set; }
        public bool IsDelivered { get; private set; }
        public string PhoneNumber { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string PaymentMethod { get; private set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; private set; }

        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; set; }
        public Payment? Payment { get; set; }

        // Private collection for OrderDetails
        private readonly List<OrderDetails> _details = new();
        public IReadOnlyCollection<OrderDetails> Details => _details.AsReadOnly();

        public OrderStatus Status { get; private set; }

        // Constructor to set values
        public Order(decimal totalPrice, bool isPaied, bool isDelivered, string phoneNumber, string city, string street, string paymentMethod, string userId)
        {
            TotalPrice = totalPrice;
            IsPaied = isPaied;
            IsDelivered = isDelivered;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            PaymentMethod = paymentMethod;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Pending;  // Default status
            IsDeleted = false;
        }

        // Method to update the order status
        public bool UpdateStatus(OrderStatus newStatus)
        {
            if (newStatus == Status)
                return false; // No change

            Status = newStatus;
            ModifiedOn = DateTime.UtcNow;
            return true;
        }

        public bool Delete(string? user)
        {
            if (user == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = user;
            DeletedOn = DateTime.UtcNow;
            return true;
        }

        public bool Edit(string? user, decimal totalPrice, string phoneNumber, string city, string street, string paymentMethod)
        {
            if (user == null) return false;

            TotalPrice = totalPrice;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            PaymentMethod = paymentMethod;
            ModifiedBy = user;
            ModifiedOn = DateTime.UtcNow;
            return true;
        }

        public void AddOrderDetail(OrderDetails detail)
        {
            _details.Add(detail); // Assuming _details is your private list of OrderDetails
        }
    }
}
