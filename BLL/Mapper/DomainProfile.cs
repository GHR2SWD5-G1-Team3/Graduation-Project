namespace BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            //User
            CreateMap<SignUpvM, User>();
            CreateMap<SignInVM, User>();
            CreateMap<AddNewUserVM, User>();
            CreateMap<DisplayUser, EditUser>();

            CreateMap<User, DisplayUser>();
            CreateMap<AddNewUserVM, User>();

            //Role
            CreateMap<Role, DisplayRoleVm>();

            #region Order Mappings
            // Map Cart to CreateOrderVM (for Checkout)
            CreateMap<Cart, CreateOrderVM>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.CartProducts.Select(cd => new OrderProductVM
                {
                    Id = cd.ProductId,
                    Name = cd.Product.Name,
                    Quantity = cd.Quantity,
                    UnitPrice = cd.Product.UnitPrice,
                    Imagepath = cd.Product.ImagePath
                }).ToList()));

            // Order to DisplayOrderVM (display order summary)
            CreateMap<Order, DisplayOrderVM>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderDetails.Select(od => new OrderProductVM
                {
                    Id = od.ProductId,
                    Name = od.Product.Name,
                    Quantity = od.Quantity,
                    UnitPrice = od.Price,
                    Imagepath = od.Product.ImagePath
                }).ToList()))
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.OrderDetails.Sum(od => od.Price * od.Quantity)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Sum(od => od.Price * od.Quantity)))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.OrderDetails));

            // Order to OrderDetailsVM (for detailed order view)
            CreateMap<Order, OrderDetailsVM>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Sum(od => od.Price * od.Quantity)))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderDetails.Select(od => new OrderProductVM
                {
                    Id = od.ProductId,
                    Name = od.Product.Name,
                    Quantity = od.Quantity,
                    UnitPrice = od.Price,
                    Imagepath = od.Product.ImagePath
                }).ToList()));
            #endregion

            // Coupon Mappings
            CreateMap<dynamic, Coupon>();

            // CartDetails Mappings
            CreateMap<DisplayCartDetailsVM, CartDetails>().ReverseMap();

            #region Category Mappings
            CreateMap<Category, CategoryVM>()
                .ForMember(dest => dest.ExistingImagePath,
                opt => opt.MapFrom(src => "/images/" + src.Image))
                .ReverseMap()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Category, GetAllCategoryVM>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Image))
                .ReverseMap();
            #endregion

            #region SubCategory Mappings
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

            //Product Mappings
            CreateMap<Product, DisplayProductInShopVM>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SubCategory.Category.Name))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory.Name))
                .ReverseMap();

            CreateMap<CreateProductVM, Product>().ReverseMap();
            CreateMap<DisplayCartDetailsVM, Cart>().ReverseMap();
            CreateMap<ProductDetailsVM, Product>().ReverseMap();
            CreateMap<EditProductVM, Product>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId ?? string.Empty)) // Default to empty string if null
                .ReverseMap();
            CreateMap<Product, DeletedProductsVM>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // or src.User.UserName
                .ReverseMap();

            // Coupon Mappings
            CreateMap<Coupon, EditCouponVM>().ReverseMap();
            CreateMap<Coupon, CreateCouponVM>().ReverseMap();

            // Payment Mappings
            CreateMap<Payment, PaymentVM>().ReverseMap();
            CreateMap<CreatePaymentVM, Payment>().ReverseMap();

            #region Cart Mappings
            CreateMap<CartDetails, DisplayCartDetailsVM>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImagePath))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.UnitPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.UnitPrice));

            CreateMap<DisplayCartDetailsVM, CartDetails>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
            #endregion

            // From OrderDetail to full display product info
            CreateMap<OrderDetails, OrderProductVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.UnitPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.Product.DiscountPrecentage))
                .ForMember(dest => dest.Imagepath, opt => opt.MapFrom(src => src.Product.ImagePath));

            // From Order to VMs that need full product list
            CreateMap<Order, DisplayOrderVM>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Sum(od => od.Quantity * od.Product.UnitPrice))));
       

            CreateMap<Order, OrderDetailsVM>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Sum(od => od.Quantity * od.Product.UnitPrice)));

            // From OrderDetail to minimal admin display
            CreateMap<OrderDetails, OrderItemVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.UnitPrice));

        }
    }
    
}
