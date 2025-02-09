using AutoMapper;
using ProductManagement.Api.Models;
using ProductManagement.Data.Entities;

namespace ProductManagement.Domain.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
