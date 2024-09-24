namespace Application.UseCases.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .Length(1, 250)
            .When(v => !string.IsNullOrEmpty(v.Name));

        RuleFor(v => v.Description)
            .Length(1, 2000)
            .When(v => !string.IsNullOrEmpty(v.Description));

        RuleFor(v => v.Price)
            .GreaterThanOrEqualTo(0)
            .When(v => v.Price.HasValue);
    }
}
