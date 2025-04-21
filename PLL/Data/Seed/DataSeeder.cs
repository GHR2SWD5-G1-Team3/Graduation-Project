using DAL.DataBase;
using Microsoft.CodeAnalysis.Operations;

namespace PLL.Data.Seed
{
    public class DataSeederService : IDataSeederService
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ISubCategoryServices _subCategoryServices;
        private readonly UserManager<User> _userManager;

        public DataSeederService(
            ICategoryServices categoryServices,
            ISubCategoryServices subCategoryServices,
            UserManager<User> userManager)
        {
            _categoryServices = categoryServices;
            _subCategoryServices = subCategoryServices;
            _userManager = userManager;
        }

        public async Task SeedCategoriesAndSubCategoriesAsync()
        {
            var adminUser = await _userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null) return;

            var categories = await _categoryServices.GetAllActivateCategories(); 
            if (categories.Any()) return;

            // Seed categories
            var fruitsResult = await _categoryServices.CreateFromSeederAsync(
                "Fruits", "All kinds of fruits", "fruits.jpg", adminUser.Id);
            var vegetablesResult = await _categoryServices.CreateFromSeederAsync(
                "Vegetables", "All kinds of vegetables", "vegetables.jpg", adminUser.Id);

            // Get created categories with IDs (if not returned directly)
            var allCategories = await _categoryServices.GetAllActivateCategories();
            var fruitsCategory = allCategories.FirstOrDefault(c => c.Name == "Fruits");
            var vegetablesCategory = allCategories.FirstOrDefault(c => c.Name == "Vegetables");

            if (fruitsCategory != null)
            {
                await _subCategoryServices.CreateFromSeederAsync("Fresh Fruits", "Freshly picked fruits", "fresh-fruits.jpg", fruitsCategory.Id, adminUser.Id);
                await _subCategoryServices.CreateFromSeederAsync("Organic Fruits", "100% organic fruits", "organic-fruits.jpg", fruitsCategory.Id, adminUser.Id);
            }

            if (vegetablesCategory != null)
            {
                await _subCategoryServices.CreateFromSeederAsync("Fresh Vegetables", "Fresh green vegetables", "fresh-vegetables.jpg", vegetablesCategory.Id, adminUser.Id);
                await _subCategoryServices.CreateFromSeederAsync("Organic Vegetables", "Organic and healthy vegetables", "organic-vegetables.jpg", vegetablesCategory.Id, adminUser.Id);
            }
        }
    }

}
