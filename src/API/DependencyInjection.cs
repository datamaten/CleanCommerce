using API.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSwag.Generation.Processors;
using Persistence.Context;
using NJsonSchema;
using NSwag;
using API.Middleware;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();
        services.AddScoped<IApiGuard, ApiGuard>();
        services.AddHttpContextAccessor();
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();
        services.AddApiDocument();

        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddApiDocument(this IServiceCollection services)
    {
        services.AddOpenApiDocument(options =>
        {
            options.PostProcess = document =>
            {
                document.Info = new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clean Commerce API",
                    Description = "An API for managing Clean Commerce",
                };
            };

            options.OperationProcessors.Add(new OperationProcessor(context =>
            {
                context.OperationDescription.Operation.Responses
                    .Where(r => int.TryParse(r.Key, out var statusCode) && statusCode >= 400)
                    .ToList()
                    .ForEach(r => r.Value.Schema = JsonSchema.FromType<ProblemDetails>());

                return true;
            }));
        });

        return services;
    }
}
