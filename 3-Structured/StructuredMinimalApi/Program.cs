using StructuredMinimalApi.Database;
using StructuredMinimalApi.Services.Product;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProductDb();
builder.Services.AddProductServices();

var app = builder.Build();


app.MapProductEndpoints();

app.Run();