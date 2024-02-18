using FluentValidation;

namespace Burile.Financial.Api.Features.Portfolios.CreatePortfolio;

public sealed class CreatePortfolioRequestValidator : AbstractValidator<CreatePortfolioRequest>
{
    public CreatePortfolioRequestValidator()
        => RuleFor(static request => request.Name).NotEmpty();
}