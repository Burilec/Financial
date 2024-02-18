namespace Burile.Financial.Api.Client.Models;

public sealed class CreatePortfolioRequest(string name)
{
    public string Name { get; } = name;
}