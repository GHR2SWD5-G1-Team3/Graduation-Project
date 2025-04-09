
namespace BLL.ModelVM.Product
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string Description { get; set; }
        [Display(Name = "Product Image")]
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }
        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0.01, 100000, ErrorMessage = "Unit price must be greater than 0.")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public long Quantity { get; set; }
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public int? DiscountPrecentage { get; set; }
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Sub-category is required.")]
        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }
    }
}
