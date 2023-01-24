using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edstem.Services.OrderAPI.Models;

public class OrderHeader
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderHeaderId { get; set; }
    public string UserId { get; set; }
    public double OrderTotal { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime PickupDateTime { get; set; }
    public DateTime OrderTime { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }   
    public int CartTotalItems { get; set; }
    public List<OrderDetails> OrderDetails { get; set; }
}