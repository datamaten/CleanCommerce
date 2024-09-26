using System.Reflection;

namespace API.Infrastructure;

public static class WebApplicationExtensions
{
    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName}")
            .WithGroupName(groupName)
            .WithTags(groupName)
            .WithOpenApi();
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        // Get IHttpContextAccessor from services
        var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                // Set IHttpContextAccessor to allow accessing HttpContext
                instance.SetHttpContextAccessor(httpContextAccessor);
                instance.Map(app);
            }
        }
        return app;
    }
}
