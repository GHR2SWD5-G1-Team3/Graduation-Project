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

    }
}
