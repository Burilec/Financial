using Burile.Financial.Domain.Entities;

namespace Burile.Financial.Api.Features.Portfolios.CreatePortfolio;

public sealed class CreatePortfolioResponse(Guid apiId, string name)
{
    public Guid ApiId { get; } = apiId;
    public string Name { get; } = name;

    internal static CreatePortfolioResponse FromPortfolio(Portfolio portfolio)
        => new(portfolio.ApiId, portfolio.Name);
}