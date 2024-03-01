namespace Burile.Financial.Api.Features.Products.RetrieveProducts.Filters;

public sealed class RetrieveProductFilter(
    IEnumerable<FilterGuid>? apiIds,
    IEnumerable<FilterString>? symbols,
    IEnumerable<FilterString>? names,
    IEnumerable<FilterString>? currencies,
    IEnumerable<FilterString>? exchanges,
    IEnumerable<FilterString>? countries,
    IEnumerable<FilterString>? micCodes)
{
    public IEnumerable<FilterGuid>? ApiIds { get; } = apiIds;
    public IEnumerable<FilterString>? Symbols { get; } = symbols;
    public IEnumerable<FilterString>? Names { get; } = names;
    public IEnumerable<FilterString>? Currencies { get; } = currencies;
    public IEnumerable<FilterString>? Exchanges { get; } = exchanges;
    public IEnumerable<FilterString>? Countries { get; } = countries;
    public IEnumerable<FilterString>? MicCodes { get; } = micCodes;
}