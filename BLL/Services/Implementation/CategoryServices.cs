﻿namespace BLL.Services.Implementation
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

        //create
        public async Task<(bool, string?)> CreateAsync(CreateCategoryVM categoryVM)
        {
            try
            {
                string imagePath = null;
                if (categoryVM.Image is not null)
                {
                    imagePath = UploadFiles.UploadFile("images", categoryVM.Image);
                }

                var category = new Category(categoryVM.Name, categoryVM.Description, imagePath);
                var (result, error) = await categoryRepo.CreateAsync(category);
                return (result, error);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        //getall
        public async Task<List<GetAllCategoryVM>> GetAllActivateCategories(Expression<Func<Category, bool>>? filter = null, params Expression<Func<Category, object>>[] includeProperty)
        {
            var categories = await categoryRepo.GetAllAsync(filter: c => c.IsDeleted == false, includeProperty);
            var result = mapper.Map<List<GetAllCategoryVM>>(categories);
            return result;
        }

        //delete
        public async Task<(bool, string?)> DeleteByID(int id)
        {
            try
            {
                var category = await categoryRepo.GetAsync(e => e.Id == id);
                if (category == null)
                    return (false, "category not found");
                if (category.IsDeleted)
                    return (false, "Error: category is already deleted.");

                var isDeleted = await categoryRepo.Delete( id);
                if (!isDeleted)
                    return (false, "Failed to delete category.");

                return (true, "category soft deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        //edit
        public async Task<(bool, string)> Edit(int Id,  CategoryVM categoryVM)
        {
            try
            {
                //Check if Category Is Found In Db
                var category = await categoryRepo.GetAsync(c => c.Id == Id);
                if (category == null)
                    return (false, "category not found in local db!");
                if (categoryVM.ImagePath is not null)
                {   // Delete Old Image
                    if (!string.IsNullOrEmpty(category.Image))
                    {
                        UploadFiles.RemoveFile("images", category.Image);
                    }
                    //upload new image
                    var newImageName = UploadFiles.UploadFile("images", categoryVM.ImagePath);
                    if (string.IsNullOrEmpty(newImageName))
                    {
                        return (false, "Image upload failed!");
                    }
                    category.Image = newImageName;
                }
                mapper.Map(categoryVM, category);
                //make update  
                var (result, error) = await categoryRepo.Edit( category, Id);

                return (result, error);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }
        //getbyID
        public async Task<(CategoryVM?, bool, string?)> GetById(int id)
        {
            try
            {
                var category = await categoryRepo.GetAsync(e => e.Id == id);
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
