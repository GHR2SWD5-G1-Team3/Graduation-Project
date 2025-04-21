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
        //create
        public async Task<(bool, string?)> CreateAsync(CreateSubCategoryVM subCategoryVM)
        {
            try
            {
                string path = null;
                if (subCategoryVM.Image is not null)
                {
                    path = UploadFiles.UploadFile("images", subCategoryVM.Image);
                }
                var subcategory = new SubCategory(subCategoryVM.Name, subCategoryVM.Description, path, subCategoryVM.CategoryId, subCategoryVM.UserId);
                var result = await subCategoryRepo.CreateAsync(subcategory);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        //delete
        public async Task<(bool, string?)> DeleteByID(string? user, int id)
        {
            try
            {
                var subcategory = await subCategoryRepo.GetAsync(e => e.Id == id);
                if (subcategory == null)
                    return (false, "subcategory not found");
                if (subcategory.IsDeleted)
                    return (false, "Error: subcategory is already deleted.");
                if (user == null) return (false, "Error:Plz enter user modifier");
                var isDeleted = await subCategoryRepo.Delete(user,id);
                if (!isDeleted)
                    return (false, "Failed to delete subcategory.");

                return (true, "subcategory soft deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        //edit
        public async Task<(bool, string)> Edit(string? user, int Id, SubCategoryVM subCategoryVM)
        {
            try
            {
                var subcategory = await subCategoryRepo.GetAsync(a => a.Id == Id);
                if (subcategory == null)
                    return (false, "subcategory not found in db!");
                if (user == null) return (false, "Error:Plz enter user modifier");
                if (subCategoryVM.Image != null)
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
                mapper.Map(subCategoryVM, subcategory);
                //make update  
                var result = await subCategoryRepo.Edit(user, Id, subcategory);
                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        //get all active subcategories
        public async Task<List<GetAllSubCategoryVM>> GetAllSubCategories(Expression<Func<SubCategory, bool>>? filter = null, params Expression<Func<SubCategory, object>>[] includeProperty)
        {
            var subCategories = await subCategoryRepo.GetAllAsync(
                e => !e.IsDeleted,
                s => s.Category
            );
            var result = mapper.Map<List<GetAllSubCategoryVM>>(subCategories);
            return result;
        }
        //getbyid
        public async Task<(SubCategoryVM?, bool, string?)> GetById(int id)
        {
            try
            {
                var subCategory = await subCategoryRepo.GetAsync(e => e.Id == id);
                if (subCategory == null)
                    return (null, false, "subCategory not found");
                var result = mapper.Map<SubCategoryVM>(subCategory);

                if (subCategory.ImagePath is not null)
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
