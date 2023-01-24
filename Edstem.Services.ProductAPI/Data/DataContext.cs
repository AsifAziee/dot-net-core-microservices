using Edstem.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.ProductAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}