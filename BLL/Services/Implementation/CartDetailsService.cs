using BLL.ModelVM.CartDetails;

namespace BLL.Services.Implementation
{
    public class CartDetailsService :ICartDetailsService
    {

        private readonly ICartDetailsRepo _cartDetailsRepo;
        private readonly IMapper _mapper;

        public CartDetailsService(ICartDetailsRepo cartDetailsRepo, IMapper mapper)
        {
            _cartDetailsRepo = cartDetailsRepo;
            _mapper = mapper;
        }

        public async Task<(bool, string?)> AddToCart(DisplayCartDetailsVM cartDetails)
        {
            var result =_mapper.Map<CartDetails>(cartDetails);
            return await _cartDetailsRepo.CreateAsync(result);

        }

        public void RemoveFromCart(int cartDetailId)
        {
            _cartDetailsRepo.Delete(cartDetailId);
        }

        public async Task<List<DisplayCartDetailsVM>> GetAllCartDetails(Expression<Func<CartDetails, bool>>? filter = null)
        {
            var cartDetails =await _cartDetailsRepo.GetAllAsync(filter);
            return _mapper.Map<List<DisplayCartDetailsVM>>(cartDetails);
        }

        public async Task<DisplayCartDetailsVM> GetCartDetails(Expression<Func<CartDetails, bool>>? filter = null)
        {
            var cartDetails =await _cartDetailsRepo.GetAsync(filter);
            return _mapper.Map<DisplayCartDetailsVM>(cartDetails);
        }

        public decimal GetCartPrice(List<DisplayCartDetailsVM> cartDetails)
        {
            return cartDetails.Sum(x => x.TotalPrice);
        }
    }
}
