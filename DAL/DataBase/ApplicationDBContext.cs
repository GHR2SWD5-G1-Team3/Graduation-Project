namespace DAL.DataBase
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : IdentityDbContext<User, Role, string>(options)
    {
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

            // Set decimal precision for properties
            modelBuilder.Entity<CartDetails>()
                .Property(cd => cd.Quantity)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetails>()
                .Property(od => od.Quantity)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Details)
                   .WithOne(d => d.Order)
                   .HasForeignKey(d => d.OrderId);


            modelBuilder.Entity<Payment>()
                .Property(p => p.AmountPaied)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
