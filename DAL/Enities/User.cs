namespace DAL.Enities
{
    public class User(long id, string name, string image)
    {
        public long Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Image { get; set; } = image;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; }=false;
        public List<Review>? Reviews { get; set; }
        public List<Product>? Products { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<Coupon>? Coupons { get; set; }

    }
}
