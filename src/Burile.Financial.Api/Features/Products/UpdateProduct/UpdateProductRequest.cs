namespace Burile.Financial.Api.Features.Products.UpdateProduct;

public sealed class UpdateProductRequest(
    string? name,
    string? currency,
    string? exchange,
    string? country,
    string? micCode)
{
    public string? Name { get; } = name;
    public string? Currency { get; } = currency;
    public string? Exchange { get; } = exchange;
    public string? Country { get; } = country;
    public string? MicCode { get; } = micCode;
}