namespace BLL.Services.Abstract
{
    public interface ICartDetailsService
    {
        (bool, string) AddToCart(CartDetails cartDetails);
        void RemoveFromCart(int cartDetailId);
        List<DisplayCartDetailsVM> GetAllCartDetails(Expression<Func<CartDetails, bool>>? filter = null);
        DisplayCartDetailsVM GetCartDetails(Expression<Func<CartDetails, bool>>? filter = null);
    }

}
