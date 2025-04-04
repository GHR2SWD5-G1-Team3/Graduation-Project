namespace BLL.ModelVM.Category
{
    public class CategoryVM
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ExistingImagePath { get; set; }
    }
}
