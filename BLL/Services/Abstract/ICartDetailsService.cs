using BLL.ModelVM.CartDetails;

namespace BLL.Services.Abstract
{
    public interface ICartDetailsService
    {
        Task<(bool, string?)> AddToCart(DisplayCartDetailsVM cartDetails);
        void RemoveFromCart(long cartDetailId);
        Task< List<DisplayCartDetailsVM>> GetAllCartDetails(string UserId);
        Task<DisplayCartDetailsVM> GetCartDetails(string userId, long productId);
        decimal GetCartPrice(List<DisplayCartDetailsVM> cartDetails);
        Task UpdateAsync(DisplayCartDetailsVM cartDetails);
    }

}
