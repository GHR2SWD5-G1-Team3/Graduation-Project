

namespace DAL.DataBase
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : IdentityDbContext<User>(options)
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<UsedCoupons> UsedCoupons { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AppliedCoupon> AppliedCoupons { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppliedCoupon>().HasKey(c => new { c.CouponId, c.ProductId, c.UserId });
			modelBuilder.Entity<CartDetails>()
	           .HasOne(cd => cd.Product)
	           .WithMany(p => p.CartProducts)
	           .HasForeignKey(cd => cd.ProductId)
	           .OnDelete(DeleteBehavior.NoAction);
			base.OnModelCreating(modelBuilder);
        }

    }
}
