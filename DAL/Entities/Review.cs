namespace DAL.Entities
{
    public class Review
    {
        public long Id { get; private set; }
        public string Comment { get; private set; }
        public int Rate { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; private set; }
        public DateTime DeletedOn { get; set; }
        public string? ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Product))]
        public long ProductId { get; set; }
        public Product? Product { get; set; }

        // Constructor
        public Review(string comment, int rate, string userId, long productId)
        {
            if (string.IsNullOrEmpty(comment)) throw new ArgumentNullException(nameof(comment));
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            Comment = comment;
            Rate = rate;
            UserId = userId;
            ProductId = productId;
        }

        // Delete functionality
        public bool Delete(string? user)
        {
            if (user == null) return false;

            IsDeleted = !IsDeleted;
            DeletedBy = user;
            DeletedOn = DateTime.Now;
            return true;
        }

        // Edit functionality
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
