using FluentValidation;

namespace Burile.Financial.Api.Features.Portfolios.UpdatePortfolio;

public sealed class UpdatePortfolioRequestValidator : AbstractValidator<UpdatePortfolioRequest>
{
    public UpdatePortfolioRequestValidator()
        => RuleFor(static u => u.Name).NotEmpty();
}