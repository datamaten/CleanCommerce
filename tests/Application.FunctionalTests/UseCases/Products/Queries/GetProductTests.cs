using Application.Common.Exceptions;
using Application.UseCases.Products.Queries.GetProducts;

namespace Application.FunctionalTests.UseCases.Products.Queries;

public class GetProductTests : QueryFixture
{
    [Fact]
    public async Task Should_Return_Product()
    {
        var excpeced = await FirstOrDefaultAsync<Product>(1);
        excpeced.Should().NotBeNull();

        var result = await SendAsync(new GetProductById() { Id = excpeced!.Id });

            result.Should()
            .BeAssignableTo<ProductDto>()
            .And.BeEquivalentTo(excpeced, opt => opt
            .ExcludingMissingMembers());
    }

    [Fact]
    public async Task Should_Throw_NotFoundException()
    {
        Func<Task> act = () => SendAsync(new GetProductById() { Id = int.MaxValue });
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Should_Throw_ValidationException()
    {
        Func<Task> act = () => SendAsync(new GetProductById());
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Should_Throw_Nion()
    {
        var count = await CountAsync<Product>();
        count.Should().Be(10);
    }
}
