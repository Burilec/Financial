using System.Linq.Expressions;
using Burile.Financial.Infrastructure.Extensions;

namespace Burile.Financial.Infrastructure.Filters;

public static class FilterExpressionGenerator
{
    public static IEnumerable<Expression<Func<T, bool>>>? GenerateExpressions<T, TProperty>(
        this IEnumerable<Filter<T, TProperty>>? filters)
        => filters?.Select(static filter => filter.GenerateExpression());

    private static Expression<Func<T, bool>> GenerateExpression<T, TProperty>(this Filter<T, TProperty> filter)
        => filter.Type switch
        {
            FilterType.String => filter.FilterString(),
            FilterType.Guid => filter.FilterGuid(),
            FilterType.Number => filter.FilterNumber(),
            FilterType.Date => filter.FilterDate(),
            FilterType.Boolean => filter.FilterBoolean(),
            FilterType.Enum => filter.FilterEnum(),
            _ => throw new NotImplementedException($"{nameof(filter.Type)}:{filter.Type} not Implemented.")
        };

    private static Expression<Func<T, bool>> FilterEnum<T, TProperty>(this Filter<T, TProperty> filter)
        => filter.SearchType switch
        {
            SearchType.Equal => filter.GenerateBinary(ExpressionType.Equal, filter.Value),
            SearchType.NotEqual => filter.GenerateBinary(ExpressionType.NotEqual, filter.Value),
            _ => throw new NotImplementedException($"{nameof(filter.SearchType)}:{filter.SearchType} not Implemented.")
        };

    private static Expression<Func<T, bool>> FilterBoolean<T, TProperty>(this Filter<T, TProperty> filter)
        => filter.SearchType switch
        {
            SearchType.Equal => filter.GenerateBinary(ExpressionType.Equal, filter.Value),
            _ => throw new NotImplementedException($"{nameof(filter.SearchType)}:{filter.SearchType} not Implemented.")
        };

    private static Expression<Func<T, bool>> FilterDate<T, TProperty>(this Filter<T, TProperty> filter)
        => filter.SearchType switch
        {
            SearchType.Equal => filter.GenerateBinary(ExpressionType.Equal, filter.Value),
            SearchType.NotEqual => filter.GenerateBinary(ExpressionType.NotEqual, filter.Value),
            SearchType.GreaterThan => filter.GenerateBinary(ExpressionType.GreaterThan, filter.Value),
            SearchType.GreaterThanOrEqual => filter.GenerateBinary(ExpressionType.GreaterThanOrEqual, filter.Value),
            SearchType.LessThan => filter.GenerateBinary(ExpressionType.LessThan, filter.Value),
            SearchType.LessThanOrEqual => filter.GenerateBinary(ExpressionType.LessThanOrEqual, filter.Value),
            SearchType.Empty => filter.GenerateBinary(ExpressionType.Equal),
            SearchType.NotEmpty => filter.GenerateBinary(ExpressionType.NotEqual),
            _ => throw new NotImplementedException($"{nameof(filter.SearchType)}:{filter.SearchType} not Implemented.")
        };

    private static Expression<Func<T, bool>> FilterNumber<T, TProperty>(this Filter<T, TProperty> filter)
        => filter.SearchType switch
        {
            SearchType.Equal => filter.GenerateBinary(ExpressionType.Equal, filter.Value),
            SearchType.NotEqual => filter.GenerateBinary(ExpressionType.NotEqual, filter.Value),
            SearchType.GreaterThan => filter.GenerateBinary(ExpressionType.GreaterThan, filter.Value),
            SearchType.GreaterThanOrEqual => filter.GenerateBinary(ExpressionType.GreaterThanOrEqual, filter.Value),
            SearchType.LessThan => filter.GenerateBinary(ExpressionType.LessThan, filter.Value),
            SearchType.LessThanOrEqual => filter.GenerateBinary(ExpressionType.LessThanOrEqual, filter.Value),
            SearchType.Empty => filter.GenerateBinary(ExpressionType.Equal),
            SearchType.NotEmpty => filter.GenerateBinary(ExpressionType.NotEqual),
            _ => throw new NotImplementedException($"{nameof(filter.SearchType)}:{filter.SearchType} not Implemented.")
        };

    private static Expression<Func<T, bool>> FilterGuid<T, TProperty>(this Filter<T, TProperty> filter)
        => filter.SearchType switch
        {
            SearchType.Contains => filter.GenerateBinary(ExpressionType.Equal, filter.Value),
            SearchType.NotContains => filter.GenerateBinary(ExpressionType.NotEqual, filter.Value),
            _ => throw new NotImplementedException($"{nameof(filter.SearchType)}:{filter.SearchType} not Implemented.")
        };

    private static Expression<Func<T, bool>> FilterString<T, TProperty>(this Filter<T, TProperty> filter)
    {
        var searchValue = filter.Value as string;

        var contains =
            (Expression<Func<string, bool>>)(x => x != null && searchValue != null && x.Contains(searchValue));
        var notContains =
            (Expression<Func<string, bool>>)(x => x != null && searchValue != null && !x.Contains(searchValue));
        var equal = (Expression<Func<string?, bool>>)(x => x != null && x.Equals(searchValue));
        var notEqual = (Expression<Func<string?, bool>>)(x => x != null && !x.Equals(searchValue));
        var startsWith =
            (Expression<Func<string?, bool>>)(x => x != null && searchValue != null && x.StartsWith(searchValue));
        var endsWith =
            (Expression<Func<string?, bool>>)(x => x != null && searchValue != null && x.EndsWith(searchValue));
        var empty = (Expression<Func<string?, bool>>)(static x => string.IsNullOrWhiteSpace(x));
        var notEmpty = (Expression<Func<string?, bool>>)(static x => !string.IsNullOrWhiteSpace(x));

        return filter.SearchType switch
        {
            SearchType.Contains => filter.PropertyExpression.Modify<T>(contains),
            SearchType.NotContains => filter.PropertyExpression.Modify<T>(notContains),
            SearchType.Equal => filter.PropertyExpression.Modify<T>(equal),
            SearchType.NotEqual => filter.PropertyExpression.Modify<T>(notEqual),
            SearchType.StartsWith => filter.PropertyExpression.Modify<T>(startsWith),
            SearchType.EndsWith => filter.PropertyExpression.Modify<T>(endsWith),
            SearchType.Empty => filter.PropertyExpression.Modify<T>(empty),
            SearchType.NotEmpty => filter.PropertyExpression.Modify<T>(notEmpty),
            _ => throw new NotImplementedException($"{nameof(filter.SearchType)}:{filter.SearchType} not Implemented.")
        };
    }
}