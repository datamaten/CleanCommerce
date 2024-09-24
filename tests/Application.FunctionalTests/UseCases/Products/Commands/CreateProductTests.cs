using Application.Common.Exceptions;
using Application.UseCases.Products.Commands.CreateProduct;

namespace Application.FunctionalTests.UseCases.Products.Commands
{
    public class CreateProductTests : CommandFixture
    {
        [Fact]
        public async Task Should_Create_Product()
        {
            var command = new CreateProductCommand()
            {
                Name = "Name",
                Description = "Description",
                Price = 10,
            };

            var id = await SendAsync(command);

            var result = await FindAsync<Product>(id);

            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(command, opt => opt
                .ExcludingMissingMembers());
        }

        [Theory]
        [InlineData(null, null, 0)]
        [InlineData("name", "description", -1)]
        [InlineData("name", null, 10)]
        [InlineData(null, "description", 10)]
        public async Task Should_Throw_ValidationException(string? name, string? description, decimal price)
        {
            var command = new CreateProductCommand()
            {
                Name = name!,
                Description = description!,
                Price = price,
            };

            Func<Task> act = () => SendAsync(command);
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
