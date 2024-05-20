using Burile.Financial.Infrastructure.Sorts.Types;

namespace Burile.Financial.Infrastructure.Sorts;

public class Sort(int index, SortType sortType)
{
    public int Index { get; } = index;
    public SortType SortType { get; } = sortType;
}