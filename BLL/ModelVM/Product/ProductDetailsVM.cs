using Microsoft.Extensions.Primitives;

namespace BLL.ModelVM.Product
{
    public class ProductDetailsVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
    }
}
