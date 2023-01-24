using AutoMapper;
using Edstem.Services.OrderAPI.Data;
using Edstem.Services.OrderAPI.Models;
using Edstem.Services.OrderAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.OrderAPI.Repository.Impl;

public class OrderHeaderRepository : IOrderHeaderRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public OrderHeaderRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<OrderHeaderDto> CreateOrderHeadersAsync(OrderHeaderDto orderHeaderDto)
    {
        var orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);
        await _dataContext.OrderHeaders.AddAsync(orderHeader);

        // update order details
        foreach (var orderDetail in orderHeader.OrderDetails)
        {
            orderDetail.OrderHeaderId = orderHeader.OrderHeaderId;
        }
        await _dataContext.SaveChangesAsync();

        return _mapper.Map<OrderHeaderDto>(orderHeader);

    }

    public async Task<OrderHeaderDto> GetOrderHeadersAsync(int orderHeaderId)
    {
        var order = await _dataContext.OrderHeaders
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync(s => s.OrderHeaderId == orderHeaderId);
        return _mapper.Map<OrderHeaderDto>(order);

    }
}