using Burile.Financial.Domain.Entities;
using Burile.Financial.Infrastructure.Data.Contexts;

namespace Burile.Financial.Api.Features.Portfolios.CreatePortfolio;

public sealed class CreatePortfolioCommandHandler(FinancialContext financialContext)
    : IRequestHandler<CreatePortfolioCommand, CreatePortfolioResponse>
{
    public async Task<CreatePortfolioResponse> Handle(CreatePortfolioCommand request,
                                                      CancellationToken cancellationToken)
    {
        var newPortfolio = new Portfolio(request.Name);

        var portfolio = await financialContext.Portfolios.AddAsync(newPortfolio, cancellationToken)
                                              .ConfigureAwait(false);

        await financialContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return CreatePortfolioResponse.FromPortfolio(portfolio.Entity);
    }
}