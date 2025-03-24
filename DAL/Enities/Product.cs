namespace DAL.Enities
{
    public class Product(string name, string description, string imagePath, decimal unitPrice, long quantity, int? discountPrecentage, long userId, int subCategoryId)
    {
        public long Id { get; private set; }
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public string ImagePath { get; private set; } = imagePath;
        public decimal UnitPrice { get; private set; } = unitPrice;
        public long Quantity { get; private set; } = quantity;
        public long SoldCount { get;private set; } = 0;
        public int? DiscountPrecentage { get; set; } = discountPrecentage;
        [ForeignKey(nameof(User))]
        public long UserId { get; private set; } = userId;
        public User? User { get; set; }
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; private set; } = subCategoryId;
        public SubCategory? SubCategory { get; set; }   
        
        public List<CartDetails>? CartProducts { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }


    }
}
