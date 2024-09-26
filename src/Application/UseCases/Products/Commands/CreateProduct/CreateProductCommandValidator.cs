namespace Application.UseCases.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .Length(1, 250);

        RuleFor(v => v.Description)
            .NotEmpty()
            .Length(1, 2000);

        RuleFor(v => v.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}
