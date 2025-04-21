using System.ComponentModel.DataAnnotations;

namespace BLL.ModelVM.SubCategory
{
    public class CreateSubCategoryVM
    {
        [Required]
        public string Name { get;  set; }
        public string? Description { get;  set; }
        public string? UserId { get; set; }
        public IFormFile? Image { get;  set; }
        public int CategoryId { get; set; }
    }
}
