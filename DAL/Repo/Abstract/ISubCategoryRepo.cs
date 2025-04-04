
namespace DAL.Repo.Abstract
{
    public interface ISubCategoryRepo:IGenericRepo<SubCategory>
    {
        (bool, string) Edit(string user, int id,SubCategory subCategory );
        bool Delete(int id,string user);
        List<SubCategory> All(Expression<Func<SubCategory, bool>>? filter = null);
    }
}
