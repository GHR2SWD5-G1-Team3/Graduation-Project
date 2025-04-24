using DAL.Repo.Implementation;

namespace BLL.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<(bool, string?)> CreateAsync(CreateCategoryVM categoryVM);
        Task<List<GetAllCategoryVM>> GetAllActivateCategories(Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includeProperty);
        Task<(bool, string)> Edit(string? user,int Id, CategoryVM categoryVM);
        Task<(CategoryVM?, bool, string?)> GetById(int id);
        Task<(bool, string?)> DeleteByID(string? user,int id);
        Task<(bool, string?)> CreateFromSeederAsync(string name, string description, string imagePath, string userId);


    }
}
