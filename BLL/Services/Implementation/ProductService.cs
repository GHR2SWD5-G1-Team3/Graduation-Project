﻿namespace BLL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo productRepo;
        private readonly IMapper mapper;

        public ProductService(IProductRepo productRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
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

    }
}
