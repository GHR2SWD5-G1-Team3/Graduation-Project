namespace DAL.Repo.Implementation
{
    public class ProductRepo(ApplicationDBContext context) : GenericRepo<Product>(context), IProductRepo
    {
        public async Task<(bool, string?)> Edit(string user, Product product , long Id)
        {
            try
            {
                var prod = await Db.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();
                if (prod == null)
                    return (false, "product Not Found Database");
                var result = prod.Edit(user, product.Name, product.Description,product.ImagePath,product.UnitPrice,product.Quantity,product.DiscountPrecentage, product.UserId, product.SubCategoryId);
                if (result)
                {
                    await Db.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<bool> DeleteById(string user, long Id)
        {
            try
            {
                var Product = await Db.Products.FirstOrDefaultAsync(prod => prod.Id == Id);
                if (Product == null)
                    return false;
                var result = Product.Delete(user);
                if (result)
                {
                    await Db.SaveChangesAsync();
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
        // top products
        public async Task<List<Product>> TopProducts(int count)
        {
            try
            {
                return await Db.Products
                .Where(p => p.SoldCount != null && p.SoldCount > 0)
                .Include(p => p.SubCategory)
                .ThenInclude(s => s.Category)
                .OrderByDescending(p => p.SoldCount)
                .Take(count)
                .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //best products(high rated)
        public async Task<List<Product>> BestProducts(int count)
        {
            return await Db.Products
                .Include(p => p.SubCategory)
                .ThenInclude(s => s.Category)
                .OrderByDescending(p => p.Reviews.Average(r => r.Rate))
                .Where(p => p.Reviews.Any())
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProducts(string category, string subCategory, int page, int pageSize)
        {
            var query = Db.Products
                .Include(p => p.SubCategory)
                .Include(p => p.SubCategory.Category)
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.SubCategory.Category.Name == category);

            if (!string.IsNullOrEmpty(subCategory))
                query = query.Where(p => p.SubCategory.Name == subCategory);

            return await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        //public async Task<int> GetAllProductsCountAsync()
        //{
        //    return await Db.Products.Where(p => p.IsDeleted == false).CountAsync();
        //}

    }
}
