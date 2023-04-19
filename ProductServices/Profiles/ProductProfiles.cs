using AutoMapper;
using ProductServices.Dtos;
using ProductServices.Models;

namespace ProductServices.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles() 
        {
            CreateMap<Product, ReadProductDto>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<CreateProductDto, Product>();
        }
    }
}
