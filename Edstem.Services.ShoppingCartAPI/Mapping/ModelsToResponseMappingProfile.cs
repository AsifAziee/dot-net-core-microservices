using AutoMapper;
using Edstem.Services.ShoppingCartAPI.Models;
using Edstem.Services.ShoppingCartAPI.Models.Dto;

namespace Edstem.Services.ShoppingCartAPI.Mapping;

public class DomainToResponseMappingProfile : Profile
{
    public DomainToResponseMappingProfile()
    {
        CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
        CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
        CreateMap<Cart, CartDto>().ReverseMap();
    }
}