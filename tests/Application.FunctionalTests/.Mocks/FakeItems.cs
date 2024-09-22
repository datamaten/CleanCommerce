using Application.FunctionalTests.Mocks.Fakes;
using Bogus;
using Domain.Entities;

namespace Application.FunctionalTests.Mocks;
public static class FakeItems
{
    private const int Seed = 123;
    public static List<Product> Products { get; private set; }


    static FakeItems()
    {
        Randomizer.Seed = new Random(Seed);

        Products = FakeProducts.Generate(10);
    }
}
