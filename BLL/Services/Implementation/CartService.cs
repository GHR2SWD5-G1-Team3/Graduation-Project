using Microsoft.EntityFrameworkCore;
namespace BLL.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo cartRepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly ApplicationDBContext _context;
        public CartService(ICartRepo repo , IMapper map ,IProductService productService, IHttpContextAccessor httpContextAccessor,ApplicationDBContext context)
        {
            cartRepo = repo;
            mapper = map;
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }
        public async Task AddProductToCartAsync(string userId, long productId, decimal price, decimal quantity)
        {
            try
            {
                var cart = await cartRepo.GetAsync(c => c.UserId == userId);
                if (cart == null)
                {
                    cart = new Cart(userId);
                    await cartRepo.CreateAsync(cart);
                }

                var cartItem = await cartRepo.GetCartItemAsync(cart.Id, productId);
                if (cartItem != null)
                {
                    cartItem.IncreaseQuantity(quantity);
                    await cartRepo.UpdateCartItemAsync(cartItem);
                }
                else
                {
                    var product = await _productService.GetProductAsync(
                        p => p.Id == productId );

                    if (product == null) throw new Exception("Product not found");

                    cartItem = new CartDetails(product.Name, price, quantity, productId, cart.Id);
                    await cartRepo.AddCartItemAsync(cartItem);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<int> GetCartItemCountAsync(string userId)
        {
            try
            {
                var cart = await cartRepo.GetAsync(c => c.UserId == userId, c => c.CartProducts);

                if (cart == null || cart.CartProducts == null || !cart.CartProducts.Any())
                    return 0;

                int totalQuantity = 0;
                foreach (var item in cart.CartProducts)
                {
                    totalQuantity += (int)item.Quantity;
                }

                return totalQuantity;
            }
            catch (Exception)
            {

                throw;
            }
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

        public async Task<Cart> GetCarts(Expression<Func<Cart, bool>>? filter = null)
        {
            return await cartRepo.GetAsync(filter);
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
            var carts = _context.Carts.Include(cart => cart.CartProducts).ThenInclude(cd => cd.Product) .AsQueryable();

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
                    Quantity = (int)cd.Quantity
                })
                .ToListAsync();

            return cartItems;
        }

       
    }
}
