using Microsoft.EntityFrameworkCore;
using MinimalApiWithScanning.Models;

namespace MinimalApiWithScanning.Database;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; } = default!;
}