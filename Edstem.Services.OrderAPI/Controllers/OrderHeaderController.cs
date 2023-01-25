using AutoMapper;
using Edstem.Services.OrderAPI.Models;
using Edstem.Services.OrderAPI.Models.Dto;
using Edstem.Services.OrderAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Edstem.Services.OrderAPI.Controllers;

public class OrderHeaderController : Controller
{
    private readonly IOrderHeaderRepository _orderHeaderRepository;
    private readonly ILogger<OrderHeaderController> _logger;
    private readonly IMapper _mapper;

    public OrderHeaderController(IOrderHeaderRepository orderHeaderRepository, ILogger<OrderHeaderController> logger, IMapper mapper)
    {
        _orderHeaderRepository = orderHeaderRepository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("/api/orders/{orderHeaderId}")]
    public async Task<IActionResult> Get([FromRoute] int orderHeaderId)
    {
        try
        {
            var response = await _orderHeaderRepository.GetOrder(orderHeaderId);
            _logger.LogInformation($"Successfully retreived data with orderId: {orderHeaderId}");
            return Ok(_mapper.Map<OrderHeaderDto>(response));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while fetching data with orderId: {orderHeaderId}", ex);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("/api/orders")]
    public async Task<IActionResult> Create([FromBody] OrderHeaderDto orderHeaderDto)
    {
        try
        {
            var orderHeader = _mapper.Map<OrderHeader>(orderHeaderDto);
            var order = await _orderHeaderRepository.AddOrder(orderHeader);
            var uri = "/api/orders/" + order.OrderHeaderId;
            _logger.LogInformation($"Successfully placed order by user with userId: {order.UserId}");
            return Created(uri, order);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while creating order", ex);
            return BadRequest(ex.Message);
        }
    }
}