using Edstem.Services.ProductAPI.Models;

namespace Edstem.Services.ProductAPI.Repository;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductAsync(int productId);
    Task<bool> CreateProductAsync(Product product);

    Task<bool> DeleteAsync(int productId);


    Task<bool> UpdateProductAsync(int productId, Product product);
}