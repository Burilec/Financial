namespace Burile.Financial.Api.Client.Models;

public sealed class ProductResponse(
    Guid apiId,
    string symbol,
    string? name,
    string? currency,
    string? exchange,
    string? country,
    string? micCode)
{
    public Guid ApiId { get; } = apiId;
    public string Symbol { get; } = symbol;
    public string? Name { get; } = name;
    public string? Currency { get; } = currency;
    public string? Exchange { get; } = exchange;
    public string? Country { get; } = country;
    public string? MicCode { get; } = micCode;
}