using BLL.Helpers;
using BLL.ModelVM.Category;
namespace BLL.Services.Implementation
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepo categoryRepo;
        private readonly IMapper mapper;
        public CategoryServices(ICategoryRepo categoryRepo,IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public (bool, string?) Create(CreateCategoryVM categoryVM)
        {
            try
            {
                string Path = null;
                if (categoryVM.Image is not null)
                {
                    Path = UploadFiles.UploadFile("images", categoryVM.Image);
                }
                var category = new Category(categoryVM.Name, categoryVM.Description, Path);
                var result = categoryRepo.Create(category);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public (bool, string?) DeleteByID(int id, string user)
        {
            try
            {
                var category = categoryRepo.Get(e => e.Id == id);
                if (category == null)
                    return (false, "category not found");
                if (category.IsDeleted)
                    return (false, "Error: category is already deleted.");

                var isDeleted = categoryRepo.Delete(user,id);
                if (!isDeleted)
                    return (false, "Failed to delete category.");

                return (true, "category soft deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public (bool, string) Edit(int Id, string user, CategoryVM categoryVM)
        {
            try
            {
                //Check if Employee Is Found In Db
                var category = categoryRepo.Get(c => c.Id == Id);
                if (category == null)
                    return (false, "Employee not found in local db!");
                if (categoryVM.Image != null)
                {   // Delete Old Image
                    if (!string.IsNullOrEmpty(category.Image))
                    {
                        UploadFiles.RemoveFile("images", category.Image);
                    }
                    //upload new image
                    var newImageName = UploadFiles.UploadFile("images", categoryVM.Image);
                    if (string.IsNullOrEmpty(newImageName))
                    {
                        return (false, "Image upload failed!");
                    }
                    category.UpdateImagePath(newImageName);
                }
                // Log Image Path Before Updating
                Console.WriteLine("New Image Path: " + category.Image);
                mapper.Map(categoryVM, category);
                //make update  
                var result = categoryRepo.Edit( user, category,Id);

                return result;
            }
            catch (Exception ex)
            {

                return (false, ex.Message);
            }

        }

        public List<GetAllCategoryVM> GetAllActivateCategories()
        {
            var categories = categoryRepo.GetAll(e => !e.IsDeleted);
            var result = mapper.Map<List<GetAllCategoryVM>>(categories);
            return result;
        }

        public (CategoryVM?, bool, string?) GetById(int id)
        {
            try
            {
                var category = categoryRepo.Get(e => e.Id == id);
                if (category == null)
                    return (null, false, "category not found");
                var result = mapper.Map<CategoryVM>(category);

                if (category.Image is not null)
                {
                    result.ExistingImagePath = "/images/" + category.Image;
                }
                else
                {
                    result.ExistingImagePath = null;
                }
                return (result, true, null);
            }
            catch (Exception ex)
            {
                return (null, false, ex.Message);
            }

        }
    }
}
