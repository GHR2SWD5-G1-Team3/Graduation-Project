using BLL.ModelVM.Category;
using BLL.ModelVM.SubCategory;

namespace BLL.Services.Abstract
{
    public interface ISubCategoryServices
    {
        (bool, string?) Create(CreateSubCategoryVM subCategoryVM);
        List<GetAllSubCategoryVM> GetAllActivateCategories();
        (bool, string) Edit(int Id, string user, SubCategoryVM subCategory);
        (SubCategoryVM?, bool, string?) GetById(int id);
        (bool, string?) DeleteByID(int id, string user);
    }
}
