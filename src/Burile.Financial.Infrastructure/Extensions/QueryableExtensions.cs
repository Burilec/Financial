using System.Linq.Expressions;

namespace Burile.Financial.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Where<T>(this IQueryable<T> query,
                                         IEnumerable<Expression<Func<T, bool>>>? expressions)
        => expressions == null
            ? query
            : expressions.Aggregate(query, static (current, expression) => current.Where(expression));
}