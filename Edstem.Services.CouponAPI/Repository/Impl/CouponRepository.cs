using Edstem.Services.CouponAPI.Data;
using Edstem.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.CouponAPI.Repository.Impl;

public class CouponRepository : ICouponRepository
{
    private readonly DataContext _dataContext;

    public CouponRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Coupon?> GetCouponsByIdAsync(int couponId)
    {
        return await _dataContext.Coupons.SingleOrDefaultAsync(c => c.CouponId == couponId);
    }

    public async Task<List<Coupon>> GetCouponsAsync()
    {
        return await _dataContext.Coupons.ToListAsync();
    }

    public async Task<Coupon?> GetCouponsByCodeAsync(string couponCode)
    {
        return await _dataContext.Coupons.SingleOrDefaultAsync(c => c.CouponCode == couponCode);
    }

    public async Task<bool> CreateCouponAsync(Coupon coupon)
    {
        await _dataContext.Coupons.AddAsync(coupon);
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCouponAsync(Coupon coupon)
    {
        _dataContext.Coupons.Update(coupon);
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCouponAsync(Coupon coupon)
    {
            _dataContext.Coupons.Remove(coupon);
            return await _dataContext.SaveChangesAsync() > 0;
    }
}