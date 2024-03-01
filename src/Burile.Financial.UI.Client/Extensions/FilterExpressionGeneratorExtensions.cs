using Burile.Financial.Api.Client.Models.Filters;
using Burile.Financial.Api.Client.Models.Filters.Interfaces;
using Burile.Financial.Api.Client.Models.Filters.Types;
using Burile.Financial.Api.Client.Models.Sorts;
using Burile.Financial.Api.Client.Models.Sorts.Types;
using MudBlazor;

namespace Burile.Financial.UI.Client.Extensions;

internal static class FilterExpressionGeneratorExtensions
{
    internal static IEnumerable<IFilter>? GenerateFilterApi<T>(this IEnumerable<IFilterDefinition<T>> filterDefinitions,
                                                               string propertyName)
    {
        var internalFilterDefinitions = filterDefinitions.Where(definition => definition.Title == propertyName)
                                                         .ToList();

        if (internalFilterDefinitions.Count == 0)
        {
            return null;
        }

        var internalFilterType = GetInternalFilterType(internalFilterDefinitions);

        return GetFilterString(internalFilterType, internalFilterDefinitions);
    }

    internal static Sort? GenerateSortApi<T>(this IEnumerable<SortDefinition<T>> definitions, string propertyName)
    {
        var definition = definitions.FirstOrDefault(definition => definition.SortBy == propertyName);

        return definition is null
            ? null
            : new Sort(definition.Index, definition.Descending ? SortType.Descending : SortType.Ascending);
    }

    private static IEnumerable<IFilter> GetFilterString<T>(InternalFilterType internalFilterType,
                                                           IEnumerable<IFilterDefinition<T>> internalFilterDefinitions)
        => internalFilterType switch
        {
            InternalFilterType.String => ToFilterString(internalFilterDefinitions),
            InternalFilterType.Guid => ToFilterGuid(internalFilterDefinitions),
            InternalFilterType.Number => throw new
                NotImplementedException($"{nameof(internalFilterType)}:{internalFilterType} not Implemented."),
            InternalFilterType.Enum => throw new
                NotImplementedException($"{nameof(internalFilterType)}:{internalFilterType} not Implemented."),
            InternalFilterType.Bool => throw new
                NotImplementedException($"{nameof(internalFilterType)}:{internalFilterType} not Implemented."),
            InternalFilterType.DateTime => throw new
                NotImplementedException($"{nameof(internalFilterType)}:{internalFilterType} not Implemented."),
            _ => throw new
                NotImplementedException($"{nameof(internalFilterType)}:{internalFilterType} not Implemented.")
        };

    private static IEnumerable<IFilter> ToFilterGuid<T>(IEnumerable<IFilterDefinition<T>> internalFilterDefinitions)
        => internalFilterDefinitions.Select(static definition =>
        {
            var searchValue = definition.Value as string ?? string.Empty;
            var definitionOperator = definition.Operator ?? throw new InvalidOperationException();
            return new FilterGuid(searchValue.ToGuid(), FilterGuidType.GetFilterType(definitionOperator));
        });

    private static IEnumerable<IFilter> ToFilterString<T>(IEnumerable<IFilterDefinition<T>> internalFilterDefinitions)
        => internalFilterDefinitions.Select(static definition =>
        {
            var searchValue = definition.Value as string ?? string.Empty;
            var definitionOperator = definition.Operator ?? throw new InvalidOperationException();
            return new FilterString(searchValue, FilterStringType.GetFilterType(definitionOperator));
        });

    private static InternalFilterType GetInternalFilterType<T>(this ICollection<IFilterDefinition<T>> filterDefinitions)
    {
        if (filterDefinitions.Is(static definition => definition.FieldType.IsString))
        {
            return InternalFilterType.String;
        }

        if (filterDefinitions.Is(static definition => definition.FieldType.IsNumber))
        {
            return InternalFilterType.Number;
        }

        if (filterDefinitions.Is(static definition => definition.FieldType.IsEnum))
        {
            return InternalFilterType.Enum;
        }

        if (filterDefinitions.Is(static definition => definition.FieldType.IsDateTime))
        {
            return InternalFilterType.DateTime;
        }

        if (filterDefinitions.Is(static definition => definition.FieldType.IsBoolean))
        {
            return InternalFilterType.Bool;
        }

        if (filterDefinitions.Is(static definition => definition.FieldType.IsGuid))
        {
            return InternalFilterType.Guid;
        }

        throw new NotImplementedException($"GetInternalFilterType not Implemented.");
    }

    private static bool Is<T>(this IEnumerable<IFilterDefinition<T>> filterDefinitions,
                              Func<IFilterDefinition<T>, bool> predicate)
        => filterDefinitions.All(predicate);

    private enum InternalFilterType
    {
        String,
        Number,
        Enum,
        Guid,
        Bool,
        DateTime,
    }

    private static class FilterStringType
    {
        public static SearchStringType GetFilterType(string mudFilter)
            => mudFilter switch
            {
                FilterOperator.String.Contains => SearchStringType.Contains,
                FilterOperator.String.NotContains => SearchStringType.NotContains,
                FilterOperator.String.Equal => SearchStringType.Equal,
                FilterOperator.String.NotEqual => SearchStringType.NotEqual,
                FilterOperator.String.EndsWith => SearchStringType.EndsWith,
                FilterOperator.String.Empty => SearchStringType.Empty,
                FilterOperator.String.NotEmpty => SearchStringType.NotEmpty,
                _ => throw new ArgumentOutOfRangeException(nameof(mudFilter), mudFilter,
                                                           $"{nameof(mudFilter)}:{mudFilter} not Implemented.")
            };
    }

    private static class FilterGuidType
    {
        public static SearchGuidType GetFilterType(string mudFilter)
            => mudFilter switch
            {
                FilterOperator.Guid.Equal => SearchGuidType.Equal,
                FilterOperator.Guid.NotEqual => SearchGuidType.NotEqual,
                _ => throw new ArgumentOutOfRangeException(nameof(mudFilter), mudFilter,
                                                           $"{nameof(mudFilter)}:{mudFilter} not Implemented.")
            };
    }
}