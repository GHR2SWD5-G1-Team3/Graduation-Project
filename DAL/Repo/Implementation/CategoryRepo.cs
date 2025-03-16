using DAL.DataBase;
using DAL.Repo.Abstract;
using DAL.Shared.Generic;

namespace DAL.Repo.Implementation
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDBContext context) : base(context)
        {
        }
    }
}
