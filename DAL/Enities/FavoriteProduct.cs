using static System.Net.Mime.MediaTypeNames;

namespace DAL.Enities
{
    public class FavoriteProduct(long userId,long productId)
    {
        public int Id { get; private set; }
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        [ForeignKey(nameof(User))]
        public long UserId { get;private set; } = userId;
        public User? User { get;  set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; } = productId;
        public Product? Product { get; private set; }
        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user, long userId, long productId)
        {
            if (user == null) return false;
            UserId = userId;
            ProductId = productId;
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }


    }
}
