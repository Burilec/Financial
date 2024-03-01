using Burile.Financial.Api.Features.Products.RetrieveProducts.Filters.Interfaces;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Filters.Types;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts.Filters;

public sealed class FilterGuid(Guid searchValue, SearchGuidType type) : IFilter
{
    public Guid SearchValue { get; set; } = searchValue;
    public SearchGuidType Type { get; set; } = type;
}