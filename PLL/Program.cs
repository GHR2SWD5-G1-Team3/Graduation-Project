using BLL.Configuration;

namespace PLL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Get the connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });

            
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.Configure<EmailSettings>(
                 builder.Configuration.GetSection("EmailSettings"));
            //Scopped Repos
            builder.Services.AddScoped<IOrderRepo,OrderRepo>();
            builder.Services.AddScoped<IOrderDetailsRepo, OrderDetailsRepo>();
            builder.Services.AddScoped<ICartDetailsRepo, CartDetailsRepo>();
			builder.Services.AddScoped<IAppliedCouponRepo, AppliedCouponRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ICouponRepo, CouponRepo>();
            builder.Services.AddScoped<ICartRepo ,CartRepo>();
            builder.Services.AddScoped<IUsedCouponRepo, UsedCouponRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<ISubCategoryRepo, SubCategoryRepo>();
            builder.Services.AddScoped<IReviewRepo, ReviewRepo>(); 
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IRoleRepo, RoleRepo>();
            builder.Services.AddScoped<IFavoriteProductRepo, FavoriteProductRepo>();
            builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
            builder.Services.AddScoped<IFavoriteProductRepo, FavoriteProductRepo>();
            // Scoped Services
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICartDetailsService, CartDetailsService>();
            builder.Services.AddScoped<IAppliedCouponService, AppliedCouponService>();
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IUsedCouponService, UsedCouponService>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
            builder.Services.AddScoped<ICartService,CartService>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IRoleServices, RoleServices>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IProfileServices, ProfileServices>();
            builder.Services.AddScoped<IFavoriteProductServices, FavoriteProductServices>();
            builder.Services.AddScoped<IDataSeederService, DataSeederService>();

            // Mapping
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            // Identity
            builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<ApplicationDBContext>()
                            .AddSignInManager<SignInManager<User>>()
                            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

            builder.Services.AddAuthentication()
                            .AddCookie(options =>
                            {
                                options.LoginPath = "/Account/Login";
                                options.AccessDeniedPath = "/Account/AccessDenied";
                            });

            var app = builder.Build();

            var supportedCultures = new[] {
                new CultureInfo("ar-EG"),
                new CultureInfo("en-US"),
                new CultureInfo(""),
            };

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders =
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            });
            // Define the route for areas
            app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
 );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await IdentitySeeder.SeedRolesAndAdminAsync(services);
                var seederService = services.GetRequiredService<IDataSeederService>();
                await seederService.SeedCategoriesAndSubCategoriesAsync();
            }
            app.Run();
        }
    }
}
