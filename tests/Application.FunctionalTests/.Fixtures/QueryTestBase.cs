using MediatR;

namespace Application.FunctionalTests.Fixtures;

public abstract class QueryTestBase : IClassFixture<QueryCollectionFixture>
{
    protected readonly QueryCollectionFixture _fixture;

    protected QueryTestBase(QueryCollectionFixture fixture)
    {
        _fixture = fixture;
    }

    protected static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        return await QueryCollectionFixture.SendAsync(request);
    }

    protected static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        return await QueryCollectionFixture.FindAsync<TEntity>(keyValues);
    }

    protected static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        await QueryCollectionFixture.AddAsync(entity);
    }

    protected static async Task<int> CountAsync<TEntity>()
        where TEntity : class
    {
        return await QueryCollectionFixture.CountAsync<TEntity>();
    }
}
