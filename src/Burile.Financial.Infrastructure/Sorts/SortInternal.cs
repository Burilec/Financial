using System.Linq.Expressions;
using Burile.Financial.Infrastructure.Sorts.Types;

namespace Burile.Financial.Infrastructure.Sorts;

public sealed class SortInternal<T, TProperty>(int index, SortType sortType, Expression<Func<T, TProperty>> property)
    : Sort(index, sortType)
{
    public Expression<Func<T, TProperty>> Property { get; } = property;
}