using BLL.ModelVM.CartDetails;
namespace BLL.Services.Abstract
{
    public interface ICartService
    {
        Task AddProductToCartAsync(string userId, long productId, decimal price, decimal quantity);
        Task<int> GetCartItemCountAsync(string userId);
        Task<(bool, string?)> AddCart(Cart cart);
        Task<bool> RemoveCart(int cartId);
        Task<List<DisplayCartDetailsVM>> GetAllCarts();
        Task<Cart> GetCarts(Expression<Func<Cart, bool>>? filter = null);
        Task GetCartAsync(object id);
        Task<List<DisplayCartDetailsVM>> GetCartItemsAsync(Expression<Func<Cart, bool>>? filter = null);


    }
}
