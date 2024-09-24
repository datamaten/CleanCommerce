using Application.FunctionalTests.Database;
using Application.FunctionalTests.Factory;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Application.FunctionalTests.Fixtures;
public abstract class FixtureBase : IAsyncLifetime
{
    protected SqliteDatabase _database = null!;
    protected CustomWebApplicationFactory _factory = null!;
    protected IServiceScopeFactory _scopeFactory = null!;

    public virtual async Task InitializeAsync()
    {
        var database = new SqliteDatabase();
        await database.InitializeAsync();

        _database = database;
        _factory = new CustomWebApplicationFactory(_database.GetConnection());
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public async Task DisposeAsync()
    {
        await _database.DisposeAsync();
    }

    public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    public async Task AddRangeAsync<TEntity>(List<TEntity> entity) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Set<TEntity>().AddRange(entity);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    public async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        return await context.Set<TEntity>().FindAsync(keyValues);
    }

    public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(int id)
        where TEntity : BaseEntity
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }
}
