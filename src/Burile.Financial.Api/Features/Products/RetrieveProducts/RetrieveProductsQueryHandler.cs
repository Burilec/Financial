using System.Linq.Expressions;
using Burile.Financial.Api.Features.Products.RetrieveProducts.Sorts;
using Burile.Financial.Domain.Entities;
using Burile.Financial.Infrastructure;
using Burile.Financial.Infrastructure.Data.Contexts;
using Burile.Financial.Infrastructure.Extensions;
using Burile.Financial.Infrastructure.Filters;
using Burile.Financial.Infrastructure.Sorts;
using Burile.Financial.Infrastructure.Sorts.Types;
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

        var sortInternals = request.RetrieveProductRequest?.Sort.GetSortGuid()
                                   .Concat(request.RetrieveProductRequest?.Sort.GetSortString() ?? []);

        query = Queryable(query, sortInternals);

        var products = await query.Skip(itemsToSkip)
                                  .Take(request.PagingOptions.PageSize)
                                  .AsNoTracking()
                                  .ToListAsync(cancellationToken);

        return new(request.PagingOptions.PageNumber,
                   request.PagingOptions.PageSize,
                   totalRecords,
                   RetrieveProductResponse.FromProducts(products));
    }

    private static IQueryable<Product> Queryable(IQueryable<Product> query,
                                                 IEnumerable<SortInternal<Product, dynamic>>? sortInternals)
    {
        if (sortInternals == null)
        {
            return query;
        }

        var first = true;
        IOrderedQueryable<Product>? queryOrder = null;

        foreach (var sortInternal in sortInternals.OrderBy(static x => x.Index))
        {
            if (first)
            {
                first = false;
                queryOrder = sortInternal.SortType switch
                {
                    SortType.Descending => query.OrderByDescending(sortInternal.Property),
                    SortType.Ascending => query.OrderBy(sortInternal.Property),
                    _ => throw new NotImplementedException()
                };
            }
            else
            {
                queryOrder = sortInternal.SortType switch
                {
                    SortType.Descending => queryOrder!.ThenByDescending(sortInternal.Property),
                    SortType.Ascending => queryOrder!.ThenBy(sortInternal.Property),
                    _ => throw new NotImplementedException()
                };
            }
        }

        return queryOrder ?? query;
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
            SortType.Descending => query.OrderByDescending(keySelector),
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
}