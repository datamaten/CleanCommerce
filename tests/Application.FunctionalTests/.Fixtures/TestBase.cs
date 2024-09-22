using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Application.FunctionalTests.Fixtures;

public abstract class TestBase : IAsyncLifetime
{
    private readonly SqliteTestDatabase _database;
    protected readonly IServiceProvider _serviceProvider;
    private static IServiceScopeFactory scopeFactory = default!;

    protected TestBase()
    {
        var applicationAssembly = typeof(IApplicationDbContext).Assembly;
        _database = new SqliteTestDatabase(applicationAssembly);
        _serviceProvider = _database.CreateServiceProvider();
        scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _database.DisposeAsync();
    }

    protected virtual Task SeedDataAsync()
    {
        return Task.CompletedTask;
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.Set<TEntity>().FindAsync(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }
}
