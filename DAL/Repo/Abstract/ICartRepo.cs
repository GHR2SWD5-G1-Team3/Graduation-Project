namespace DAL.Repo.Abstract
{
    public interface ICartRepo :IGenericRepo<Cart>
    {
        //Task<Cart?> GetUserCartAsync(string userId);
        //Task CreateCartAsync(Cart cart);
        Task<CartDetails?> GetCartItemAsync(long cartId, long productId);
        Task AddCartItemAsync(CartDetails item);
        Task UpdateCartItemAsync(CartDetails item);
        //Task SaveAsync();
        Task<bool> DeleteById(string user, long id);

      
    }
}
