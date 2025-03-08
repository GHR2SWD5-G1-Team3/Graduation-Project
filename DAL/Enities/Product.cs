namespace DAL.Enities
{
    public class Product
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int SoldCount { get; set; }
        public int DiscountPrecentage { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }   
        
        public List<Cart_Products>? CartProducts { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }


    }
}
