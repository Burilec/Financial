namespace Burile.Financial.Api.Features.Portfolios.CreatePortfolio;

public sealed class CreatePortfolioRequest(string name)
{
    public string Name { get; set; } = name;
}