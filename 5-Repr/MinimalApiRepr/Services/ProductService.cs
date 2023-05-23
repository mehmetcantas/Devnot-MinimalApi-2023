using Microsoft.EntityFrameworkCore;
using MinimalApiRepr.Database;

namespace MinimalApiRepr.Services;

public class ProductService : IProductService
{
    private readonly ProductDbContext _dbContext;
    public ProductService(ProductDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddProduct(Entity.Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateProduct(Entity.Product product)
    {
        _dbContext.Products.Update(product);
        var result = await _dbContext.SaveChangesAsync();

        return result > 0;
    }
    public async Task<Entity.Product?> GetProduct(int id)
    {
        if (id == 0)
            return null;
        
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Entity.Product>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
            return false;
        
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}