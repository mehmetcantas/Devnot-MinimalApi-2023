using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace MinimalApiSwagger;

public static class OpenApiAuthExtension
{
    public static TBuilder WithAuthentication<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        var scheme = new OpenApiSecurityScheme()
        {
            Type = SecuritySchemeType.Http,
            Name = JwtBearerDefaults.AuthenticationScheme,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Reference = new()
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        };
        builder.WithOpenApi(operation => new(operation)
        {
            Security =
            {
                new()
                {
                    [scheme] = new List<string>()
                }
            }
        });
        return builder;
    }
}