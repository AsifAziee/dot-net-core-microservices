using Edstem.Services.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.ShoppingCartAPI;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<CartDetails> CartDetails { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
}