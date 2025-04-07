namespace PLL
{
    public class Program
    {
        public static void Main(string[] args)
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
            //Scopped Repos
            builder.Services.AddScoped<IOrderRepo,OrderRepo>();
			builder.Services.AddScoped<ICartDetailsRepo, CartDetailsRepo>();
			builder.Services.AddScoped<IAppliedCouponRepo, AppliedCouponRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ICouponRepo, CouponRepo>();
            builder.Services.AddScoped<IAppliedCouponRepo, AppliedCouponRepo>();




            //Scopped Services
            builder.Services.AddScoped<IOrderServices, OrderServices>();
			builder.Services.AddScoped<ICartDetailsService, CartDetailsService>();
			builder.Services.AddScoped<IAppliedCouponService, AppliedCouponService>();
            builder.Services.AddScoped<IAccountServices, AccountServices>();
            builder.Services.AddScoped<IProductService, ProductService>();


            //Mapping
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            //Identity
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });

            builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
                            .AddEntityFrameworkStores<ApplicationDBContext>()
                            .AddSignInManager<SignInManager<User>>()
                            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                [
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                ]
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
