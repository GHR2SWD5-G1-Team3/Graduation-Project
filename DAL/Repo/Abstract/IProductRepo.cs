
namespace DAL.Repo.Abstract
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        (bool, string?) Edit(string user, Product product, long Id);
        bool DeleteById(string user, long id);

    }
}
