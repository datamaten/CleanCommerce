using Application.FunctionalTests.Entities.Fakes;
using Bogus;

namespace Application.FunctionalTests.Fake;

public static class FakeEntities
{
    private const int Seed = 10203040;
    public static List<Product> Products { get; private set; }

    static FakeEntities()
    {
        Randomizer.Seed = new Random(Seed);

        Products = FakeProducts.GenerateMany(10);
    }
}
