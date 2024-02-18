using Burile.Financial.Infrastructure.Data.Contexts;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.Portfolios.RetrievePortfolio;

public sealed class RetrievePortfolioQueryHandler(FinancialContext financialContext)
    : IRequestHandler<RetrievePortfolioQuery, Result<RetrievePortfolioResponse>>
{
    public async Task<Result<RetrievePortfolioResponse>> Handle(RetrievePortfolioQuery request,
                                                                CancellationToken cancellationToken)
    {
        var portfolio = await financialContext.Portfolios
                                              .Where(portfolio1 => portfolio1.ApiId == request.ApiId)
                                              .AsNoTracking()
                                              .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        return portfolio == null
            ? Result.Fail("Not found")
            : RetrievePortfolioResponse.FromPortfolio(portfolio).ToResult();
    }
}