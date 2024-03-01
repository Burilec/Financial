using Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts.Types;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts;

public sealed class Sort(int index, SortType sortType)
{
    public int Index { get; } = index;
    public SortType SortType { get; } = sortType;
}