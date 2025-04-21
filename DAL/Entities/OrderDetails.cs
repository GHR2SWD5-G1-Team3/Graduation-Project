public class OrderDetails
{
    public long Id { get; private set; }

    [ForeignKey(nameof(Product))]
    public long ProductId { get; private set; }
    public Product? Product { get; set; }

    [ForeignKey(nameof(Order))]
    public long OrderId { get; private set; }
    public Order? Order { get; set; }

    public decimal Price { get; private set; }
    public decimal Quantity { get; private set; }

    public decimal Subtotal => Price * Quantity;

    public OrderDetails(long productId, long orderId, decimal price, decimal quantity)
    {
        ProductId = productId;
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
    }
}
