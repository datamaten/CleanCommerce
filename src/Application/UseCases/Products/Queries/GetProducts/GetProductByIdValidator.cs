namespace Application.UseCases.Products.Queries.GetProducts;

public class GetProductByIdValidator : AbstractValidator<GetProductById>
{
    public GetProductByIdValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty();
    }
}
