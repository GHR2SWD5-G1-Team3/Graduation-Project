namespace BLL.Services.Implementation
{
    public class ProductService(IProductRepo productRepo, IMapper mapper) : IProductService
    {
        private readonly IProductRepo productRepo = productRepo;
        private readonly IMapper mapper = mapper;

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

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>>? filter = null, params Expression<Func<Product, object>>[] include)
        {
            try
            {
                return await productRepo.GetAsync(filter, include);
            } catch 
            {
                return null;
            }
        }
        //top products
        public async Task<List<DisplayProductInShopVM>> TopProducts()
        {
            var products = await productRepo.TopProducts(8);
            var result = mapper.Map<List<DisplayProductInShopVM>>(products);
            return result;
        }
        //best products
        public async Task<List<DisplayProductInShopVM>> BestProducts()
        {
            var products = await productRepo.BestProducts(9);
            var result = mapper.Map<List<DisplayProductInShopVM>>(products);
            return result;
        }
        public async Task<bool> PermentDelete(Product target)
        {
            try
            {
                var product = await productRepo.GetAsync(p=>p.Id == target.Id);
                if(product == null)
                    return false;
                var result = await productRepo.PermentDelete(product);
                if(!result) return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<DisplayProductInShopVM>> GetProducts(string category, string subCategory, int page, int pageSize)
        {
            var products = await productRepo.GetProducts(category, subCategory, page, pageSize);
            return mapper.Map<List<DisplayProductInShopVM>>(products) ?? new();
        }
    }
}
