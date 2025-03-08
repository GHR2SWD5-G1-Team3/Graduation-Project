namespace DAL.Enities
{
    public class Review(string comment, int rate, DateTime createdAt, long userId)
    {
        public long Id { get; private set; }
        public string Comment { get; private set; } = comment;
        public int Rate { get; private set; } = rate;
        public DateTime CreatedAt { get; private set; } = createdAt;
        [ForeignKey(nameof(User))]
        public long UserId { get; set; } = userId;
        public User? User { get; set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
