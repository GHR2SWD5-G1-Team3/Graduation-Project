namespace BLL.Services.Abstract
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(Product product);
        Task<Product> GetProductAsync(Expression<Func<Product, bool>>? filter = null);
        Task<List<Product>> GetAllProductsAsync(Expression<Func<Product,bool>>? filter = null, params Expression<Func<Product, object>>[] includeProperty);
        Task<bool> EditAsync(long productId,Product updatedProduct, string userName);
        Task<bool> DeleteAsync(long productId, string userName);
    }
}
