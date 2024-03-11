using System.Linq.Expressions;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Filters;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts.Types;
using Burile.Financial.Domain.Entities;
using Burile.Financial.Infrastructure;
using Burile.Financial.Infrastructure.Data.Contexts;
using Burile.Financial.Infrastructure.Extensions;
using Burile.Financial.Infrastructure.Filters;
using Burile.Financial.Infrastructure.Filters.Types;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

public sealed class RetrieveProductsQueryHandler(FinancialContext financialContext)
    : IRequestHandler<RetrieveProductsQuery, PaginatedResult<RetrieveProductResponse>>
{
    public async Task<PaginatedResult<RetrieveProductResponse>> Handle(RetrieveProductsQuery request,
                                                                       CancellationToken cancellationToken)
    {
        var itemsToSkip = request.PagingOptions.PageSize * request.PagingOptions.PageNumber;

        var query = financialContext.Products.AsQueryable();

        query = query.Where(static product => !product.IsRemoved)
                     .Where(request.RetrieveProductRequest?.Filter.GetGuidFilters().GenerateExpressions())
                     .Where(request.RetrieveProductRequest?.Filter.GetStringFilters().GenerateExpressions());

        var totalRecords = query.Count();

        query = FilterSort(query, request.RetrieveProductRequest.Sort);

        query = query.Skip(itemsToSkip)
                     .Take(request.PagingOptions.PageSize);

        var products = await query
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

        var retrieveProductsResponses = RetrieveProductResponse.FromProducts(products);

        var paginatedResult = new PaginatedResult<RetrieveProductResponse>(request.PagingOptions.PageNumber,
                                                                           request.PagingOptions.PageSize,
                                                                           totalRecords,
                                                                           retrieveProductsResponses);

        return paginatedResult;
    }

    private static IQueryable<Product> FilterSort(IQueryable<Product> query, RetrieveProductSort? sort)
    {
        if (sort == null)
        {
            return query;
        }

        query = Sort(query, sort.ApiId?.SortType, static p => p.ApiId);
        query = Sort(query, sort.Symbol?.SortType, static p => p.Symbol);
        query = Sort(query, sort.Name?.SortType, static p => p.Name);
        query = Sort(query, sort.Currency?.SortType, static p => p.Currency);
        query = Sort(query, sort.Exchange?.SortType, static p => p.Exchange);
        query = Sort(query, sort.Country?.SortType, static p => p.Country);
        query = Sort(query, sort.MicCode?.SortType, static p => p.MicCode);

        return query;
    }

    private static IQueryable<T> Sort<T, TO>(IQueryable<T> query, SortType? sortType,
                                             Expression<Func<T, TO>> keySelector)
        => sortType switch
        {
            SortType.Descending => query.OrderBy(keySelector).ThenBy(),
            SortType.Ascending => query.OrderBy(keySelector),
            null => query,
            _ => throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null)
        };

    private static IQueryable<T> SortThenBy<T, TO>(IOrderedQueryable<T> query, SortType? sortType,
                                                   Expression<Func<T, TO>> keySelector)
        => sortType switch
        {
            SortType.Descending => query.ThenByDescending(keySelector),
            SortType.Ascending => query.ThenBy(keySelector),
            null => query,
            _ => throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null)
        };

    private static IQueryable<Product> FilterQuery(IQueryable<Product> query, RetrieveProductFilter? filter)
    {
        if (filter is null)
        {
            return query;
        }

        if (filter.ApiIds is not null)
        {
            query = filter.ApiIds.Aggregate(query, static (queryable, f) => f.Type switch
            {
                SearchGuidType.Equal => queryable.Where(product => product.ApiId == f.SearchValue),
                SearchGuidType.NotEqual => queryable.Where(product => product.ApiId != f.SearchValue),
                _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
            });
        }

        query = FilterStrings(query, filter);

        return query;
    }

    private static IQueryable<Product> FilterStrings(IQueryable<Product> query, RetrieveProductFilter filter)
    {
        var expressions = new List<Expression<Func<Product, bool>>>();


        if (filter.Symbols is not null)
        {
            foreach (var symbol in filter.Symbols)
                query = FilterSymbol(symbol, query);
        }

        if (filter.Names is not null)
        {
            query = filter.Names.Aggregate(query, static (current, filter) => FilterName(filter, current));
        }

        if (filter.Currencies is not null)
        {
            query = filter.Currencies.Aggregate(query, static (current, filter) => FilterCurrency(filter, current));
        }

        if (filter.Exchanges is not null)
        {
            query = filter.Exchanges.Aggregate(query, static (current, filter) => FilterExchange(filter, current));
        }

        if (filter.Countries is not null)
        {
            query = filter.Countries.Aggregate(query, static (current, filter) => FilterCountry(filter, current));
        }

        if (filter.MicCodes is not null)
        {
            query = filter.MicCodes.Aggregate(query, static (current, filter) => FilterMicCode(filter, current));
        }

        return query;
    }

    private static Expression<Func<Product, bool>> Filter(FilterString f, Expression<Func<Product, string>> property)
    {
        return f.Type switch
        {
            // SearchStringType.Contains => property.Modify<Product>((Expression<Func<string?, bool>>)(x => x != null && f.SearchValue != null && x.Contains(f.SearchValue))),
            SearchStringType.NotContains => p => !p.Symbol.Contains(f.SearchValue),
            SearchStringType.Equal => p => p.Symbol.Equals(f.SearchValue),
            SearchStringType.NotEqual => p => !p.Symbol.Equals(f.SearchValue),
            SearchStringType.StartsWith => p => p.Symbol.StartsWith(f.SearchValue),
            SearchStringType.EndsWith => p => p.Symbol.EndsWith(f.SearchValue),
            SearchStringType.Empty => static p => p.Symbol == string.Empty,
            SearchStringType.NotEmpty => static p => p.Symbol != string.Empty,
            _ => throw new
                NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
        };
    }

    private static Expression<Func<Product, bool>> FilterSymbol(FilterString f, IQueryable<Product> q)
        => f.Type switch
        {
            SearchStringType.Contains => p => p.Symbol.Contains(f.SearchValue),
            SearchStringType.NotContains => p => !p.Symbol.Contains(f.SearchValue),
            SearchStringType.Equal => p => p.Symbol.Equals(f.SearchValue),
            SearchStringType.NotEqual => p => !p.Symbol.Equals(f.SearchValue),
            SearchStringType.StartsWith => p => p.Symbol.StartsWith(f.SearchValue),
            SearchStringType.EndsWith => p => p.Symbol.EndsWith(f.SearchValue),
            SearchStringType.Empty => static p => p.Symbol == string.Empty,
            SearchStringType.NotEmpty => static p => p.Symbol != string.Empty,
            _ => throw new
                NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
        };

    private static IQueryable<Product> FilterName(FilterString f, IQueryable<Product> q)
        => f.Type switch
        {
            SearchStringType.Contains => q.Where(p => p.Name != null && p.Name.Contains(f.SearchValue)),
            SearchStringType.NotContains => q.Where(p => p.Name != null && !p.Name.Contains(f.SearchValue)),
            SearchStringType.Equal => q.Where(p => p.Name != null && p.Name.Equals(f.SearchValue)),
            SearchStringType.NotEqual => q.Where(p => p.Name != null && !p.Name.Equals(f.SearchValue)),
            SearchStringType.StartsWith => q.Where(p => p.Name != null && p.Name.StartsWith(f.SearchValue)),
            SearchStringType.EndsWith => q.Where(p => p.Name != null && p.Name.EndsWith(f.SearchValue)),
            SearchStringType.Empty => q.Where(static p => p.Name == string.Empty),
            SearchStringType.NotEmpty => q.Where(static p => p.Name != string.Empty),
            _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
        };

    private static IQueryable<Product> FilterCurrency(FilterString f, IQueryable<Product> q)
        => f.Type switch
        {
            SearchStringType.Contains => q.Where(p => p.Currency != null && p.Currency.Contains(f.SearchValue)),
            SearchStringType.NotContains => q.Where(p => p.Currency != null && !p.Currency.Contains(f.SearchValue)),
            SearchStringType.Equal => q.Where(p => p.Currency != null && p.Currency.Equals(f.SearchValue)),
            SearchStringType.NotEqual => q.Where(p => p.Currency != null && !p.Currency.Equals(f.SearchValue)),
            SearchStringType.StartsWith => q.Where(p => p.Currency != null && p.Currency.StartsWith(f.SearchValue)),
            SearchStringType.EndsWith => q.Where(p => p.Currency != null && p.Currency.EndsWith(f.SearchValue)),
            SearchStringType.Empty => q.Where(static p => p.Currency == string.Empty),
            SearchStringType.NotEmpty => q.Where(static p => p.Currency != string.Empty),
            _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
        };

    private static IQueryable<Product> FilterExchange(FilterString f, IQueryable<Product> q)
        => f.Type switch
        {
            SearchStringType.Contains => q.Where(p => p.Exchange != null && p.Exchange.Contains(f.SearchValue)),
            SearchStringType.NotContains => q.Where(p => p.Exchange != null && !p.Exchange.Contains(f.SearchValue)),
            SearchStringType.Equal => q.Where(p => p.Exchange != null && p.Exchange.Equals(f.SearchValue)),
            SearchStringType.NotEqual => q.Where(p => p.Exchange != null && !p.Exchange.Equals(f.SearchValue)),
            SearchStringType.StartsWith => q.Where(p => p.Exchange != null && p.Exchange.StartsWith(f.SearchValue)),
            SearchStringType.EndsWith => q.Where(p => p.Exchange != null && p.Exchange.EndsWith(f.SearchValue)),
            SearchStringType.Empty => q.Where(static p => p.Exchange == string.Empty),
            SearchStringType.NotEmpty => q.Where(static p => p.Exchange != string.Empty),
            _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
        };

    private static IQueryable<Product> FilterCountry(FilterString f, IQueryable<Product> q)
        => f.Type switch
        {
            SearchStringType.Contains => q.Where(p => p.Country != null && p.Country.Contains(f.SearchValue)),
            SearchStringType.NotContains => q.Where(p => p.Country != null && !p.Country.Contains(f.SearchValue)),
            SearchStringType.Equal => q.Where(p => p.Country != null && p.Country.Equals(f.SearchValue)),
            SearchStringType.NotEqual => q.Where(p => p.Country != null && !p.Country.Equals(f.SearchValue)),
            SearchStringType.StartsWith => q.Where(p => p.Country != null && p.Country.StartsWith(f.SearchValue)),
            SearchStringType.EndsWith => q.Where(p => p.Country != null && p.Country.EndsWith(f.SearchValue)),
            SearchStringType.Empty => q.Where(static p => p.Country == string.Empty),
            SearchStringType.NotEmpty => q.Where(static p => p.Country != string.Empty),
            _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
        };

    private static IQueryable<Product> FilterMicCode(FilterString f, IQueryable<Product> q)
        => f.Type switch
        {
            SearchStringType.Contains => q.Where(p => p.MicCode != null && p.MicCode.Contains(f.SearchValue)),
            SearchStringType.NotContains => q.Where(p => p.MicCode != null && !p.MicCode.Contains(f.SearchValue)),
            SearchStringType.Equal => q.Where(p => p.MicCode != null && p.MicCode.Equals(f.SearchValue)),
            SearchStringType.NotEqual => q.Where(p => p.MicCode != null && !p.MicCode.Equals(f.SearchValue)),
            SearchStringType.StartsWith => q.Where(p => p.MicCode != null && p.MicCode.StartsWith(f.SearchValue)),
            SearchStringType.EndsWith => q.Where(p => p.MicCode != null && p.MicCode.EndsWith(f.SearchValue)),
            SearchStringType.Empty => q.Where(static p => p.MicCode == string.Empty),
            SearchStringType.NotEmpty => q.Where(static p => p.MicCode != string.Empty),
            _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
        };

    // private static IQueryable<Product> FilterSymbol(FilterString f, IQueryable<Product> q)
    //     => f.Type switch
    //     {
    //         SearchStringType.Contains => q.Where(p => p.Symbol.Contains(f.SearchValue)),
    //         SearchStringType.NotContains => q.Where(p => !p.Symbol.Contains(f.SearchValue)),
    //         SearchStringType.Equal => q.Where(p => p.Symbol.Equals(f.SearchValue)),
    //         SearchStringType.NotEqual => q.Where(p => !p.Symbol.Equals(f.SearchValue)),
    //         SearchStringType.StartsWith => q.Where(p => p.Symbol.StartsWith(f.SearchValue)),
    //         SearchStringType.EndsWith => q.Where(p => p.Symbol.EndsWith(f.SearchValue)),
    //         SearchStringType.Empty => q.Where(static p => p.Symbol == string.Empty),
    //         SearchStringType.NotEmpty => q.Where(static p => p.Symbol != string.Empty),
    //         _ => throw new
    //             NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
    //     };
    //
    // private static IQueryable<Product> FilterName(FilterString f, IQueryable<Product> q)
    //     => f.Type switch
    //     {
    //         SearchStringType.Contains => q.Where(p => p.Name != null && p.Name.Contains(f.SearchValue)),
    //         SearchStringType.NotContains => q.Where(p => p.Name != null && !p.Name.Contains(f.SearchValue)),
    //         SearchStringType.Equal => q.Where(p => p.Name != null && p.Name.Equals(f.SearchValue)),
    //         SearchStringType.NotEqual => q.Where(p => p.Name != null && !p.Name.Equals(f.SearchValue)),
    //         SearchStringType.StartsWith => q.Where(p => p.Name != null && p.Name.StartsWith(f.SearchValue)),
    //         SearchStringType.EndsWith => q.Where(p => p.Name != null && p.Name.EndsWith(f.SearchValue)),
    //         SearchStringType.Empty => q.Where(static p => p.Name == string.Empty),
    //         SearchStringType.NotEmpty => q.Where(static p => p.Name != string.Empty),
    //         _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
    //     };
    //
    // private static IQueryable<Product> FilterCurrency(FilterString f, IQueryable<Product> q)
    //     => f.Type switch
    //     {
    //         SearchStringType.Contains => q.Where(p => p.Currency != null && p.Currency.Contains(f.SearchValue)),
    //         SearchStringType.NotContains => q.Where(p => p.Currency != null && !p.Currency.Contains(f.SearchValue)),
    //         SearchStringType.Equal => q.Where(p => p.Currency != null && p.Currency.Equals(f.SearchValue)),
    //         SearchStringType.NotEqual => q.Where(p => p.Currency != null && !p.Currency.Equals(f.SearchValue)),
    //         SearchStringType.StartsWith => q.Where(p => p.Currency != null && p.Currency.StartsWith(f.SearchValue)),
    //         SearchStringType.EndsWith => q.Where(p => p.Currency != null && p.Currency.EndsWith(f.SearchValue)),
    //         SearchStringType.Empty => q.Where(static p => p.Currency == string.Empty),
    //         SearchStringType.NotEmpty => q.Where(static p => p.Currency != string.Empty),
    //         _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
    //     };
    //
    // private static IQueryable<Product> FilterExchange(FilterString f, IQueryable<Product> q)
    //     => f.Type switch
    //     {
    //         SearchStringType.Contains => q.Where(p => p.Exchange != null && p.Exchange.Contains(f.SearchValue)),
    //         SearchStringType.NotContains => q.Where(p => p.Exchange != null && !p.Exchange.Contains(f.SearchValue)),
    //         SearchStringType.Equal => q.Where(p => p.Exchange != null && p.Exchange.Equals(f.SearchValue)),
    //         SearchStringType.NotEqual => q.Where(p => p.Exchange != null && !p.Exchange.Equals(f.SearchValue)),
    //         SearchStringType.StartsWith => q.Where(p => p.Exchange != null && p.Exchange.StartsWith(f.SearchValue)),
    //         SearchStringType.EndsWith => q.Where(p => p.Exchange != null && p.Exchange.EndsWith(f.SearchValue)),
    //         SearchStringType.Empty => q.Where(static p => p.Exchange == string.Empty),
    //         SearchStringType.NotEmpty => q.Where(static p => p.Exchange != string.Empty),
    //         _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
    //     };
    //
    // private static IQueryable<Product> FilterCountry(FilterString f, IQueryable<Product> q)
    //     => f.Type switch
    //     {
    //         SearchStringType.Contains => q.Where(p => p.Country != null && p.Country.Contains(f.SearchValue)),
    //         SearchStringType.NotContains => q.Where(p => p.Country != null && !p.Country.Contains(f.SearchValue)),
    //         SearchStringType.Equal => q.Where(p => p.Country != null && p.Country.Equals(f.SearchValue)),
    //         SearchStringType.NotEqual => q.Where(p => p.Country != null && !p.Country.Equals(f.SearchValue)),
    //         SearchStringType.StartsWith => q.Where(p => p.Country != null && p.Country.StartsWith(f.SearchValue)),
    //         SearchStringType.EndsWith => q.Where(p => p.Country != null && p.Country.EndsWith(f.SearchValue)),
    //         SearchStringType.Empty => q.Where(static p => p.Country == string.Empty),
    //         SearchStringType.NotEmpty => q.Where(static p => p.Country != string.Empty),
    //         _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
    //     };
    //
    // private static IQueryable<Product> FilterMicCode(FilterString f, IQueryable<Product> q)
    //     => f.Type switch
    //     {
    //         SearchStringType.Contains => q.Where(p => p.MicCode != null && p.MicCode.Contains(f.SearchValue)),
    //         SearchStringType.NotContains => q.Where(p => p.MicCode != null && !p.MicCode.Contains(f.SearchValue)),
    //         SearchStringType.Equal => q.Where(p => p.MicCode != null && p.MicCode.Equals(f.SearchValue)),
    //         SearchStringType.NotEqual => q.Where(p => p.MicCode != null && !p.MicCode.Equals(f.SearchValue)),
    //         SearchStringType.StartsWith => q.Where(p => p.MicCode != null && p.MicCode.StartsWith(f.SearchValue)),
    //         SearchStringType.EndsWith => q.Where(p => p.MicCode != null && p.MicCode.EndsWith(f.SearchValue)),
    //         SearchStringType.Empty => q.Where(static p => p.MicCode == string.Empty),
    //         SearchStringType.NotEmpty => q.Where(static p => p.MicCode != string.Empty),
    //         _ => throw new NotImplementedException($"{nameof(f.Type)}:{f.Type} not Implemented.")
    //     };
}