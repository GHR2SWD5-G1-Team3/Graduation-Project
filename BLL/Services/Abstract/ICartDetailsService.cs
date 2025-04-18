using BLL.ModelVM.CartDetails;

namespace BLL.Services.Abstract
{
    public interface ICartDetailsService
    {
        Task<(bool, string?)> AddToCart(DisplayCartDetailsVM cartDetails);
        void RemoveFromCart(int cartDetailId);
       Task< List<DisplayCartDetailsVM>> GetAllCartDetails(Expression<Func<CartDetails, bool>>? filter = null);
       Task< DisplayCartDetailsVM> GetCartDetails(Expression<Func<CartDetails, bool>>? filter = null);
    }

}
