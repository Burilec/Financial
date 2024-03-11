using System.Linq.Expressions;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Filters;
using Burile.Financial.Domain.Entities;
using Burile.Financial.Infrastructure.Filters;
using Burile.Financial.Infrastructure.Filters.Types;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

internal static class RetrieveProductFilterExtensions
{
    public static IEnumerable<Filter<Product, string>>? GetStringFilters(
        this RetrieveProductFilter? retrieveProductFilter)
        => retrieveProductFilter?
          .Symbols.StringFilters(static product => product.Symbol)
          .Concat(retrieveProductFilter.Names.StringFilters(static product => product.Name!))
          .Concat(retrieveProductFilter.Currencies.StringFilters(static product => product.Currency!))
          .Concat(retrieveProductFilter.Exchanges.StringFilters(static product => product.Exchange!))
          .Concat(retrieveProductFilter.Countries.StringFilters(static product => product.Country!))
          .Concat(retrieveProductFilter.MicCodes.StringFilters(static product => product.MicCode!));


    private static IEnumerable<Filter<Product, string>> StringFilters(
        this IEnumerable<FilterString>? filterStrings, Expression<Func<Product, string>> propertyExpression)
        => filterStrings == null
            ? Enumerable.Empty<Filter<Product, string>>()
            : filterStrings.Select(filterString => new Filter<Product, string>(propertyExpression,
                                                                               filterString.SearchValue,
                                                                               FilterType.String,
                                                                               GetSearchStringType(filterString.Type)));

    private static SearchType GetSearchStringType(SearchStringType type)
        => type switch
        {
            SearchStringType.Contains => SearchType.Contains,
            SearchStringType.NotContains => SearchType.NotContains,
            SearchStringType.Equal => SearchType.Equal,
            SearchStringType.NotEqual => SearchType.Equal,
            SearchStringType.StartsWith => SearchType.StartsWith,
            SearchStringType.EndsWith => SearchType.EndsWith,
            SearchStringType.Empty => SearchType.Empty,
            SearchStringType.NotEmpty => SearchType.NotEmpty,
            _ => throw new NotImplementedException($"{nameof(type)}:{type} not Implemented.")
        };

    public static IEnumerable<Filter<Product, Guid>>? GetGuidFilters(this RetrieveProductFilter? retrieveProductFilter)
        => retrieveProductFilter?.ApiIds.GuidFilters(static product => product.ApiId);

    private static IEnumerable<Filter<Product, Guid>> GuidFilters(this IEnumerable<FilterGuid>? filters,
                                                                  Expression<Func<Product, Guid>> propertyExpression)
        => filters == null
            ? Enumerable.Empty<Filter<Product, Guid>>()
            : filters.Select(filterString => new Filter<Product, Guid>(propertyExpression,
                                                                       filterString.SearchValue,
                                                                       FilterType.String,
                                                                       GetSearchStringType(filterString.Type)));

    private static SearchType GetSearchStringType(SearchGuidType type)
        => type switch
        {
            SearchGuidType.Equal => SearchType.Equal,
            SearchGuidType.NotEqual => SearchType.NotEqual,
            _ => throw new NotImplementedException($"{nameof(type)}:{type} not Implemented.")
        };
}