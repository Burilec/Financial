using Burile.Financial.Api.Features.Products.RetrieveProducts.Filters.Interfaces;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Filters.Types;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts.Filters;

public sealed class FilterString(string searchValue, SearchStringType type) : IFilter
{
    public string SearchValue { get; } = searchValue;
    public SearchStringType Type { get; } = type;
}