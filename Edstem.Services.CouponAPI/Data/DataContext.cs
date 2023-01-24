using Edstem.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.CouponAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }
}