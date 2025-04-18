namespace BLL.ModelVM.Shop
{
    public class ShopVM
    {
        public List<DAL.Entities.Product> Products { get; set; }
        public List<DAL.Entities.Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedSubcategoryId { get; set; }
    }
}
