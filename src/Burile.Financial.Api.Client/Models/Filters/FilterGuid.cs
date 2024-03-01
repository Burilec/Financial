using Burile.Financial.Api.Client.Models.Filters.Interfaces;
using Burile.Financial.Api.Client.Models.Filters.Types;

namespace Burile.Financial.Api.Client.Models.Filters;

public sealed class FilterGuid(Guid searchValue, SearchGuidType type) : IFilter
{
    public Guid SearchValue { get; set; } = searchValue;
    public SearchGuidType Type { get; set; } = type;
}