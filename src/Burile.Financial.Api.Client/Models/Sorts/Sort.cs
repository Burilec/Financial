using Burile.Financial.Api.Client.Models.Sorts.Types;

namespace Burile.Financial.Api.Client.Models.Sorts;

public sealed class Sort(int index, SortType sortType)
{
    public int Index { get; } = index;
    public SortType SortType { get; } = sortType;
}