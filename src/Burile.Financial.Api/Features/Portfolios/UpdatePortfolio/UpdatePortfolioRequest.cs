namespace Burile.Financial.Api.Features.Portfolios.UpdatePortfolio;

public sealed class UpdatePortfolioRequest(string name)
{
    public string Name { get; } = name;
}