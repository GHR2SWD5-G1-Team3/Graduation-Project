
namespace DAL.Repo.Abstract
{
    public interface ISubCategoryRepo:IGenericRepo<SubCategory>
    {
        Task<(bool, string)> Edit(string? user, int id, SubCategory subCategory);
        Task<bool> Delete(string? user, int id);
    }
}
