namespace BLL.Services.Abstract
{
    public interface IFavoriteProductServices
    {
        //Command
        Task<(bool, string?)> Create(string userId, long productId);
        Task<(bool, string?)> Delete(string userId, long productId);
        //Query
        Task<bool> IsFavouriteAsync(string userId, long productId);
        Task<List<DisplayProductInShopVM>> GetUserFavourites(string userId);
    }
}
