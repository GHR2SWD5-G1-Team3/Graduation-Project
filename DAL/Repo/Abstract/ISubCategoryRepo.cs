
namespace DAL.Repo.Abstract
{
    public interface ISubCategoryRepo:IGenericRepo<SubCategory>
    {
        Task<(bool, string)> Edit( int id, SubCategory subCategory);
        Task<bool> Delete(int id);
    }
}
