﻿using BLL.ModelVM.CartDetails;
using BLL.ModelVM.Category;
using BLL.ModelVM.Product;
using BLL.ModelVM.SubCategory;

namespace BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            //User
            CreateMap<SignUpvM, User>();
            CreateMap<SignInVM, User>();


            //Order
            CreateMap<CreateOrderVM, Order>();
            CreateMap<Order, DisplayOrderVM>();
            //coupon
            CreateMap<dynamic, Coupon>();

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
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.SubCategory.Category.Name)).ReverseMap();
            CreateMap<CreateProductVM, Product>().ReverseMap();
            CreateMap<DisplayCartDetailsVM ,DAL.Enities.Cart>().ReverseMap();
        }
    }
}
