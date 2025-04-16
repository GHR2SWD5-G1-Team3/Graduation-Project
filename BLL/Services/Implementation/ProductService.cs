namespace BLL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo productRepo;

        public ProductService(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                if (product != null) 
                {
                    var result = await productRepo.CreateAsync(product);
                    return true;
                }
                return false;
            }
            catch  
            { 
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long productId, string userName)
        {
            try
            {
                var result = await productRepo.DeleteById(userName, productId);
                if(result)
                    return true;
                return false;
            } catch 
            {
                return false;
            }
        }

        public async Task<bool> EditAsync(long productId, Product updatedProduct, string userName)
        {
            try
            {
                var result = await productRepo.Edit(userName, updatedProduct, productId);
                if(result.Item1)
                    return true;
                return false;
            } catch 
            {
                return false;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync(Expression<Func<Product, bool>>? filter = null, params Expression<Func<Product, object>>[] includeProperty)
        {
            try
            {
                return await productRepo.GetAllAsync(filter, includeProperty);
            }
            catch  
            {
                return [];
            }
        }

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>>? filter = null)
        {
            try
            {
                return await productRepo.GetAsync(filter);
            } catch 
            {
                return null;
            }
        }
    }
}
