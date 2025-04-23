namespace BLL.Services.Implementation
{
    public class ProfileServices(UserManager<User> userManager, IProductService productService, IMapper mapper, IFavoriteProductServices favoriteProductServices) : IProfileServices
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IProductService _productService = productService;
        private readonly IMapper _mapper = mapper;
        private readonly IFavoriteProductServices _favoriteProductServices = favoriteProductServices;
        public async Task<ProfileVm> GetUserProfileAsync(string userId, string currentUserId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;
            var model = new ProfileVm
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Image = user.Image,
                City = user.City,
                Government = user.Government,
                CreatedAt = user.CreatedAt,
                IsCurrentUser = user.Id == currentUserId,
                MyProducts = [],
                WishList = []
            };
            var myproducts= await _productService.GetAllProductsAsync(a => a.UserId == user.Id);
            model.MyProducts=_mapper.Map<List<DisplayProductInShopVM>>(myproducts);
            if (model.IsCurrentUser)
            { 
               model.WishList = await _favoriteProductServices.GetUserFavourites(user.Id);
            }

            return model;
        }

    }
}
