using BLL.ModelVM.Category;

namespace BLL.Services.Abstract
{
    public interface ICategoryServices
    {
        (bool, string?) Create(CreateCategoryVM categoryVM );
        List<GetAllCategoryVM> GetAllActivateCategories();
        (bool, string) Edit(int Id, string user, CategoryVM categoryVM);
        (CategoryVM?, bool, string?) GetById(int id);
        (bool, string?) DeleteByID(int id, string user);
    }
}
