
namespace Persistence.Context;

public interface IApplicationDbContextInitialiser
{
    Task InitialiseAsync();
    Task SeedAsync();
    Task TrySeedAsync();
}