namespace MinimalApiRepr.Services;

public interface IProductService
{
    Task AddProduct(Entity.Product product);
    Task<Entity.Product?> GetProduct(int id);
    Task<IEnumerable<Entity.Product>> GetProducts();
    Task<bool> UpdateProduct(Entity.Product product);
    Task<bool> Delete(int id);
}