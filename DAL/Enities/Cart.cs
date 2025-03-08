namespace DAL.Enities
{
    public class Cart
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public bool IsChecked { get; set; }= false;
        public List<Cart_Products>? CartProducts { get; set; }

    }
}
