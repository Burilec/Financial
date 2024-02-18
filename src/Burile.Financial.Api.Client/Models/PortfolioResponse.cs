namespace Burile.Financial.Api.Client.Models;

public sealed class PortfolioResponse(
    Guid apiId,
    string name)
{
    public Guid ApiId { get; } = apiId;
    public string Name { get; } = name;
}