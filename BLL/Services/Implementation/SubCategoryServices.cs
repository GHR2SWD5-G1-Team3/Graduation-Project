namespace BLL.Services.Implementation
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepo subCategoryRepo;
        private readonly IMapper mapper;
        public SubCategoryServices(ISubCategoryRepo subCategoryRepo,IMapper mapper)
        {
            this.subCategoryRepo = subCategoryRepo;
            this.mapper = mapper;
        }
        public (bool, string?) Create(CreateSubCategoryVM subCategoryVM)
        {
            try
            {
                string Path = null;
                if(subCategoryVM.Image is not null)
                {
                    Path = UploadFiles.UploadFile("images", subCategoryVM.Image);
                }
                var subcategory = new SubCategory(subCategoryVM.Name, subCategoryVM.Description, Path, subCategoryVM.CategoryId);
                var result = subCategoryRepo.Create(subcategory);
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
                var subcategory = subCategoryRepo.Get(e => e.Id == id);
                if (subcategory == null)
                    return (false, "subcategory not found");
                if (subcategory.IsDeleted)
                    return (false, "Error: subcategory is already deleted.");

                var isDeleted = subCategoryRepo.Delete(id, user);
                if (!isDeleted)
                    return (false, "Failed to delete subcategory.");

                return (true, "subcategory soft deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public (bool, string) Edit(int Id, string user, SubCategoryVM subCategoryVM)
        {
            try
            {
                //Check if Employee Is Found In Db
                var subcategory = subCategoryRepo.Get(a => a.Id == Id);
                if (subcategory == null)
                    return (false, "subcategory not found in db!");
                if(subCategoryVM.Image != null)
                {   // Delete Old Image
                    if (!string.IsNullOrEmpty(subcategory.ImagePath))
                    {
                        UploadFiles.RemoveFile("images", subcategory.ImagePath);
                    }
                    //upload new image
                    var newImageName = UploadFiles.UploadFile("images", subCategoryVM.Image);
                    if (string.IsNullOrEmpty(newImageName))
                    {
                        return (false, "Image upload failed!");
                    }
                    subcategory.UpdateImagePath(newImageName);
                }
                // Log Image Path Before Updating
                Console.WriteLine("New Image Path: " + subcategory.ImagePath);
                mapper.Map(subCategoryVM, subcategory);
                //make update  
                var result = subCategoryRepo.Edit(user,Id, subcategory);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public List<GetAllSubCategoryVM> GetAllActivateCategories()
        {
            var subCategories = subCategoryRepo.GetAll(e => !e.IsDeleted);
            var result = mapper.Map<List<GetAllSubCategoryVM>>(subCategories);
            return result;
        }

        public (SubCategoryVM?, bool, string?) GetById(int id)
        {
            try
            {
                var subCategory = subCategoryRepo.Get(e => e.Id == id);
                if (subCategory == null)
                    return (null, false, "subCategory not found");
                var result = mapper.Map<SubCategoryVM>(subCategory);

                if (!string.IsNullOrEmpty(subCategory.ImagePath))
                {
                    result.ExistingImagePath = "/images/" + subCategory.ImagePath;
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
