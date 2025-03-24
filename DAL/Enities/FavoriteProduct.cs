namespace DAL.Enities
{
    public class FavoriteProduct(long userId,long productId)
    {
        public int Id { get; private set; }
        [ForeignKey(nameof(User))]
        public long UserId { get;private set; } = userId;
        public User? User { get;  set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; } = productId;
        public Product? Product { get; private set; }
    }
}
