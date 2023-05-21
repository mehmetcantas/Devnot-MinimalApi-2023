namespace MinimalApiWithScanning;

public static class EndpointDefinitionExtensions
{
    public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] markers)
    {
        var endpointDefinitions = new List<IEndpointDefinition>();

        foreach (var marker in markers)
        {
            endpointDefinitions.AddRange(marker.Assembly.ExportedTypes
                .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>());
        }
        
        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineServices(services);
        }
        
        services.AddSingleton<IEnumerable<IEndpointDefinition>>(endpointDefinitions);
    }
    
    public static void UseEndpointDefinitions(this WebApplication app, params Type[] markers)
    {
        var endpointDefinitions = app.Services.GetRequiredService<IEnumerable<IEndpointDefinition>>();

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineEndpoints(app);
        }
    }
}