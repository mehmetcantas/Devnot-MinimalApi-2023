using MinimalApiRepr.Services;

namespace MinimalApiRepr.Product.Create;

public static class Endpoint
{
    public static WebApplication MapProductCreateEndpoint(this WebApplication app)
    {
        app.MapPost("api/product", async (Request req,IProductService productService) =>
        {
            await productService.AddProduct(req.ToEntity());
            return Results.Created($"/products/{req.Id}", new Response
            {
                Id = req.Id,
                Name = req.Name,
                Price = req.Price,
                Stock = req.Stock
            });
        });


        return app;
    }
}