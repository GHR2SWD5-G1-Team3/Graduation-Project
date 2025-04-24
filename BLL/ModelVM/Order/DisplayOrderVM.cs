namespace BLL.ModelVM.Order
{
        public class DisplayOrderVM
        {
            public long Id { get; set; }
            public long ProductId { get; set; }
            public string? Name { get; set; }
            public decimal Price { get; set; }
            public decimal Quantity { get; set; }
            public decimal Subtotal { get; set; }
            public decimal TotalPrice { get; set; }
            public bool IsPaied { get; set; }
            public bool IsDelivered { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string PaymentMethod { get; set; }
            public string UserId { get; set; }
            public string UserEmail { get; set; } // The email of the user who made the order
            public OrderStatus Status { get; set; } // Order status (Pending, Delivered, Cancelled)
            public DateTime CreatedAt { get; set; } // Date when the order was created
            public DateTime? UpdatedAt { get; set; } // Date when the order was last updated (nullable)
        public List<OrderProductVM> Products { get; set; }
        public IEnumerable<OrderItemVM> OrderDetails { get; set; }

        public List<DisplayCartDetailsVM> CartItems { get; set; } = new List<DisplayCartDetailsVM>();
    }
    public class OrderItemVM
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    

}
