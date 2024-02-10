using Burile.Financial.Domain.Entities;

namespace Burile.Financial.Api.Features.RetrieveProductByApiId;

public sealed class RetrieveProductByApiIdResponse(
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

    internal static RetrieveProductByApiIdResponse FromProduct(Product product) =>
        new(product.ApiId, product.Symbol, product.Name,
            product.Currency, product.Exchange, product.Country,
            product.MicCode);
}