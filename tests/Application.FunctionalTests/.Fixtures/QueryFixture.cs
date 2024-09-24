using Application.FunctionalTests.Fake;

namespace Application.FunctionalTests.Fixtures;

[Collection("QueryCollection")]
public class QueryFixture : FixtureBase
{
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        await AddRangeAsync(FakeEntities.Products);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryFixture> { }
