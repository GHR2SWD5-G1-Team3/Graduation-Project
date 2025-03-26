namespace BLL.Services.Abstract
{
    public interface IFavoriteProductServices
    {
        //Command
        (bool, string?) Create(long userId, long productId);
        (bool, string?) Delete(long id, string deletedBy);
        //Query
       // DisplayOrderVM Get(Expression<Func<Order, bool>>? filter = null);
       // List<DisplayOrderVM> GetAll(Expression<Func<Order, bool>>? filter = null);
    }
}
