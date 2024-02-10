using Burile.Financial.Api.Client.Models;

namespace Burile.Financial.UI.Client.Pages.Products;

internal sealed class Product(
    Guid apiId,
    string symbol,
    string? name,
    string? currency,
    string? exchange,
    string? country,
    string? micCode)
{
    internal Product(ProductResponse response)
        : this(response.ApiId, response.Symbol, response.Name, response.Currency, response.Exchange, response.Country,
               response.MicCode)
    {
    }

    public Guid ApiId { get; } = apiId;
    public string Symbol { get; } = symbol;
    public string? Name { get; } = name;
    public string? Currency { get; } = currency;
    public string? Exchange { get; } = exchange;
    public string? Country { get; } = country;
    public string? MicCode { get; } = micCode;
}