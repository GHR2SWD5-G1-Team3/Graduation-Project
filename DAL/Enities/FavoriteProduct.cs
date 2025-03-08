namespace DAL.Enities
{
    public class FavoriteProduct
    {
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
