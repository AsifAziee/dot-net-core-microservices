using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edstem.Services.ShoppingCartAPI.Models;

public class CartHeader
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartHeaderId { get; set; }

    public string UserId { get; set; }

    public string? CouponCode { get; set; }
}