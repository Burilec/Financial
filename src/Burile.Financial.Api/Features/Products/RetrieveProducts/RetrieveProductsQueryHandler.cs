using Burile.Financial.Infrastructure;
using Burile.Financial.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

public sealed class RetrieveProductsQueryHandler(FinancialContext financialContext)
    : IRequestHandler<RetrieveProductsQuery, PaginatedResult<RetrieveProductResponse>>
{
    public async Task<PaginatedResult<RetrieveProductResponse>> Handle(RetrieveProductsQuery request,
                                                                       CancellationToken cancellationToken)
    {
        var totalRecords = financialContext.Products.Count(static product => !product.IsRemoved);

        var itemsToSkip = request.PagingOptions.PageSize * request.PagingOptions.PageNumber;

        var products = await financialContext.Products
                                             .Where(static product => !product.IsRemoved)
                                             .OrderBy(static product => product.Id)
                                             .Skip(itemsToSkip)
                                             .Take(request.PagingOptions.PageSize)
                                             .AsNoTracking()
                                             .ToListAsync(cancellationToken);

        var retrieveProductsResponses = RetrieveProductResponse.FromProducts(products);

        var paginatedResult = new PaginatedResult<RetrieveProductResponse>(request.PagingOptions.PageNumber,
                                                                           request.PagingOptions.PageSize,
                                                                           totalRecords,
                                                                           retrieveProductsResponses);

        return paginatedResult;
    }
}