using MinimalApiWithScanning.Models;

namespace MinimalApiWithScanning.Services;

public interface IProductService
{
    Task AddProduct(Product product);
    Task<Product?> GetProduct(int id);
    Task<IEnumerable<Product>> GetProducts();
    Task<bool> UpdateProduct(Product product);
    Task<bool> Delete(int id);
}