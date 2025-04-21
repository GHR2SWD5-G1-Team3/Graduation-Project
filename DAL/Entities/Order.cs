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

    // EF Core needs this
    public ICollection<OrderDetails> OrderDetails { get; private set; } = new List<OrderDetails>();

    public OrderStatus Status { get; private set; }

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
        Status = OrderStatus.Pending;
        IsDeleted = false;
    }

    public bool UpdateStatus(OrderStatus newStatus)
    {
        if (newStatus == Status)
            return false;

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
        OrderDetails.Add(detail); // Uses EF-mapped collection
    }
}
