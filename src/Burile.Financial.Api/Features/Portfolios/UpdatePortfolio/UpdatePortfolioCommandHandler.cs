using Burile.Financial.Infrastructure.Data.Contexts;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.Portfolios.UpdatePortfolio;

public sealed class UpdatePortfolioCommandHandler(FinancialContext financialContext)
    : IRequestHandler<UpdatePortfolioCommand, Result>
{
    public async Task<Result> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await financialContext.Portfolios
                                              .FirstOrDefaultAsync(p => p.ApiId == request.ApiId, cancellationToken)
                                              .ConfigureAwait(false);

        if (portfolio == null)
        {
            return Result.Fail("Portfolio not found");
        }

        financialContext.Portfolios.Update(portfolio.Update(request));

        await financialContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok();
    }
}