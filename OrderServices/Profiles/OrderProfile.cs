using AutoMapper;
using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderServices.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<CreateOrderDto, Order>();
            CreateMap<Order, ReadOrderDto>();
            CreateMap<Order, ReadAllOrder>();
            CreateMap<Product, ProductOutDto>();
            CreateMap<Wallet, WalletOutDto>();
        }
    }
}
