namespace DAL.Repo.Implementation
{
    public class SubCategoryRepo : GenericRepo<SubCategory>, ISubCategoryRepo
    {
        public SubCategoryRepo(ApplicationDBContext context) : base(context)
        {
        }
        //Edit
        public (bool, string) Edit(SubCategory subCategory, string user, int id)
        {
            try
            {
                var cat = Db.SubCategories.Where(c => c.Id == id).FirstOrDefault();
                if (cat == null)
                    return (false, "SubCategory Not Found in Database");
                var result = cat.Edit(user, subCategory.Name, subCategory.Description, subCategory.ImagePath, subCategory.CategoryId);
                if (result)
                {
                    Db.SaveChanges();
                    return (true, null);
                }
                return (false, "Please Enter User Modifier");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        //delete
        public bool DeleteById(string user, int Id)
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

    }
}
