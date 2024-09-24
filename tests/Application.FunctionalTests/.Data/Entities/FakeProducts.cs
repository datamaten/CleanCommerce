using Bogus;

namespace Application.FunctionalTests.Entities.Fakes;
public static class FakeProducts
{
    public static Faker<Product> Rule =>
        new Faker<Product>()
            .RuleFor(x => x.Id, f => 0)
            .RuleFor(x => x.Name, f => f.Random.String2(10, 200))
            .RuleFor(x => x.Description, f => f.Random.Words(50))
            .RuleFor(x => x.Price, f => f.Random.Decimal(1, 1000));

    public static List<Product> GenerateMany(int count)
    {
        return Rule.Generate(count);
    }
}
