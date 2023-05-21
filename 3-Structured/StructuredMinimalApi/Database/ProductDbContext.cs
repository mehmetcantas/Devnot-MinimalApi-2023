using Microsoft.EntityFrameworkCore;
using StructuredMinimalApi.Services.Product;

namespace StructuredMinimalApi.Database;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; } = default!;
}