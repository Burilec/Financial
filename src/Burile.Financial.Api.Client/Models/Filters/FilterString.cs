using Burile.Financial.Api.Client.Models.Filters.Interfaces;
using Burile.Financial.Api.Client.Models.Filters.Types;

namespace Burile.Financial.Api.Client.Models.Filters;

public sealed class FilterString(string searchValue, SearchStringType type) : IFilter
{
    public string SearchValue { get; } = searchValue;
    public SearchStringType Type { get; } = type;
}