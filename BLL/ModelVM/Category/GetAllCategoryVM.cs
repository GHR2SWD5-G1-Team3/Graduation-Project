using static System.Net.Mime.MediaTypeNames;

namespace BLL.ModelVM.Category
{
    public class GetAllCategoryVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } 
        public string? Image { get; set; }
    }
}
