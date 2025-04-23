using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo cartRepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDBContext _context;
        public CartService(ICartRepo repo , IMapper map , IHttpContextAccessor httpContextAccessor,ApplicationDBContext context)
        {
            cartRepo = repo;
            mapper = map;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<(bool, string?)> AddCart(Cart cart)
        {
          return await cartRepo.CreateAsync(cart);
        }

        public Task<List<DisplayCartDetailsVM>> GetAllCarts()
        {
            throw new NotImplementedException();
        }

        public Task GetCartAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<DisplayCartDetailsVM> GetCarts(Expression<Func<Cart, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCart(int cartId)
        {
            string username = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (username !=null)
            {
                await cartRepo.DeleteById(username, cartId);
                return true;
            }
            return false;
        }
        public async Task<List<DisplayCartDetailsVM>> GetCartItemsAsync(Expression<Func<Cart, bool>>? filter = null)
        {
            // Query the Carts table and include the related CartDetails (eager loading)
            var carts = _context.Carts.Include(cart => cart.CartProducts)
             .ThenInclude(cd => cd.Product)
         .AsQueryable();

            // Apply the filter if provided
            if (filter != null)
            {
                carts = carts.Where(filter);
            }

            // Fetch the cart items by selecting the CartDetails and projecting them to DisplayCartDetailsVM
            var cartItems = await carts
                .SelectMany(cart => cart.CartProducts)  // Flatten the CartDetails of each Cart
                .Select(cd => new DisplayCartDetailsVM
                {
                    Name = cd.Product.Name,
                    Price = cd.Product.UnitPrice,
                    Quantity = cd.Quantity
                })
                .ToListAsync();

            return cartItems;
        }

    }
}
