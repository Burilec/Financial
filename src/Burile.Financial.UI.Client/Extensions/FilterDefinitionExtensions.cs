using MudBlazor;

namespace Burile.Financial.UI.Client.Extensions;

internal static class FilterDefinitionExtensions
{
    internal static IEnumerable<IFilterDefinition<T>> Clone<T>(this IEnumerable<IFilterDefinition<T>> filterDefinitions)
        => filterDefinitions?.Select(static filterDefinition => filterDefinition.InternalClone())
        ?? Enumerable.Empty<IFilterDefinition<T>>();

    private static FilterDefinition<T> InternalClone<T>(this IFilterDefinition<T> value)
        => new()
        {
            Id = value.Id,
            Column = value.Column,
            Operator = value.Operator,
            Title = value.Title,
            Value = value.Value,
        };

    internal static bool Equal<T>(this ICollection<IFilterDefinition<T>>? a, ICollection<IFilterDefinition<T>>? b)
        => a != null && b != null && a.Count == b.Count
        && a.Zip(b).All(static value => value.First.Equal(value.Second));

    private static bool Equal<T>(this IFilterDefinition<T> a, IFilterDefinition<T> b)
        => a.Id == b.Id
        && a.Title == b.Title
        && a.Operator == b.Operator
        && a.Value == b.Value;
}