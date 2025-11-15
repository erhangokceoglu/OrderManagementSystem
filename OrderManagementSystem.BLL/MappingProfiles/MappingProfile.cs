using AutoMapper;
using OrderManagementSystem.BLL.DTOs.CustomerDtos;
using OrderManagementSystem.BLL.DTOs.OrderDtos;
using OrderManagementSystem.BLL.DTOs.OrderInfoDtos;
using OrderManagementSystem.BLL.DTOs.ProductDtos;
using OrderManagementSystem.DAL.Entities;
namespace OrderManagementSystem.BLL.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Customer, CustomerDto>();
        CreateMap<Order, OrderDto>().ForMember(dest => dest.OrderInfos, opt => opt.MapFrom(src => src.OrderInfos));
        CreateMap<OrderInfo, OrderInfoDto>().ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product!.Name));
    }
}
