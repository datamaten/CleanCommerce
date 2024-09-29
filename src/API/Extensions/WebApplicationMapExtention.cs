using API.Modules.Base;

namespace API.Extensions;

public static class WebApplicationMapExtension
{
    public static RouteGroupBuilder MapModule(this WebApplication app, ModuleConfiguration group)
    {
        return app
            .MapGroup($"/api/{group.Endpoint}")
            .WithGroupName(group.GroupName)
            .WithTags(group.Tags)
            .WithOpenApi();
    }

    public static WebApplication MapModuleEndpoints(this WebApplication app)
    {
        var moduleType = typeof(Module);

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        var modules = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(moduleType));


        foreach (var type in modules)
        {
            if (Activator.CreateInstance(type) is Module instance)
            {
                instance.Map(app);
            }
        }
        return app;
    }
}
