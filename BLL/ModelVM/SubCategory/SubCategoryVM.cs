namespace BLL.ModelVM.SubCategory
{
    public class SubCategoryVM
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ExistingImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
