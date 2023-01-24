using AutoMapper;
using Edstem.Services.CouponAPI.Models;
using Edstem.Services.CouponAPI.Models.Dto;

namespace Edstem.Services.CouponAPI.Mapping;

public class ModelMappingProfile : Profile
{
    public ModelMappingProfile()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}