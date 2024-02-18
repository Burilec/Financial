using FluentValidation;

namespace Burile.Financial.Api.Features.Products.UpdateProduct;

public sealed class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(static u => u.Name).NotEmpty();
        RuleFor(static u => u.Currency).NotEmpty();
        RuleFor(static u => u.Exchange).NotEmpty();
        RuleFor(static u => u.Country).NotEmpty();
        RuleFor(static u => u.MicCode).NotEmpty();
    }
}