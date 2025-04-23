namespace BLL.ModelVM.Profile
{
    public class ProfileVm
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Government { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RoleName { get; set; }
        public bool IsCurrentUser { get; set; }
        public List<DisplayProductInShopVM>? MyProducts { get; set; }
        public List<DisplayProductInShopVM>? WishList { get; set; }
    }
}
