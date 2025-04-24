using BLL.ModelVM.CartDetails;
using DAL.Entities;

namespace BLL.Services.Implementation
{
    public class CartDetailsService :ICartDetailsService
    {
        private readonly ICartRepo cartRepo;
        private readonly ICartDetailsRepo _cartDetailsRepo;
        private readonly IMapper _mapper;

        public CartDetailsService(ICartDetailsRepo cartDetailsRepo, IMapper mapper ,ICartRepo cartRepo)
        {
            _cartDetailsRepo = cartDetailsRepo;
            _mapper = mapper;
            this.cartRepo= cartRepo;
        }

        public async Task<(bool, string?)> AddToCart(DisplayCartDetailsVM cartDetails)
        {
            var result =_mapper.Map<CartDetails>(cartDetails);
            return await _cartDetailsRepo.CreateAsync(result);

        }

        public void RemoveFromCart(long cartDetailId)
        {
            _cartDetailsRepo.Delete(cartDetailId);
        }

        public async Task<List<DisplayCartDetailsVM>> GetAllCartDetails(string UserId)
        {
            var cart = await cartRepo.GetAsync(a => a.UserId == UserId);
            var cartDetails = await _cartDetailsRepo.GetAllAsync(a => a.CartId == cart.Id,a=>a.Product);
            return _mapper.Map<List<DisplayCartDetailsVM>>(cartDetails);
        }

        public decimal GetCartPrice(List<DisplayCartDetailsVM> cartDetails)
        {
            return cartDetails.Sum(x => x.TotalPrice);
        }
    }
}
