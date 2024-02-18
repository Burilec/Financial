using Burile.Financial.Infrastructure;

namespace Burile.Financial.Api.Features.Products.RetrieveProducts;

public sealed class RetrieveProductsQuery(PagingOptions pagingOptions)
    : IRequest<PaginatedResult<RetrieveProductResponse>>
{
    public PagingOptions PagingOptions { get; } = pagingOptions;
}