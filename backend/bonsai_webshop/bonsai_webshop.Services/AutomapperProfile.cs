using AutoMapper;
using bonsai_webshop.DataContext.DTOs;
using bonsai_webshop.DataContext.Entities;

namespace bonsai_webshop.Services
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() {
            CreateMap<Product, ProductDTO>();

            CreateMap<UserCreateDTO, User>();

            CreateMap<Order, OrderDTO>();

            CreateMap<ProductCategory, ProductCategoryDTO>();

            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
