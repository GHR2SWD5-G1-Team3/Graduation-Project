using DAL.DataBase;
namespace DAL.Repo.Implementation
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDBContext context) : base(context) { }
        public async Task<(bool, string?)> Edit(Category category, int Id)
        {
            try
            {
                var cat = await Db.Categories.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (cat == null)
                    return (false, "category Not Found Database");
                var imagePath = category.Image is not null ? category.Image : cat.Image;

                var result = cat.Edit(category.Name, category.Description, imagePath);
                if (result)
                {
                    await Db.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "Error updating Category");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<bool> Delete( int Id)
        {
            try
            {
                var category = await Db.Categories.FirstOrDefaultAsync(cat => cat.Id == Id);
                if (category == null)
                    return false;
                var result = category.Delete();
                if (result)
                {
                    await Db.SaveChangesAsync();
                    return (true);
                }
                return (false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                return false;
            }
        }

    }
}
