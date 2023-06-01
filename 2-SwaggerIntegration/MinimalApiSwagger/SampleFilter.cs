namespace MinimalApiSwagger;

public class SampleFilter: IEndpointFilter
{
    protected readonly ILogger Logger;
    private readonly string _methodName;

    public SampleFilter(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<SampleFilter>();
        _methodName = GetType().Name;
    }

    public virtual async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        Logger.LogInformation($"Received request for {context.HttpContext.Request.Path}");
        var result = await next(context);
        Logger.LogInformation("{MethodName} executed after endpoint", _methodName);
        return result;
    }
}