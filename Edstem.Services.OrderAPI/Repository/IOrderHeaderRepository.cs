using Edstem.Services.OrderAPI.Models.Dto;

namespace Edstem.Services.OrderAPI.Repository;

public interface IOrderHeaderRepository
{
    Task<OrderHeaderDto> GetOrderHeadersAsync(int orderHeaderId);
    Task<OrderHeaderDto> CreateOrderHeadersAsync(OrderHeaderDto orderHeader);
}