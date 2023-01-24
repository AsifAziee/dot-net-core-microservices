using Edstem.Services.ShoppingCartAPI.Models.Dto;

namespace Edstem.Services.ShoppingCartAPI.Repository;

public interface ICouponRepository
{
    Task<CouponDto?> GetCoupon(string couponCode);
}