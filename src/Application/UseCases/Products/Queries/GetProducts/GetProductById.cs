using Application.Common.Exceptions;
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


        var items = context.Products.ToList();



        if(entity == null)
            throw new NotFoundException(request.Id.ToString(), "Products");

        return mapper.Map<ProductDto>(entity);

    }
}
