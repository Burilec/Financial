using Burile.Financial.Domain.Entities;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolio;

public sealed class RetrievePortfolioResponse(Guid apiId, string name)
{
    public Guid ApiId { get; } = apiId;
    public string Name { get; } = name;

    internal static RetrievePortfolioResponse FromPortfolio(Portfolio portfolio)
        => new(portfolio.ApiId, portfolio.Name);
}