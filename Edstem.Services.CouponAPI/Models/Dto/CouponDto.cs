using System.ComponentModel.DataAnnotations;

namespace Edstem.Services.CouponAPI.Models.Dto;

public class CouponDto
{
    public int CouponId { get; set; }

    [Required] public string? CouponCode { get; set; }

    [Range(1.0, 1000.0)] public double DiscountAmount { get; set; }
}