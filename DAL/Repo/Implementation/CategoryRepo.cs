﻿using DAL.DataBase;
namespace DAL.Repo.Implementation
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDBContext context) : base(context)
        {
        }
        public (bool, string?) Edit(string user,Category category ,int Id)
        {
            try
            {
                var cat = Db.Categories.Where(c => c.Id == Id).FirstOrDefault();
                if (cat == null)
                    return (false, "category Not Found Database");
                var result = cat.Edit(user, category.Name, category.Description,category.Image);
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
        public bool DeleteById(string user,int Id)
        {
            try
            {
                var category = Db.Categories.FirstOrDefault(cat => cat.Id == Id);
                if (category == null)
                    return false;
                var result = category.Delete(user);
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
