namespace DAL.Entities
{
    public class CartDetails( string name,decimal price, decimal quantity, long productId, long cartId)
    {
        public long Id { get;private set; }
        public string Name { get; set; } = name;

        public decimal Price { get; private set; } = price;
        public decimal Quantity { get; private set; } = quantity;
        [ForeignKey(nameof(Product))]
        public long ProductId { get; private set; } = productId;
        public Product? Product { get; set; }
        [ForeignKey(nameof(Cart))]
        public long CartId { get; private set; } = cartId;
        public Cart? Cart { get; set; }

    }
}
