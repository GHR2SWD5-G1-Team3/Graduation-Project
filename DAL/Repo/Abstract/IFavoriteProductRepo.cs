namespace DAL.Repo.Abstract
{
    public interface IFavoriteProductRepo :IGenericRepo<FavoriteProduct>
    {
        (bool, string?) Edit(string user, FavoriteProduct favoriteProduct , int Id);
        bool DeleteById(string user, int id);
    }
}
