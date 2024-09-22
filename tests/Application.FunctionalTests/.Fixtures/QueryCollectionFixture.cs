using Application.FunctionalTests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Application.FunctionalTests.Fixtures;


public class QueryCollectionFixture : TestBase, ICollectionFixture<QueryCollectionFixture>
{
    public QueryCollectionFixture()
    {
        SeedData();
    }

    public override async Task InitializeAsync()
    {
        await SeedDataAsync();
    }

    private void SeedData()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!context.Products.Any())
        {
            context.Products.AddRange(FakeItems.Products);
            context.SaveChanges();
        }
    }
}
