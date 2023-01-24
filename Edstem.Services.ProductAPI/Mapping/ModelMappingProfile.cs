using AutoMapper;
using Edstem.Services.ProductAPI.Models;
using Edstem.Services.ProductAPI.Models.Dto;

namespace Edstem.Services.ProductAPI.Mapping;

public class ModelMappingProfile : Profile
{
    public ModelMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}