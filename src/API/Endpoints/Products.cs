using API.Infrastructure;
using Application.UseCases.Products.Queries.GetProducts;
using MediatR;

namespace API.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetProduct)
            .MapGet(GetProductById, "{id}");
    }

    public Task<ProductDto> GetProduct(ISender sender, [AsParameters] GetProductById query)
    {
        return sender.Send(query);
    }

    public Task<ProductDto> GetProductById(ISender sender, int id)
    {
        return sender.Send(new GetProductById { Id = id});
    }
}
