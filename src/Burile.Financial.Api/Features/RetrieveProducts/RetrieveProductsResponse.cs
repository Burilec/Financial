using Burile.Financial.Domain.Entities;

namespace Burile.Financial.Api.Features.RetrieveProducts;

public sealed class RetrieveProductsResponse(
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


    private static RetrieveProductsResponse FromProduct(Product product) =>
        new(product.ApiId, product.Symbol, product.Name,
            product.Currency, product.Exchange, product.Country,
            product.MicCode);

    internal static IEnumerable<RetrieveProductsResponse> FromProducts(IEnumerable<Product> products)
        => products.Select(FromProduct);
}