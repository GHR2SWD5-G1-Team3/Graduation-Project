namespace BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            //User
            CreateMap<SignUpvM, User>();
            CreateMap<SignInVM, User>();
            CreateMap<AddNewUserVM,User>();
            CreateMap<User,DisplayUser>();
            CreateMap<AddNewUserVM, User>();
            //Role
            CreateMap<Role,DisplayRoleVm>();
            //Order
            CreateMap<CreateOrderVM, Order>();
            CreateMap<Order, DisplayOrderVM>();
            //coupon
            CreateMap<dynamic, Coupon>();
            //CartDetails
            CreateMap<DisplayCartDetailsVM,CartDetails>().ReverseMap();

            #region Category
            CreateMap<Category, CategoryVM>()
             .ForMember(dest => dest.ExistingImagePath,
             opt => opt.MapFrom(src => "/images/" + src.Image))
             .ReverseMap()
             .ForMember(dest => dest.Image, opt => opt.Ignore());

            // GetAllCategoryVM
            CreateMap<Category, GetAllCategoryVM>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image))
            .ReverseMap();
            #endregion

            #region SubCategory
            CreateMap<SubCategory, GetAllSubCategoryVM>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImagePath))
            .ReverseMap();

            CreateMap<SubCategory, SubCategoryVM>()
            .ForMember(dest => dest.ExistingImagePath,
            opt => opt.MapFrom(src => "/images/" + src.ImagePath))
            .ReverseMap()
            .ForMember(dest => dest.ImagePath, opt => opt.Condition(src => src.Image != null));
            #endregion

            //Product
            CreateMap<Product, DisplayProductInShopVM>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.SubCategory.Category.Name)
            ).ForMember(
                dest => dest.SubCategoryName,
                opt => opt.MapFrom(src => src.SubCategory.Name))
            .ReverseMap();

            CreateMap<CreateProductVM, Product>().ReverseMap();
            CreateMap<DisplayCartDetailsVM ,Cart>().ReverseMap();
            CreateMap<ProductDetailsVM, Product>().ReverseMap();
            CreateMap<EditProductVM, Product>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId ?? string.Empty)) // Default to empty string if null
                .ReverseMap();
            CreateMap<Product, DeletedProductsVM>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // or src.User.UserName
                .ReverseMap();
            //Coupon
            CreateMap<Coupon, EditCouponVM>().ReverseMap();
            CreateMap<Coupon, CreateCouponVM>().ReverseMap();
        }
    }
}
