using DAL.Shared;
namespace DAL.Enities
{
    public class User(long id, string name, string image) 
    {
        public long Id { get; set; } = id;
        public string Name { get; set; } = name;

       
        public string Image { get; set; } = image;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        public List<Review>? Reviews { get; set; }
        public List<Product>? Products { get; set; }
        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<Coupon>? Coupons { get; set; }
        public Cart? Cart { get; set; }
        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user, string name)
        {
            if (user == null) return false;
            Name = name;
            
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }
    }
}
