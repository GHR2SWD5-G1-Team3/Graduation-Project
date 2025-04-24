namespace BLL.ModelVM.Product
{
    public class RealtedProductsVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string SubCategoryName { get; set; }
        public int ReviewRate { get; set; }
    }
}
