using Edstem.Services.OrderAPI.Models;

namespace Edstem.Services.OrderAPI.Repository;

public interface IOrderHeaderRepository
{
    Task<OrderHeader> GetOrder(int orderHeaderId);
    Task<OrderHeader> AddOrder(OrderHeader orderHeader);
}