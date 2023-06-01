using System.Reflection;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.IO;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinimalApiSwagger;

public static class StreamManager
{
    public static readonly RecyclableMemoryStreamManager Instance = new();
}

public class XmlResult<T> : IResult, IEndpointMetadataProvider
{
    private static readonly XmlSerializer _serializer = new (typeof(T));
    private readonly T _result;

    public XmlResult(T result)
    {
        _result = result;
    }
    
    public Task ExecuteAsync(HttpContext httpContext)
    {
        using var memoryStream = StreamManager.Instance.GetStream();
        
        _serializer.Serialize(memoryStream, _result);
        httpContext.Response.ContentType = "application/xml";

        memoryStream.Position = 0;
        return memoryStream.CopyToAsync(httpContext.Response.Body);
    }

    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
        builder.Metadata.Add(new XmlEndpointMetadata(typeof(T)));
    }
}

public class XmlEndpointMetadata : IProducesResponseTypeMetadata
{
    public XmlEndpointMetadata(Type? type)
    {
        Type = type;
    }

    public Type Type { get; }

    public int StatusCode { get; set; } = StatusCodes.Status200OK;

    public IEnumerable<string> ContentTypes { get; set; } = new[] {"application/xml"};
}

public static class XmlResultExtensions
{
    public static XmlResult<T> Xml<T>(this IResultExtensions _, T result) => new XmlResult<T>(result);
}

public class XmlOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        foreach (var (_, response) in operation.Responses)
        {
            if (!response.Content.TryGetValue("application/xml", out var xmlResponseContent))
            {
                continue;
            }

            xmlResponseContent.Schema.Xml = new OpenApiXml()
            {
                Name = "Root",
            };
        }
    }
}