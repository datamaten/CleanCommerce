using API.Extensions;
using API.Modules.Base;
using API.Services;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.DeleteProduct;
using Application.UseCases.Products.Commands.UpdateProduct;
using Application.UseCases.Products.Queries.GetProducts;

namespace API.Modules;

public class ProductModule : Module
{
    public override ModuleConfiguration Configuration => new("Products");

    public override void Map(WebApplication app)
    {
        app.MapModule(Configuration)
            .AllowAnonymous()
            .MapGet(GetProduct, "{id}")
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}");
    }

    public Task<ProductDto> GetProduct(ISender sender, int id)
    {
        return sender.Send(new GetProductById(id));
    }

    public async Task<IResult> CreateProduct(ISender sender, CreateProductCommand command)
    {
        await sender.Send(command);
        return Results.Created();
    }

    public async Task<IResult> UpdateProduct(ISender sender, IApiGuard guard, int id, UpdateProductCommand command)
    {
        guard.ValidateIds(id, command.Id);
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return Results.NoContent();
    }
}
