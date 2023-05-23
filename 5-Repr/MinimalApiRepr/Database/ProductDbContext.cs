using Microsoft.EntityFrameworkCore;

namespace MinimalApiRepr.Database;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Entity.Product> Products { get; set; } = default!;
}