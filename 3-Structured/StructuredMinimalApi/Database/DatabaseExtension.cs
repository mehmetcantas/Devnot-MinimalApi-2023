using Microsoft.EntityFrameworkCore;

namespace StructuredMinimalApi.Database;

public static class DatabaseExtension
{
    public static IServiceCollection AddProductDb(this IServiceCollection services)
    {
        services.AddDbContext<ProductDbContext>(opt =>
        {
            opt.UseInMemoryDatabase(databaseName: "InMemoryDb");
        });

        return services;
    }
}