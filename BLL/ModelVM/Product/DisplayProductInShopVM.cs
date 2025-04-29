namespace BLL.ModelVM.Product
{
    public class DisplayProductInShopVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsFavorite { get; set; }
    }
}
