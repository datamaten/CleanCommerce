using System.Reflection;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Application.FunctionalTests.Factory;
public class TestServiceProviderFactory
{
    public static IServiceProvider Create(DbContextOptions<ApplicationDbContext> dbContextOptions, params Assembly[] assemblies)
    {
        var services = new ServiceCollection();

        // Tilføj DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(dbContextOptions.FindExtension<SqliteOptionsExtension>().Connection));

        // Registrer IApplicationDbContext
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Tilføj AutoMapper
        services.AddAutoMapper(assemblies);

        // Tilføj FluentValidation
        services.AddValidatorsFromAssemblies(assemblies);

        // Tilføj MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

        // Tilføj andre services efter behov
        // services.AddScoped<IYourService, YourService>();

        return services.BuildServiceProvider();
    }
}
