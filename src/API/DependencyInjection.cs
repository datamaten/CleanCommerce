using API.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSwag.Generation.Processors;
using Persistence.Context;
using NJsonSchema;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();
        services.AddHttpContextAccessor();
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddScoped<ApiGuard>();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "Your API Name";
            configure.Version = "v1";

            configure.OperationProcessors.Add(new OperationProcessor(context =>
            {
                context.OperationDescription.Operation.Responses
                    .Where(r => int.TryParse(r.Key, out var statusCode) && statusCode >= 400)
                    .ToList()
                    .ForEach(r => r.Value.Schema = JsonSchema.FromType<ProblemDetails>());

                return true;
            }));
        });

        services.AddHttpContextAccessor();

        return services;
    }
}
