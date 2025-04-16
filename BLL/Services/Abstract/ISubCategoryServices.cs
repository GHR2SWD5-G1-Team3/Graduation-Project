namespace BLL.Services.Abstract
{
    public interface ISubCategoryServices
    {
        Task<(bool, string?)> CreateAsync(CreateSubCategoryVM subCategoryVM);
        Task<List<GetAllSubCategoryVM>> GetAllSubCategories(Expression<Func<SubCategory, bool>>? filter = null, params Expression<Func<SubCategory, object>>[] includeProperty);
        Task<(bool, string)> Edit(int Id, string user, SubCategoryVM subCategory);
        Task<(SubCategoryVM?, bool, string?)> GetById(int id);
        Task<(bool, string?)> DeleteByID(int id, string user);


    }
}
