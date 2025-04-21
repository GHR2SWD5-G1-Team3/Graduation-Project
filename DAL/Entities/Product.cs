namespace DAL.Entities
{
    public class Product(string name, string description, string imagePath, decimal unitPrice, decimal quantity, int? discountPrecentage, string userId, int subCategoryId)
    {
        public long Id { get; private set; }
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
        public string ImagePath { get; private set; } = imagePath;
        public decimal UnitPrice { get; private set; } = unitPrice;
        public decimal Quantity { get; private set; } = quantity;
        public long SoldCount { get;private set; } = 0;
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime? DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
        public int? DiscountPrecentage { get; set; } = discountPrecentage;
        [ForeignKey(nameof(User))]
        public string UserId { get; private set; } = userId;
        public User? User { get; set; }
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; private set; } = subCategoryId;
        public SubCategory? SubCategory { get; set; }   
        
        public List<CartDetails>? CartProducts { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string user,string name, string description, string imagePath, decimal unitPrice, decimal quantity, int? discountPrecentage, string userId, int subCategoryId)
        {
            if (user == null) return false;
            Name = name;
            Description = description;
            ImagePath = imagePath;
            UnitPrice = unitPrice;
            Quantity = quantity;
            DiscountPrecentage = discountPrecentage;
            UserId = userId;
            SubCategoryId = subCategoryId;

            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }
        public void ReduceQuantity(decimal quantity)
        {
            if (quantity <= 0 || quantity > Quantity)
                throw new InvalidOperationException("Invalid quantity reduction");

            Quantity -= quantity;
        }



    }
}
