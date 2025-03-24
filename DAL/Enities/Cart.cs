namespace DAL.Enities
{
    public class Cart(long userId)
    {
        public long Id { get; private set; }
        public DateTime CreatedAt { get; private set; }= DateTime.Now;
        public bool IsChecked { get; private set; }= false;
        [ForeignKey(nameof(User))]
        public long UserId { get; private set; } = userId;
        public User? User { get;  set; }
        public List<CartDetails>? CartProducts { get; set; }

    }
}
