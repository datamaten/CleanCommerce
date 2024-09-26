using Application.Common.Interfaces;

namespace Application.UseCases.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal? Price { get; set; }
}

public class UpdateProductCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Products
            .FindAsync([request.Id], cancellationToken);

        Guard.NotFound(request.Id, entity);

        if (!string.IsNullOrEmpty(request.Name))
            entity.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Description))
            entity.Description = request.Description;

        if (request.Price.HasValue)
            entity.Price = request.Price.Value;

        await context.SaveChangesAsync(cancellationToken);
    }
}
