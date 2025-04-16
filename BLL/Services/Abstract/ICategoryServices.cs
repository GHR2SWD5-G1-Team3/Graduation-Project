namespace BLL.Services.Abstract
{
    public interface ICategoryServices
    {
        Task<(bool, string?)> CreateAsync(CreateCategoryVM categoryVM);
        Task<List<GetAllCategoryVM>> GetAllActivateCategories(Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includeProperty);
        Task<(bool, string)> Edit(int Id, string user, CategoryVM categoryVM);
        Task<(CategoryVM?, bool, string?)> GetById(int id);
        Task<(bool, string?)> DeleteByID(int id, string user);

    }
}
