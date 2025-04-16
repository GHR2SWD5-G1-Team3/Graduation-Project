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

        public (bool, string) AddToCart(CartDetails cartDetails)
        {
            return _cartDetailsRepo.Create(cartDetails);

        }

        public void RemoveFromCart(int cartDetailId)
        {
            _cartDetailsRepo.Delete(cartDetailId);
        }

        public List<DisplayCartDetailsVM> GetAllCartDetails(Expression<Func<CartDetails, bool>>? filter = null)
        {
            var cartDetails = _cartDetailsRepo.GetAll(filter);
            return _mapper.Map<List<DisplayCartDetailsVM>>(cartDetails);
        }

        public DisplayCartDetailsVM GetCartDetails(Expression<Func<CartDetails, bool>>? filter = null)
        {
            var cartDetails = _cartDetailsRepo.Get(filter);
            return _mapper.Map<DisplayCartDetailsVM>(cartDetails);
        }
    }
}
