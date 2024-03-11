using System.Linq.Expressions;

namespace Burile.Financial.Infrastructure.Filters;

public class Filter<T, TProperty>(
    Expression<Func<T, TProperty>> propertyExpression,
    TProperty value,
    FilterType type,
    SearchType searchType)
{
    public Expression<Func<T, TProperty>> PropertyExpression { get; } = propertyExpression;
    public TProperty Value { get; } = value;
    public FilterType Type { get; } = type;
    public SearchType SearchType { get; } = searchType;
}