using Application.Common.Interfaces;

namespace Application.UseCases.Products.Queries.GetProducts;
public record GetProductById : IRequest<ProductDto>
{
    public int Id { get; set; }
}

public class GetProductByIdHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetProductById, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        var entity = await context.Products
            .FindAsync([request.Id], cancellationToken);

        Guard.NotFound(request.Id, entity);

        return mapper.Map<ProductDto>(entity);
    }
}
