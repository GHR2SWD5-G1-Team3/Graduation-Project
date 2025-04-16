using BLL.ModelVM.CartDetails;
namespace BLL.Services.Abstract
{
    public interface ICartService
    {
       Task<(bool, string?)> AddCart(Cart cart);
        Task<bool> RemoveFromCart(int cartId);
        Task<List<DisplayCartDetailsVM>> GetAllCarts();
        Task<DisplayCartDetailsVM> GetCarts(Expression<Func<Cart, bool>>? filter = null);
    }
}
