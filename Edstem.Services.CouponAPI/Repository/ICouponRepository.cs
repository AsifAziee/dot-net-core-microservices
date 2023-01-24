
using Edstem.Services.CouponAPI.Models;

namespace Edstem.Services.CouponAPI.Repository;

public interface ICouponRepository
{
    Task<List<Coupon>> GetCouponsAsync();
    Task<Coupon?> GetCouponsByIdAsync(int couponId);
    Task<Coupon?> GetCouponsByCodeAsync(string couponCode);
    Task<bool> CreateCouponAsync(Coupon coupon);
    Task<bool> UpdateCouponAsync(Coupon coupon);
    Task<bool> DeleteCouponAsync(Coupon coupon);
}
