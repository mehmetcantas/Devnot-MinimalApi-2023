using FluentValidation;
using StructuredMinimalApi.Services.Product.Validators;

namespace StructuredMinimalApi.Services.Product;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/products", async (IProductService productService) =>
        {
            var result = await productService.GetProducts();
            return Results.Ok(result.Select(x => new ProductResponse(x.Id, x.Name, x.Price, x.Stock)));
        });

        app.MapGet("/products/{id}", async (int id, IProductService productService) =>
        {
            var product = await productService.GetProduct(id);
            if (product == null)
                return Results.NotFound(new {Message = "Product not found"});

            return Results.Ok(new ProductResponse(product.Id, product.Name, product.Price, product.Stock));
        });

        app.MapPost("/products",
            async (Product product, IProductService productService) =>
            {
                await productService.AddProduct(product);
                return Results.Created($"/products/{product.Id}", product);
            }).WithValidation<Product>();

        app.MapPut("/products/{id}", async (int id, Product product, IProductService productService, IValidator<Product> validator) =>
        {
            var validationResult = await validator.ValidateAsync(product);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);
            
            var result = await productService.UpdateProduct(product);

            if (result)
                return Results.Ok(new {Message = "Product updated"});

            return Results.NotFound(new {Message = "Product not found"});
        });

        app.MapDelete("/products/{id}", async (int id, IProductService productService) =>
        {
            var result = await productService.Delete(id);

            if (result)
                return Results.Ok(new {Message = "Product deleted"});

            return Results.NotFound(new {Message = "Product not found"});
        });
    }

    public static void AddProductServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Product>, ProductValidator>();
        services.AddScoped<IProductService, ProductService>();
    }
}