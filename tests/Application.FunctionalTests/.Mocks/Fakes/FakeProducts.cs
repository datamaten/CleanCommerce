using Bogus;
using Domain.Entities;

namespace Application.FunctionalTests.Mocks.Fakes;
public static class FakeProducts
{
    public static List<Product> Generate(int count)
    {
        var productRule = new Faker<Product>()
            .RuleFor(x => x.Id, f => 0)
            .RuleFor(x => x.Name, f => f.Random.String2(10, 200))
            .RuleFor(x => x.Description, f => f.Random.Words(50))
            .RuleFor(x => x.Price, f => f.Random.Decimal(1, 1000));

        return productRule.Generate(count);
    }
}
