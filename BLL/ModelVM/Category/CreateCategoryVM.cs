using System.ComponentModel.DataAnnotations;

namespace BLL.ModelVM.Category
{
    public class CreateCategoryVM
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
