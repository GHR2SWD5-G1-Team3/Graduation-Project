
namespace DAL.Repo.Abstract
{
    public interface ISubCategoryRepo:IGenericRepo<SubCategory>
    {
        Task<(bool, string)> Edit(string user, int id, SubCategory subCategory);
        Task<bool> Delete(int id, string user);
    }
}
