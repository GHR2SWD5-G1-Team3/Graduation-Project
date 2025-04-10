
using BLL.ModelVM.CartDetails;

namespace BLL.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo cartRepo;
        private readonly IMapper mapper;
        public CartService(ICartRepo repo , IMapper map)
        {
            cartRepo = repo;
            mapper = map;
        }
        public async Task<(bool, string?)> AddCart(DAL.Enities.Cart cart)
        {
          return await cartRepo.CreateAsync(cart);
        }
        public async Task<List<DisplayCartDetailsVM>> GetAllCarts()
        {  
            var result = await cartRepo.GetAllAsync();
            return mapper.Map<List<DisplayCartDetailsVM>>(result);
        }
        public async Task<DisplayCartDetailsVM> GetCarts(Expression<Func<DAL.Enities.Cart, bool>>? filter = null)
        {
            var result = await cartRepo.GetAsync(filter);
            return  mapper.Map<DisplayCartDetailsVM>(result);
        }

        public async Task<bool> RemoveFromCart(int cartId)
        {
           return await cartRepo.DeleteById("admin",cartId);
        }
    }
}
