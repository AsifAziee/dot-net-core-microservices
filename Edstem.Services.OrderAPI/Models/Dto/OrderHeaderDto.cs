namespace Edstem.Services.OrderAPI.Models.Dto;

public class OrderHeaderDto
{
    public int OrderHeaderId { get; set; }
    public string UserId { get; set; }
    public double OrderTotal { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PickupDateTime { get; set; }
    public string OrderTime { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int CartTotalItems { get; set; }
    public List<OrderDetailsDto> OrderDetails { get; set; }
}