using FluentResults;

namespace Burile.Financial.Api.Features.Portfolios.UpdatePortfolio;

public sealed class UpdatePortfolioCommand(Guid apiId, UpdatePortfolioRequest updatePortfolioRequest) : IRequest<Result>
{
    public Guid ApiId { get; } = apiId;
    public string Name { get; } = updatePortfolioRequest.Name;
}