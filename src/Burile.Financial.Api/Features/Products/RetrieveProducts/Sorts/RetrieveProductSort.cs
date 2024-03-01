namespace Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts;

public sealed class RetrieveProductSort(
    Sort? apiId,
    Sort? symbol,
    Sort? name,
    Sort? currency,
    Sort? exchange,
    Sort? country,
    Sort? micCode)
{
    public Sort? ApiId { get; } = apiId;
    public Sort? Symbol { get; } = symbol;
    public Sort? Name { get; } = name;
    public Sort? Currency { get; } = currency;
    public Sort? Exchange { get; } = exchange;
    public Sort? Country { get; } = country;
    public Sort? MicCode { get; } = micCode;
}