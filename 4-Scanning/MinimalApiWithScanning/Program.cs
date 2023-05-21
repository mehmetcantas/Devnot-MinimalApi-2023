using MinimalApiWithScanning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions();

var app = builder.Build();

app.UseEndpointDefinitions();
app.Run();