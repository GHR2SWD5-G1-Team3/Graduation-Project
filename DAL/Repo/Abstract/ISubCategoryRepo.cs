
namespace DAL.Repo.Abstract
{
    public interface ISubCategoryRepo:IGenericRepo<SubCategory>
    {
        (bool, string) Edit(SubCategory subCategory, string user, int id);
        bool DeleteById(string user, int id);
    }
}
