using Microsoft.Extensions.Primitives;

namespace BLL.ModelVM.Product
{
    public class ProductDetailsVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal UnitPrice { get; set; }
        public string SubCategoryName { get; set; }
        public int ReviewRate { get; set; }
    }
}
