using API.Services;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.DeleteProduct;
using Application.UseCases.Products.Commands.UpdateProduct;
using Application.UseCases.Products.Queries.GetProducts;

namespace API.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapGet(GetProduct)
            .MapGet(GetProductById, "{id}")
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}");
    }

    public Task<ProductDto> GetProduct(ISender sender, [AsParameters] GetProductById query)
    {
        return sender.Send(query);
    }

    public Task<ProductDto> GetProductById(ISender sender, int id)
    {
        return sender.Send(new GetProductById { Id = id});
    }

    public async Task<IResult> CreateProduct(ISender sender, CreateProductCommand command)
    {
        await sender.Send(command);
        return Results.Created();
    }

    public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        ApiGuard.ThrowIfIdMismatch(id, command.Id);
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return Results.NoContent();
    }
}
