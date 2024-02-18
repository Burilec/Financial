using Burile.Financial.Domain.Entities;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolios;

public sealed class RetrievePortfoliosResponse(Guid apiId, string name)
{
    public Guid ApiId { get; } = apiId;
    public string Name { get; } = name;

    private static RetrievePortfoliosResponse FromPortfolio(Portfolio portfolio) =>
        new(portfolio.ApiId, portfolio.Name);

    internal static IEnumerable<RetrievePortfoliosResponse> FromPortfolios(IEnumerable<Portfolio> portfolios)
        => portfolios.Select(FromPortfolio);
}