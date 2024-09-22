using Application.FunctionalTests.Mocks;
using Application.UseCases.Products.Queries.GetProducts;
using Domain.Entities;

namespace Application.FunctionalTests.UseCases.Products
{
    [Collection("QueryCollection")]
    public class ProductsQueryTests(QueryCollectionFixture fixture) : QueryTestBase(fixture)
    {
        [Fact]
        public async Task ShouldReturnPriorityLevels()
        {
            var query = new GetProductById() { Id = 9 };

            var result = await SendAsync(query);


            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturnPriorityLevsels()
        {
            var query = new GetProductById() { Id = 9 };

            var result = await SendAsync(query);


            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturnPrissorityLevsels()
        {
            var query = new GetProductById() { Id = 1 };

            var result = await SendAsync(query);


            result.Should().NotBeNull();

            var count = await CountAsync<Product>();

            count.Should().Be(10);
        }
    }
}
