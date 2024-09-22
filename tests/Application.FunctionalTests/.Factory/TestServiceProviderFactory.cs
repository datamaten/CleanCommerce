using System.Data.Common;
using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.FunctionalTests.Mocks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence.Context;

namespace Application.FunctionalTests.Factory;

public static class TestServiceProviderFactory
{
    private static readonly Assembly ApplicationAssembly = typeof(IApplicationDbContext).Assembly;
    public static IServiceProvider Create(DbConnection connection)
    {
        var services = new ServiceCollection();

        services.AddDbContext<ApplicationDbContext>((_, options) => options.UseSqlite(connection));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddAutoMapper(ApplicationAssembly);

        services.AddValidatorsFromAssembly(ApplicationAssembly);

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(ApplicationAssembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });


        services.AddLogging(builder => builder.AddProvider(new MockLoggerProvider()));

        return services.BuildServiceProvider();
    }
}
