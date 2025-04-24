using BLL.ModelVM.CartDetails;
using DAL.Entities;
using DAL.Repo.Implementation;

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

        public async Task<DisplayCartDetailsVM> GetCartDetails(string userId, long productId)
        {
            var cart = await cartRepo.GetAsync(c=>c.UserId == userId);
            var target =  await _cartDetailsRepo.GetAsync(cd => cd.CartId == cart.Id && cd.ProductId == productId);
            var VM = _mapper.Map<DisplayCartDetailsVM>(target);
            return VM;
        }


        public async Task UpdateAsync(DisplayCartDetailsVM cartDetailsVM)
        {
            var cartDetails = await _cartDetailsRepo.GetAsync(c => c.Id == cartDetailsVM.Id);
            cartDetails.Edit(cartDetailsVM.Quantity, cartDetailsVM.Price, cartDetailsVM.Name);
            await _cartDetailsRepo.UpdateAsync(cartDetails);
        }
    }
}
