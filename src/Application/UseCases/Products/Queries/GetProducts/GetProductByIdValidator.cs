using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.UseCases.Products.Queries.GetProducts;
public class GetProductByIdValidator : AbstractValidator<GetProductById>
{
    public GetProductByIdValidator()
    {
        RuleFor(v => v.Id)
            .NotEqual(9)
            .NotEmpty();
    }
}
