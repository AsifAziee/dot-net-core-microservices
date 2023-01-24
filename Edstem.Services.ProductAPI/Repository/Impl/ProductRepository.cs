using Edstem.Services.ProductAPI.Data;
using Edstem.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Edstem.Services.ProductAPI.Repository.Impl;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _dataContext;

    public ProductRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<bool> CreateProductAsync(Product product)
    {
        await _dataContext.Products.AddAsync(product);
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public async Task<Product?> GetProductAsync(int productId)
    {
        return await _dataContext.Products.SingleOrDefaultAsync(s => s.ProductId == productId);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _dataContext.Products.ToListAsync();
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        var prod = _dataContext.Products.SingleOrDefault(x => x.ProductId == productId);
        if (prod != null)
        {
            _dataContext.Products.Remove(prod);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        else
        {
            return false;
        }
    }


    public async Task<bool> UpdateProductAsync(int productId, Product product)
    {
        var original = await _dataContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == productId);

        if (original != null)
        {
            product.ProductId = original.ProductId;
            _dataContext.Products.Update(product);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        else
        {
            return false;
        }
    }
}