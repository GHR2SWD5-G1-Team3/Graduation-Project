using BLL.ModelVM.CartDetails;

namespace BLL.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo cartRepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(ICartRepo repo , IMapper map , IHttpContextAccessor httpContextAccessor)
        {
            cartRepo = repo;
            mapper = map;
            _httpContextAccessor = httpContextAccessor;
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
    }
}
