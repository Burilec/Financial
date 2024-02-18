using Burile.Financial.Infrastructure;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolios;

public sealed class RetrievePortfoliosQuery(PagingOptions pagingOptions)
    : IRequest<PaginatedResult<RetrievePortfoliosResponse>>
{
    public PagingOptions PagingOptions { get; } = pagingOptions;
}