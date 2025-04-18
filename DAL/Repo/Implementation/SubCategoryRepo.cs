namespace DAL.Repo.Implementation
{
    public class SubCategoryRepo : GenericRepo<SubCategory>, ISubCategoryRepo
    {
        public SubCategoryRepo(ApplicationDBContext context) : base(context){}
        //Edit
        public async Task<(bool, string)> Edit(int id, SubCategory subCategory)
        {
            try
            {
                var subCat = await Db.SubCategories.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (subCat == null)
                    return (false, "SubCategory Not Found in Database");
                var imagePath = subCategory.ImagePath is not null ? subCategory.ImagePath : subCat.ImagePath;
                var result = subCat.Edit( subCategory.Name, subCategory.Description, imagePath, subCategory.CategoryId);
                if (result)
                {
                    await Db.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "Error updating SubCategory:Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        //delete
        public async Task<bool> Delete(int Id)
        {
            try
            {
                var subCategory = await Db.SubCategories.FirstOrDefaultAsync(s => s.Id == Id);
                if (subCategory == null)
                    return false;
                var result = subCategory.Delete();
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
