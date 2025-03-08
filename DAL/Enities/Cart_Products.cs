namespace DAL.Enities
{
    public class Cart_Products
    {
        public long Id { get; set; }
        public Product? Product { get; set; }
        public Cart? Cart { get; set; }
    }
}
