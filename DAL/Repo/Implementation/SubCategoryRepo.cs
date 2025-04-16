namespace DAL.Repo.Implementation
{
    public class SubCategoryRepo : GenericRepo<SubCategory>, ISubCategoryRepo
    {
        public SubCategoryRepo(ApplicationDBContext context) : base(context)
        {
        }
        //Edit
        public (bool, string) Edit(string user, int id,SubCategory subCategory)
        {
            try
            {
                var subCat = Db.SubCategories.Where(c => c.Id == id).FirstOrDefault();
                if (subCat == null)
                    return (false, "SubCategory Not Found in Database");
                var imagePath = subCategory.ImagePath is not null ? subCategory.ImagePath : subCat.ImagePath;
                var result = subCat.Edit(user, subCategory.Name, subCategory.Description, imagePath, subCategory.CategoryId);
                if (result)
                {
                    Db.SaveChanges();
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
        public bool Delete(int Id,string user)
        {
            try
            {
                var subCategory = Db.SubCategories.FirstOrDefault(s => s.Id == Id);
                if (subCategory == null)
                    return false;
                var result = subCategory.Delete(user);
                if (result)
                {
                    Db.SaveChanges();
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

        public List<SubCategory> All(Expression<Func<SubCategory, bool>>? filter = null)
        {
            //using Explicit Loading
            List<SubCategory> subCategories;
            if (filter == null)
            {
                subCategories = Db.SubCategories.ToList();
            }
            else
            {
                subCategories = Db.SubCategories.Where(filter).ToList();
            }
            foreach (var item in subCategories)
            {
                Db.Entry(item).Reference(e => e.Category).Load();
            }
            return subCategories;
        }
    }
}
