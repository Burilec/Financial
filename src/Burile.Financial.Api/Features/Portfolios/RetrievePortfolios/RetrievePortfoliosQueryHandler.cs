using Burile.Financial.Infrastructure;
using Burile.Financial.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolios;

public sealed class RetrievePortfoliosQueryHandler(FinancialContext financialContext)
    : IRequestHandler<RetrievePortfoliosQuery, PaginatedResult<RetrievePortfoliosResponse>>
{
    public async Task<PaginatedResult<RetrievePortfoliosResponse>> Handle(RetrievePortfoliosQuery request,
                                                                          CancellationToken cancellationToken)
    {
        var totalRecords = financialContext.Portfolios.Count();

        var itemsToSkip = request.PagingOptions.PageSize * request.PagingOptions.PageNumber;

        var portfolios = await financialContext.Portfolios
                                               .OrderBy(static s => s.Id)
                                               .Skip(itemsToSkip)
                                               .Take(request.PagingOptions.PageSize)
                                               .AsNoTracking()
                                               .ToListAsync(cancellationToken);

        var retrievePortfoliosResponses = RetrievePortfoliosResponse.FromPortfolios(portfolios);

        var paginatedResult = new PaginatedResult<RetrievePortfoliosResponse>(request.PagingOptions.PageNumber,
                                                                              request.PagingOptions.PageSize,
                                                                              totalRecords,
                                                                              retrievePortfoliosResponses);

        return paginatedResult;
    }
}