using FluentValidation;
using FluentValidation.Results;

namespace Burile.Financial.UI.Client.Pages.Products;

public sealed class ProductValidator : AbstractValidator<ProductForm>
{
    internal ProductValidator()
    {
        RuleFor(static x => x.ApiId).NotEmpty();
        RuleFor(static x => x.Symbol)
           .NotEmpty()
           .Length(2, 15)
           .MustAsync(async (value, cancellationToken) => await IsUniqueAsync(value, cancellationToken));
        RuleFor(static x => x.Name)
           .Length(0, 112);
        RuleFor(static x => x.Currency)
           .NotEmpty()
           .Length(3);
        RuleFor(static x => x.Exchange)
           .NotEmpty()
           .Length(3, 8);
        RuleFor(static x => x.Country)
           .Length(0, 14);
        RuleFor(static x => x.MicCode)
           .Length(3, 8);
    }

    internal Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(ValidationContext<ProductForm>.CreateWithOptions((ProductForm)model,
                                         x => x.IncludeProperties(propertyName)));
        return result.IsValid
            ? Array.Empty<string>()
            : result.Errors.Select<ValidationFailure, string>(static e => e.ErrorMessage);
    };

    private async Task<bool> IsUniqueAsync(string symbol, CancellationToken cancellationToken)
    {
        //Todo: check symbol is unique
        return true;
    }
}