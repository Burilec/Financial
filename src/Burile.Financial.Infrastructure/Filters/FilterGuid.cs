using Burile.Financial.Infrastructure.Filters.Types;

namespace Burile.Financial.Infrastructure.Filters;

public sealed class FilterGuid(Guid searchValue, SearchGuidType type)
{
    public Guid SearchValue { get; set; } = searchValue;
    public SearchGuidType Type { get; set; } = type;
}