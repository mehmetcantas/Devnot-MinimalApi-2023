using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinimalApiSwagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.OperationFilter<XmlOperationFilter>();
});

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// JWT authentication Settings
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication()
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.Configure<SwaggerGeneratorOptions>(opt => { opt.InferSecuritySchemes = true; });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin_policy", policy =>
        policy
            .RequireClaim(ClaimTypes.Role, "Admin"));



var app = builder.Build();

var devnotGroup = app.MapGroup("/devnot")
    .AddEndpointFilter<SampleFilter>();

devnotGroup.MapGet("/hello", () => "Hello Devnot!");

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Ok(new List<string> {"Hello World!"}))
    .AllowAnonymous()
    .Produces<string>(200, "application/json")
    .Produces(StatusCodes.Status404NotFound)
    .WithDescription("Devnot'a özel bir endpoint")
    .WithSummary("Devnot'a özel bir endpoint")
    .WithName("DevnotEndpoint")
    .WithTags("Devnot")
    .WithOpenApi();

app.MapGet("/hello", (string name) => $"Hello {name}!")
    .RequireAuthorization("admin_policy");

app.MapPost("/token", CreateToken);

app.MapGet("/products", GetProducts);

app.Run();

async Task<Results<XmlResult<List<Product>>,NotFound>> GetProducts()
{
    return Results.Extensions.Xml(new List<Product>
    {
        new Product
        {
            Name = "Product 1",
            Price = 10,
            Stock = 100
        },
        new Product
        {
            Name = "Product 2",
            Price = 20,
            Stock = 200
        },
        new Product
        {
            Name = "Product 3",
            Price = 30,
            Stock = 300
        },
    });
}

IResult CreateToken(LoginUserDto userDto)
{

    if (userDto.Username == "devnot" && userDto.Password == "devnot")
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Username),
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: "devnot.com",
            audience: "devnot.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(tokenString);
    }
    
    return Results.NotFound(new {Message = "User not found!"});
}
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

