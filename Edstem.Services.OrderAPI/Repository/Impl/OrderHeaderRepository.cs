using AutoMapper;
using Edstem.Services.OrderAPI.Data;
using Edstem.Services.OrderAPI.Models;
using Edstem.Services.OrderAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.OrderAPI.Repository.Impl;

public class OrderHeaderRepository : IOrderHeaderRepository
{
    private readonly DbContextOptions<DataContext> _dbOptions;

    public OrderHeaderRepository(DbContextOptions<DataContext> dbOptions)
    {
        _dbOptions = dbOptions;
    }

    public async Task<OrderHeader> AddOrder(OrderHeader orderHeader)
    {
        //var orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);
        await using var db = new DataContext(_dbOptions);
        await db.OrderHeaders.AddAsync(orderHeader);

        // update order details
        foreach (var orderDetail in orderHeader.OrderDetails)
        {
            orderDetail.OrderHeaderId = orderHeader.OrderHeaderId;
        }
        await db.SaveChangesAsync();

        return orderHeader;

    }

    public async Task<OrderHeader> GetOrder(int orderHeaderId)
    {
        await using var db = new DataContext(_dbOptions);
        var order = await db.OrderHeaders
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync(s => s.OrderHeaderId == orderHeaderId);
        return order;
    }
}