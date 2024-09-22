using System.Reflection;
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
    private readonly Assembly[] _assemblies;

    public SqliteTestDatabase(params Assembly[] assemblies)
    {
        _assemblies = assemblies;

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
        return TestServiceProviderFactory.Create(_contextOptions, _assemblies);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}
