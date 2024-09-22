using Application.FunctionalTests.Factory;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.FunctionalTests.Fixtures;

public class SqliteTestDatabase : IAsyncDisposable
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<ApplicationDbContext> _contextOptions;
    private bool _disposed;

    public SqliteTestDatabase()
    {
        _connection = new SqliteConnection(InMemoryConnectionString);
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new ApplicationDbContext(_contextOptions);
        context.Database.EnsureCreated();
    }

    public ApplicationDbContext CreateContext()
    {
        return new ApplicationDbContext(_contextOptions);
    }

    public IServiceProvider CreateServiceProvider()
    {
        return TestServiceProviderFactory.Create(_connection);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (_disposed)
        {
            return;
        }

        await _connection.CloseAsync().ConfigureAwait(false);
        await _connection.DisposeAsync().ConfigureAwait(false);

        _disposed = true;
    }

    ~SqliteTestDatabase()
    {
        DisposeAsyncCore().AsTask().GetAwaiter().GetResult();
    }
}
