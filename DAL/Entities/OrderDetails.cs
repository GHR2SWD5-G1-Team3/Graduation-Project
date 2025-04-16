namespace DAL.Entities
{
    public class OrderDetails(long productId, long orderId, double price, decimal quantity)
    {
        public long Id { get; private set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; private set; } = productId;
        public Product? Product { get; set; }
        [ForeignKey(nameof(Order))]
        public long OrderId { get; private set; } = orderId;
        public Order? Order { get; set; }
        public double Price { get;private set; } = price;
        public decimal Quantity { get; private set; } = quantity;
    }
}
