using Burile.Financial.Api.Client.Models;

namespace Burile.Financial.UI.Client.Pages.Portfolios;

internal sealed class Portfolio(Guid apiId, string name)
{
    internal Portfolio(PortfolioResponse response)
        : this(response.ApiId, response.Name)
    {
    }

    public Guid ApiId { get; } = apiId;
    public string Name { get; } = name;
}