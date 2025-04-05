using static System.Net.Mime.MediaTypeNames;

namespace DAL.Enities
{
    public class Review(string comment, int rate, DateTime createdAt, string userId, long productId)
    {
        public long Id { get; private set; }
        public string Comment { get; private set; } = comment;
        public int Rate { get; private set; } = rate;
        public DateTime CreatedAt { get; private set; } = createdAt;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = userId;
        public User? User { get; set; }
        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; } = productId;
        public Product? Product { get; set; }

        public bool Delete(string? User)
        {
            if (User == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = User;
            DeletedOn = DateTime.Now;
            return true;
        }
        public bool Edit(string? user, string comment, int rate, string userId, long productId)
        {
            if (user == null) return false;
            Comment = comment;
            Rate = rate;
            UserId = userId;
            ProductId = productId;
            ModifiedBy = user;
            ModifiedOn = DateTime.Now;
            return true;
        }

    }
}
