using DAL.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class FavoriteProductServices (ApplicationDBContext dbContext, IFavoriteProductRepo favoriteProductRepo, IMapper mapper) : IFavoriteProductServices
    {
        private readonly ApplicationDBContext dbContext = dbContext;
        private readonly IFavoriteProductRepo _favoriteProductRepo = favoriteProductRepo;
        private readonly IMapper _mapper = mapper;

        //Command
        public async Task<(bool, string?)> Create(string userId, long productId)
        {
            try
            {
                var favoriteProduct=new FavoriteProduct(userId, productId);   
                var result= await _favoriteProductRepo.CreateAsync(favoriteProduct);
                return result;
            }
            catch (Exception ex) 
            { 
                return(false, ex.Message);
            }
        }

        public async Task<(bool, string?)> Delete(string userId, long productId)
        {
            try
            {
                var favoriteProduct = await _favoriteProductRepo.GetAsync(a => a.ProductId == productId && a.UserId == userId);
                if (favoriteProduct == null)
                {
                    return (false, "Product Not Found in Your WishList!");
                }
                dbContext.FavoriteProducts.Remove(favoriteProduct);
                var result = await dbContext.SaveChangesAsync();

                if (result>0)
                {
                    return (true, "Deleted Sucessfully !");
                }
                return (false, "Something Went Wrong !");
            
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        //Query
        public async Task<bool> IsFavouriteAsync(string userId, long productId)
        {
            return await dbContext.FavoriteProducts.AnyAsync(f => f.UserId == userId && f.ProductId == productId);
        }
     
        public async Task<List<DisplayProductInShopVM>> GetUserFavourites(string userId)
        {
            var favorites = await _favoriteProductRepo.GetAllAsync(a => a.UserId == userId, a => a.Product);
            List<DisplayProductInShopVM> displayProductsfav = _mapper.Map<List<DisplayProductInShopVM>>(favorites);
            return displayProductsfav;
        }

    }
}
