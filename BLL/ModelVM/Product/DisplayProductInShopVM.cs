namespace BLL.ModelVM.Product
{
    public class DisplayProductInShopVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; private set; }
    }
}
