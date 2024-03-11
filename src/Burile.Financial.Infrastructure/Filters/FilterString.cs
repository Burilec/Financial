using Burile.Financial.Infrastructure.Filters.Types;

namespace Burile.Financial.Infrastructure.Filters;

public sealed class FilterString(string searchValue, SearchStringType type)
{
    public string SearchValue { get; } = searchValue;
    public SearchStringType Type { get; } = type;
}