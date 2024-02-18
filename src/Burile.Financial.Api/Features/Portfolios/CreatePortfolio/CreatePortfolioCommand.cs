namespace Burile.Financial.Api.Features.Portfolios.CreatePortfolio;

public sealed class CreatePortfolioCommand(CreatePortfolioRequest createPortfolioRequest)
    : IRequest<CreatePortfolioResponse>
{
    public string Name { get; } = createPortfolioRequest.Name;
}