using Burile.Financial.Domain.Entities;

namespace Burile.Financial.Api.Features.Portfolios.UpdatePortfolio;

public static class PortfolioExtensions
{
    internal static Portfolio Update(this Portfolio portfolio, UpdatePortfolioCommand command)
        => portfolio.Update(command.Name);
}