namespace DAL.Repo.Implementation
{
    public class FavoriteProductRepo(ApplicationDBContext context) : GenericRepo<FavoriteProduct>(context), IFavoriteProductRepo
    {
        public (bool, string?) Edit(string user, FavoriteProduct favoriteProduct , int Id)
        {
            try
            {
                var favProduct = Db.FavoriteProducts.Where(f => f.Id == Id).FirstOrDefault();
                if (favProduct == null)
                    return (false, "Favourite product Not Found in Database");
                var result = favProduct.Edit(user, favoriteProduct.UserId,favoriteProduct.ProductId);
                if (result)
                {
                    Db.SaveChanges();
                    return (true, null);
                }
                return (false, "Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public bool DeleteById(string user, int Id)
        {
            try
            {
                var favProduct = Db.FavoriteProducts.FirstOrDefault(f => f.Id == Id);
                if (favProduct == null)
                    return false;
                var result = favProduct.Delete(user);
                if (result)
                {
                    Db.SaveChanges();
                    return (true);
                }
                return (false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entity: {ex.Message}");
                return false;
            }
        }
    
    }
}
