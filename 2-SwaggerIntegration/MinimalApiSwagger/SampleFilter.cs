namespace MinimalApiSwagger;

public class SampleFilter: IEndpointFilter
{
    protected readonly ILogger Logger;
    private readonly string _methodName;

    protected SampleFilter(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger<SampleFilter>();
        _methodName = GetType().Name;
    }

    public virtual async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        Logger.LogInformation("{MethodName} Before next", _methodName);
        var result = await next(context);
        Logger.LogInformation("{MethodName} After next", _methodName);
        return result;
    }
}