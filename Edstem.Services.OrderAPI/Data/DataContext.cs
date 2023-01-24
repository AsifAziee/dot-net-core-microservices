using Edstem.Services.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.OrderAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) 
    { 
    }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<OrderDetails>()
            .HasKey(s => new { s.OrderHeaderId, s.OrderDetailsId });
    }
}