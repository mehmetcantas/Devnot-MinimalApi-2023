using MinimalApiRepr.Services;

namespace MinimalApiRepr.Product.Delete;

public static class Endpoint
{
    public static WebApplication MapProductDeleteEndpoint(this WebApplication app)
    {
        app.MapDelete("api/product/{id}", async (int id,IProductService productService) =>
        {
            await productService.Delete(id);
            return Results.Ok(new {Message = "Product deleted"});
        });

        return app;
    }
}