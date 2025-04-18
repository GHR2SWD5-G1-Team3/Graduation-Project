using BLL.ModelVM.Product;

namespace BLL.ModelVM.Home
{
    public class HomeVM
    {
        public List<DisplayProductInShopVM> TopProducts { get; set; } 
        public List<DisplayProductInShopVM> BestProducts { get; set; } 
    }
}
