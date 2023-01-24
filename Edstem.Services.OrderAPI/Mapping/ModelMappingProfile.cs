using System.Globalization;
using AutoMapper;
using Edstem.Services.OrderAPI.Models;
using Edstem.Services.OrderAPI.Models.Dto;

namespace Edstem.Services.OrderAPI.Mapping;

public class ModelMappingProfile : Profile
{
    public ModelMappingProfile()
    {
        CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConvertor());
        CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();
        CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
    }
}
    
public class DateTimeTypeConvertor : ITypeConverter<string, DateTime>
{
    public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    {
        return DateTime.SpecifyKind(DateTime.ParseExact(source, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc);
    }
}