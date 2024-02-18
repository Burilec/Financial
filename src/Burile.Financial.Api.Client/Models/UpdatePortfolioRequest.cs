namespace Burile.Financial.Api.Client.Models;

public sealed class UpdatePortfolioRequest(string name)
{
    public string Name { get; } = name;
}