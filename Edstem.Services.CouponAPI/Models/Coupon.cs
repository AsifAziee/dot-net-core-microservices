using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edstem.Services.CouponAPI.Models;

public class Coupon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int CouponId { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 4, ErrorMessage = "* Part numbers must be between 3 and 50 character in length.")]
    public string? CouponCode { get; set; }

    public double DiscountAmount { get; set; }
}