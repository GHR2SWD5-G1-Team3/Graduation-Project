namespace BLL.Services.Implementation
{
    public class FavoriteProductServices (ApplicationDBContext dbContext, IFavoriteProductRepo favoriteProductRepo) : IFavoriteProductServices
    {
        private readonly ApplicationDBContext dbContext = dbContext;
        private readonly IFavoriteProductRepo favoriteProductRepo = favoriteProductRepo;

        //Command
        public (bool, string?) Create(string userId, long productId)
        {
            try
            {
                var favoriteProduct=new FavoriteProduct(userId, productId);   
                var result= favoriteProductRepo.Create(favoriteProduct);
                return result;
            }
            catch (Exception ex) 
            { 
                return(false, ex.Message);
            }
        }

        public (bool, string?) Delete(long id, string deletedBy)
        {
            try
            {
                var favoriteProduct = dbContext.FavoriteProducts.FirstOrDefault(a => a.Id == id);
                if (favoriteProduct == null)
                {
                    return (false, "Product Not Found in Your WishList!");
                }
                var result = favoriteProduct.Delete(deletedBy);
                if (result)
                {
                    dbContext.SaveChanges();
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
     /*   public DisplayOrderVM Get(Expression<Func<Order, bool>>? filter = null)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public List<DisplayOrderVM> GetAll(Expression<Func<Order, bool>>? filter = null)
        {
            try
            {

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }*/
    }
}
