using Application.Common.Interfaces;

namespace Application.UseCases.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

public class DeleteProductCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        Guard.NotFound(request.Id, entity);

        context.Products .Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
