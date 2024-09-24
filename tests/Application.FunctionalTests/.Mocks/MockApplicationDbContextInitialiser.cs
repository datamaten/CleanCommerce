using Persistence.Context;

namespace Application.FunctionalTests.Factory;
public class MockApplicationDbContextInitialiser : IApplicationDbContextInitialiser
{
    public Task InitialiseAsync() => Task.CompletedTask;

    public Task SeedAsync() => Task.CompletedTask;

    public Task TrySeedAsync() => Task.CompletedTask;
}
