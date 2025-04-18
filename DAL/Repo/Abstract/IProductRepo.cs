
namespace DAL.Repo.Abstract
{
    public interface IProductRepo : IGenericRepo<Product>
    {
        Task<(bool, string?)> Edit(string user, Product product, long Id);
        Task<bool> DeleteById(string user, long id);
        Task<List<Product>> TopProducts(int count);
        Task<List<Product>> BestProducts(int count);

    }
}
