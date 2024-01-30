using Burile.Financial.Infrastructure;
using Burile.Financial.Infrastructure.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Burile.Financial.Api.Features.RetrieveProducts;

public sealed class RetrieveProductsQueryHandler(FinancialContext financialContext)
    : IRequestHandler<RetrieveProductsQuery, PaginatedResult<RetrieveProductsResponse>>
{
    public async Task<PaginatedResult<RetrieveProductsResponse>> Handle(RetrieveProductsQuery request,
                                                                        CancellationToken cancellationToken)
    {
        var totalRecords = financialContext.Products.Count();

        var itemsToSkip = request.PagingOptions.PageSize * (request.PagingOptions.PageNumber - 1);

        var products = await financialContext.Products
                                             .OrderBy(static s => s.Id)
                                             .Skip(itemsToSkip)
                                             .Take(request.PagingOptions.PageSize)
                                             .AsNoTracking()
                                             .ToListAsync(cancellationToken);

        var retrieveProductsResponses = RetrieveProductsResponse.FromProducts(products);

        var paginatedResult = new PaginatedResult<RetrieveProductsResponse>(request.PagingOptions.PageNumber,
                                                                            request.PagingOptions.PageSize,
                                                                            totalRecords,
                                                                            retrieveProductsResponses);

        return paginatedResult;
    }
}