using FluentResults;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolio;

public sealed class RetrievePortfolioQuery(Guid apiId) : IRequest<Result<RetrievePortfolioResponse>>
{
    public Guid ApiId { get; } = apiId;
}