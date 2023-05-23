using MinimalApiRepr.Database;
using MinimalApiRepr.Product.Create;
using MinimalApiRepr.Product.Delete;
using MinimalApiRepr.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



builder.Services.AddProductDb();
builder.Services.AddScoped<IProductService, ProductService>();


app.MapGet("/", () => "Hello World!");

app.MapProductCreateEndpoint()
    .MapProductDeleteEndpoint();

app.Run();